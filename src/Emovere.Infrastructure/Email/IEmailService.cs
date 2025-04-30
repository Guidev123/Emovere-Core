using Emovere.Infrastructure.Email.Models;

namespace Emovere.Infrastructure.Email;

public interface IEmailService
{
    Task SendAsync(EmailMessage email);
}