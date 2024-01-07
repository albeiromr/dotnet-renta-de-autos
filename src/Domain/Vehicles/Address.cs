namespace Domain.Vehicles;

// Address es un object value que representa toda la información de 
// la dirección, notese que a diferencia de una entidad, un object
// value no tiene un identificador único´(Id), y aún asi su información
// es única por que no hay dos direcciones iguales
public record Address
(
    // representa el País donde se recogerá el vehiculo
    string Country,
    // representa el departamento donde se recogerá el vehiculo
    string Department,
    // Representa la provincia donde se recogerá el vehiculo
    string Province,
    // representa la ciudad donde se recogerá el vehiculo
    string City,
    // dirección donde se recogerá el vehiculo
    string PickUpAddress
);

