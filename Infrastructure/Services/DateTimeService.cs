using Application.Commons.Interfaces;

namespace Infrastructure.Services;

internal sealed class DateTimeService : IDateTimeService
{
    public DateTime CurrentTime => DateTime.UtcNow;
}
