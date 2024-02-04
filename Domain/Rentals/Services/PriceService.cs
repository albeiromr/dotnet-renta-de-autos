using Domain.Commons.Enums;
using Domain.Commons.ObjectValues;
using Domain.Rentals.ObjectValues;
using Domain.Vehicles;

namespace Domain.Rentals.Services;

public class PriceService
{
    // retorna un precio dependiendo de la lógica de negocio 
    // que necesitemos, este método es solo un ejemplo de como
    // consumir domain services
    public decimal CalculatePrices(Vehicle vehicle)
    {
        // implementar lógica de negocio para calcular el precio aquí
        return vehicle.Price;
    }
}
