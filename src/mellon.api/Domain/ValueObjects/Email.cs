using System.Text.RegularExpressions;
using Mellon.Domain.Errors;

namespace Mellon.Domain.ValueObjects.Identity;

public sealed partial record Email
{
    private string Value { get; init; } = string.Empty;

    public Email(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ValidationException("Email não pode ser vazio");

        var normalized = NormalizeEmail(email);
        ValidateEmail(normalized);
        Value = normalized;
    }

    public static implicit operator Email?(string? email) =>
        string.IsNullOrWhiteSpace(email) ? null : new Email(email);

    public static implicit operator string(Email? email) =>
        email is null ? string.Empty : email.Value;

    public override string ToString() => Value;
    public bool Equals(Email? other) => other is not null && Value == other.Value;
    public override int GetHashCode() => Value.GetHashCode();

    private static string NormalizeEmail(string email) => email.Trim().ToLowerInvariant();

    private static void ValidateEmail(string email)
    {
        if (!EmailRegex().IsMatch(email))
            throw new ValidationException("Email inválido");
    }

    [GeneratedRegex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.None, matchTimeoutMilliseconds: 1000)]
    private static partial Regex EmailRegex();
}
