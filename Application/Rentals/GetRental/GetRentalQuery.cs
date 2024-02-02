using Application.Commons.Interfaces;
using Application.Rentals.Responses;

namespace Application.Rentals.GetRental;

public sealed record GetRentalQuery(
    Guid rentalId
) : IQuery<RentalResponse>;
