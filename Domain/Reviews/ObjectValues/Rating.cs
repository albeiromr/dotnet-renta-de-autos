using Domain.Commons.Clases;
using Domain.Commons.ObjectValues;

namespace Domain.Reviews.ObjectValues;

// object value que representa la calificación que el usuario 
// ha dejado por el servicio prestado
public sealed record Rating
{
    // representa el puntaje dado por el usuario
    public int Value { get; init; }

    public static readonly Error InvalidScore = new(
        "Rating.InvalidScore",
        "The provided rating score isn´t valid"
    );

    private Rating(int value)
    {
        Value = value;
    }

    public static Result<Rating> Create(int value)
    {
        if (value < 1 || value > 5)
            return Result.CreateWithFailureStatus<Rating>(InvalidScore);

        return new Rating(value);
    }
}


