using Domain.Commons.ObjectValues;

namespace Domain.Rentals.ObjectValues;

// object value que representa los detalles de precios de un arrendamiento
public record PriceDetails(
    // precio que se le cobrará al usuario por el periodo de arrendamiento sin el mantenimiento
    Price RentalPeriodPrice,
    // precio que se le cobrará al usuario por el mantenimiento del vehiculo 
    Price MaintainancePrice,
    // represen el precio de los accesorios premium que el usuario contrate
    Price PremiumAccessoriesPrice,
    // representa el precio total del arrendamiento
    Price TotalPrice
);