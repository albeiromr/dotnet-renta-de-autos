using Domain.Abstractions;

namespace Domain.Vehicles;

public sealed class Vehicle: Entity
{
    public Vehicle(Guid id) : base(id) { }

    // representa el model del vehiculo
    public Model? Model { get; private set; }

    // vin es como el número de serie del carro
    public Vin? Vin { get; private set; }

    //representa toda la información de dirección del vehiculo 
    public Address? Address { get; private set; }

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

