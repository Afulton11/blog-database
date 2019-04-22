using Domain.Business;
using EnsureThat;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Services.Email
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailSettings emailSettings;
        private readonly IHostingEnvironment environment;
        public EmailSender(
            IOptions<EmailSettings> emailSettings,
            IHostingEnvironment environment)
        {
            EnsureArg.IsNotNull(emailSettings, nameof(emailSettings));
            EnsureArg.IsNotNull(environment, nameof(environment));

            this.emailSettings = emailSettings.Value;
            this.environment = environment;
        }
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            try
            {
                var mimeMessage = new MimeMessage();

                mimeMessage.From.Add(new MailboxAddress(emailSettings.SenderName, emailSettings.Sender));

                mimeMessage.To.Add(new MailboxAddress(email));

                mimeMessage.Subject = subject;

                mimeMessage.Body = new TextPart("html")
                {
                    Text = htmlMessage
                };

                using (var client = new SmtpClient())
                {
                    // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                    if (environment.IsDevelopment())
                    {
                        // The third parameter is useSSL (true if the client should make an SSL-wrapped
                        // connection to the server; otherwise, false).
                        await client.ConnectAsync(emailSettings.MailServer, emailSettings.MailPort, true);
                    }
                    else
                    {
                        await client.ConnectAsync(emailSettings.MailServer);
                    }

                    // Note: only needed if the SMTP server requires authentication
                    await client.AuthenticateAsync(emailSettings.Sender, emailSettings.Password);

                    await client.SendAsync(mimeMessage);

                    await client.DisconnectAsync(true);
                }

            }
            catch (Exception ex)
            {
                // TODO: handle exception
                throw new InvalidOperationException(ex.Message);
            }
        }
    }
}
