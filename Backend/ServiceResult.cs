using System.Net;

namespace BatchPayments.Utility
{
    public enum ServiceResultStatus
    {
        Success = 0,
        Failure = 1
    }

    public class Error
    {
        public string Code { get; set; }
        public string Message { get; set; }
    }

    public class ServiceResult
    {
        public ServiceResultStatus Status => IsSuccess ? ServiceResultStatus.Success : ServiceResultStatus.Failure;

        public bool IsSuccess => Error == null;

        public Error Error { get; set; }

    }

    public class HttpServiceResult : ServiceResult
    {
        public HttpStatusCode HttpStatusCode { get; set; }
    }

    public class HttpServiceResult<T> : HttpServiceResult
    {
        public T Result { get; set; }
    }
}