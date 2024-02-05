using Domain.Commons.ObjectValues;

namespace Domain.Rentals.Errors;

// Representa todos los posibles errores que podemos tener dentro de la 
// entidad rental
public static class RentalErrors
{
    public static readonly Error NotFound = new Error(
        "Rental.NotFound",
        "The rental with the specified Id wasn´t found"
    );

    public static readonly Error Overlap = new Error(
        "Rental.Overlap",
        "The vehicle is not available for rental in the required date range"
    );
}
