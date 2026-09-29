namespace Mellon.Domain.ValueObjects.Identity;

using Mellon.Domain.Errors;

public abstract record DocumentNumber
{
    protected string Value { get; init; } = string.Empty;

    public static implicit operator string(DocumentNumber? d) => d?.Value ?? string.Empty;

    public abstract override string ToString();

    public static DocumentNumber Parse(string document)
    {
        if (string.IsNullOrWhiteSpace(document))
            throw new ValidationException("Documento não pode ser vazio.");

        var normalized = document.Trim()
            .Replace(".", string.Empty)
            .Replace("-", string.Empty)
            .Replace("/", string.Empty);

        return normalized.Length switch
        {
            11 => new Cpf(normalized),
            14 => new Cnpj(normalized),
            _ => throw new ValidationException("Documento deve ser um CPF ou CNPJ válido.")
        };
    }
}