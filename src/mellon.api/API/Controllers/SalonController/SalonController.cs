using Microsoft.AspNetCore.Mvc;
using Mellon.Application.UseCase;
using Mellon.Application.UseCase.SalonUseCases.Creates;

using Mellon.Domain.Entities;


namespace Mellon.Api.Controllers.SalonController;

[ApiController]
[Route("api/[controller]")]
public class SalonController(
     IUseCase<Task<Salon>, CreateSalonInput> createSalon
   )
    : ControllerBase
{

    [HttpPost]
    public async Task<SalonResponse> Create([FromBody] CreateSalonRequest request)
    {
        var salon = await createSalon.Execute(request.ToInput());
        return SalonResponse.FromDomain(salon);
    }

}