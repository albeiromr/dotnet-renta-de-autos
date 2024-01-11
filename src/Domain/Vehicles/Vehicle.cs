using Domain.Abstractions;
using Domain.Commons;
using System;
using System.Collections.Generic;

namespace Domain.Vehicles;

public sealed class Vehicle: Entity
{
    public Vehicle(
        Guid id,
        Model model,
        Vin vin,
        Location location,
        Price rentPrice,
        Price maintanancePrice,
        DateTime lastRentDate,
        List<PremiumService> premiumServices

    ) : base(id) 
    {
        Model = model;
        Vin = vin;
        Location = location;
        RentPrice = rentPrice;
        MaintenancePrice = maintanancePrice;
        LastRentDate = lastRentDate;
        PremiumServices = premiumServices;
    }

    // representa el model del vehiculo
    public Model? Model { get; private set; }

    // vin es como el número de serie del carro
    public Vin? Vin { get; private set; }

    //representa toda la información de la ubicación del vehiculo
    public Location? Location { get; private set; }

    //representa el precio de la renta del vehiculo
    public Price? RentPrice { get; private set; }

    //representa el precio de mantenimiento del vehiculo
    public Price? MaintenancePrice { get; private set; }

    // representa la fecha del ultimo alquiler
    public DateTime? LastRentDate { get; private set; }

    // Representa los servicios premium que pueden ser incluidos en el vehiculo
    public List<PremiumService> PremiumServices { get; private set; } = new List<PremiumService>();
}

