namespace Domain.Vehicles;

public record CurrencyType
{
    private CurrencyType(string code)
    {
        Code = code;
    }
    public string? Code { get; init; }

    public static readonly CurrencyType Usd = new CurrencyType("USD");

    public static readonly CurrencyType Eur = new CurrencyType("EUR");

    public static readonly CurrencyType None = new CurrencyType("");

    public static readonly IReadOnlyCollection<CurrencyType> All = new[]
    {
        Usd,
        Eur
    };

    public static CurrencyType FromCode(string code)
    {
        return All.FirstOrDefault(c => c.Code == code) ?? 
            throw new ApplicationException("The currency code is not valid");
    }
}

