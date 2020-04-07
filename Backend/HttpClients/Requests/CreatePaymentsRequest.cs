using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BatchPayments.Utility.Models;

namespace BatchPayments.Utility.HttpClients.Requests
{
    public class CreatePaymentsRequest
    {
        public IEnumerable<Payment> Payments { get; set; }
    }
}
