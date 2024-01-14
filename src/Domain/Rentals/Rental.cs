using Domain.Abstractions.Clases;
using Domain.Rentals.Enums;
using Domain.Rentals.Errors;
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
    public static Rental Create(
        Vehicle vehicle,
        Guid userId,
        DateRange duration,
        DateTime creationDate,
        PriceService priceService
    )
    {
        PriceDetails priceDetails = priceService.CalculatePrices(
            vehicle,
            duration
        );

        Rental rental = new Rental(
            Guid.NewGuid(),
            vehicle.Id,
            userId,
            duration,
            priceDetails,
            RentalStatus.Reserved,
            creationDate
        );

        vehicle.LastRentDate = creationDate;

        rental.DispatchDomainEvent(new RentalReservedDomainEvent(rental.Id));

        return rental;
    }

    // confirma el arrendamiento de un vehiculo cuando el
    // usuario ya ha dicho que está seguro de tomar dicho
    // arrendamiento
    public Result ConfirmRental(DateTime utcNow)
    {
        if(Status != RentalStatus.Reserved)
        {
            return Result.CreateWithFailureStatus(RentalErrors.NotReserved);
        }

        Status = RentalStatus.confirmed;
        ConfirmationDate = utcNow;

        DispatchDomainEvent(new RentalConfirmedDomainEvent(Id));

        return Result.CreateWithSuccessStatus();
    }

    public Result RefuseRental(DateTime utcNow)
    {
        if (Status != RentalStatus.Reserved)
        {
            return Result.CreateWithFailureStatus(RentalErrors.NotReserved);
        }

        Status = RentalStatus.refused;
        RefusalDate = utcNow;

        DispatchDomainEvent(new RentalRefusedDomainEvent(Id));

        return Result.CreateWithSuccessStatus();
    }

    public Result CancelRental(DateTime utcNow)
    {
        if (Status != RentalStatus.confirmed)
        {
            return Result.CreateWithFailureStatus(RentalErrors.NotConfirmed);
        }

        // validando las fechas del alquiler, por que si el arrendamiento
        // ya está en proceso, es decir ya estamos en las fechas que el usuario 
        // confirmó, no se podrá cancellar el arrendamiento
        var currentDate = DateOnly.FromDateTime(utcNow);
        if (currentDate > Duration!.Init)
            return Result.CreateWithFailureStatus(RentalErrors.AreadyStarted);

        Status = RentalStatus.cancelled;
        CancelDate = utcNow;

        DispatchDomainEvent(new RentalCancelledDomainEvent(Id));

        return Result.CreateWithSuccessStatus();
    }

    public Result CompleteRental(DateTime utcNow)
    {
        if (Status != RentalStatus.confirmed)
        {
            return Result.CreateWithFailureStatus(RentalErrors.NotConfirmed);
        }

        // validando las fechas del alquiler, por que si el arrendamiento
        // ya está en proceso, es decir ya estamos en las fechas que el usuario 
        // confirmó, no se podrá cancellar el arrendamiento
        var currentDate = DateOnly.FromDateTime(utcNow);
        if (currentDate > Duration!.Init)
            return Result.CreateWithFailureStatus(RentalErrors.AreadyStarted);

        Status = RentalStatus.completed;
        CompleatedDate = utcNow;

        DispatchDomainEvent(new RentalCompletedDomainEvent(Id));

        return Result.CreateWithSuccessStatus();
    }
}

