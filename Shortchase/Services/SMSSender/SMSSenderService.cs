using Amazon;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Shortchase.Entities;
using Shortchase.Helpers;

namespace Shortchase.Services
{
    public partial class SMSSenderService : ISMSSenderService
    {
        private readonly IErrorLogService errorLogService;

        public SMSSenderService
        (
            IErrorLogService errorLogService
        )
        {
            this.errorLogService = errorLogService;
        }

        

        public async Task<bool> SendSMS(string Recipient, string Message)
        {
            try
            {
                var accessKey = "AKIAJXUYY4EWOXYHWZQA";
                var secretKey = "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";
                var client = new AmazonSimpleNotificationServiceClient(accessKey, secretKey, RegionEndpoint.USEast1);
                var messageAttributes = new Dictionary<string, MessageAttributeValue>();
                var smsType = new MessageAttributeValue
                {
                    DataType = "String",
                    StringValue = "Transactional"
                };

                messageAttributes.Add("AWS.SNS.SMS.SMSType", smsType);

                PublishRequest request = new PublishRequest
                {
                    Message = Message,
                    PhoneNumber = Recipient,
                    MessageAttributes = messageAttributes
                };

                var result = await client.PublishAsync(request).ConfigureAwait(true);

                if (result.HttpStatusCode == HttpStatusCode.OK)
                {
                    return true;
                }
                else throw new Exception("SMS could not be sent");
            }
            catch (Exception e)
            {
                if (!await errorLogService.InsertException(e).ConfigureAwait(false)) throw;
                return false;
            }
        }



    }
}