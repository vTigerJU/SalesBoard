using Microsoft.AspNetCore.Identity.UI.Services;

namespace SalesBoard.Models
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            Console.WriteLine($"Sending email to {email} with subject {subject}");
            return Task.CompletedTask;
        }
    }
}
