using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using BatchPayments.Utility.HttpClients;
using BatchPayments.Utility.HttpClients.Requests;
using BatchPayments.Utility.Models;
using CsvHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Serilog;

namespace BatchPayments.Utility.ApiControllers
{
    public class FileInputModel: ApigeeSettings
    {
        public IFormFile FileAttachment { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public FileUploadController(ILogger logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }

        // 1. Disable the form value model binding here to take control of handling 
        //    potentially large files.

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<JsonResult> UploadFile([FromForm] FileInputModel fileInputModel)
        {
            string fileName;
            var fileAttachment = fileInputModel.FileAttachment;
            if (fileAttachment == null) throw new ArgumentNullException(nameof(fileAttachment));

            IEnumerable<Payment> payments = null;
            var parsedContentDisposition = ContentDispositionHeaderValue.Parse(fileAttachment.ContentDisposition);
            using (var reader = new StreamReader(fileAttachment.OpenReadStream()))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Configuration.HeaderValidated = null;
                fileName = parsedContentDisposition.FileName.ToString();
                var records = csv.GetRecords<CsvRecord>();
                payments = _mapper.Map<IEnumerable<Payment>>(records);
            }

            var apigeeSettings = new ApigeeSettings
            {
                ApigeeBaseAddress = fileInputModel.ApigeeBaseAddress,
                ApigeeClientId = fileInputModel.ApigeeClientId,
                ApigeeClientSecret = fileInputModel.ApigeeClientSecret,
            };

            var apigeeClient = ApigeeClient.Build(apigeeSettings);
            
            var tokenResult = await apigeeClient.GetAccessTokenAsync();
            if (!tokenResult.IsSuccess)
            {
                return new JsonResult(new
                {
                    IsSuccess = false,
                    tokenResult.Error
                });
            }

            //return new JsonResult(new
            //{
            //    IsSuccess = true,
            //    Result = new
            //    {
            //        fileName,
            //        paymentBatchId = 10
            //    }
                
            //});

            var paymentBatchesClient = PaymentBatchesClient.Build(apigeeSettings);
            var paymentBatchResult = await paymentBatchesClient.CreatePaymentBatchAsync(tokenResult.Result,
                new CreatePaymentBatchRequest());

            if (!paymentBatchResult.IsSuccess)
            {
                return new JsonResult(new
                {
                    IsSuccess = false,
                    paymentBatchResult.Error
                });
            }

            var paymentsResult = await paymentBatchesClient.CreatePaymentsAsync(tokenResult.Result, paymentBatchResult.Result.PaymentBatchId,
                new CreatePaymentsRequest
                {
                    Payments = payments
                });

            if (!paymentsResult.IsSuccess)
            {
                return new JsonResult(new
                {
                    IsSuccess = false,
                    paymentsResult.Error
                });
            }

            return new JsonResult(new
            {
                IsSuccess = true,
                Result = new
                {
                    fileName,
                    paymentBatchResult.Result.Created,
                    paymentBatchResult.Result.PaymentBatchId,
                }
            });
        }
    }
}