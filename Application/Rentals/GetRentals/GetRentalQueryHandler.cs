using Application.Commons.Interfaces;
using Dapper;
using Domain.Commons.Clases;

namespace Application.Rentals.GetRentals;

// los handlers deben ser internal para que no queden expuestos
internal sealed class GetRentalQueryHandler : IQueryHandler<GetRentalQuery, GetRentalQueryResponse>
{
    private readonly ISqlConnectionFactory _connectionFactory;

    public GetRentalQueryHandler(ISqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<Result<GetRentalQueryResponse>> Handle(GetRentalQuery request, CancellationToken cancellationToken)
    {
        using var connection = _connectionFactory.CreateConnection();

        var sql = """
            SELECT 
            id AS Id,
            vehicle_id AS VehicleId,
            user_id AS UserId,
            status AS Status,
            price_details_rental_period_price_amount AS RentalPeriodPrice,
            price_details_rental_period_price_currency AS RentalPeriodPriceCurrency,
            price_details_maintainance_price_amount AS MaintainancePrice,
            price_details_maintainance_price_currency AS MaintainancePriceCurrency,
            price_details_premium_accessories_price_amount AS PremiumAccessoriesPrice,
            price_details_premium_accessories_price_currency AS PremiumAccessoriesPriceCurrency,
            price_details_total_price_amount AS TotalPrice,
            price_details_total_price_currency AS TotalPriceCurrency,
            duration_init AS DurationInit,
            duration_end AS DurationEnd,
            creation_date As CreationDate
            FROM alquileres WHERE id=@rentalId
         """;

        var rental = await connection.QueryFirstOrDefaultAsync<GetRentalQueryResponse>(
            sql,
            new
            {
                request.rentalId
            }
        );

        return rental!;
    }
}
