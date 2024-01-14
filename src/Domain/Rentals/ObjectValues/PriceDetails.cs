using Domain.Abstractions.ObjectValues;

namespace Domain.Rentals.ObjectValues;

// object value que representa los detalles de precios de un arrendamiento
public record PriceDetails(
    // precio que se le cobrará al usuario por el periodo de arrendamiento sin el mantenimiento
    Price RentalPeriodPice,
    // precio que se le cobrará al usuario por el mantenimiento del vehiculo 
    Price MaintainancePrice,
    // represen el precio de los servicios premium que el usuario contrate
    Price PremiumServicesPrice,
    // representa el precio total del arrendamiento
    Price TotalPrice
);