using System;
using System.Collections.Generic;
using Domain.Rentals.events;
using Domain.Vehicles.ObjectValues;
using Domain.Commons.Clases;
using Domain.Commons.Enums;

namespace Domain.Vehicles;

public sealed class Vehicle : Entity
{
    // representa el model del vehiculo
    public Model? Model { get; private set; }

    // vin es como el número de serie del carro
    public Vin? Vin { get; private set; }

    //representa toda la información de la ubicación del vehiculo
    public Location? Location { get; private set; }

    //representa el precio de la renta del vehiculo por cada día
    public decimal Price { get; private set; }

    // representa la fecha del ultimo alquiler
    public DateTime? LastRentDate { get; internal set; }

    // Representa los servicios premium que pueden ser incluidos en el vehiculo
    public List<PremiumAccessory> PremiumAccessories { get; private set; } = new List<PremiumAccessory>();

    private Vehicle(
        Guid id,
        Model model,
        Vin vin,
        Location location,
        decimal price,
        DateTime lastRentDate,
        List<PremiumAccessory> premiumAccessories

    ) : base(id)
    {
        Model = model;
        Vin = vin;
        Location = location;
        Price = price;
        LastRentDate = lastRentDate;
        PremiumAccessories = premiumAccessories;
    }

    // este constructor es solo para poder ejecutar las migraciones con ef
    private Vehicle()
    {

    }


    // para crear un nuevo objeto de tipo Vehicle se usa el método 
    // create junto con el constructor privado para que ningún ente 
    // o programa externo pueda crear objetos de tipo User
    public static Vehicle Create(
        Guid id,
        Model model,
        Vin vin,
        Location location,
        decimal price,
        DateTime lastRentDate,
        List<PremiumAccessory> premiumServices
    )
    {
        Vehicle vehicle = new Vehicle(
            Guid.NewGuid(),
            model,
            vin,
            location,
            price,
            lastRentDate,
            premiumServices
        );

        vehicle.DispatchDomainEvent(new VehicleCreatedDomainEvent(vehicle.Id));
        return vehicle;
    }
}

