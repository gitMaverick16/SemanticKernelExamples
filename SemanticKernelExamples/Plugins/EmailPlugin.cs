using Microsoft.SemanticKernel;
using System.ComponentModel;
using System.Net;
using System.Net.Mail;

namespace SemanticKernelExamples.Plugins
{
    public class EmailPlugin
    {
        [KernelFunction("verify_parameter_of_sending_email")]
        [Description("Before you send an email you have to tell the user a summarize of the parameters asking for a validation")]
        [return: Description("A text asking for a confirmation with the parameters")]
        public string ValidateParameterOfEmail(string emailTo, string subject, string message)
        {
            return $"I am gogint o send an email to {emailTo} with the subject {subject} and the content {message}. Are you sure?";
        }

        [KernelFunction("send_email")]
        [Description("Send an email with the parameters email to, subject, and message, ask for that parameter and before sending the email send a summarize of what you aare going to send")]
        [return: Description("The confirmation of the email")]
        public async Task<bool> SendEmailAsync(string emailTo, string subject, string message)
        {
            var mail = "test@test.com";
            var password = "test";

            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(mail, password),
                UseDefaultCredentials = false
            };

            await client.SendMailAsync(new MailMessage(from: mail, to: emailTo, subject, message));
            return true;
        }

    }

}
