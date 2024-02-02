using Application.Commons.Interfaces;
using Application.Vehicles.Responses;
using Dapper;
using Domain.Commons.Clases;
using Domain.Rentals.Enums;

namespace Application.Vehicles.SearchVehicles;

// los handlers deben ser internal para que no queden expuestos
internal sealed class SearchVehiclesQueryHandler : IQueryHandler<SearchVehiclesQuery, IReadOnlyList<VehicleResponse>>
{
    private static readonly int[]? ActiveRentalsStatuses = {
        (int)RentalStatus.Reserved,
        (int)RentalStatus.confirmed,
        (int)RentalStatus.completed
    };
    private readonly ISqlConnectionFactory _connectionFactory;

    public SearchVehiclesQueryHandler(ISqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<Result<IReadOnlyList<VehicleResponse>>> Handle(SearchVehiclesQuery request, CancellationToken cancellationToken)
    {
        if (request.initDate > request.endDate)
        {
            return new List<VehicleResponse>();
        }

        using var connection = _connectionFactory.CreateConnection();

        const string sql = """
            SELECT 
                a.id AS Id,
                a.model AS Model,
                a.vin AS Vin,
                a.rent_price_amount AS Price,
                a.rent_price_currency AS Currency,
                a.location_country AS Country,
                a.location_department AS Department,
                a.location_city AS City,
                a.location_pick_up_address AS PickUpAddress
            FROM vehicles AS a
            WHERE NOT EXISTS
            (
                SELECT 1
                FROM rentals AS b
                WHERE 
                    b.vehicle_id = a.id
                    b.duration_init <= @endDate AND
                    b.duration_end >= @startDate AND
                    b.status = ANY(@ActiveRentalsStatuses)
            )
            """;

        // en el siguiente método se hace un query que trae varios objetos desde la db y luego
        // cada uno de esos objetos se divide entre dos clases diferentes (VehicleResponse, AddressResponse),
        // y luego esos dos clases se unen en una sola para retornar una lista de VehicleResponse
        var vehicles = await connection.QueryAsync<VehicleResponse, AddressResponse, VehicleResponse>(
            sql,
            (vehicle, Adress) =>
            {
                vehicle.Address = Adress;
                return vehicle;
            },
            new
            {
                startDate = request.initDate,
                endDate = request.initDate,
                ActiveRentalsStatuses = ActiveRentalsStatuses
            },
            splitOn: "Country"
        );

        return vehicles.ToList();
    }
}
