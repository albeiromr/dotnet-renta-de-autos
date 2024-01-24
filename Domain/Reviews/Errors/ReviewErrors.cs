using Domain.Commons.ObjectValues;

namespace Domain.Reviews.Errors;

// Representa todos los posibles errores que podemos tener dentro de la 
// entidad Review
public static class ReviewErrors
{
    public static readonly Error NotEligible = new Error(
        "Review.NotEligible",
        "this review can´t be made because the rental is not completed yet"
    );
}
