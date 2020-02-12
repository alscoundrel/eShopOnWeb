using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using System.Threading.Tasks;
using SendGrid.Helpers.Mail;
using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using SendGrid;
using System.Net;
using Microsoft.Extensions.Logging;

namespace Microsoft.eShopWeb.Infrastructure.Services
{
    public class EmailSenderSendGrid : IEmailSender
    {
        private readonly ILogger<EmailSenderSendGrid> _logger;
        private readonly IConfiguration _configuration;

        public EmailSenderSendGrid(ILoggerFactory loggerFactory, IConfiguration configuration){
            _logger = loggerFactory.CreateLogger<EmailSenderSendGrid>();
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var apiKeyString = _configuration.GetValue<string>("SendGrid:apiKey");
            
            if(string.IsNullOrEmpty(apiKeyString)){
                throw new Exception("SendGrid apiKey is null or empty");
            }

            var from = _configuration.GetValue<string>("SendGrid:from");
            var fromName = _configuration.GetValue<string>("SendGrid:fromName");

            if(string.IsNullOrEmpty(from)){
                throw new Exception("Email From is null or empty");
            }

            var emailAddressFrom = new EmailAddress(from, fromName);
            var emailAddressTo   = new EmailAddress(email);
            string plainTextContent = "";
            SendGridMessage sendGridMessage = MailHelper.CreateSingleEmail(emailAddressFrom, emailAddressTo, subject, plainTextContent, message);

            var client = new SendGridClient(apiKeyString);
            var response = await client.SendEmailAsync(sendGridMessage);
            if(response.StatusCode == HttpStatusCode.Accepted){
                _logger.LogInformation($"Send e-mail to {email} is accepted.");
            }
            else{
                _logger.LogError($"Send e-mail to {email} is not accepted. {response.ToString()}");
                throw new Exception(response.ToString());
            }
            // Wire this up to actual email sending logic via SendGrid, local SMTP, etc.
        }
    }
}
