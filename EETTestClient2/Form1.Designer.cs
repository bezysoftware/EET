namespace EETTestClient2
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnSend = new System.Windows.Forms.Button();
            this.txtProxy = new System.Windows.Forms.TextBox();
            this.rtbError = new System.Windows.Forms.RichTextBox();
            this.cbxProxy = new System.Windows.Forms.CheckBox();
            this.tabXML = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.rtbRequest = new System.Windows.Forms.RichTextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.rtbResponse = new System.Windows.Forms.RichTextBox();
            this.cbxTest = new System.Windows.Forms.CheckBox();
            this.tabError = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.rtbWarning = new System.Windows.Forms.RichTextBox();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.rtbPKP = new System.Windows.Forms.RichTextBox();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.rtbBKP = new System.Windows.Forms.RichTextBox();
            this.lblFIK = new System.Windows.Forms.Label();
            this.txtFIK = new System.Windows.Forms.TextBox();
            this.lblDIC = new System.Windows.Forms.Label();
            this.txtDIC = new System.Windows.Forms.TextBox();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.lblProvozovna = new System.Windows.Forms.Label();
            this.txtProvozovna = new System.Windows.Forms.TextBox();
            this.lblPokladna = new System.Windows.Forms.Label();
            this.txtPokladna = new System.Windows.Forms.TextBox();
            this.lblUctenka = new System.Windows.Forms.Label();
            this.txtUctenka = new System.Windows.Forms.TextBox();
            this.lblDatum = new System.Windows.Forms.Label();
            this.txtDatum = new System.Windows.Forms.TextBox();
            this.cbxZakladZakladni = new System.Windows.Forms.CheckBox();
            this.txtZakladZakladni = new System.Windows.Forms.TextBox();
            this.cbxZakladSnizena = new System.Windows.Forms.CheckBox();
            this.txtZakladSnizena = new System.Windows.Forms.TextBox();
            this.lblSazbaZakladni = new System.Windows.Forms.Label();
            this.txtDPHZakladni = new System.Windows.Forms.TextBox();
            this.txtDPHSnizena = new System.Windows.Forms.TextBox();
            this.lblCelkem = new System.Windows.Forms.Label();
            this.txtCelkem = new System.Windows.Forms.TextBox();
            this.lblSazbaSnizena = new System.Windows.Forms.Label();
            this.lblCertifikat = new System.Windows.Forms.Label();
            this.txtCertifikat = new System.Windows.Forms.TextBox();
            this.txtHeslo = new System.Windows.Forms.TextBox();
            this.lblHeslo = new System.Windows.Forms.Label();
            this.txtZaokrouhleni = new System.Windows.Forms.TextBox();
            this.txtTrzba = new System.Windows.Forms.TextBox();
            this.lblKc = new System.Windows.Forms.Label();
            this.lblZaokrouhleni = new System.Windows.Forms.Label();
            this.lblTrzba = new System.Windows.Forms.Label();
            this.cbxTimeout = new System.Windows.Forms.CheckBox();
            this.txtTimeout = new System.Windows.Forms.TextBox();
            this.lblTimeout = new System.Windows.Forms.Label();
            this.tabXML.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabError.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tabPage6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(276, 483);
            this.btnSend.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(276, 44);
            this.btnSend.TabIndex = 35;
            this.btnSend.Tag = "Odeslat tržbu|Jen otestovat spojení";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // txtProxy
            // 
            this.txtProxy.Enabled = false;
            this.txtProxy.Location = new System.Drawing.Point(276, 383);
            this.txtProxy.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtProxy.Name = "txtProxy";
            this.txtProxy.Size = new System.Drawing.Size(272, 31);
            this.txtProxy.TabIndex = 30;
            this.txtProxy.Text = "http://127.0.0.1:8888/";
            // 
            // rtbError
            // 
            this.rtbError.Location = new System.Drawing.Point(12, 12);
            this.rtbError.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.rtbError.Name = "rtbError";
            this.rtbError.ReadOnly = true;
            this.rtbError.Size = new System.Drawing.Size(512, 158);
            this.rtbError.TabIndex = 5;
            this.rtbError.Text = "";
            // 
            // cbxProxy
            // 
            this.cbxProxy.AutoSize = true;
            this.cbxProxy.Location = new System.Drawing.Point(30, 383);
            this.cbxProxy.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.cbxProxy.Name = "cbxProxy";
            this.cbxProxy.Size = new System.Drawing.Size(213, 29);
            this.cbxProxy.TabIndex = 29;
            this.cbxProxy.Text = "Debugging proxy:";
            this.cbxProxy.UseVisualStyleBackColor = true;
            this.cbxProxy.CheckedChanged += new System.EventHandler(this.cbxProxy_CheckedChanged);
            // 
            // tabXML
            // 
            this.tabXML.Controls.Add(this.tabPage1);
            this.tabXML.Controls.Add(this.tabPage2);
            this.tabXML.Location = new System.Drawing.Point(618, 383);
            this.tabXML.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tabXML.Name = "tabXML";
            this.tabXML.SelectedIndex = 0;
            this.tabXML.Size = new System.Drawing.Size(738, 475);
            this.tabXML.TabIndex = 36;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.rtbRequest);
            this.tabPage1.Location = new System.Drawing.Point(8, 39);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tabPage1.Size = new System.Drawing.Size(722, 428);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "XML Request";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // rtbRequest
            // 
            this.rtbRequest.Location = new System.Drawing.Point(14, 13);
            this.rtbRequest.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.rtbRequest.Name = "rtbRequest";
            this.rtbRequest.ReadOnly = true;
            this.rtbRequest.Size = new System.Drawing.Size(692, 396);
            this.rtbRequest.TabIndex = 0;
            this.rtbRequest.Text = "";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.rtbResponse);
            this.tabPage2.Location = new System.Drawing.Point(8, 39);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tabPage2.Size = new System.Drawing.Size(722, 428);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "XML Response";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // rtbResponse
            // 
            this.rtbResponse.Location = new System.Drawing.Point(14, 13);
            this.rtbResponse.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.rtbResponse.Name = "rtbResponse";
            this.rtbResponse.ReadOnly = true;
            this.rtbResponse.Size = new System.Drawing.Size(692, 396);
            this.rtbResponse.TabIndex = 0;
            this.rtbResponse.Text = "";
            // 
            // cbxTest
            // 
            this.cbxTest.AutoSize = true;
            this.cbxTest.Location = new System.Drawing.Point(30, 490);
            this.cbxTest.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.cbxTest.Name = "cbxTest";
            this.cbxTest.Size = new System.Drawing.Size(188, 29);
            this.cbxTest.TabIndex = 34;
            this.cbxTest.Text = "Ověřovací mód";
            this.cbxTest.UseVisualStyleBackColor = true;
            this.cbxTest.CheckedChanged += new System.EventHandler(this.cbxTest_CheckedChanged);
            // 
            // tabError
            // 
            this.tabError.Controls.Add(this.tabPage3);
            this.tabError.Controls.Add(this.tabPage4);
            this.tabError.Controls.Add(this.tabPage5);
            this.tabError.Controls.Add(this.tabPage6);
            this.tabError.Location = new System.Drawing.Point(30, 560);
            this.tabError.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tabError.Name = "tabError";
            this.tabError.SelectedIndex = 0;
            this.tabError.Size = new System.Drawing.Size(558, 235);
            this.tabError.TabIndex = 37;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.rtbError);
            this.tabPage3.Location = new System.Drawing.Point(8, 39);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tabPage3.Size = new System.Drawing.Size(542, 188);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Tag = "Chyby|! Chyby !";
            this.tabPage3.Text = "Chyby";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.rtbWarning);
            this.tabPage4.Location = new System.Drawing.Point(8, 39);
            this.tabPage4.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tabPage4.Size = new System.Drawing.Size(542, 188);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Tag = "Varování|! Varování !";
            this.tabPage4.Text = "Varování";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // rtbWarning
            // 
            this.rtbWarning.Location = new System.Drawing.Point(12, 12);
            this.rtbWarning.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.rtbWarning.Name = "rtbWarning";
            this.rtbWarning.ReadOnly = true;
            this.rtbWarning.Size = new System.Drawing.Size(512, 158);
            this.rtbWarning.TabIndex = 0;
            this.rtbWarning.Text = "";
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.rtbPKP);
            this.tabPage5.Location = new System.Drawing.Point(8, 39);
            this.tabPage5.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(542, 188);
            this.tabPage5.TabIndex = 2;
            this.tabPage5.Text = "PKP";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // rtbPKP
            // 
            this.rtbPKP.Location = new System.Drawing.Point(12, 12);
            this.rtbPKP.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.rtbPKP.Name = "rtbPKP";
            this.rtbPKP.ReadOnly = true;
            this.rtbPKP.Size = new System.Drawing.Size(512, 158);
            this.rtbPKP.TabIndex = 1;
            this.rtbPKP.Text = "";
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.rtbBKP);
            this.tabPage6.Location = new System.Drawing.Point(8, 39);
            this.tabPage6.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Size = new System.Drawing.Size(542, 188);
            this.tabPage6.TabIndex = 3;
            this.tabPage6.Text = "BKP";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // rtbBKP
            // 
            this.rtbBKP.Location = new System.Drawing.Point(12, 12);
            this.rtbBKP.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.rtbBKP.Name = "rtbBKP";
            this.rtbBKP.ReadOnly = true;
            this.rtbBKP.Size = new System.Drawing.Size(512, 158);
            this.rtbBKP.TabIndex = 2;
            this.rtbBKP.Text = "";
            // 
            // lblFIK
            // 
            this.lblFIK.AutoSize = true;
            this.lblFIK.Location = new System.Drawing.Point(24, 823);
            this.lblFIK.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblFIK.Name = "lblFIK";
            this.lblFIK.Size = new System.Drawing.Size(50, 25);
            this.lblFIK.TabIndex = 38;
            this.lblFIK.Text = "FIK:";
            // 
            // txtFIK
            // 
            this.txtFIK.Location = new System.Drawing.Point(88, 817);
            this.txtFIK.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtFIK.Name = "txtFIK";
            this.txtFIK.ReadOnly = true;
            this.txtFIK.Size = new System.Drawing.Size(488, 31);
            this.txtFIK.TabIndex = 39;
            // 
            // lblDIC
            // 
            this.lblDIC.AutoSize = true;
            this.lblDIC.Location = new System.Drawing.Point(24, 17);
            this.lblDIC.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblDIC.Name = "lblDIC";
            this.lblDIC.Size = new System.Drawing.Size(53, 25);
            this.lblDIC.TabIndex = 0;
            this.lblDIC.Text = "DIČ:";
            // 
            // txtDIC
            // 
            this.txtDIC.Location = new System.Drawing.Point(192, 12);
            this.txtDIC.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtDIC.Name = "txtDIC";
            this.txtDIC.Size = new System.Drawing.Size(228, 31);
            this.txtDIC.TabIndex = 1;
            this.txtDIC.Text = "CZ00000019";
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // lblProvozovna
            // 
            this.lblProvozovna.AutoSize = true;
            this.lblProvozovna.Location = new System.Drawing.Point(472, 17);
            this.lblProvozovna.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblProvozovna.Name = "lblProvozovna";
            this.lblProvozovna.Size = new System.Drawing.Size(132, 25);
            this.lblProvozovna.TabIndex = 2;
            this.lblProvozovna.Text = "Provozovna:";
            // 
            // txtProvozovna
            // 
            this.txtProvozovna.Location = new System.Drawing.Point(618, 12);
            this.txtProvozovna.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtProvozovna.Name = "txtProvozovna";
            this.txtProvozovna.Size = new System.Drawing.Size(116, 31);
            this.txtProvozovna.TabIndex = 3;
            this.txtProvozovna.Text = "356";
            // 
            // lblPokladna
            // 
            this.lblPokladna.AutoSize = true;
            this.lblPokladna.Location = new System.Drawing.Point(788, 17);
            this.lblPokladna.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblPokladna.Name = "lblPokladna";
            this.lblPokladna.Size = new System.Drawing.Size(108, 25);
            this.lblPokladna.TabIndex = 4;
            this.lblPokladna.Text = "Pokladna:";
            // 
            // txtPokladna
            // 
            this.txtPokladna.Location = new System.Drawing.Point(910, 12);
            this.txtPokladna.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtPokladna.Name = "txtPokladna";
            this.txtPokladna.Size = new System.Drawing.Size(228, 31);
            this.txtPokladna.TabIndex = 5;
            this.txtPokladna.Text = "POKLADNA_1";
            // 
            // lblUctenka
            // 
            this.lblUctenka.AutoSize = true;
            this.lblUctenka.Location = new System.Drawing.Point(24, 65);
            this.lblUctenka.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblUctenka.Name = "lblUctenka";
            this.lblUctenka.Size = new System.Drawing.Size(153, 25);
            this.lblUctenka.TabIndex = 6;
            this.lblUctenka.Text = "Číslo účtenky::";
            // 
            // txtUctenka
            // 
            this.txtUctenka.Location = new System.Drawing.Point(192, 60);
            this.txtUctenka.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtUctenka.Name = "txtUctenka";
            this.txtUctenka.Size = new System.Drawing.Size(306, 31);
            this.txtUctenka.TabIndex = 7;
            this.txtUctenka.Text = "PD2016/8-22";
            // 
            // lblDatum
            // 
            this.lblDatum.AutoSize = true;
            this.lblDatum.Location = new System.Drawing.Point(708, 65);
            this.lblDatum.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblDatum.Name = "lblDatum";
            this.lblDatum.Size = new System.Drawing.Size(191, 25);
            this.lblDatum.TabIndex = 8;
            this.lblDatum.Text = "Datum a čas tržby:";
            // 
            // txtDatum
            // 
            this.txtDatum.Location = new System.Drawing.Point(910, 60);
            this.txtDatum.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtDatum.Name = "txtDatum";
            this.txtDatum.Size = new System.Drawing.Size(234, 31);
            this.txtDatum.TabIndex = 9;
            this.txtDatum.Text = "18.7.2016 15:23:17";
            // 
            // cbxZakladZakladni
            // 
            this.cbxZakladZakladni.AutoSize = true;
            this.cbxZakladZakladni.Checked = true;
            this.cbxZakladZakladni.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxZakladZakladni.Location = new System.Drawing.Point(30, 133);
            this.cbxZakladZakladni.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.cbxZakladZakladni.Name = "cbxZakladZakladni";
            this.cbxZakladZakladni.Size = new System.Drawing.Size(302, 29);
            this.cbxZakladZakladni.TabIndex = 10;
            this.cbxZakladZakladni.Text = "Základ pro základní sazbu:";
            this.cbxZakladZakladni.UseVisualStyleBackColor = true;
            this.cbxZakladZakladni.CheckedChanged += new System.EventHandler(this.cbxZakladZakladni_CheckedChanged);
            // 
            // txtZakladZakladni
            // 
            this.txtZakladZakladni.Location = new System.Drawing.Point(352, 129);
            this.txtZakladZakladni.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtZakladZakladni.Name = "txtZakladZakladni";
            this.txtZakladZakladni.Size = new System.Drawing.Size(196, 31);
            this.txtZakladZakladni.TabIndex = 11;
            this.txtZakladZakladni.Text = "8725,50";
            this.txtZakladZakladni.TextChanged += new System.EventHandler(this.txtZakladZakladni_TextChanged);
            // 
            // cbxZakladSnizena
            // 
            this.cbxZakladSnizena.AutoSize = true;
            this.cbxZakladSnizena.Location = new System.Drawing.Point(30, 181);
            this.cbxZakladSnizena.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.cbxZakladSnizena.Name = "cbxZakladSnizena";
            this.cbxZakladSnizena.Size = new System.Drawing.Size(309, 29);
            this.cbxZakladSnizena.TabIndex = 14;
            this.cbxZakladSnizena.Text = "Základ pro sníženou sazbu:";
            this.cbxZakladSnizena.UseVisualStyleBackColor = true;
            this.cbxZakladSnizena.CheckedChanged += new System.EventHandler(this.cbxZakladSnizena_CheckedChanged);
            // 
            // txtZakladSnizena
            // 
            this.txtZakladSnizena.Location = new System.Drawing.Point(352, 177);
            this.txtZakladSnizena.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtZakladSnizena.Name = "txtZakladSnizena";
            this.txtZakladSnizena.ReadOnly = true;
            this.txtZakladSnizena.Size = new System.Drawing.Size(196, 31);
            this.txtZakladSnizena.TabIndex = 15;
            this.txtZakladSnizena.TextChanged += new System.EventHandler(this.txtZakladSnizena_TextChanged);
            // 
            // lblSazbaZakladni
            // 
            this.lblSazbaZakladni.AutoSize = true;
            this.lblSazbaZakladni.Location = new System.Drawing.Point(596, 131);
            this.lblSazbaZakladni.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblSazbaZakladni.Name = "lblSazbaZakladni";
            this.lblSazbaZakladni.Size = new System.Drawing.Size(87, 25);
            this.lblSazbaZakladni.TabIndex = 12;
            this.lblSazbaZakladni.Text = "% DPH:";
            // 
            // txtDPHZakladni
            // 
            this.txtDPHZakladni.Location = new System.Drawing.Point(716, 127);
            this.txtDPHZakladni.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtDPHZakladni.Name = "txtDPHZakladni";
            this.txtDPHZakladni.ReadOnly = true;
            this.txtDPHZakladni.Size = new System.Drawing.Size(196, 31);
            this.txtDPHZakladni.TabIndex = 13;
            // 
            // txtDPHSnizena
            // 
            this.txtDPHSnizena.Location = new System.Drawing.Point(714, 177);
            this.txtDPHSnizena.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtDPHSnizena.Name = "txtDPHSnizena";
            this.txtDPHSnizena.ReadOnly = true;
            this.txtDPHSnizena.Size = new System.Drawing.Size(196, 31);
            this.txtDPHSnizena.TabIndex = 17;
            // 
            // lblCelkem
            // 
            this.lblCelkem.AutoSize = true;
            this.lblCelkem.Location = new System.Drawing.Point(24, 237);
            this.lblCelkem.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblCelkem.Name = "lblCelkem";
            this.lblCelkem.Size = new System.Drawing.Size(159, 25);
            this.lblCelkem.TabIndex = 18;
            this.lblCelkem.Text = "Částka celkem:";
            // 
            // txtCelkem
            // 
            this.txtCelkem.Location = new System.Drawing.Point(192, 231);
            this.txtCelkem.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtCelkem.Name = "txtCelkem";
            this.txtCelkem.ReadOnly = true;
            this.txtCelkem.Size = new System.Drawing.Size(196, 31);
            this.txtCelkem.TabIndex = 19;
            // 
            // lblSazbaSnizena
            // 
            this.lblSazbaSnizena.AutoSize = true;
            this.lblSazbaSnizena.Location = new System.Drawing.Point(596, 183);
            this.lblSazbaSnizena.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblSazbaSnizena.Name = "lblSazbaSnizena";
            this.lblSazbaSnizena.Size = new System.Drawing.Size(87, 25);
            this.lblSazbaSnizena.TabIndex = 16;
            this.lblSazbaSnizena.Text = "% DPH:";
            // 
            // lblCertifikat
            // 
            this.lblCertifikat.AutoSize = true;
            this.lblCertifikat.Location = new System.Drawing.Point(24, 308);
            this.lblCertifikat.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblCertifikat.Name = "lblCertifikat";
            this.lblCertifikat.Size = new System.Drawing.Size(208, 25);
            this.lblCertifikat.TabIndex = 25;
            this.lblCertifikat.Text = "Certifikát poplatníka:";
            // 
            // txtCertifikat
            // 
            this.txtCertifikat.Location = new System.Drawing.Point(260, 302);
            this.txtCertifikat.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtCertifikat.Name = "txtCertifikat";
            this.txtCertifikat.Size = new System.Drawing.Size(288, 31);
            this.txtCertifikat.TabIndex = 26;
            this.txtCertifikat.Text = "00000019.p12";
            // 
            // txtHeslo
            // 
            this.txtHeslo.Location = new System.Drawing.Point(754, 302);
            this.txtHeslo.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtHeslo.Name = "txtHeslo";
            this.txtHeslo.PasswordChar = '*';
            this.txtHeslo.Size = new System.Drawing.Size(288, 31);
            this.txtHeslo.TabIndex = 28;
            this.txtHeslo.Text = "eet";
            // 
            // lblHeslo
            // 
            this.lblHeslo.AutoSize = true;
            this.lblHeslo.Location = new System.Drawing.Point(668, 308);
            this.lblHeslo.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblHeslo.Name = "lblHeslo";
            this.lblHeslo.Size = new System.Drawing.Size(73, 25);
            this.lblHeslo.TabIndex = 27;
            this.lblHeslo.Text = "Heslo:";
            // 
            // txtZaokrouhleni
            // 
            this.txtZaokrouhleni.Location = new System.Drawing.Point(714, 229);
            this.txtZaokrouhleni.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtZaokrouhleni.Name = "txtZaokrouhleni";
            this.txtZaokrouhleni.ReadOnly = true;
            this.txtZaokrouhleni.Size = new System.Drawing.Size(196, 31);
            this.txtZaokrouhleni.TabIndex = 21;
            // 
            // txtTrzba
            // 
            this.txtTrzba.Location = new System.Drawing.Point(1106, 231);
            this.txtTrzba.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtTrzba.Name = "txtTrzba";
            this.txtTrzba.ReadOnly = true;
            this.txtTrzba.Size = new System.Drawing.Size(196, 31);
            this.txtTrzba.TabIndex = 23;
            // 
            // lblKc
            // 
            this.lblKc.AutoSize = true;
            this.lblKc.Location = new System.Drawing.Point(1318, 237);
            this.lblKc.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblKc.Name = "lblKc";
            this.lblKc.Size = new System.Drawing.Size(37, 25);
            this.lblKc.TabIndex = 24;
            this.lblKc.Text = "Kč";
            // 
            // lblZaokrouhleni
            // 
            this.lblZaokrouhleni.AutoSize = true;
            this.lblZaokrouhleni.Location = new System.Drawing.Point(464, 235);
            this.lblZaokrouhleni.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblZaokrouhleni.Name = "lblZaokrouhleni";
            this.lblZaokrouhleni.Size = new System.Drawing.Size(233, 25);
            this.lblZaokrouhleni.TabIndex = 20;
            this.lblZaokrouhleni.Text = "Haléřové zaokrouhlení:";
            // 
            // lblTrzba
            // 
            this.lblTrzba.AutoSize = true;
            this.lblTrzba.Location = new System.Drawing.Point(946, 237);
            this.lblTrzba.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblTrzba.Name = "lblTrzba";
            this.lblTrzba.Size = new System.Drawing.Size(147, 25);
            this.lblTrzba.TabIndex = 22;
            this.lblTrzba.Text = "Tržba celkem:";
            // 
            // cbxTimeout
            // 
            this.cbxTimeout.AutoSize = true;
            this.cbxTimeout.Location = new System.Drawing.Point(30, 438);
            this.cbxTimeout.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.cbxTimeout.Name = "cbxTimeout";
            this.cbxTimeout.Size = new System.Drawing.Size(237, 29);
            this.cbxTimeout.TabIndex = 31;
            this.cbxTimeout.Text = "Mezní doba odezvy:";
            this.cbxTimeout.UseVisualStyleBackColor = true;
            this.cbxTimeout.CheckedChanged += new System.EventHandler(this.cbxTimeout_CheckedChanged);
            // 
            // txtTimeout
            // 
            this.txtTimeout.Enabled = false;
            this.txtTimeout.Location = new System.Drawing.Point(276, 433);
            this.txtTimeout.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtTimeout.Name = "txtTimeout";
            this.txtTimeout.Size = new System.Drawing.Size(112, 31);
            this.txtTimeout.TabIndex = 32;
            this.txtTimeout.Text = "3000";
            // 
            // lblTimeout
            // 
            this.lblTimeout.AutoSize = true;
            this.lblTimeout.Location = new System.Drawing.Point(404, 440);
            this.lblTimeout.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblTimeout.Name = "lblTimeout";
            this.lblTimeout.Size = new System.Drawing.Size(40, 25);
            this.lblTimeout.TabIndex = 33;
            this.lblTimeout.Text = "ms";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1390, 894);
            this.Controls.Add(this.lblTimeout);
            this.Controls.Add(this.txtTimeout);
            this.Controls.Add(this.cbxTimeout);
            this.Controls.Add(this.lblTrzba);
            this.Controls.Add(this.lblZaokrouhleni);
            this.Controls.Add(this.lblKc);
            this.Controls.Add(this.txtTrzba);
            this.Controls.Add(this.txtZaokrouhleni);
            this.Controls.Add(this.lblHeslo);
            this.Controls.Add(this.txtHeslo);
            this.Controls.Add(this.txtCertifikat);
            this.Controls.Add(this.lblCertifikat);
            this.Controls.Add(this.lblSazbaSnizena);
            this.Controls.Add(this.txtCelkem);
            this.Controls.Add(this.lblCelkem);
            this.Controls.Add(this.txtDPHSnizena);
            this.Controls.Add(this.txtDPHZakladni);
            this.Controls.Add(this.lblSazbaZakladni);
            this.Controls.Add(this.txtZakladSnizena);
            this.Controls.Add(this.cbxZakladSnizena);
            this.Controls.Add(this.txtZakladZakladni);
            this.Controls.Add(this.cbxZakladZakladni);
            this.Controls.Add(this.txtDatum);
            this.Controls.Add(this.lblDatum);
            this.Controls.Add(this.txtUctenka);
            this.Controls.Add(this.lblUctenka);
            this.Controls.Add(this.txtPokladna);
            this.Controls.Add(this.lblPokladna);
            this.Controls.Add(this.txtProvozovna);
            this.Controls.Add(this.lblProvozovna);
            this.Controls.Add(this.txtDIC);
            this.Controls.Add(this.lblDIC);
            this.Controls.Add(this.txtFIK);
            this.Controls.Add(this.lblFIK);
            this.Controls.Add(this.tabError);
            this.Controls.Add(this.cbxTest);
            this.Controls.Add(this.tabXML);
            this.Controls.Add(this.cbxProxy);
            this.Controls.Add(this.txtProxy);
            this.Controls.Add(this.btnSend);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "EETTestClient2";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabXML.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabError.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.tabPage6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox txtProxy;
        private System.Windows.Forms.RichTextBox rtbError;
        private System.Windows.Forms.CheckBox cbxProxy;
        private System.Windows.Forms.TabControl tabXML;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.RichTextBox rtbRequest;
        private System.Windows.Forms.RichTextBox rtbResponse;
        private System.Windows.Forms.CheckBox cbxTest;
        private System.Windows.Forms.TabControl tabError;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.RichTextBox rtbWarning;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.RichTextBox rtbPKP;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.RichTextBox rtbBKP;
        private System.Windows.Forms.Label lblFIK;
        private System.Windows.Forms.TextBox txtFIK;
        private System.Windows.Forms.Label lblDIC;
        private System.Windows.Forms.TextBox txtDIC;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.TextBox txtProvozovna;
        private System.Windows.Forms.Label lblProvozovna;
        private System.Windows.Forms.TextBox txtPokladna;
        private System.Windows.Forms.Label lblPokladna;
        private System.Windows.Forms.TextBox txtUctenka;
        private System.Windows.Forms.Label lblUctenka;
        private System.Windows.Forms.TextBox txtDatum;
        private System.Windows.Forms.Label lblDatum;
        private System.Windows.Forms.Label lblSazbaZakladni;
        private System.Windows.Forms.TextBox txtZakladSnizena;
        private System.Windows.Forms.CheckBox cbxZakladSnizena;
        private System.Windows.Forms.TextBox txtZakladZakladni;
        private System.Windows.Forms.CheckBox cbxZakladZakladni;
        private System.Windows.Forms.TextBox txtDPHSnizena;
        private System.Windows.Forms.TextBox txtDPHZakladni;
        private System.Windows.Forms.TextBox txtCelkem;
        private System.Windows.Forms.Label lblCelkem;
        private System.Windows.Forms.Label lblSazbaSnizena;
        private System.Windows.Forms.Label lblCertifikat;
        private System.Windows.Forms.TextBox txtCertifikat;
        private System.Windows.Forms.Label lblHeslo;
        private System.Windows.Forms.TextBox txtHeslo;
        private System.Windows.Forms.Label lblTrzba;
        private System.Windows.Forms.Label lblZaokrouhleni;
        private System.Windows.Forms.Label lblKc;
        private System.Windows.Forms.TextBox txtTrzba;
        private System.Windows.Forms.TextBox txtZaokrouhleni;
        private System.Windows.Forms.CheckBox cbxTimeout;
        private System.Windows.Forms.Label lblTimeout;
        private System.Windows.Forms.TextBox txtTimeout;
    }
}

