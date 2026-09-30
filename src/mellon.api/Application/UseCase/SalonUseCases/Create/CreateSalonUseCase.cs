using Mellon.Application.Interfaces;
using Mellon.Application.Interfaces.Repository;
using Mellon.Domain.Entities;
using Mellon.Domain.Errors;
using Mellon.Domain.ValueObjects;


namespace Mellon.Application.UseCase.SalonUseCases.Creates;

public class CreateSalonUseCase(
    IRepositoryCommand<Task<Salon>, Salon> createCommand)
    : IUseCase<Task<Salon>, CreateSalonInput>
{
    public async Task<Salon> Execute(CreateSalonInput input)
    {

        var salon = Salon.Create(
            name: input.Name,
            cnpj: input.Cnpj,
            email: input.Email,
            phone: input.Phone
        );

        return await createCommand.Handle(salon);
    }
}

public record CreateSalonInput(
    Name Name,
    Cnpj Cnpj,
    Cpf Cpf,
    Phone Phone,
    Email Email);
