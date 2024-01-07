namespace Domain.Vehicles;

// Model es un object value que representa el modelo del vehiculo, notese
// que a diferencia de una entidad, un object value no tiene un identificador
// único´(Id)
public record Model(
    string value
);

