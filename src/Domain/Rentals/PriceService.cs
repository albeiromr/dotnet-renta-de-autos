using Domain.Commons;
using Domain.Vehicles;

namespace Domain.Rentals;

public static class PriceService
{
    // calcula y fabrica los precios asociados a la renta de un vehiculo
    public static PriceDetails CalculatePrices(Vehicle vehicle, DateRange rentalTimePerriod)
    {
        // obteniendo el tipo de moneda en la cual hay que calcular los montos
        Currency currency = vehicle.RentPrice!.currency;

        // obteniendo el precio que tendra arrendar el vehiculo por el rango de tiempo
        // especificado por el cliente
        Price costPerPeriodTime = new Price(
        rentalTimePerriod.DayQuantity * vehicle.RentPrice.amount,
        currency
        );

        // representa el porcentaje del precio que se debe aumentar por el uso de 
        // los servicios premium
        decimal porcentageAument = 0M;

        // obteniendo el precio por los servicios premium
        foreach (var premiumService in vehicle.PremiumServices)
        { 
            // agregando porcentajes de aumento dependiendo del valor de cada servicio premium
            // nueva sintaxis del switch toca estudiarla.
            porcentageAument += premiumService switch
            {
                PremiumService.AppleCar or PremiumService.AndroidCar => 0.05m,
                PremiumService.AirConditioning => 0.01m,
                PremiumService.Maps => 0.01m,
                PremiumService.wifi => 0.01m,
            };
        }

        // inicializando en cero el precio de los servicios premium
        var PremiumServicesCharges = Price.GetInZero(currency);

        if (porcentageAument > 0)
        {
            PremiumServicesCharges = new Price(
                costPerPeriodTime.amount * porcentageAument,
                currency
            );
        }

        // inicializando en cero el precio total del arrendamiento
        var totalPrice = Price.GetInZero(currency);
        totalPrice += costPerPeriodTime;


        if(!vehicle.MaintenancePrice!.IsZero())
        {
            totalPrice += vehicle.MaintenancePrice;
        }


        totalPrice += PremiumServicesCharges;

        return new PriceDetails(
            costPerPeriodTime,
            vehicle.MaintenancePrice,
            PremiumServicesCharges,
            totalPrice
        );
    }
}
