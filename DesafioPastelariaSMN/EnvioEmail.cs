using System.Net.Mail;
using System.Net;

namespace emailTest
{
    class program
    {
        static void Main(string[] args)
        {
            string fromMail = "fulviost@gmail.com";
            string fromPassword = "cfko uvvx edbn obzh";
            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.Subject = "Teste de aplicação";
            message.To.Add(new MailAddress("oportunidades@smn.com.br"));
            message.Body = @$"O funcionário {}";


            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromMail, fromPassword),
                EnableSsl = true,
            };
            smtpClient.Send(message);

        }
    }
}



