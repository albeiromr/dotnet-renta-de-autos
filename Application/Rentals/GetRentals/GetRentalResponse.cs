namespace Application.Rentals.GetRentals;

public sealed class GetRentalResponse
{
    public Guid RentalId { get; init; }
    public Guid UserId { get; init; }
    public Guid VehicleId { get; init; }
    public int Status { get; init; }
    public decimal RentalPrice { get; init; }
    public string? RentalCurrency { get; init; }
    public decimal MaintainancePrice { get; init; }
    public string? MaintainanceCurrency { get; init; }
    public decimal AccessoriesPrice { get; init; }
    public string? AccessoriesPriceCurrency { get; init; }
    public decimal TotalPrice { get; init; }
    public decimal TotalPriceCurrency { get; init; }
    public DateOnly RentalStartDate { get; init; }
    public DateOnly RentalEndDate { get; init; }
    public DateOnly RentalCreationDate { get; init; }
}
