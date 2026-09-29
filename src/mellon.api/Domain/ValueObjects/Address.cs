using Mellon.Domain.Errors;

namespace Mellon.Domain.ValueObjects;

public sealed record Address
{
    public string  ZipCode      { get; init; } = string.Empty;
    public string  Street       { get; init; } = string.Empty;
    public string  Number       { get; init; } = string.Empty;
    public string? Complement   { get; init; }
    public string  Neighborhood { get; init; } = string.Empty;
    public string  City         { get; init; } = string.Empty;
    public string  State        { get; init; } = string.Empty;

    public Address(
        string zipCode,
        string street,
        string number,
        string? complement,
        string neighborhood,
        string city,
        string state)
    {
        if (string.IsNullOrWhiteSpace(zipCode))
            throw new ValidationException("CEP é obrigatório.");
        if (string.IsNullOrWhiteSpace(city))
            throw new ValidationException("Cidade é obrigatória.");
        if (string.IsNullOrWhiteSpace(state) || state.Trim().Length != 2)
            throw new ValidationException("Estado deve conter exatamente 2 caracteres.");

        ZipCode      = NormalizeZipCode(zipCode);
        Street       = street.Trim();
        Number       = number?.Trim() ?? string.Empty;
        Complement   = complement?.Trim();
        Neighborhood = neighborhood?.Trim() ?? string.Empty;
        City         = city.Trim();
        State        = state.Trim().ToUpper();
    }

    public override string ToString() =>
        $"{Street}, {Number}" +
        (string.IsNullOrEmpty(Complement) ? "" : $" - {Complement}") +
        $" - {Neighborhood}, {City}/{State} - CEP: {ZipCode[..5]}-{ZipCode[5..]}";

    public bool Equals(Address? other) =>
        other is not null &&
        ZipCode      == other.ZipCode      &&
        Street       == other.Street       &&
        Number       == other.Number       &&
        Complement   == other.Complement   &&
        Neighborhood == other.Neighborhood &&
        City         == other.City         &&
        State        == other.State;

    public override int GetHashCode() =>
        HashCode.Combine(ZipCode, Street, Number, Neighborhood, City, State);

    private static string NormalizeZipCode(string zipCode) =>
        zipCode.Trim().Replace("-", string.Empty);
}
