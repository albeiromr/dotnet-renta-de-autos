using Domain.Users.ObjectValues;

namespace Application.Commons.Interfaces;

public interface IEmailService
{
    Task SendAsync(Email recipient, string subject, string body);
}
