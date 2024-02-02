using Application.Commons.Interfaces;

namespace Application.Rentals.GetRentals;

public sealed record GetRentalQuery(
    Guid rentalId
) : IQuery<GetRentalQueryResponse>;
