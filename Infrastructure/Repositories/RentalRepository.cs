using Domain.Rentals;
using Domain.Rentals.Enums;
using Domain.Rentals.Interfaces;
using Domain.Rentals.ObjectValues;
using Domain.Vehicles;
using Infrastructure.Databases;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

internal sealed class RentalRepository : Repository<Rental>, IRentalRepository
{
    private static readonly RentalStatus[] ActiveRentalStatuses =
    {
        RentalStatus.Reserved,
        RentalStatus.confirmed,
        RentalStatus.completed
    };
    public RentalRepository(ApplicationDbContext? context) : base(context)
    {
    }

    public Task<bool> IsOverlappingAsync(
        Vehicle vehicle,
        DateRange duration,
        CancellationToken cancellationToken = default
    )
    {
        var dbSet = _context!.Set<Rental>();
        var canBeBooked = dbSet.AnyAsync(rental =>
                rental.VehicleId == vehicle.Id
                && rental.Duration!.Init <= duration.End
                && rental.Duration.End >= duration.Init
                && ActiveRentalStatuses.Contains(rental.Status),
                cancellationToken
            );

        return canBeBooked!;
    }
}
