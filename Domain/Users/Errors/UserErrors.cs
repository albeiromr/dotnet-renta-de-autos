using Domain.Commons.ObjectValues;

namespace Domain.Users.Errors;

public static class VehicleErrors
{
    public static readonly Error NotFound = new Error(
        "User.NotFound",
        "There is not users with the required ID"
    );

    public static readonly Error InvalidCredentials = new Error(
        "User.InvalidCredentials",
        "The provided credentials are incorrect"
    );
}
