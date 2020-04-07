namespace BatchPayments.Utility
{
    public static class Constants
    {
        public const string ApplicationName = "batch-payments-utility";

        public static class Headers
        {
            public static string Authorization => $"{nameof(Authorization)}";
            public static string ContentType => "Content-Type";
        }

        public static class Apigee
        {
            public static readonly string ClientId = "ClientId";
            public static readonly string ClientSecret = "ClientSecret";
            public static readonly string TokenGrantType = "client_credentials";
            public static readonly string TokenScope = "payments users batchpayments";
        }
    }
}
