namespace Domain.Vehicles.ObjectValues;

// object value que representa toda la información de donde está
// ubicado el vehiculo
public record Location
(
    // ciudad donde está el vehiculo
    string City,
    // dirección donde está el vehiculo
    string PickUpAddress
);

