using System;
using System.Net.Http;
using System.Threading.Tasks;
using BatchPayments.Utility.ExtensionHelpers;
using BatchPayments.Utility.HttpClients.Requests;
using BatchPayments.Utility.HttpClients.Responses;
using BatchPayments.Utility.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace BatchPayments.Utility.HttpClients
{

    public interface IPaymentBatchesClient
    {
        Task<HttpServiceResult<CreatePaymentBatchResponse>> CreatePaymentBatchAsync(string token, CreatePaymentBatchRequest request);
        Task<HttpServiceResult<CreatePaymentsResponse>> CreatePaymentsAsync(string token, string paymentBatchId, CreatePaymentsRequest request);
    }

    public class PaymentBatchesClient : IPaymentBatchesClient
    {
        private HttpClient _httpClient;
        private PaymentBatchesClient()
        {
            
        }

        public static PaymentBatchesClient Build(ApigeeSettings apigeeSettings)
        {
            return new PaymentBatchesClient
            {
                _httpClient = new HttpClient
                {
                    BaseAddress = new Uri(apigeeSettings.ApigeeBaseAddress)
                },
            };
        }


        public async Task<HttpServiceResult<CreatePaymentBatchResponse>> CreatePaymentBatchAsync(string token,
            CreatePaymentBatchRequest request)
        {
            try
            {

                var (responseMessage, responseContent) =
                    await ApiLogger.WrapCall(
                        async () =>
                        {
                            using (var requestMessage = new HttpRequestMessage
                            {
                                Method = HttpMethod.Post,
                                Headers =
                                {
                                    { Constants.Headers.Authorization, $"Bearer {token}"  }
                                },
                                Content = request.ToCamelcaseJsonStringContent(),
                                RequestUri = new Uri("/v1/batch-payments/paymentbatches", UriKind.Relative),
                            })
                            {
                                return await _httpClient.SendAsync(requestMessage);
                            }
                        }
                    );

                if (responseMessage.IsSuccessStatusCode == false)
                {
                    return new HttpServiceResult<CreatePaymentBatchResponse>
                    {
                        HttpStatusCode = responseMessage.StatusCode,
                        Error = new Error
                        {
                            Message = $"Failed to create payment batch on {_httpClient.BaseAddress} \n " +
                                      $"http code {responseMessage.StatusCode} \n " +
                                      $"Response Content {responseContent}"
                        }
                    };
                }

                return new HttpServiceResult<CreatePaymentBatchResponse>
                {
                    HttpStatusCode = responseMessage.StatusCode,
                    Result = JsonConvert.DeserializeObject<CreatePaymentBatchResponse>(responseContent)
                };

            }
            catch (Exception ex)
            {
                return new HttpServiceResult<CreatePaymentBatchResponse>
                {
                    Error = new Error
                    {
                        Message = ex.Message
                    }
                };
            }

        }

        public async Task<HttpServiceResult<CreatePaymentsResponse>> CreatePaymentsAsync(string token, string paymentBatchId, CreatePaymentsRequest request)
        {
            try
            {
                var (responseMessage, responseContent) =
                    await ApiLogger.WrapCall(
                        async () =>
                        {
                            using (var requestMessage = new HttpRequestMessage
                            {
                                Method = HttpMethod.Post,
                                Headers =
                                {
                                    { Constants.Headers.Authorization, $"Bearer {token}"  }
                                },
                                Content = request.ToCamelcaseJsonStringContent(),
                                RequestUri = new Uri($"/v1/batch-payments/paymentbatches/{paymentBatchId}/payments", UriKind.Relative),
                            })
                            {
                                return await _httpClient.SendAsync(requestMessage);
                            }
                        }
                    );

                if (responseMessage.IsSuccessStatusCode == false)
                {
                    return new HttpServiceResult<CreatePaymentsResponse>
                    {
                        HttpStatusCode = responseMessage.StatusCode,
                        Error = new Error
                        {
                            Message = $"Failed to add payments to a batch {_httpClient.BaseAddress} \n " +
                                      $"http code {responseMessage.StatusCode} \n " +
                                      $"Response Content {responseContent}"
                        }
                    };
                }

                return new HttpServiceResult<CreatePaymentsResponse>
                {
                    HttpStatusCode = responseMessage.StatusCode,
                    Result = JsonConvert.DeserializeObject<CreatePaymentsResponse>(responseContent)
                };

            }
            catch (Exception ex)
            {
                return new HttpServiceResult<CreatePaymentsResponse>
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
