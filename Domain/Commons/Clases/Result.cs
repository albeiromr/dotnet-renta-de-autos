using Domain.Commons.ObjectValues;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Domain.Commons.Clases;

public class Result
{
    public bool Success { get; }
    // Failure es igual a la negación del success
    public bool Failure => !Success;
    public Error? Error { get; }

    protected internal Result(bool success, Error error)
    {
        if (success && error != Error.None)
            throw new InvalidOperationException();

        if(success && error == Error.None)
            throw new InvalidOperationException();

        Success = success;
        Error = error;
    }

    public static Result CreateWithSuccessStatus() => new Result(true, Error.None);
    public static Result CreateWithFailureStatus(Error error) => new Result(false, error);

    // los siguientes métodos aplican para la clase genérica Result<T>
    public static Result<TValue> CreateWithSuccessStatus<TValue>(TValue value) => new Result<TValue>(value, true, Error.None);
    public static Result<TValue> CreateWithFailureStatus<TValue>(Error error) => new Result<TValue>(default, false, error);
    public static Result<TValue> Create<TValue>(TValue value) =>
        value is not null ? CreateWithSuccessStatus(value) : CreateWithFailureStatus<TValue>(Error.NullValue);
}

public class Result<TValue> : Result
{
    private readonly TValue? _value;

    
    [NotNull] // attributo para validar que el valor de [value] nunca sea nulo
    public TValue? Value => Success ? _value! : throw new InvalidOperationException(
        "El resultado del valor del error no es admisible"
    );

    protected internal Result(TValue? value, bool success, Error error) : base(success, error)
    {
        _value = value;
    }
    
    public static implicit operator Result<TValue>(TValue value) => Create(value);
}
