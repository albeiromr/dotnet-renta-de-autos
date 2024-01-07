namespace Domain.Vehicles;

public record Currency(decimal moneyAmount, CurrencyType currencyType)
{
    // Esta es una sobrecarga de operador para validar si el cliente
    // está pagando con el mismo tipo de moneda con el que pago su
    // renta de vehiculos los meses anteriores.
    public static Currency operator + (Currency left, Currency right)
    {
        if (left.currencyType != right.currencyType)
            throw new InvalidOperationException("The currency type must be the same");

        return new Currency(left.moneyAmount + right.moneyAmount, left.currencyType);
    }

    public static Currency GetInZero() => new Currency(0, CurrencyType.None);
    public static Currency GetInZero(CurrencyType type) => new Currency(0, type);
    public bool IsZero() => this == GetInZero(currencyType);
}

