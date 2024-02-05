using Application.Commons.Interfaces;
using Application.Rentals.Responses;
using Dapper;
using Domain.Commons.Clases;

namespace Application.Rentals.GetRental;

// los handlers deben ser internal para que no queden expuestos
internal sealed class GetRentalQueryHandler : IQueryHandler<GetRentalQuery, RentalResponse>
{
    private readonly ISqlConnectionFactory _connectionFactory;

    public GetRentalQueryHandler(ISqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<Result<RentalResponse>> Handle(GetRentalQuery request, CancellationToken cancellationToken)
    {
        using var connection = _connectionFactory.CreateConnection();

        var sql = """
            SELECT 
            id AS Id,
            vehicle_id AS VehicleId,
            user_id AS UserId,
            status AS Status,
            price AS Price,
            duration_init AS DurationInit,
            duration_end AS DurationEnd,
            creation_date AS CreationDate
            FROM rentals WHERE id=@rentalId
         """;

        var rental = await connection.QueryFirstOrDefaultAsync<RentalResponse>(
            sql,
            new
            {
                request.rentalId
            }
        );

        return rental!;
    }
}
