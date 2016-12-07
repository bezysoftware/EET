using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Serialization;
using System.IO;
using System.Configuration;
using EETLibrary;

namespace EETTestClient2
{
    public partial class Form1 : Form
    {

        // application variables
        private EETHelper eet;

        const int dphZakladni = 21;
        const int dphSnizena = 15;

        public Form1()
        {
            InitializeComponent();

            // Initialize UI controls
            this.Text += " "+Application.ProductVersion + "  by  " + Application.CompanyName +
                  "    (EET Library " + typeof(EETHelper).Assembly.GetName().Version+")";
            this.btnSend.Text = this.btnSend.Tag.ToString().Split('|')[0];
            this.lblSazbaZakladni.Text = dphZakladni + this.lblSazbaZakladni.Text;
            this.lblSazbaSnizena.Text = dphSnizena + this.lblSazbaSnizena.Text;
            this.clearControls();

            // Initialize EET
            eet = new EETHelper();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            prepocitejCastky();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (!this.validujVsechnyZadaneUdaje())
                return;

            Cursor.Current = Cursors.WaitCursor;

            // Clear controls
            this.clearControls();
            this.Refresh();

            // Debugging proxy setting e.g. Telerik Fiddler
            eet.Proxy = this.cbxProxy.Checked && !String.IsNullOrEmpty(this.txtProxy.Text) ? this.txtProxy.Text : null;

            // EET globalni nastaveni
            eet.UrlAddress = ConfigurationManager.AppSettings["EETServiceUrlAddress"];
            eet.Timeout = this.cbxTimeout.Checked && !String.IsNullOrEmpty(this.txtTimeout.Text) ? int.Parse(this.txtTimeout.Text) : 0;
            eet.LoadCertificate(Application.StartupPath + "\\" + this.txtCertifikat.Text, this.txtHeslo.Text);
            
            // EET Hlavicka
            eet.Hlavicka.uuid_zpravy = eet.GenerateUUID();
            // eet.Hlavicka.uuid_zpravy = @"a272e701-d9bc-405e-b2cd-b9f47ce7dafe";
            eet.Hlavicka.dat_odesl = eet.FormatDate(DateTime.Now);
            eet.Hlavicka.prvni_zaslani = true;
            eet.Hlavicka.overeniSpecified = true;
            eet.Hlavicka.overeni = this.cbxTest.Checked;

            // EET Data
            // eet.Data.dic_popl = "CZ1212121218";
            // eet.Data.dic_popl = "CZ68710712";
            // eet.Data.dic_popl = "CZ2415961224";
            eet.Data.dic_popl = this.txtDIC.Text;
            // eet.Data.id_provoz = 356;
            eet.Data.id_provoz = int.Parse(this.txtProvozovna.Text);
            // eet.Data.id_pokl = "POKLADNA_1";
            eet.Data.id_pokl = this.txtPokladna.Text;
            eet.Data.rezim = 0;
            // eet.Data.porad_cis = "PD2016/8-22";
            eet.Data.porad_cis = this.txtUctenka.Text;
            // eet.Data.dat_trzby = eet.FormatDate(DateTime.Parse("2016-08-18 15:23:17"));
            // eet.Data.dat_trzby = eet.FormatDate(DateTime.Parse("2016-07-18 15:23:17"));
            eet.Data.dat_trzby = eet.FormatDate(DateTime.Parse(this.txtDatum.Text));
            // eet.Data.celk_trzba = eet.FormatMoney(71953.50m);
            eet.Data.celk_trzba = eet.FormatMoney(decimal.Parse(this.txtTrzba.Text));
            // Zakladni sazba DPH
            if (!String.IsNullOrEmpty(this.txtZakladZakladni.Text))
            {
                eet.Data.zakl_dan1Specified = true;
                eet.Data.zakl_dan1= eet.FormatMoney(decimal.Parse(this.txtZakladZakladni.Text));
            }
            else
                eet.Data.zakl_dan1Specified = false;
            if (!String.IsNullOrEmpty(this.txtDPHZakladni.Text))
            {
                eet.Data.dan1Specified = true;
                eet.Data.dan1 = eet.FormatMoney(decimal.Parse(this.txtDPHZakladni.Text));
            }
            else
                eet.Data.dan1Specified = false;
            // Snizena sazba DPH
            if (!String.IsNullOrEmpty(this.txtZakladSnizena.Text))
            {
                eet.Data.zakl_dan2Specified = true;
                eet.Data.zakl_dan2 = eet.FormatMoney(decimal.Parse(this.txtZakladSnizena.Text));
            }
            else
                eet.Data.zakl_dan2Specified = false;
            if (!String.IsNullOrEmpty(this.txtDPHSnizena.Text))
            {
                eet.Data.dan2Specified = true;
                eet.Data.dan2 = eet.FormatMoney(decimal.Parse(this.txtDPHSnizena.Text));
            }
            else
                eet.Data.dan2Specified = false;

            // EET vypocet PKP a BKP kontrolnich kodu - lze je pak pripadne pouzit pro offline
            eet.VypoctiPKPaBKP();
            if (eet.PKP !=null && eet.BKP!=null)
            {
                this.rtbPKP.Text = eet.PKP;
                this.rtbBKP.Text = eet.BKP;
            }

            // EET vlastni odeslani trzby
            bool result = eet.OdeslaniTrzby();
            
            // Report success
            if (result)
            {
                // Kyzeny FIK
                this.txtFIK.Text = eet.Potvrzeni.fik;

                // Report warnings
                List<EETLibrary.EET.OdpovedVarovaniType> warnList = eet.Varovani;
                if (warnList.Count > 0)
                {
                    this.tabError.TabPages[1].Text = this.tabError.TabPages[1].Tag.ToString().Split('|')[1];
                    foreach (EETLibrary.EET.OdpovedVarovaniType warn in warnList)
                    {
                        this.rtbWarning.Text += eet.ConcatText(warn.Text) + Environment.NewLine;
                    }
                }

                //  EET validace odpovedi
                List<string> errors;
                result = eet.ValidujOdpoved(out errors);

                // Report validation errors
                if (!result)
                {
                    this.tabError.TabPages[0].Text = this.tabError.TabPages[0].Tag.ToString().Split('|')[1];
                    this.rtbError.Text += Environment.NewLine + "Chyby při validaci odpovědi:" + Environment.NewLine;
                    foreach (string err in errors)
                    {
                        this.rtbError.Text += err + Environment.NewLine;
                    }
                }
            }
            // Report error
            else
            {
                this.tabError.TabPages[0].Text = this.tabError.TabPages[0].Tag.ToString().Split('|')[1];
                this.rtbError.Text += eet.ConcatText(eet.Chyba.Text) + Environment.NewLine;
            }

            // Report komunikacnich XML zprav
            this.rtbRequest.Text = eet.XMLRequest;
            this.rtbResponse.Text = eet.XMLResponse;

            Cursor.Current = Cursors.Default;
        }

        private void cbxProxy_CheckedChanged(object sender, EventArgs e)
        {
            this.txtProxy.Enabled = cbxProxy.Checked;
        }

        private void cbxTimeout_CheckedChanged(object sender, EventArgs e)
        {
            this.txtTimeout.Enabled = cbxTimeout.Checked;
        }

        private void cbxTest_CheckedChanged(object sender, EventArgs e)
        {
            this.btnSend.Text = this.btnSend.Tag.ToString().Split('|')[cbxTest.Checked ? 1 : 0];
        }

        private void clearControls()
        {
            this.rtbRequest.Text = "";
            this.rtbResponse.Text = "";
            this.tabError.TabPages[0].Text = this.tabError.TabPages[0].Tag.ToString().Split('|')[0];
            this.rtbError.Text = "";
            this.tabError.TabPages[1].Text = this.tabError.TabPages[1].Tag.ToString().Split('|')[0];
            this.rtbWarning.Text = "";
            this.rtbPKP.Text = "";
            this.rtbBKP.Text = "";
            this.txtFIK.Text = "";
        }

        private bool validujVsechnyZadaneUdaje()
        {
            bool isValid = true;
            string error;

            // DIC
            if (zjistiChybyProProstyText(this.txtDIC)!="")
                isValid = false;

            // Provozovna
            error = "";
            this.errorProvider.SetError(this.txtProvozovna, error);
            if ((error = validujZadani(this.txtProvozovna.Text)) != "")
                this.errorProvider.SetError(this.txtProvozovna, error);
            else
            {
                if ((error = validujCislo(this.txtProvozovna.Text)) != "")
                    this.errorProvider.SetError(this.txtProvozovna, error);
            }
            if (error != "")
                isValid = false;

            // Pokladna
            if (zjistiChybyProProstyText(this.txtPokladna) != "")
                isValid = false;

            // Uctenka
            if (zjistiChybyProProstyText(this.txtUctenka) != "")
                isValid = false;
            
            // Datum
            error = "";
            this.errorProvider.SetError(this.txtDatum, error);
            if ((error = validujZadani(this.txtDatum.Text)) != "")
                this.errorProvider.SetError(this.txtDatum, error);
            else
            {
                if ((error = validujDatum(this.txtDatum.Text)) != "")
                    this.errorProvider.SetError(this.txtDatum, error);
            }
            if (error != "")
                isValid = false;

            // Zaklad pro zakladni sazbu
            if (this.cbxZakladZakladni.Checked)
            {
                if (zjistiChybyProCastku(this.txtZakladZakladni) != "")
                    isValid = false;
            }
            else
                this.errorProvider.SetError(this.txtZakladZakladni, "");

            // Zaklad pro snizenou sazbu
            if (this.cbxZakladSnizena.Checked)
            {
                if (zjistiChybyProCastku(this.txtZakladSnizena) != "")
                    isValid = false;
            }
            else
                this.errorProvider.SetError(this.txtZakladSnizena, "");

            // Zaklad pro zakladni ci snizenou musi byt zadan
            error = "";
            if (!this.cbxZakladZakladni.Checked && !this.cbxZakladSnizena.Checked)
            {
                error = "Jeden z údajů \"" + this.cbxZakladZakladni.Text + "\""+
                        " či \""+ this.cbxZakladSnizena.Text + "\" musí být zadán.";
                this.errorProvider.SetError(this.txtZakladZakladni, error);
                this.errorProvider.SetError(this.txtZakladSnizena, error);
            }
            if (error != "")
                isValid = false;

            // Certifikat
            if ((error = zjistiChybyProProstyText(this.txtCertifikat)) == "")
            {
                string certifikatFileName = Application.StartupPath + "\\" + this.txtCertifikat.Text;
                if (!File.Exists(certifikatFileName))
                {
                    error = "Soubor s certifikátem poplatníka \"" + certifikatFileName + "\" nebyl nalezen.";
                    this.errorProvider.SetError(this.txtCertifikat, error);
                }
            }
            if (error != "")
                isValid = false;

            // Heslo k privatnimu klici certifikatu poplatnika
            if (zjistiChybyProProstyText(this.txtHeslo) != "")
                isValid = false;

            // Mezni doba odezvy
            if (this.cbxTimeout.Checked)
            {
                error = "";
                this.errorProvider.SetError(this.txtTimeout, error);
                if ((error = validujZadani(this.txtTimeout.Text)) != "")
                    this.errorProvider.SetError(this.txtTimeout, error);
                else
                {
                    if ((error = validujCislo(this.txtTimeout.Text)) != "")
                        this.errorProvider.SetError(this.txtTimeout, error);
                }
                if (error != "")
                    isValid = false;
            }
            else
                this.errorProvider.SetError(this.txtTimeout, "");

            return isValid;
        }

        private string zjistiChybyProProstyText(Control control)
        {
            string error = "";
            this.errorProvider.SetError(control, error);
            if ((error = validujZadani(control.Text)) != "")
                this.errorProvider.SetError(control, error);
            return error;
        }

        private string zjistiChybyProCastku(Control control)
        {
            string error = "";
            this.errorProvider.SetError(control, error);
            if ((error = validujZadani(control.Text)) != "")
                this.errorProvider.SetError(control, error);
            else
            {
                if ((error = validujCastku(control.Text)) != "")
                    this.errorProvider.SetError(control, error);
            }
            return error;
        }

        private string validujZadani(string text)
        {
            if (String.IsNullOrEmpty(text))
                return "Údaj musí být zadán.";
            else
                return "";
        }

        private string validujCislo(string text)
        {
            try
            {
                int val = int.Parse(text);
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private string validujDatum(string text)
        {
            try
            {
                DateTime val = DateTime.Parse(text);
                DateTime val2 = DateTime.Parse(val.ToString("yyyy-MM-ddTHH:mm:ss"));
                if (val != val2)
                    throw new Exception("Zadejte datum a čas s přesností maximálně na vteřiny.");
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private string validujCastku(string text)
        {
            try
            {
                decimal val = decimal.Parse(text);
                if (val==0m)
                    throw new Exception("Zadejte nenulovou částku.");
                if (eet.FormatMoney(val)!= val)
                    throw new Exception("Zadejte částku s přesností maximálně na haléře.");
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private void prepocitejCastky()
        {
            decimal dph, zaokrouhleni, trzba;
            decimal castkaCelkem = 0m;
            
            // DPH zakladni sazba
            try
            {
                decimal zakladZakladni = decimal.Parse(this.txtZakladZakladni.Text);
                dph = eet.FormatMoney((decimal)dphZakladni / 100m * zakladZakladni);
                this.txtDPHZakladni.Text = eet.GetMoneyAsText(dph);
                castkaCelkem += zakladZakladni + dph;
            }
            catch
            {
                this.txtDPHZakladni.Text = "";
            }

            // DPH snizena
            try
            {
                decimal zakladSnizena = decimal.Parse(this.txtZakladSnizena.Text);
                dph = eet.FormatMoney((decimal)dphSnizena / 100m * zakladSnizena);
                this.txtDPHSnizena.Text = eet.GetMoneyAsText(dph);
                castkaCelkem += zakladSnizena + dph;
            }
            catch
            {
                this.txtDPHSnizena.Text = "";
            }

            // Castka celkem
            this.txtCelkem.Text = eet.GetMoneyAsText(castkaCelkem);

            // Trzba - lide uz neplati v halerovych mincich
            trzba = decimal.Round(castkaCelkem,0);
            this.txtTrzba.Text = eet.GetMoneyAsText(trzba);

            // Halerove zaokrohelni
            zaokrouhleni = trzba - castkaCelkem;
            this.txtZaokrouhleni.Text = eet.GetMoneyAsText(zaokrouhleni);
        }

        private void cbxZakladZakladni_CheckedChanged(object sender, EventArgs e)
        {
            if (!this.cbxZakladZakladni.Checked)
            {
                this.txtZakladZakladni.Text = "";
                this.errorProvider.SetError(this.txtZakladZakladni, "");
                this.txtZakladZakladni.ReadOnly = true;
            }
            else
                this.txtZakladZakladni.ReadOnly = false;
            this.prepocitejCastky();
        }

        private void cbxZakladSnizena_CheckedChanged(object sender, EventArgs e)
        {
            if (!this.cbxZakladSnizena.Checked)
            {
                this.txtZakladSnizena.Text = "";
                this.errorProvider.SetError(this.txtZakladSnizena, "");
                this.txtZakladSnizena.ReadOnly = true;
            }
            else
                this.txtZakladSnizena.ReadOnly = false;
            this.prepocitejCastky();
        }

        private void txtZakladZakladni_TextChanged(object sender, EventArgs e)
        {
            zjistiChybyProCastku(this.txtZakladZakladni);
            this.prepocitejCastky();
        }

        private void txtZakladSnizena_TextChanged(object sender, EventArgs e)
        {
            zjistiChybyProCastku(this.txtZakladSnizena);
            this.prepocitejCastky();
        }
    }
}
