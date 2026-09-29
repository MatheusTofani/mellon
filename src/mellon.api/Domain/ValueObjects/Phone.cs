using System.Text.RegularExpressions;
using Mellon.Domain.Errors;

namespace Mellon.Domain.ValueObjects;

public sealed partial record Phone
{
    private string Value { get; init; } = string.Empty;

    public Phone(string phone)
    {
        if (string.IsNullOrWhiteSpace(phone))
            throw new ValidationException("Telefone não pode ser vazio.");

        var digits = NonDigit().Replace(phone, string.Empty);
        Validate(digits);
        Value = digits;
    }

    public static implicit operator Phone?(string? phone) =>
        string.IsNullOrWhiteSpace(phone) ? null : new Phone(phone);

    public static implicit operator string(Phone? phone) =>
        phone is null ? string.Empty : phone.Value;

    public override string ToString() => Value.Length == 11
        ? $"({Value[..2]}) {Value[2]}{Value[3..7]}-{Value[7..]}"
        : $"({Value[..2]}) {Value[2..6]}-{Value[6..]}";

    public bool Equals(Phone? other) => other is not null && Value == other.Value;
    public override int GetHashCode() => Value.GetHashCode();

    private static void Validate(string digits)
    {
        if (digits.Length is not (10 or 11))
            throw new ValidationException("Telefone deve conter 10 ou 11 dígitos incluindo o DDD.");

        var ddd = int.Parse(digits[..2]);
        if (ddd < 11 || ddd > 99)
            throw new ValidationException($"DDD inválido: {ddd}.");

        if (digits.Length == 11 && digits[2] != '9')
            throw new ValidationException("Número de celular deve iniciar com 9 após o DDD.");
    }

    [GeneratedRegex(@"\D", RegexOptions.None, matchTimeoutMilliseconds: 1000)]
    private static partial Regex NonDigit();
}
