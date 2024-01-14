using Domain.Abstractions;
using Domain.Commons;
using System;
using System.Collections.Generic;
using Domain.Rentals.events;

namespace Domain.Vehicles;

public sealed class Vehicle: Entity
{
    // representa el model del vehiculo
    public Model? Model { get; private set; }

    // vin es como el número de serie del carro
    public Vin? Vin { get; private set; }

    //representa toda la información de la ubicación del vehiculo
    public Location? Location { get; private set; }

    //representa el precio de la renta del vehiculo por cada día
    public Price? RentPrice { get; private set; }

    //representa el precio de mantenimiento del vehiculo
    public Price? MaintenancePrice { get; private set; }

    // representa la fecha del ultimo alquiler
    public DateTime? LastRentDate { get; private set; }

    // Representa los servicios premium que pueden ser incluidos en el vehiculo
    public List<PremiumAccessory> PremiumAccessories { get; private set; } = new List<PremiumAccessory>();

    private Vehicle(
        Guid id,
        Model model,
        Vin vin,
        Location location,
        Price rentPrice,
        Price maintanancePrice,
        DateTime lastRentDate,
        List<PremiumAccessory> premiumAccessories

    ) : base(id) 
    {
        Model = model;
        Vin = vin;
        Location = location;
        RentPrice = rentPrice;
        MaintenancePrice = maintanancePrice;
        LastRentDate = lastRentDate;
        PremiumAccessories = premiumAccessories;
    }

    
    // para crear un nuevo objeto de tipo Vehicle se usa el método 
    // create junto con el constructor privado para que ningún ente 
    // o programa externo pueda crear objetos de tipo User
    public static Vehicle Create(
        Guid id,
        Model model,
        Vin vin,
        Location location,
        Price rentPrice,
        Price maintanancePrice,
        DateTime lastRentDate,
        List<PremiumAccessory> premiumServices
    )
    {
        Vehicle vehicle = new Vehicle(
            Guid.NewGuid(),
            model,
            vin,
            location,
            rentPrice,
            maintanancePrice,
            lastRentDate,
            premiumServices
        );

        vehicle.DispatchDomainEvent(new VehicleCreatedDomainEvent(vehicle.Id));
        return vehicle;
    }
}

