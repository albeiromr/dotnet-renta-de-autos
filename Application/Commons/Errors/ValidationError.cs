namespace Application.Commons.Errors;

public sealed record ValidationError(string propertyName, string errorMessage);
