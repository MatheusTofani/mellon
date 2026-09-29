using Mellon.Domain.Errors;

namespace Mellon.Domain.ValueObjects;

public sealed record Cpf : DocumentNumber
{
    public Cpf(string cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf))
            throw new ValidationException("CPF não pode ser vazio");

        ValidateCpf(cpf);
        Value = NormalizeCpf(cpf);
    }

    public static implicit operator Cpf?(string? cpf) =>
        string.IsNullOrWhiteSpace(cpf) ? null : new Cpf(cpf);

    public override string ToString() => MaskCpf(Value);
    public bool Equals(Cpf? other) => other is not null && Value == other.Value;
    public override int GetHashCode() => Value.GetHashCode();

    private static string MaskCpf(string cpf) =>
        $"{cpf[..3]}.{cpf[3..6]}.{cpf[6..9]}-{cpf[9..11]}";

    private static string NormalizeCpf(string cpf) =>
        cpf.Trim().Replace(".", string.Empty).Replace("-", string.Empty);

    private static void ValidateCpf(string cpf)
    {
        int[] multiplicador1 = [10, 9, 8, 7, 6, 5, 4, 3, 2];
        int[] multiplicador2 = [11, 10, 9, 8, 7, 6, 5, 4, 3, 2];

        cpf = cpf.Trim().Replace(".", string.Empty).Replace("-", string.Empty);

        if (cpf.Length != 11)
            throw new ValidationException("CPF deve conter 11 dígitos");

        if (!long.TryParse(cpf, out _))
            throw new ValidationException("CPF deve conter apenas números");

        for (int j = 0; j < 10; j++)
            if (cpf == j.ToString().PadLeft(11, j.ToString()[0]))
                throw new ValidationException("CPF inválido");

        string tempCpf = cpf[..9];
        int soma = 0;

        for (int i = 0; i < 9; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

        int resto = soma % 11;
        resto = resto < 2 ? 0 : 11 - resto;

        string digito = resto.ToString();
        tempCpf += digito;
        soma = 0;

        for (int i = 0; i < 10; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

        resto = soma % 11;
        resto = resto < 2 ? 0 : 11 - resto;
        digito += resto.ToString();

        if (!cpf.EndsWith(digito))
            throw new ValidationException("CPF inválido");
    }
}
