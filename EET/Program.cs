namespace EET
{
    using EETLibrary;

    using System;
    using System.Configuration;
    using System.IO;
    using System.Reflection;
    using log4net;
    using log4net.Config;
    using Newtonsoft.Json;
    using System.Linq;
    using System.Threading;

    public class Program
    {
        private static EETHelper helper;
        private static ILog log;
        private static string[] eetParams;
        private static Input input;
        private static Output output;
        private static bool success;

        public static int Main(string[] args)
        {
            eetParams = args;

            try
            {
                InitializeLogger();
                InitializeHelper();
                LoadInput();
                PopulateData();
                SendPayload();
                ProcessResult();
                WriteOutput();

                return success ? 0 : -1;
                
            }
            catch (Exception ex)
            {
                log.Error("Uncaught top level exception.", ex);
                return -1;
            }
        }

        private static void ProcessResult()
        {
            log.Debug("Processing result");
            if (success)
            { 
                log.Info("Result indicates success");
            }
            else
            {
                log.Error("Result indicates failure");
            }

            output.BKP = helper.BKP;
            output.PKP = helper.PKP;
            output.FIK = helper.Potvrzeni?.fik ?? string.Empty;
            output.Success = success;
            output.Errors = string.Join("\n", helper.Chyba?.Text ?? Enumerable.Empty<string>());
            output.Warnings = string.Join("\n", helper.Varovani?.SelectMany(w => w.Text) ?? Enumerable.Empty<string>());

            if (!string.IsNullOrEmpty(output.Errors))
            {
                log.ErrorFormat("Received errors: {0}", output.Errors);
            }

            if (!string.IsNullOrEmpty(output.Warnings))
            {
                log.WarnFormat("Received warnings: {0}", output.Warnings);
            }

            log.Debug("Result processed");
        }

        private static void LoadInput()
        {
            log.Debug("Loading input");

            var txt = File.ReadAllText(eetParams[0] + ".in");
            input = JsonConvert.DeserializeObject<Input>(txt);

            log.DebugFormat("Input read: {0}", eetParams[0] + ".in");
        }

        private static void WriteOutput()
        {
            log.Debug("Writing output");

            var txt = JsonConvert.SerializeObject(output, Formatting.Indented);
            using (var writter = File.CreateText(eetParams[0] + ".out"))
            {
                writter.WriteLine(txt);
            }

            log.DebugFormat("Output written: {0}", eetParams[0] + ".out");
        }

        private static void SendPayload()
        {
            log.Debug("Sending payload");
            helper.VypoctiPKPaBKP();

            output = new Output();
            success = false;
            var cooldown = int.Parse(ConfigurationManager.AppSettings["MilisecondsBetweenFails"]);

            for (int i = 0; i < 5; i++)
            {
                try
                {
                    success = helper.OdeslaniTrzby();
                    log.Info("Payload sent");
                    log.DebugFormat("Reqeust: {0} \n Response: {1}", helper.XMLRequest, helper.XMLResponse);

                    return;
                }
                catch(Exception ex)
                {
                    log.WarnFormat("Error while sending payload: \n {0} \n Reqeust: {1} \n Response: {2}", ex, helper.XMLRequest, helper.XMLResponse);
                    helper.Hlavicka.prvni_zaslani = false;
                    Thread.Sleep(cooldown);
                }
            }

            log.ErrorFormat("Error while sending payload: \n Reqeust: {0} \n Response: {1}", helper.XMLRequest, helper.XMLResponse);
        }

        private static void PopulateData()
        {
            helper.Data.porad_cis = input.ReceiptNumber;
            helper.Data.dat_trzby = helper.FormatDate(input.ReceiptTime);
            helper.Data.celk_trzba = helper.FormatMoney(input.TotalAmount);

            helper.Data.dan1Specified = input.Tax1 != 0;
            helper.Data.dan2Specified = input.Tax2 != 0;
            helper.Data.dan3Specified = input.Tax3 != 0;

            helper.Data.zakl_dan1Specified = input.AmountForTax1 != 0;  
            helper.Data.zakl_dan2Specified = input.AmountForTax2 != 0;
            helper.Data.zakl_dan3Specified = input.AmountForTax3 != 0;
            helper.Data.zakl_nepodl_dphSpecified = input.AmountForNoTax != 0;

            helper.Data.zakl_dan1 = helper.FormatMoney(input.AmountForTax1);
            helper.Data.zakl_dan2 = helper.FormatMoney(input.AmountForTax2);
            helper.Data.zakl_dan3 = helper.FormatMoney(input.AmountForTax3);
            helper.Data.zakl_nepodl_dph = helper.FormatMoney(input.AmountForNoTax);

            helper.Data.dan1 = helper.FormatMoney(input.Tax1);
            helper.Data.dan2 = helper.FormatMoney(input.Tax2);
            helper.Data.dan3 = helper.FormatMoney(input.Tax3);

            helper.Hlavicka.prvni_zaslani = !input.RepeatedAttempt;
        }

        private static void InitializeHelper()
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            log.DebugFormat("Initializing EET helper, path: {0}", path);

            var eet = new EETHelper();

            eet.UrlAddress = ConfigurationManager.AppSettings["EETServiceUrlAddress"];
            eet.LoadCertificate(path + "\\" + ConfigurationManager.AppSettings["CertificateFileName"], ConfigurationManager.AppSettings["CertificatePassword"]);

            eet.Hlavicka.uuid_zpravy = eet.GenerateUUID();
            eet.Hlavicka.dat_odesl = eet.FormatDate(DateTime.Now);
            eet.Hlavicka.prvni_zaslani = true;
            eet.Hlavicka.overeniSpecified = true;
            eet.Hlavicka.overeni = false;

            eet.Data.dic_popl = ConfigurationManager.AppSettings["DIC"];
            eet.Data.id_provoz = int.Parse(ConfigurationManager.AppSettings["StoreId"]);
            eet.Data.id_pokl = ConfigurationManager.AppSettings["RegisterId"];
            eet.Data.rezim = 0;

            helper = eet;

            log.Debug("EET helper initialized");
        }

        private static void InitializeLogger()
        {
            XmlConfigurator.Configure();
            log = LogManager.GetLogger("EET");
            log.InfoFormat("EET is starting with params: {0}.", string.Join(", ", eetParams));
        }
    }
}
