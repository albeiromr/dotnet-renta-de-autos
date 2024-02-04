using Domain.Vehicles;
using Domain.Vehicles.Interfaces;
using Infrastructure.Databases;

namespace Infrastructure.Repositories;

internal sealed class VehicleRepository : Repository<Vehicle>, IVehicleRepository
{
    public VehicleRepository(ApplicationDbContext? context) : base(context)
    {
    }
}
