using System.Net;
using System.Net.Mail;

namespace bike_store_2.Conforming_Services.Email_Service
{




    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, string body);
    }




    public class EmailService : IEmailService
    {

        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var email = _configuration["Gmail:SmtpEmail"];
            var password = _configuration["Gmail:SmtpPassword"];

            // تحديد مضيف البريد SMTP الخاص بجوجل
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                // هو المستخدم عادة لإرسال البريد عبر SMTP باستخدام TLS (تشفير).
                Port = 587,
                Credentials = new NetworkCredential(email, password),
                EnableSsl = true, // تفعيل SSL/TLS   
            };
            var mailMessage = new MailMessage
            {
                From = new MailAddress(email),
                Subject = subject,
                Body = body,
                IsBodyHtml = true, // تحديد أن المحتوى هو HTML
            };            
            mailMessage.To.Add(to); // إضافة المستلم
            try
            {
                await smtpClient.SendMailAsync(mailMessage); // إرسال البريد
            }
            catch (Exception ex)
            {
                // Handle exception (e.g., log it)
                throw new Exception("Failed to send email", ex);
            }
        }
    }
}
