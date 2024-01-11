using Domain.Abstractions;
using Domain.Commons;
using System;

namespace Domain.Rentals;

// Representa la entidad rental, es decir los datos del arrendamiento de 
// un vehiculo
public sealed class Rental : Entity
{
    private Rental(
        Guid id,
        Guid vehicleId,
        Guid userId,
        DateRange duration,
        Price rentalPeriodPrice,
        Price maintainancePrice,
        Price premiumServicesPrice,
        Price totalPrice,
        RentalStatus status,
        DateTime creationDate
    ) : base(id)
    {
        VehicleId = vehicleId;
        UserId = userId;
        Duration = duration;
        RentalPeriodPice = rentalPeriodPrice;
        MaintainancePrice = maintainancePrice;
        PremiumServicesPrice = premiumServicesPrice; 
        TotalPrice = totalPrice;
        Status = status;
        CreationDate = creationDate;
    }

    // representa el id del vehiculo que se está arrendando
    public Guid VehicleId { get; private set; }

    // representa el usuario que está arrendando el vehiculo
    public Guid UserId { get; private set; }

    // precio que se le cobrará al usuario por el periodo de arrendamiento sin el mantenimiento
    public Price? RentalPeriodPice { get; private set; }

    // precio que se le cobrará al usuario por el mantenimiento del vehiculo 
    public Price? MaintainancePrice { get; private set; }

    // represen el precio de los servicios premium que el usuario contrate
    public Price? PremiumServicesPrice { get; private set; }

    // representa el precio total del arrendamiento
    public Price? TotalPrice { get; set; }

    // representa el estado de el arrendamiento
    public RentalStatus Status { get; private set; }

    // representa el lapso de tiempo durante la cual se realizará el arrendamiento
    public DateRange? Duration { get; private set; }

    // representa la fecha de creación de un nuevo registro de tipo Rental en el sistema
    public DateTime? CreationDate { get; private set;}

    // representa la fecha en la cual el usuario se arrepiente de  arrendar el vhiculo
    public DateTime? RefusalDate { get; private set; }

    // representa la fecha en la se cancela en el sistema el arrendamiento debido a un arreentimiento del usuario
    public DateTime? CancelDate { get; private set; }

    // representa la fecha en la cual el usuario confirma que si arrendará el vhiculo
    public DateTime? ConfirmationDate { get; private set; }

    // representa la fecha en la cual todo el proceso de arrendamiento se ha completado
    public DateTime? CompleatedDate { get; private set; }


    // para crear un nuevo objeto de tipo Rental se usa el método 
    // create junto con el constructor privado para que ningún ente 
    // o programa externo pueda crear objetos de tipo Rental
    public static Rental create(
        Guid vehicleId,
        Guid userId,
        DateRange duration,
        DateTime creationDate
    ) { }
}

