using Domain.Commons.Clases;
using Domain.Rentals;
using Domain.Rentals.Enums;
using Domain.Reviews.Errors;
using Domain.Reviews.Events;
using Domain.Reviews.ObjectValues;
using System;

namespace Domain.Reviews;

public sealed class Review : Entity
{
    // representa el de la entidad vehicle al cual hace referencia el review
    public Guid VehicleId { get; private set; }

    // representa el id de la entidad Rental al cual hace referencia el review
    public Guid RentalId { get; private set; }

    // representa el id de la entidad user al cual hace referencia el review
    public Guid UserId { get; private set; }

    // representa la calificación que el usuario ha dejado por el servicio prestado
    public Rating? Rating { get; private set; }

    // representa el comentario que el usuario dejó consu reiew
    public Comment? Comment { get; private set; }

    // representa la fecha en la que se crea el review
    public DateTime? CreationDate { get; private set; }

    private Review(
        Guid id,
        Guid vehicleId,
        Guid rentalId,
        Guid userId,
        Rating? rating,
        Comment? comment,
        DateTime? creationDate
    ) : base(id)
    {
        VehicleId = vehicleId;
        RentalId = rentalId;
        UserId = userId;
        Rating = rating;
        Comment = comment;
        CreationDate = creationDate;
    }

    // este constructor es solo para poder ejecutar las migraciones con ef
    private Review()
    {

    }

    public static Result<Review> Create(
        Rental rental,  
        Rating rating,
        Comment comment,
        DateTime creationDate
    )
    {
        // si el estatus del alquiler no está como completado, no podras dejar un review
        if (rental.Status != RentalStatus.completed)
            return Result.CreateWithFailureStatus<Review>(ReviewErrors.NotEligible);

        var review = new Review(
            Guid.NewGuid(),
            rental.VehicleId,
            rental.Id,
            rental.UserId,
            rating,
            comment,
            creationDate
        );

        review.DispatchDomainEvent(new ReviewCreatedDomainEvent(review.Id));

        return review;
    }
}

