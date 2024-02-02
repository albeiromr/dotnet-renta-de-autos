namespace Application.Rentals.GetRentals;

public sealed class GetRentalQueryResponse
{
    public Guid Id { get; init; }
    public Guid UserId { get; init; }
    public Guid VehicleId { get; init; }
    public int Status { get; init; }
    public decimal RentalPeriodPrice { get; init; }
    public string? RentalPeriodPriceCurrency { get; init; }
    public decimal MaintainancePrice { get; init; }
    public string? MaintainancePriceCurrency { get; init; }
    public decimal PremiumAccessoriesPrice { get; init; }
    public string? PremiumAccessoriesPriceCurrency { get; init; }
    public decimal TotalPrice { get; init; }
    public decimal TotalPriceCurrency { get; init; }
    public DateOnly DurationInit { get; init; }
    public DateOnly DurationEnd { get; init; }
    public DateOnly CreationDate { get; init; }
}
