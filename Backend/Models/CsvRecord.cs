using System;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;

namespace BatchPayments.Utility.Models
{
    public class CsvRecord
    {
        [Index(0)]
        public string IssCode { get; set; }
        [Index(1)]
        public string CaId { get; set; }
        [Index(2)]
        public DateTime PaymentDate { get; set; }
        [Index(3)]
        public string OfxClientId { get; set; }
        [Index(4)]
        public string InvoiceCode { get; set; }
        [Index(5)]
        public decimal NetAmount { get; set; }
        [Index(6)]
        public string PaymentId { get; set; }
        [Index(7)]
        public string PaymentRef { get; set; }
    }


    //public sealed class CsvRecordMap : ClassMap<CsvRecord>
    //{
    //    public CsvRecordMap()
    //    {
    //        Map(m => m.IssCode).Name("ISS-CODE");
    //        Map(m => m.CaId).Name("CA-ID");
    //        Map(m => m.PaymentDate).Name("PAYMENT-DATE");
    //        Map(m => m.OfxClientId).Name("OFX Client ID");
    //        Map(m => m.InvoiceCode).Name("INV Cde");
    //        Map(m => m.NetAmount).Name("NET AMT($)");
    //        Map(m => m.PaymentId).Name("PAYMENT-ID");
    //        Map(m => m.PaymentRef).Name("PAYEE REF");
    //    }
    //}
}
