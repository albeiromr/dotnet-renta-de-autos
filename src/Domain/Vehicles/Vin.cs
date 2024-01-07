namespace Domain.Vehicles;

// Vin es un object value que representa el número de serie del vehiculo, notese
// que a diferencia de una entidad, un object value no tiene un identificador
// único (Id) y sin embargo no pueden existir dos números de de serie iguales
public record Vin (
    string value
);

