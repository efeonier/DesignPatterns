using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using WebApp.ObserverPattern.Events;

namespace WebApp.ObserverPattern.EventHandlers;

public class SendEmailEventHandler : INotificationHandler<UserCreatedEvent>
{
    private readonly ILogger<SendEmailEventHandler> _logger;

    public SendEmailEventHandler(ILogger<SendEmailEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
    {
        var mailMessage = new MailMessage();
        var smtpClient = new SmtpClient("mail.efeonier.com.tr");
        mailMessage.From = new MailAddress("info@efeonier.com.tr");
        mailMessage.To.Add(new MailAddress(notification.AppUser.Email));
        mailMessage.Subject = "Sitemize Hoşgeldiniz";
        mailMessage.Body = "<p>Sitemizin genel kuralları</p>";
        mailMessage.IsBodyHtml = true;
        smtpClient.Port = 587;
        smtpClient.Credentials = new NetworkCredential("info@efeonier.com.tr", "12345");
        ////smtpClient.Send(mailMessage);
        _logger.LogInformation("User Created. Mail Send");

        return Task.CompletedTask;
    }
}