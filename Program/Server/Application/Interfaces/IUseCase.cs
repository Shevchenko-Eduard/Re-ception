namespace Application.Interfaces;

public interface IUseCase
{
    Task Execute();
}
public interface IUseCase<TInput>
{
    Task Execute(TInput input);
}
public interface IUseCase<TInput, TOutput>
{
    Task<TOutput> Execute(TInput input);
}
