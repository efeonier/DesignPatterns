using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using WebbApp.ChainResponsibility.Services.Abstract;

namespace WebbApp.ChainResponsibility.Services.Concrete;

public class SendEmailProcessHandler : ProcessHandler
{
    private readonly string _fileName;
    private readonly string _toMail;

    public SendEmailProcessHandler(string fileName, string toMail)
    {
        _fileName = fileName;
        _toMail = toMail;
    }

    public override object Handle(object o)
    {
        var mailMessage = new MailMessage();
        mailMessage.From = new MailAddress("info@efeonier.com.tr");
        mailMessage.To.Add(new MailAddress(_toMail));
        mailMessage.Subject = "Zip Dosyası";
        mailMessage.Body = "Zip Dosyası Ektedir";
        mailMessage.IsBodyHtml = true;
        if (o is MemoryStream zipMemoryStream)
        {
            zipMemoryStream.Position = 0;
            var attachment = new Attachment(zipMemoryStream, _fileName, MediaTypeNames.Application.Zip);
            mailMessage.Attachments.Add(attachment);
        }

        var smtpClient = new SmtpClient("mail.efeonier.com.tr");
        smtpClient.Port = 587;
        smtpClient.Credentials = new NetworkCredential("info@efeonier.com.tr", "12345");
        ////smtpClient.Send(mailMessage);

        return base.Handle(null);
    }
}
