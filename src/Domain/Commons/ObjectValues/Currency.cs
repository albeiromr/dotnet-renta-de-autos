using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Abstractions.ObjectValues;

// object value que representa cada uno de los tipos de moneda con 
// los que un usuario podría hacer un pago, como dolares o euros
public record Currency
{
    public string? Code { get; init; }

    public static readonly Currency Usd = new Currency("USD");

    public static readonly Currency Eur = new Currency("EUR");

    public static readonly Currency None = new Currency("");

    public static readonly IReadOnlyCollection<Currency> All = new[] { Usd, Eur };

    private Currency(string code)
    {
        Code = code;
    }

    public static Currency FromCode(string code)
    {
        return All.FirstOrDefault(c => c.Code == code) ??
            throw new ApplicationException("The currency code is not valid");
    }
}

