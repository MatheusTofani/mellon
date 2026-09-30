namespace Mellon.Application.Interfaces.Repository;

public interface IRepositoryCommand<out TOutput, in TInput>
{
    TOutput Handle(TInput input);
}

public interface IRepositoryCommandInputOnly<in TInput>
{
    void Handle(TInput input);
}
