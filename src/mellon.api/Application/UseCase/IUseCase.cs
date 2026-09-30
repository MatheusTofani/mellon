namespace Mellon.Application.UseCase;

public interface IUseCase<out TOutput, in TInput>
{
    TOutput Execute(TInput input);
}

public interface IUseCaseInputOnly<in TInput>
{
    Task Execute(TInput input);
}

public interface IUseCaseOutputOnly<out TOutput>
{
    TOutput Execute();
}
