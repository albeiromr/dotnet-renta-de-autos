using Domain.Abstractions;
using System;

namespace Domain.Rental;

// Representa la entidad rental, es decir los datos del arrendamiento de 
// un vehiculo
public sealed class Rental : Entity
{
    public Rental(Guid id) : base(id)
    {
    }

    // representa el estado de el arrendamiento
    public RentalStatus Status { get; private set; }

    // representa el lapso de tiempo durante la cual se realizará el arrendamiento
    public DateRange? Duration { get; private set; }
}

