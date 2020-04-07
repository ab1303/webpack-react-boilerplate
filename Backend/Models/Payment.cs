using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatchPayments.Utility.Models
{
    public class Payment
    {
        public string PaymentId { get; set; }
        public string RecipientType { get; set; }
        public string RecipientId { get; set; }
        public string PayeeReference { get; set; }
        public decimal Amount { get; set; }

    }
}
