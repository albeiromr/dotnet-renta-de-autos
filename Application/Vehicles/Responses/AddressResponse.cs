namespace Application.Vehicles.Responses;

public sealed class AddressResponse
{
    public string? Country { get; init; }
    public string? Department { get; init; }
    public string? City { get; init; }
    public string? PickUpAddress { get; init; }
}
