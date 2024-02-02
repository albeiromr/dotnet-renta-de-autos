using Application.Commons.Interfaces;
using Application.Vehicles.Responses;

namespace Application.Vehicles.SearchVehicles;

public sealed record SearchVehiclesQuery(
    DateOnly initDate,
    DateOnly endDate
) : IQuery<IReadOnlyList<VehicleResponse>>;

