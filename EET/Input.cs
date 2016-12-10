namespace EET
{
    using System;

    public class Input
    {
        public string ReceiptNumber { get; set; }

        public DateTime ReceiptTime { get; set; }

        public decimal TotalAmount { get; set; }

        public decimal Tax1 { get; set; }

        public decimal Tax2 { get; set; }

        public decimal Tax3 { get; set; }

        public decimal AmountForTax1 { get; set; }

        public decimal AmountForTax2 { get; set; }

        public decimal AmountForTax3 { get; set; }

        public decimal AmountForNoTax { get; set; }

        public bool RepeatedAttempt { get; set; }
    }
}
