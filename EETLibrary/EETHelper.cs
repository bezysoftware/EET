using System;
using System.Xml;
using System.Xml.Serialization;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using System.Text;
using System.Reflection;
using System.Web.Services.Protocols;
using System.Net;
using System.Globalization;
using System.Collections.Generic;
using SignSoapMessage;

namespace EETLibrary
{
    // Help functions pro EET
    public class EETHelper
    {
        // Certifikat pooplatnika
        private X509Certificate2 _certPopl = null;

        // XML request a response
        private string _xmlRequest;
        public string XMLRequest { get { return _xmlRequest; } }
        private string _xmlResponse;
        public string XMLResponse { get { return _xmlResponse; } }

        // Web sluzba
        public string UrlAddress { get; set; }
        public string Proxy { get; set; }
        
        // Timeout Web sluzby
        private int _timeout;
        /// <summary>
        /// Cas v milisekundach, po ktery klient webove sluzby ceka na odpoved. <para />
        /// 0 - defaultni hodnota pro .NET tj. 100s, Timeout.Infinite - ceka se nekonecne dlouho <para />
        /// Pokud k timeoutu dojde, je generovana WebException, ktera ma nastavenu polozku Status na WebExceptionStatus.Timeout <para />
        /// </summary>
        public int Timeout { get { return _timeout; } set { _timeout = value; } }

        // Pomocne info o Web sluzbe
        private string _nameSpace;
        private string _soapAction;

        // Trzba
        private EET.TrzbaHlavickaType _hlavicka;
        public EET.TrzbaHlavickaType Hlavicka { get { return _hlavicka; } set { _hlavicka = value; } }
        private EET.TrzbaDataType _data;
        public EET.TrzbaDataType Data { get { return _data; } set { _data = value; } }

        // Kontrolni kody
        private string _pkp;
        public string PKP { get { return _pkp; } }
        private string _bkp;
        public string BKP { get { return _bkp; } }
        private EET.TrzbaKontrolniKodyType _kontrolniKody;

        // Odpoved na zaslani trzby
        private EET.OdpovedHlavickaType _odpovedHlavicka;
        public EET.OdpovedHlavickaType OdpovedHlavicka { get { return _odpovedHlavicka; } }
        private EET.OdpovedChybaType _odpovedChyba;
        public EET.OdpovedChybaType Chyba { get { return _odpovedChyba; } }
        private EET.OdpovedPotvrzeniType _odpovedPotvrzeni;
        public EET.OdpovedPotvrzeniType Potvrzeni { get { return _odpovedPotvrzeni; } }
        private List<EET.OdpovedVarovaniType> _odpovedVarovani;
        public List<EET.OdpovedVarovaniType> Varovani { get { return _odpovedVarovani; } }

        // Hlasky - vyhledove vsechny presunout do resources
        private const string _message_incorrect_format= "Došlo k neočekávané chybě. Odpověď nemá správný formát.";

        public EETHelper()
        {
            _hlavicka = new EET.TrzbaHlavickaType();
            _data = new EET.TrzbaDataType();
            _kontrolniKody=new EET.TrzbaKontrolniKodyType();
            _kontrolniKody.pkp = new EET.PkpElementType();
            _kontrolniKody.bkp = new EET.BkpElementType();
            _timeout = 0;
            getServiceInfo();
        }

        /// <summary>
        /// Odesle trzbu. <para />
        /// Pred zaslanim je treba: <para />
        ///     1) nejaka globalni nastaveni ala UrlAddress <para />
        ///           pozn.: Ta Proxy nutna neni, je to jen pro ucely debugerovani. <para />
        ///     2) nahrat certifikat poplatnika viz. LoadCertificate resp. SetCertificate <para />
        ///     3) naplnit udaje o trzbe viz. vlastnosti Hlavicka a Data <para />
        ///     4) vypocist kontrolni kody viz. VypoctiPKPaBKP
        /// Po zaslani: <para />
        ///     1) nadrizena aplikace ma pristup k vlastnostem OdpovedHlavicka, Potvrzeni, Chyba, Varovani <para />
        ///          pozn.: - kyzeny FIK je v Potvrzeni.fik <para />
        ///                 - OdpovedHlavicka je nastavena na nejake udaje vzdy (teda pokud nedojde k nejake vyjimce) <para />
        ///                 - Chyba je nastavena na nejake udaje jen pokud k ni dojde, jinak je to null <para />
        ///                 - Potvrzeni je nastaveno jen pri uspechu, jinak je to null <para />
        ///                 - Varovani je nastaveno jen pri uspechu, jinak je to null <para />
        ///                   prislusny list vsak muze byt i prazdny, pokud proste zadna varovani nejsou k dispozici <para />
        ///     2) vlastnosti XMLRequest a XMLResponse obsahuji text prislusnych komunikacnich SOAP zprav <para />
        ///          pozn.: zvlaste uzitecne, pokud neni k dispozici nejaka debug/logging proxy ala Telerik Fiddler <para />
        ///                 take se hodi k archivaci komunikace pro nasledne spory <para />
        /// </summary>
        /// <returns>true - pri uspechu, false - pri chybe</returns>
        public bool OdeslaniTrzby()
        {
            // Clear
            _xmlRequest = null;
            _xmlResponse = null;
            _odpovedHlavicka = null;
            _odpovedChyba = null;
            _odpovedPotvrzeni=null;
            _odpovedVarovani=null;

            // Kontrola, zda byl nacten certifikat poplatnika
            checkValidCertificate();
            
            // Kontrola, zda byly vypocteny kontrolni PKP a BKP kody
            if (_kontrolniKody == null || _pkp==null || _bkp == null)
                throw new Exception("Nebyly nalezeny platné PKP a BKP kontrolní kódy.");

            // Vytvoreni soap xml zpravy nesouci info o trzbe
            Trzba trzba = new Trzba(_hlavicka, _data, _kontrolniKody);
            XmlDocument doc = new XmlDocument();
            MemoryStream ms = new MemoryStream();
            new XmlSerializer(typeof(Trzba)).Serialize(ms, trzba);
            ms.Position = 0;
            using (StreamReader rd = new StreamReader(ms))
            {
                doc.LoadXml(rd.ReadToEnd());
            }
            XmlNode node = doc.SelectSingleNode("//*[local-name()='Trzba']");
            XmlAttribute xa = doc.CreateAttribute("xmlns");
            xa.Value = _nameSpace;
            node.Attributes.Append(xa);
            _xmlRequest = node.OuterXml;

            // Podepsani soap xml zpravy
            _xmlRequest = signSoapMessage(XMLRequest, _certPopl, SignSoapMessage.SignAlgorithm.SHA256);

            // A finalni odeslani
            _xmlResponse = callWebService(UrlAddress, _soapAction, XMLRequest, Proxy, _timeout);

            // Zpracovani/analyza odpovedi
            try
            {
                // Prepare XML document
                XmlDocument docResponse = new XmlDocument();
                docResponse.LoadXml(XMLResponse);
                XmlNode nodeOdpoved = docResponse.SelectSingleNode("//*[local-name()='Odpoved']");
                XmlNode nodeHlavicka = nodeOdpoved.SelectSingleNode("//*[local-name()='Hlavicka']");
                XmlNode nodeChyba = nodeOdpoved.SelectSingleNode("//*[local-name()='Chyba']");
                XmlNode nodePotvrzeni = nodeOdpoved.SelectSingleNode("//*[local-name()='Potvrzeni']");
                XmlNodeList nodeVarovaniAll = nodeOdpoved.SelectNodes("//*[local-name()='Varovani']");
                doc = new XmlDocument();
                XmlElement newRoot = doc.CreateElement("Odpoved");
                doc.AppendChild(newRoot);
                addXMLNode(newRoot, nodeHlavicka, typeof(EET.OdpovedHlavickaType).Name);
                if (nodeChyba!=null)
                    addXMLNode(newRoot, nodeChyba, typeof(EET.OdpovedChybaType).Name);
                if (nodePotvrzeni!=null)
                    addXMLNode(newRoot, nodePotvrzeni, typeof(EET.OdpovedPotvrzeniType).Name);
                foreach (XmlNode nodeVarovani in nodeVarovaniAll)
                {
                    addXMLNode(newRoot, nodeVarovani, typeof(EET.OdpovedVarovaniType).Name);
                }
                nodeHlavicka = doc.SelectSingleNode("//"+typeof(EET.OdpovedHlavickaType).Name);
                nodeChyba = doc.SelectSingleNode("//" + typeof(EET.OdpovedChybaType).Name);
                nodePotvrzeni = doc.SelectSingleNode("//"+ typeof(EET.OdpovedPotvrzeniType).Name);
                nodeVarovaniAll = doc.SelectNodes("//"+ typeof(EET.OdpovedVarovaniType).Name);

                // Deserialize
                _odpovedHlavicka = (EET.OdpovedHlavickaType)deserializeXMLNode(nodeHlavicka, typeof(EET.OdpovedHlavickaType));
                if (nodeChyba != null)
                    _odpovedChyba = (EET.OdpovedChybaType)deserializeXMLNode(nodeChyba, typeof(EET.OdpovedChybaType));
                if (nodePotvrzeni != null)
                {
                    _odpovedPotvrzeni = (EET.OdpovedPotvrzeniType)deserializeXMLNode(nodePotvrzeni, typeof(EET.OdpovedPotvrzeniType));
                    _odpovedVarovani = new List<EET.OdpovedVarovaniType>();
                }
                if (_odpovedVarovani!=null && nodeVarovaniAll != null)
                {
                    foreach (XmlNode nodeVarovani in nodeVarovaniAll)
                    {
                        _odpovedVarovani.Add((EET.OdpovedVarovaniType)deserializeXMLNode(nodeVarovani, typeof(EET.OdpovedVarovaniType)));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(_message_incorrect_format + Environment.NewLine + ex.Message);
            }

            return (_odpovedPotvrzeni!=null);
        }

        /// <summary>
        /// Vypocte kontrolni kody PKP a BKP. <para />
        /// Pred vypoctem je treba: <para />
        ///     1) nahrat certifikat poplatnika viz. LoadCertificate resp. SetCertificate <para />
        /// Po vypoctu: <para />
        ///     1) vlastnosti PKP a BKP obsahuji prislusne vypoctene kontrolni kody <para />
        ///     2) nasledne se vola vlastni odeslani trzby viz. OdeslaniTrzby <para />
        /// </summary>
        public void VypoctiPKPaBKP()
        {
            // Clear
            _pkp = null;
            _bkp = null;

            // Kontrola, zda byl nacten certifikat poplatnika
            checkValidCertificate();

            // Vypocet kontrolnich kodu
            _kontrolniKody.pkp.cipher = EET.PkpCipherType.RSA2048;
            _kontrolniKody.pkp.digest = EET.PkpDigestType.SHA256;
            _kontrolniKody.pkp.encoding = EET.PkpEncodingType.base64;
            _kontrolniKody.pkp.Text = new string[] { getPKP(_data) };
            _pkp = _kontrolniKody.pkp.Text[0];
            _kontrolniKody.bkp.digest = EET.BkpDigestType.SHA1;
            _kontrolniKody.bkp.encoding = EET.BkpEncodingType.base16;
            _kontrolniKody.bkp.Text = new string[] { getBKP(_kontrolniKody.pkp.Text[0]) };
            _bkp = _kontrolniKody.bkp.Text[0];
        }

        /// <summary>
        /// Nahraje certifikat poplatnika ze souboru. Musi se volat pred odeslanim trzby. <para />
        /// Ne vsak nutne vzdy, jenom, kdyz se najak certifikat zmeni. <para />
        /// To, kdy se zmeni, je vec logiky nadrizene aplikace. <para />
        /// </summary>
        /// <param name="fileName">Plna cesta k souboru, ktery obsahuje certifikat poplatnika</param>
        /// <param name="password">Heslo k privatnimu klici certifikatu</param>
        public void LoadCertificate(string fileName, string password)
        {
            // Clear
            _certPopl = null;

            using (BinaryReader br = new BinaryReader(new FileStream(fileName, FileMode.Open, FileAccess.Read)))
            {
                byte[] buffer;
                buffer = br.ReadBytes((int)new FileInfo(fileName).Length);
                try
                {
                    X509Certificate2Collection certStore = new X509Certificate2Collection();
                    certStore.Import(buffer, password, X509KeyStorageFlags.Exportable);
                    foreach (X509Certificate2 cert in certStore)
                    {
                        // Find the first certificate with a private key
                        if (cert.HasPrivateKey)
                        {
                            _certPopl = cert;
                            break;
                        }
                    }
                    if (_certPopl == null)
                        throw new Exception("Nepodařilo se nalézt žádný použitelný certifikát poplatníka v souboru: \"" + fileName + "\".");
                }
                catch (CryptographicException ex)
                {
                    // Pozn.: Technicky presneji by tam mela byt hlaska, ze se jedna o heslo k privatnimu 
                    //        klici certifikatu, ale to by bezneho uzivatele spise matlo.
                    throw new Exception("Vámi zadané heslo k certifikátu poplatníka není pravděpodobně správné."+ Environment.NewLine + ex.Message);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        
        /// <summary>
        /// Nastavi certifikat poplatnika. <para />
        /// Alternativa k LoadCertificate pro moznost vyuzit uloziste certifikatu. <para />
        /// </summary>
        /// <param name="certificate">Certifikat poplatnika</param>
        public void SetCertificate(X509Certificate2 certificate)
        {
            _certPopl = certificate;
        }

        /// <summary>
        /// Validuje odpoved ziskanou od EET webove sluzby. <para />
        /// Napr. shoda UUID, BKP, platnost certifikatu serverove strany, <para />
        /// platnost podpisu, atd. <para />
        /// <param name="errors">Seznam zjistenych chyb</param>
        /// <returns>true - vse v poradku bez chyb, false - byly zjisteny chyby</returns>
        public bool ValidujOdpoved(out List<string> errors)
        {
            bool isValid = true;
            errors = new List<string>();
            
            // Shoda UUID
            if (_hlavicka.uuid_zpravy!=_odpovedHlavicka.uuid_zpravy)
            {
                isValid = false;
                errors.Add("Neshoduje se UUID zprávy v požadavku a odpovědi.");
            }

            // Shoda BKP
            if (_bkp!=_odpovedHlavicka.bkp)
            {
                isValid = false;
                errors.Add("Neshoduje se bezpečnostní kód poplatníka (BKP) v požadavku a odpovědi.");
            }

            try
            {
                // Overeni platnosti certifikatu EET sluzby
                // Get certificate from the binary security token
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(_xmlResponse);
                X509Certificate2 certResponse = new X509Certificate2(Convert.FromBase64String(doc.SelectSingleNode("//*[local-name()='BinarySecurityToken']").InnerText));
                if (!certResponse.Verify())
                {
                    isValid = false;
                    errors.Add("Certifikát EET služby není platný.");
                }
               
                // Overeni podpisu
                if (!validateSoapMessageSignature(_xmlResponse, certResponse))
                {
                    isValid = false;
                    errors.Add("Podpis odpovědi není platný.");
                }
            }
            catch (Exception ex)
            {
                isValid = false;
                errors.Add(_message_incorrect_format + Environment.NewLine + ex.Message);
            }

            return isValid;
        }

        public DateTime FormatDate(DateTime date)
        {
            // Odrizne nejake ty milisekundy a prida info o casove zone
            // Pokud info o zone chybi, tak ji doplni
            return TimeZoneInfo.ConvertTime(DateTime.Parse(getDateAsText(date)), TimeZoneInfo.Local);
        }

        public decimal FormatMoney(decimal value)
        {
            // Zaokrohli castku s max. presnosti na halere
            return decimal.Round(1.00m*value, 2);
        }

        public string GetMoneyAsText(decimal value)
        {
            // Naformatuje castku s max. presnosti na halere
            return value.ToString("0.00");
        }

        public string GenerateUUID()
        {
            return Guid.NewGuid().ToString();
        }

        public string NullIsEmpty(string text)
        {
            return (text == null) ? string.Empty : text;
        }

        public string ConcatText(string [] text, string separtor = "")
        {
            StringBuilder sb = new StringBuilder();
            for (int i=0; i<text.Length; i++)
            {
                if (i != 0)
                    sb.Append(separtor);
                sb.Append(text[i]);
            }
            return sb.ToString();
        }

        private void checkValidCertificate()
        {
            // Kontrola, zda byl nacten certifikat poplatnika
            if (_certPopl == null)
                throw new Exception("Nepodařilo se nalézt žadný použitelný certifikát poplatníka.");
        }

        private void getServiceInfo()
        {
            MethodInfo[] methods = typeof(EET.EETService).GetMethods();
            foreach (MethodInfo m in methods)
            {
                Attribute[] attributes = (Attribute[])m.GetCustomAttributes();
                foreach (Attribute a in attributes)
                {
                    if (a.GetType() == typeof(SoapDocumentMethodAttribute))
                    {
                        string action = ((System.Web.Services.Protocols.SoapDocumentMethodAttribute)a).Action;
                        string nameSpace = ((System.Web.Services.Protocols.SoapDocumentMethodAttribute)a).RequestNamespace;
                        string element = ((System.Web.Services.Protocols.SoapDocumentMethodAttribute)a).RequestElementName;
                        if (!String.IsNullOrEmpty(action) && !String.IsNullOrEmpty(nameSpace) && !String.IsNullOrEmpty(element) && element == "Trzba")
                        {
                            _nameSpace = nameSpace;
                            _soapAction = action;
                            break;
                        }
                    }
                }
            }
        }

        private string callWebService(string urlAddress, string soapAction, string soapEnvelope, string proxy, int timeout)
        {
            byte[] content = UTF8Encoding.UTF8.GetBytes(soapEnvelope);
            WebRequest req = WebRequest.Create(urlAddress);
            if (!String.IsNullOrEmpty(proxy))
            {
                req.Proxy = new WebProxy(proxy, false); 
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11;
            }
            else
            {
                req.Proxy = null;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            }
            if (timeout != 0)
                req.Timeout = timeout;
            req.ContentType = "text/xml;charset=UTF-8";
            req.ContentLength = content.Length;
            req.Headers.Add("SOAPAction", soapAction);
            req.Method = "POST";
            Stream reqStream = req.GetRequestStream();
            reqStream.Write(content, 0, content.Length);
            reqStream.Close();

            WebResponse resp = req.GetResponse();
            Stream respStream = resp.GetResponseStream();
            StreamReader rdr = new StreamReader(respStream, Encoding.UTF8);
            String responseString = rdr.ReadToEnd();
            return responseString;
        }

        private string getDateAsText(DateTime date)
        {
            // Odrizne nejake ty milisekundy a prida info o casove zone
            return date.ToString("yyyy-MM-ddTHH:mm:ssK");
        }

        private string getMoneyAsTextInEN_USFormat(decimal value)
        {
            // en-US 0.00 format
            return value.ToString("0.00", CultureInfo.GetCultureInfo("en-US"));
        }

        private string getBKP(string PKP)
        {
            StringBuilder sb = new StringBuilder();
            byte[] decodedBytes = SHA1.Create().ComputeHash(Convert.FromBase64String(PKP));
            for (int i = 0; i < decodedBytes.Length;)
            {
                sb.Append('-');
                sb.Append(decodedBytes[i++].ToString("X2"));
                sb.Append(decodedBytes[i++].ToString("X2"));
                sb.Append(decodedBytes[i++].ToString("X2"));
                sb.Append(decodedBytes[i++].ToString("X2"));
            }
            return sb.ToString().Substring(1);
        }

        private string getPKP(EET.TrzbaDataType data)
        {
            string PKP = null;

            // Nejprve vytvor plain text
            const char separtor = '|';
            string plainText;
            StringBuilder sb = new StringBuilder();
            sb.Append(data.dic_popl);
            sb.Append(separtor);
            sb.Append(data.id_provoz);
            sb.Append(separtor);
            sb.Append(data.id_pokl);
            sb.Append(separtor);
            sb.Append(data.porad_cis);
            sb.Append(separtor);
            sb.Append(getDateAsText(data.dat_trzby));
            sb.Append(separtor);
            sb.Append(getMoneyAsTextInEN_USFormat(data.celk_trzba));
            plainText = sb.ToString();

            // Sign data
            using (RSACryptoServiceProvider csp = new RSACryptoServiceProvider())
            {
                byte[] dataToSign = Encoding.UTF8.GetBytes(plainText);
                csp.ImportParameters(((RSACryptoServiceProvider)_certPopl.PrivateKey).ExportParameters(true));
                byte[] signature = csp.SignData(dataToSign, "SHA256");
                // Verify signature
                if (!csp.VerifyData(dataToSign, "SHA256", signature))
                    throw new Exception("Nepodařilo se vytvořit platný podpisový kód poplatníka.");
                PKP = Convert.ToBase64String(signature);
            }

            return PKP;
        }

        private string signSoapMessage(string soapBody, X509Certificate2 certificate, SignSoapMessage.SignAlgorithm hashAlgorithm)
        {
            // Create XML document from usigned message text
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(soapBody);

            // Create signed message
            SignSoapMessage.SoapMessage message = new SignSoapMessage.SoapMessage();
            message.Certificate = certificate;
            message.Body = doc.DocumentElement;
            XmlDocument result = message.GetXml(true, hashAlgorithm);

            // Convert XML document into text
            return result.OuterXml;
        }

        private void addXMLNode(XmlNode parent, XmlNode node, string nodeName)
        {
            XmlElement el = parent.OwnerDocument.CreateElement(nodeName);
            el.InnerXml = node.InnerXml;
            foreach (XmlAttribute attribute in node.Attributes)
            {
                XmlAttribute a = parent.OwnerDocument.CreateAttribute(attribute.Name);
                a.Value = attribute.Value;
                el.Attributes.Append(a);
            }
            parent.AppendChild(el);
        }

        private object deserializeXMLNode(XmlNode node, Type type)
        {
            MemoryStream ms = new MemoryStream();
            StreamWriter wr = new StreamWriter(ms);
            wr.Write(node.OuterXml);
            wr.Flush();
            ms.Position = 0;
            return new XmlSerializer(type).Deserialize(ms);
        }

        private bool validateSoapMessageSignature(string soap, X509Certificate2 certificate)
        {
            XmlDocument doc = new XmlDocument();
            doc.PreserveWhitespace = true;   // very important 
            doc.LoadXml(soap);

            // Get signature element
            XmlNode signature = doc.SelectSingleNode("//*[local-name()='Signature']");

            // Create SignedXml document
            SignedXmlWithId signedXml2 = new SignedXmlWithId(doc);
            signedXml2.LoadXml((XmlElement)signature);

            // Check signature
            return signedXml2.CheckSignature(certificate.PublicKey.Key);
        }
    }

    // Pro ucely serializace trzby
    [Serializable]
    public class Trzba
    {
        private Trzba() { }
        internal Trzba(EET.TrzbaHlavickaType hlavicka, EET.TrzbaDataType data, EET.TrzbaKontrolniKodyType kontrolniKody)
        {
            this.Hlavicka = hlavicka;
            this.Data = data;
            this.KontrolniKody = kontrolniKody;
        }

        [XmlElement]
        public EET.TrzbaHlavickaType Hlavicka { get; set; }

        [XmlElement]
        public EET.TrzbaDataType Data { get; set; }

        [XmlElement]
        public EET.TrzbaKontrolniKodyType KontrolniKody { get; set; }
    }
}
