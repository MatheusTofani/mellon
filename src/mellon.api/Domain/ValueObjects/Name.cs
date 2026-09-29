using System.Text.RegularExpressions;
using Mellon.Domain.Errors;

namespace Mellon.Domain.ValueObjects;

public sealed partial record Name
{
    private string Value { get; init; } = string.Empty;

    public Name(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ValidationException("Nome não pode ser vazio");

        Value = NormalizeName(name);
    }

    public static implicit operator Name(string name) => new(name);
    public static implicit operator string(Name name) => name.Value;

    public override string ToString() => Value;
    public bool Equals(Name? other) => other is not null && Value == other.Value;
    public override int GetHashCode() => Value.GetHashCode();

    private static string NormalizeName(string name) =>
        WhitespaceRegex().Replace(name.Trim(), " ");

    [GeneratedRegex(@"\s+", RegexOptions.None, matchTimeoutMilliseconds: 1000)]
    private static partial Regex WhitespaceRegex();
}
