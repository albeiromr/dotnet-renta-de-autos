using Application.Commons.Errors;

namespace Application.Commons.Exceptions;

//estas son las excepciones de la capa application
public sealed class DataValidationException : Exception
{
    public IEnumerable<ValidationError>? Errors { get; }
    public DataValidationException(IEnumerable<ValidationError>? errors)
    {
        Errors = errors;
    }
}
