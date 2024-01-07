namespace Domain.Vehicles;

// Model es un object value que representa el modelo del vehiculo, notese
// que a diferencia de una entidad, un object value no tiene un identificador
// único´(Id) y sin embargo no pueden existir dos modelos de vehiculo iguales
// cada modelo representa una referencia de autos unica
public record Model(
    string value
);

