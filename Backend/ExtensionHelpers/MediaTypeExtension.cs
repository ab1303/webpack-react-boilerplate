using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace BatchPayments.Utility.ExtensionHelpers
{
    public static class MediaTypeExtension
    {
        public static StringContent ToJsonStringContent(this object request)
        {
            var json = JsonConvert.SerializeObject(request);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        public static StringContent ToCamelcaseJsonStringContent(this object request)
        {
            var json = JsonConvert.SerializeObject(request, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

       
   

    }
}
