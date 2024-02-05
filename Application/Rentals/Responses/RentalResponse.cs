namespace Application.Rentals.Responses;

public sealed class RentalResponse
{
    public Guid Id { get; init; }
    public Guid UserId { get; init; }
    public Guid VehicleId { get; init; }
    public int Status { get; init; }
    public decimal Price { get; init; }
    public DateOnly DurationInit { get; init; }
    public DateOnly DurationEnd { get; init; }
    public DateOnly CreationDate { get; init; }
}
