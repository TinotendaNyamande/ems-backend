using EMS.Application.Interfaces;
using EMS.Domain.Enums;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using Azure.Identity;

namespace EMS.Infrastructure.Repository;

internal class EmailSender() : IEmailSender
{

    public async Task SendTestEmailAsync(EmailType emailType,string emailAddress,string? password,string? clientId,string? clientSecret,string? tenantId,string toEmail)
    {
        switch (emailType)
        {
            case EmailType.Gmail:
                if(string.IsNullOrEmpty(emailAddress) || string.IsNullOrEmpty(password))
                {
                    throw new ArgumentException("Email address and password must be provided for Gmail.");
                }
                await SendGmailEmail(emailAddress, password, toEmail);
                break;

            case EmailType.Office365:
                if(string.IsNullOrEmpty(tenantId) || string.IsNullOrEmpty(clientId) || string.IsNullOrEmpty(clientSecret) || string.IsNullOrEmpty(emailAddress))
                {
                    throw new ArgumentException("All credentials must be provided for Office365.");
                }
                await SendOffice365Email(tenantId, clientId, clientSecret, emailAddress, toEmail);
                break;

            default:
                throw new NotSupportedException("Unsupported email type.");
        }
    }

    private static async Task SendGmailEmail(string emailAddress, string password, string toEmail)
    {
        if(string.IsNullOrEmpty(emailAddress) || string.IsNullOrEmpty(password))
        {
            throw new ArgumentException("Email address and password must be provided for Gmail.");
        }

        var message = new MimeKit.MimeMessage();

        message.From.Add(
            new MimeKit.MailboxAddress("EMS System", emailAddress));

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

        await client.AuthenticateAsync(emailAddress, password);

        await client.SendAsync(message);

        await client.DisconnectAsync(true);
    }

    private static async Task SendOffice365Email(string tenantId, string clientId, string clientSecret, string senderEmail, string toEmail)
    {

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