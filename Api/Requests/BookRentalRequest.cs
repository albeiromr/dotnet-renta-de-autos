namespace Api.Requests;

// este record representa el body de la petición http
// es básicamente lo mismo que un dto
public sealed record BookRentalRequest(
    Guid VehicleId,
    Guid UserId,
    DateOnly StartDate,
    DateOnly EndDate
);

