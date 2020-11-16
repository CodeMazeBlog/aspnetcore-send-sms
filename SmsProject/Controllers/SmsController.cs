using System;
using Microsoft.AspNetCore.Mvc;
using SmsProject.Models;
using Twilio.Clients;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using Twilio.AspNet.Common;
using Twilio.AspNet.Core;
using Twilio.TwiML;

namespace SmsProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SmsController : TwilioController
    {
        private readonly ITwilioRestClient _client;
        public SmsController(ITwilioRestClient client)
        {
            _client = client;
        }

        [HttpGet]
        public IActionResult SendSms(SmsMessage model)
        {
            var message = MessageResource.Create(
                to: new PhoneNumber(model.To),
                from: new PhoneNumber(model.From),
                body: model.Message,
                client: _client); // pass in the custom client

            return Ok();
        }

        [HttpPost]
        public TwiMLResult ReceiveSms([FromForm] SmsRequest incomingMessage)
        {
            var messagingResponse = new MessagingResponse();
            messagingResponse.Message("Thank you. Your message was received.");

            return TwiML(messagingResponse);
        }
    }
}
