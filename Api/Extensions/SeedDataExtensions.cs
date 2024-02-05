using Application.Commons.Interfaces;
using Bogus;
using Dapper;
using Domain.Commons.Enums;

namespace Api.Extensions;

//esta clase tiene un método de extensión que nos permite agregar data de prueba 
// a la base de datos, se explica en el video número 55 del curso udemy
// (data de puebas con dapper)
public static class SeedDataExtensions
{
    public static void SeedDatabaseData(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var sqlConnectionFactory = scope.ServiceProvider.GetRequiredService<ISqlConnectionFactory>();
        using var connection = sqlConnectionFactory.CreateConnection();

        //usando librería para crear datos de prueba
        var faker = new Faker();

        List<object> vehicles = new();
        for (var i = 0; i < 100; i++)
        {
            vehicles.Add(new
            {
                Id = Guid.NewGuid(),
                Model = faker.Vehicle.Model(),
                Vin = faker.Vehicle.Vin(),
                City = faker.Address.City(),
                PickUpAddress = faker.Address.StreetAddress(),
                Price = faker.Random.Decimal(1000, 20000),
                PremiumAccessories = new List<int> { (int)PremiumAccessory.wifi, (int)PremiumAccessory.AppleCar }
            });
        }

        List<object> users = new();
        for (var i = 0; i < 100; i++)
        {
            users.Add(new
            {
                Id = Guid.NewGuid(),
                Name = faker.Name.FirstName(),
                LastName = faker.Name.LastName(),
                Email = faker.Internet.Email()
            });
        }


        const string? vehiclesSql = """
            INSERT INTO public.vehicles(
                id, 
                model, 
                vin, 
                location_city, 
                location_pick_up_address, 
                price, 
                premium_accessories
            )
            VALUES (
                @Id, 
                @Model, 
                @Vin, 
                @City, 
                @PickUpAddress, 
                @Price, 
                @PremiumAccessories
            );
            """;

        const string? usersSql = """
            INSERT INTO public.users(
                id, 
                name, 
                last_name, 
                email
            )
            VALUES (
                @Id, 
                @Name, 
                @LastName, 
                @Email
            );
            """;

        connection.Execute(vehiclesSql, vehicles);
        connection.Execute(usersSql, users);
    }
}
