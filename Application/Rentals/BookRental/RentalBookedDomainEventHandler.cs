using Application.Commons.Interfaces;
using Domain.Rentals.Events;
using Domain.Rentals.Interfaces;
using Domain.Users.Interfaces;
using MediatR;

namespace Application.Rentals.BookRental;

internal sealed class RentalBookedDomainEventHandler : INotificationHandler<RentalBookedDomainEvent>
{
    private readonly IRentalRepository? _rentalRepository;
    private readonly IUserRepository? _userRepository;
    private readonly IEmailService? _emailService;

    public RentalBookedDomainEventHandler(
        IRentalRepository? rentalRepository,
        IUserRepository? userRepository,
        IEmailService? emailService
    )
    {
        _rentalRepository = rentalRepository;
        _userRepository = userRepository;
        _emailService = emailService;
    }

    public async Task Handle(RentalBookedDomainEvent notification, CancellationToken cancellationToken)
    {
        var rental = await _rentalRepository!.GetByIdAsync(notification.id, cancellationToken);
        if (rental is null)
        {
            return;
        }

        var user = await _userRepository!.GetByIdAsync(rental.UserId, cancellationToken);
        if (user is null)
        {
            return;
        }

        await _emailService!.SendAsync(
            user.Email!,
            "Rental Booked",
            "Yo have booked a vehicle rental, don´t forget to confirme it, otherwise it will be lost"
        );
    }
}
