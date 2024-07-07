using System;
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebApp.ObserverPattern.Entities;

namespace WebApp.ObserverPattern.Observer;

public class UserObserverSendMail : IUserObserver
{
    private readonly IServiceProvider _serviceProvider;

    public UserObserverSendMail(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public void UserCreated(AppUser appUser)
    {
        var logger = _serviceProvider.GetRequiredService<ILogger<UserObserverSendMail>>();
        var mailMessage = new MailMessage();
        var smtpClient = new SmtpClient("mail.efeonier.com.tr");
        mailMessage.From = new MailAddress("info@efeonier.com.tr");
        mailMessage.To.Add(new MailAddress(appUser.Email));
        mailMessage.Subject = "Sitemize Hoşgeldiniz";
        mailMessage.Body = "<p>Sitemizin genel kuralları</p>";
        mailMessage.IsBodyHtml = true;
        smtpClient.Port = 587;
        smtpClient.Credentials = new NetworkCredential("info@efeonier.com.tr", "12345");
        //smtpClient.Send(mailMessage);
        logger.LogInformation("User Created. Mail Send");
    }
}