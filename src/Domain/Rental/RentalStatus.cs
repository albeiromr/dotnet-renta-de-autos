namespace Domain.Rental;

// representa los estados que puede tener el arrendamiento de un vehiculo
public enum RentalStatus
{
    Reserved = 1,
    confirmed = 2,
    refused = 3,
    cancelled = 4,
    completed = 5,
}

