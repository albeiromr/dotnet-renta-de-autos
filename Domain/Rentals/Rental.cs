using Domain.Commons.Clases;
using Domain.Rentals.Enums;
using Domain.Rentals.Events;
using Domain.Rentals.ObjectValues;
using Domain.Rentals.Services;
using Domain.Vehicles;
using System;

namespace Domain.Rentals;

// Representa la entidad rental, es decir los datos del arrendamiento de 
// un vehiculo
public sealed class Rental : Entity
{
    // representa el id del vehiculo que se está arrendando
    public Guid VehicleId { get; private set; }

    // representa el usuario que está arrendando el vehiculo
    public Guid UserId { get; private set; }

    // representa todos los diferentes precios que tiene un objeto de tipo rental
    public decimal Price { get; private set; }

    // representa el estado de el arrendamiento
    public RentalStatus Status { get; private set; }

    // representa el lapso de tiempo durante la cual se realizará el arrendamiento
    public DateRange? Duration { get; private set; }

    // representa la fecha de creación de un nuevo registro de tipo Rental en el sistema
    public DateTime? CreationDate { get; private set; }


    private Rental(
        Guid id,
        Guid vehicleId,
        Guid userId,
        DateRange duration,
        decimal price,
        RentalStatus status,
        DateTime creationDate
    ) : base(id)
    {
        VehicleId = vehicleId;
        UserId = userId;
        Duration = duration;
        Price = price;
        Status = status;
        CreationDate = creationDate;
    }

    // este constructor es solo para poder ejecutar las migraciones con ef
    private Rental()
    {

    }

    // para crear un nuevo objeto de tipo Rental se usa el método 
    // create junto con el constructor privado para que ningún ente 
    // o programa externo pueda crear objetos de tipo Rental
    public static Rental Book(
        Vehicle vehicle,
        Guid userId,
        DateRange duration,
        DateTime creationDate,
        PriceService priceService
    )
    {
        // se pueden crear servicios para almacenar lógica de negocio
        decimal price = priceService.CalculatePrices(vehicle);

        Rental rental = new Rental(
            Guid.NewGuid(),
            vehicle.Id,
            userId,
            duration,
            price,
            RentalStatus.Reserved,
            creationDate
        );

        rental.DispatchDomainEvent(new RentalBookedDomainEvent(rental.Id));

        return rental;
    }

}

