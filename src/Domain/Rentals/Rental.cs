using Domain.Abstractions;
using Domain.Commons;
using Domain.Rentals.events;
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
    public PriceDetails PriceDetails { get; private set; }

    // representa el estado de el arrendamiento
    public RentalStatus Status { get; private set; }

    // representa el lapso de tiempo durante la cual se realizará el arrendamiento
    public DateRange? Duration { get; private set; }

    // representa la fecha de creación de un nuevo registro de tipo Rental en el sistema
    public DateTime? CreationDate { get; private set; }

    // representa la fecha en la cual el usuario se arrepiente de  arrendar el vhiculo
    public DateTime? RefusalDate { get; private set; }

    // representa la fecha en la se cancela en el sistema el arrendamiento debido a un arreentimiento del usuario
    public DateTime? CancelDate { get; private set; }

    // representa la fecha en la cual el usuario confirma que si arrendará el vhiculo
    public DateTime? ConfirmationDate { get; private set; }

    // representa la fecha en la cual todo el proceso de arrendamiento se ha completado
    public DateTime? CompleatedDate { get; private set; }

    private Rental(
        Guid id,
        Guid vehicleId,
        Guid userId,
        DateRange duration,
        PriceDetails priceDetails,
        RentalStatus status,
        DateTime creationDate
    ) : base(id)
    {
        VehicleId = vehicleId;
        UserId = userId;
        Duration = duration;
        PriceDetails = priceDetails;
        Status = status;
        CreationDate = creationDate;
    }

    // para crear un nuevo objeto de tipo Rental se usa el método 
    // create junto con el constructor privado para que ningún ente 
    // o programa externo pueda crear objetos de tipo Rental
    public static Rental create(
        Guid vehicleId,
        Guid userId,
        DateRange duration,
        DateTime creationDate,
        PriceDetails priceDetails
    )
    {
        var rental = new Rental(
            Guid.NewGuid(),
            vehicleId,
            userId,
            duration,
            priceDetails,
            RentalStatus.Reserved,
            creationDate
        );

        rental.DispatchDomainEvent(new RentalCreatedDomainEvent(rental.Id));

        return rental;
    }
}

