using Mellon.Application.UseCase.SalonUseCases.Creates;
using Mellon.Domain.Entities;

namespace Mellon.Api.Controllers.SalonController;

public class CreateSalonRequest
{
    public required string Name { get; set; }
    public required string Cnpj { get; set; }
    public required string Cpf { get; set; }
    public required string Phone { get; set; }
    public required string Email { get; set; }

    public CreateSalonInput ToInput() =>
        new(Name, Cnpj, Cpf, Phone, Email);
}

public record SalonResponse(
    string Id,
    string Name,
    string Cnpj,
    string Phone,
    string Email)
{
    public static SalonResponse FromDomain(Salon salon) =>
        new(salon.Id, salon.Name, salon.Cnpj, salon.Phone, salon.Email);
}
