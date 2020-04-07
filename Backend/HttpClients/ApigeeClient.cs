using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using BatchPayments.Utility.HttpClients.Responses;
using BatchPayments.Utility.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace BatchPayments.Utility.HttpClients
{

    public interface IApigeeClient
    {
        Task<HttpServiceResult<string>> GetAccessTokenAsync();
    }

    public class ApigeeClient : IApigeeClient
    {
        private HttpClient _httpClient;
        private string _apigeeClientId;
        private string _apigeeClientSecret;

        private ApigeeClient() { }

        public static ApigeeClient Build(ApigeeSettings apigeeSettings)
        {
            return new ApigeeClient
            {
                _httpClient = new HttpClient
                {
                    BaseAddress = new Uri(apigeeSettings.ApigeeBaseAddress)
                },
                _apigeeClientId = apigeeSettings.ApigeeClientId,
                _apigeeClientSecret = apigeeSettings.ApigeeClientSecret
            }; 
        }

        public async Task<HttpServiceResult<string>> GetAccessTokenAsync()
        {

            var httpContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("client_id", _apigeeClientId),
                new KeyValuePair<string, string>("client_secret", _apigeeClientSecret),
                new KeyValuePair<string, string>("grant_type", Constants.Apigee.TokenGrantType),
                new KeyValuePair<string, string>("scope", Constants.Apigee.TokenScope),
            });

            try
            {
                var (responseMessage, responseContent) =
                    await ApiLogger.WrapCall(
                        async () => await _httpClient.PostAsync("/v1/oauth/token", httpContent)
                    );

                if (responseMessage.IsSuccessStatusCode == false)
                {
                    return new HttpServiceResult<string>
                    {
                        HttpStatusCode = responseMessage.StatusCode,
                        Error = new Error
                        {
                            Message = $"Failed to get access token from {_httpClient.BaseAddress} \n " +
                                      $"http code {responseMessage.StatusCode} \n " +
                                      $"Response Content {responseContent}"
                        }
                    };
                }

                return new HttpServiceResult<string>
                {
                    HttpStatusCode = responseMessage.StatusCode,
                    Result = JsonConvert.DeserializeObject<ClientCredentialsTokenResponse>(responseContent).AccessToken
                };

            }
            catch (Exception ex)
            {
                return new HttpServiceResult<string>
                {
                    Error = new Error
                    {
                        Message = ex.Message
                    }
                };
            }

        }
    }
}
