using FinancialsFunctions.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Services;
using System;
using System.IO;
using System.Threading.Tasks;

namespace FinancialsFunctions.Functions
{
    public class SendMail
    {
        private readonly ILogger _logger;
        private readonly IEmailService _emailService;

        public SendMail(ILoggerFactory loggerFactory, IEmailService emailService)
        {
            _logger = loggerFactory.CreateLogger<SendMail>();
            _emailService = emailService;
        }

        [Function("SendMail")]
        public async Task<IActionResult> RunAsync([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request to send an email.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<EmailDTO>(requestBody);
            string toEmail = data?.Destinatario;
            string body = data?.Body;

            if (string.IsNullOrEmpty(toEmail) || string.IsNullOrEmpty(body))
            {
                return new BadRequestObjectResult("Please provide both 'to' and 'body' in the request body.");
            }

            try
            {
                //_emailService.SendEmail(toEmail, body);
                return new OkObjectResult($"Email sent successfully to {toEmail}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error sending email: {ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
