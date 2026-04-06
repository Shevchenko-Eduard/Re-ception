using Domain.Entity.User.Permission;

namespace Application.Interfaces;

public interface IUseCase
{
    Permission RequiredPermission { get; }
    Task Execute();
}
public interface IUseCase<TInput>
{
    Permission RequiredPermission { get; }
    Task Execute(TInput input);
}
public interface IUseCase<TInput, TOutput>
{
    Permission RequiredPermission { get; }
    Task<TOutput> Execute(TInput input);
}
