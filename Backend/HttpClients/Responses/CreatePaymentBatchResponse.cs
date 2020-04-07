using System;

namespace BatchPayments.Utility.HttpClients.Responses
{
    public class CreatePaymentBatchResponse
    {
        public string PaymentBatchId { get; set; }
        public DateTime Created { get; set; }
    }
}
