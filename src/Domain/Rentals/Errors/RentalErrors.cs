using Domain.Abstractions.ObjectValues;

namespace Domain.Rentals.Errors;

// Representa todos los posibles errores que podemos tener dentro de la 
// entidad rental
public static class RentalErrors
{
    public static Error NotFound = new Error(
        "Rental.NotFound",
        "The rental with the specified Id wasn´t found"
    );

    public static Error Overlap = new Error(
        "Rental.Overlap",
        "The vehicle is not available for rental in the required date range"
    );

    public static Error NotReserved = new Error(
        "Rental.NotReserved",
        "The Renatl is not reserved"
    );

    public static Error NotConfirmed = new Error(
        "Rental.NotConfirmed",
        "The Renatl is not NotConfirmed"
    );

    public static Error AreadyStarted = new Error(
        "Rental.AreadyStarted",
        "The Renatl is already started"
    );
}
