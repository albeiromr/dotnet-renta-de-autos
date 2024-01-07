namespace Domain.Vehicles;

public sealed class Vehicle
{
    public Guid Id { get; private set; }

    // representa el model del vehiculo
    public string? Model { get; private set; }

    // vin es como el número de serie del carro
    public string? Vin { get; private set; }

    // representa el País donde se recogerá el vehiculo
    public string?  Country { get; set; }

    // representa el departamento donde se recogerá el vehiculo
    public string? Department { get; private set; }

    // Representa la provincia donde se recogerá el vehiculo
    public string? Province { get; private set; }

    // representa la ciudad donde se recogerá el vehiculo
    public string? City { get; private set; }

    // dirección donde se recogerá el vehiculo
    public string? PickUpAddress { get; private set; }

    //representa el precio de la renta del vehiculo
    public decimal RentPrice { get; private set; }

    // representa el tipo de moneda con el que se pagará el vehiculo
    public string? RentPriceCurrencyType { get; private set; }

    //representa el precio de mantenimiento del vehiculo
    public decimal MaintenancePrice { get; private set; }

    // representa el tipo de moneda con el que se pagará el mantenimiento del vehiculo
    public string? MaintenancePriceCurrencyType { get; private set; }

    // representa la fecha del ultimo alquiler
    public DateTime? LastRentDate { get; private set; }

    // Representa los accesorios premium que pueden ser incluidos en el vehiculo
    public List<Accessory> Accessories { get; private set; } = new List<Accessory>();
}

