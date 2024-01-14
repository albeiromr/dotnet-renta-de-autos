using System;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Vehicles.Interfaces;

public interface IVehicleRepository
{
    Task<Vehicle> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}

