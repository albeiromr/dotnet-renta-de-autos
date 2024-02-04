using Domain.Rentals.ObjectValues;
using Domain.Vehicles;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Rentals.Interfaces;

public interface IRentalRepository
{
    Task<Rental> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);  
    Task<bool> IsOverlappingAsync(
        Vehicle vehicle, 
        DateRange duration,
        CancellationToken cancellationToken = default
    );
    void Add(Rental rental);
}
