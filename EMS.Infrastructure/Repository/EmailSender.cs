using EMS.Application.Interfaces;
using EMS.Domain.Enums;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using Azure.Identity;
using EMS.Domain.Models;
using EMS.Application.Dtos.EmailAccounts;

namespace EMS.Infrastructure.Repository;

internal class EmailSender : IEmailSender
{
    private readonly IConfiguration _configuration;

    public EmailSender(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendTestEmailAsync(ValidationAndTestEmailAccountDto config, string toEmail)
    {
        switch (config.EmailType)
        {
            case EmailType.Gmail:
                await SendGmailEmail(config, toEmail);
                break;

            case EmailType.Office365:
                await SendOffice365Email(config, toEmail);
                break;

            default:
                throw new NotSupportedException("Unsupported email type.");
        }
    }

    private async Task SendGmailEmail(ValidationAndTestEmailAccountDto config, string toEmail)
    {
        var email = config.EmailAddress;
        var password = config.Password;

        var message = new MimeKit.MimeMessage();

        message.From.Add(
            new MimeKit.MailboxAddress("EMS System", email));

        message.To.Add(
            new MimeKit.MailboxAddress("", toEmail));

        message.Subject = "Test Email - EMS Configuration";

        message.Body = new MimeKit.TextPart("plain")
        {
            Text = "This is a test email confirming your configuration works correctly."
        };

        using var client = new SmtpClient();

        await client.ConnectAsync(
            "smtp.gmail.com",
            587,
            SecureSocketOptions.StartTls);

        await client.AuthenticateAsync(email, password);

        await client.SendAsync(message);

        await client.DisconnectAsync(true);
    }

    private async Task SendOffice365Email(ValidationAndTestEmailAccountDto config, string toEmail)
    {
        var tenantId = config.TenantId;
        var clientId = config.ClientId;
        var clientSecret = config.ClientSecret;
        var senderEmail = config.EmailAddress;

        var credential = new ClientSecretCredential(
            tenantId,
            clientId,
            clientSecret);

        var graphClient = new GraphServiceClient(credential);

        var message = new Message
        {
            Subject = "Test Email - EMS Configuration",

            Body = new ItemBody
            {
                ContentType = BodyType.Text,
                Content = "This is a test email confirming your configuration works correctly."
            },

            ToRecipients = new List<Recipient>
            {
                new Recipient
                {
                    EmailAddress = new EmailAddress
                    {
                        Address = toEmail
                    }
                }
            }
        };

        await graphClient
            .Users[senderEmail]
            .SendMail
            .PostAsync(new Microsoft.Graph.Users.Item.SendMail.SendMailPostRequestBody
            {
                Message = message,
                SaveToSentItems = true
            });
    }
}