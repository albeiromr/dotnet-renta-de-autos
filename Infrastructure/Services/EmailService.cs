using Application.Commons.Interfaces;
using Domain.Users.ObjectValues;

namespace Infrastructure.Services;

internal sealed class EmailService : IEmailService
{
    public Task SendAsync(Email recipient, string subject, string body)
    {
        // en este curso no se incluye la lógica de envío de los emails
        // por lo que con la siguiente línea simulamos que el envío se completó correctamente
        return Task.CompletedTask;
    }
}
