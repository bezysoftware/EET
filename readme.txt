Technicke info viz.: http://www.etrzby.cz/cs/technicka-specifikace
WSDL: http://www.etrzby.cz/assets/cs/prilohy/EETServiceSOAP.wsdl
Hesla privatnich klicu certifikatu (CA_PG_v1.zip): eet

request.cer a response.cer jsou certifikaty vyrobene z BinarySecurityToken elementu 
SOAP XML komunikacnich zprav. Obsahuji public key. A zejmena response.cer jde 
pripadne naimportovat do uloziste neduveryhodnych certifikatu a nasimulovat tak,
ze odpoved neprojde validaci na platnost certifikatu.

Nektere uzitecne ukazky aplikacniho kodu:
1) Odchyceni timeoutu webove sluzby (prekroceni mezni doby odezvy):
     try
     {
       ...
       result = eet.OdeslaniTrzby();
       ...
     }
     catch (WebException ex)
     {
       if (ex.Status == WebExceptionStatus.Timeout)
       {
         // zde nasleduje prislusne zpracovani
         ...
       }
       else
         throw ex;  // rethrow - jelikoz se evidentne jedna o jiny duvod nez ktery nas zajima
     }

2) Pouziti certifikatu poplatnika umisteneho v ulozisti certifikatu:
     X509Store store = new X509Store(StoreLocation.CurrentUser);
     try
     {
       store.Open(OpenFlags.ReadOnly);
       eet.SetCertificate(store.Certificates.Find(X509FindType.FindBySubjectName, "CZ1212121218", false)[0]);
     }
     finally
     {
       store.Close();
     }
        
