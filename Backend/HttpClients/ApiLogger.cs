using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace BatchPayments.Utility.HttpClients
{
    public static class ApiLogger
    {
        public static async Task<(HttpResponseMessage, string)> WrapCall(Func<Task<HttpResponseMessage>> apiResponseFunc,
            [System.Runtime.CompilerServices.CallerMemberName] string memberName = "")
        {
            var result = await apiResponseFunc();

            Serilog.Log.Logger.Information(result.ToString());
            
            
            //Logger.Log(result.ToString());

            var stringResult = string.Empty;
            if (result.Content != null)
            {
                stringResult = await result.Content.ReadAsStringAsync();
            }

            Serilog.Log.Logger.Information($"Result of call to {memberName}: {stringResult}");
            return (result, stringResult);
        }
    }
}
