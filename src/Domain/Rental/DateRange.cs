using System;

namespace Domain.Rental;

public sealed record DateRange
{
    private DateRange(){}

    public DateOnly Init { get; init; }
    public DateOnly End { get; init; }
    public int DayQuantity => End.DayNumber - Init.DayNumber;

    public static DateRange Create(DateOnly init, DateOnly end)
    {
        // valida que la fecha de inicio no es mayor a la fecha final
        if (init > end)
            throw new ApplicationException("the init date can´t be mayor than the end date");

        return new DateRange
        {
            Init = init,
            End = end,
        };
    }

}
    

