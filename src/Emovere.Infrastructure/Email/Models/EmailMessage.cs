using Microsoft.AspNetCore.Http;

namespace Emovere.Infrastructure.Email.Models;

public record EmailMessage(string To, string Subject, string Content, IFormFile? Attachments = null);