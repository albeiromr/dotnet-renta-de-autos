using Application.Commons.Interfaces;

namespace Application.Rentals.BookRental;

// Command para crear una nueva reserva de auto
public record BookRentalCommand(
    Guid vehicleId,
    Guid userId,
    DateOnly startDate,
    DateOnly endDate
) : ICommand<Guid>;
