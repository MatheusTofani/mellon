using Mellon.Domain.Errors;

namespace Mellon.Domain.ValueObjects;

public sealed record Cnpj : DocumentNumber
{
    public Cnpj(string cnpj)
    {
        if (string.IsNullOrWhiteSpace(cnpj))
            throw new ValidationException("CNPJ não pode ser vazio");

        ValidateCnpj(cnpj);
        Value = NormalizeCnpj(cnpj);
    }

    public static implicit operator Cnpj?(string? cnpj) =>
        string.IsNullOrWhiteSpace(cnpj) ? null : new Cnpj(cnpj);

    public override string ToString() => MaskCnpj(Value);
    public bool Equals(Cnpj? other) => other is not null && Value == other.Value;
    public override int GetHashCode() => Value.GetHashCode();

    private static string MaskCnpj(string cnpj) =>
        $"{cnpj[..2]}.{cnpj[2..5]}.{cnpj[5..8]}/{cnpj[8..12]}-{cnpj[12..14]}";

    private static string NormalizeCnpj(string cnpj) =>
        cnpj.Trim().Replace(".", string.Empty).Replace("/", string.Empty).Replace("-", string.Empty);

    private static void ValidateCnpj(string cnpj)
    {
        int[] multiplicador1 = [5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];
        int[] multiplicador2 = [6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];

        cnpj = cnpj.Trim().Replace(".", string.Empty).Replace("/", string.Empty).Replace("-", string.Empty);

        if (cnpj.Length != 14)
            throw new ValidationException("CNPJ deve conter 14 dígitos");

        if (!long.TryParse(cnpj, out _))
            throw new ValidationException("CNPJ deve conter apenas números");

        for (int j = 0; j < 10; j++)
            if (cnpj == j.ToString().PadLeft(14, j.ToString()[0]))
                throw new ValidationException("CNPJ inválido");

        string tempCnpj = cnpj[..12];
        int soma = 0;

        for (int i = 0; i < 12; i++)
            soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

        int resto = soma % 11;
        resto = resto < 2 ? 0 : 11 - resto;

        string digito = resto.ToString();
        tempCnpj += digito;
        soma = 0;

        for (int i = 0; i < 13; i++)
            soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

        resto = soma % 11;
        resto = resto < 2 ? 0 : 11 - resto;
        digito += resto.ToString();

        if (!cnpj.EndsWith(digito))
            throw new ValidationException("CNPJ inválido");
    }
}
