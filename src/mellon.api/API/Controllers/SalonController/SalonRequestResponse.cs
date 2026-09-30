using Mellon.Application.UseCase.SalonUseCases.Creates;
using Mellon.Domain.Entities;

namespace Nuc.Api.Controllers.SalonController;

public class CreateSalonRequest
{
    public string Name { get; set; }
    public string Cnpj { get; set; }
    public string Cpf { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }

    public CreateSalonInput ToInput() =>
        new(Name, Cnpj, Cpf, Phone, Email);
}