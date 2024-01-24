namespace Domain.Commons.ObjectValues;

// object value que representa un error presentado dentro
// de las operaciones de las entidades
public record Error(string errorName, string errorDescription)
{
    // representa un error inicializado con información vacía
    public static Error None = new Error(string.Empty, string.Empty);

    // representa un error que fue inicializado con un valor nulo o inexistente
    public static Error NullValue = new Error("Error.NullValue", "Un Valor Nulo fué ingrasado");
}
