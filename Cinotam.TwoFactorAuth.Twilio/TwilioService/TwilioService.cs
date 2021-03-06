﻿using Cinotam.TwoFactorAuth.Contracts;
using Cinotam.TwoFactorAuth.Twilio.Credentials;
using Cinotam.TwoFactorAuth.Twilio.Credentials.Input;
using Microsoft.AspNet.Identity;
using RestApiHelpers.Enums;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Twilio;

namespace Cinotam.TwoFactorAuth.Twilio.TwilioService
{
    public class TwilioService : ITwilioService
    {
        private readonly TwilioRestClient _client;
        /// <summary>
        /// This also should be obtained in other way
        /// </summary>
        private const string From = "+12016901854";
        private const string CorrectStatus = "queued";
        public TwilioService(ITwilioSenderCredentials twilioSenderCredentials)
        {
            _client = twilioSenderCredentials.GetClient(new TwilioCredentials()
            {
                ApiKeyName = "TApiKey",
                SecretKeyName = "TApiSecret",
                Strategy = Strategy.EnvVar
            });
        }

        public Task<SendMessageResult> SendSmsMessage(IdentityMessage message)
        {
            var destinationWithPlus = "+" + message.Destination;
            var body = message.Subject + message.Body;
            var result = _client.SendMessage(From, destinationWithPlus, body);
            Trace.TraceInformation(result.Status);

            if (result.Status == CorrectStatus)
            {
                return Task.FromResult(new SendMessageResult()
                {
                    SendStatus = SendStatus.Queued
                });
            }

            if (result.RestException != null)
            {
                return Task.FromResult(new SendMessageResult()
                {
                    SendStatus = SendStatus.Fail,
                    Properties = { ["Error"] = result.RestException.Message, ["ErrorCode"] = result.RestException.Code }
                });
            }

            return Task.FromResult(new SendMessageResult()
            {
                SendStatus = SendStatus.Fail
            });
        }

        public Task<SendMessageResult> SendEmailMessage(IdentityMessage message)
        {
            throw new NotImplementedException();
        }

        public string ServiceName => "Twilio";
        public Task SendAsync(IdentityMessage message)
        {
            throw new NotImplementedException();
        }
    }
}
