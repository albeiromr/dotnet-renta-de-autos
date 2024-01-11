using System;

namespace Domain.Commons;

// object value que representa un precio en el sistema
public record Price(decimal amount, Currency currency)
{
    // Esta es una sobrecarga de operador para evitar que el usuario haga
    // pago con dos tipos de moneda diferentes
    public static Price operator +(Price left, Price right)
    {
        if (left.currency != right.currency)
            throw new InvalidOperationException("The currency type must be the same");

        return new Price(left.amount + right.amount, left.currency);
    }

    public static Price GetInZero() => new Price(0, Currency.None);
    public static Price GetInZero(Currency newCurrency) => new Price(0, newCurrency);
    public bool IsZero() => this == GetInZero(currency);
}

