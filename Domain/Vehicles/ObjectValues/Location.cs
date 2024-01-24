namespace Domain.Vehicles.ObjectValues;

// object value que representa toda la información de donde está
// ubicado el vehiculo
public record Location
(
    // país donde está el vehiculo
    string Country,
    // departamento donde está el vehiculo
    string Department,
    // provincia donde está el vehiculo
    string Province,
    // ciudad donde está el vehiculo
    string City,
    // dirección donde está el vehiculo
    string PickUpAddress
);

