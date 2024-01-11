using System;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Vehicles;

public interface IVehicleRepository
{
    Task<Vehicle> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}

