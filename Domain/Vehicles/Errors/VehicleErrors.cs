using Domain.Commons.ObjectValues;

namespace Domain.Vehicles.Errors;

public static class VehicleErrors
{
    public static readonly Error NotFound = new Error(
        "Vehicle.NotFound",
        "There is not vehicle with the required ID"
    );
}
