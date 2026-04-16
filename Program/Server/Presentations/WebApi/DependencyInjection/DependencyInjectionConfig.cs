using Domain.Interfaces;
using Infrastructure;
using Infrastructure.Database;
using Infrastructure.Database.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace WebApi.DependencyInjection;

public static partial class DependencyInjectionConfig
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IClock, Clock>();
        // services.AddScoped<ICurrentProfile, CurrentProfile>();
        // services.AddScoped<IHasher, Hasher>();
        // services.AddScoped<IControllerUndoRedo, ManagerUndoRedo>();
        
        services.AddScoped<IConnectionStrategy, DiPostgresqlStrategy>();
        services.AddScoped<ProgramContext>();
        services.AddScoped<DbContext, ProgramContext>();

        // services.AddScoped<IProfileRepositories, EfProfileRepository>();
        // services.AddScoped<ITaskItemRepositories, EfTodoTaskRepository>();
        // services.AddScoped<IUnitOfWork, EfUnitOfWork>();

        // services.AddScoped<GetAllProfileUseCase>();
        // services.AddScoped<AddProfileUseCase>();
        // services.AddScoped<ChangeProfileUseCase>();
        // services.AddScoped<DeleteProfileUseCase>();
        // services.AddScoped<UpdateProfileUseCase>();

        // services.AddScoped<GetAllTaskUseCase>();
        // services.AddScoped<AddTaskUseCase>();
        // services.AddScoped<DeleteTaskUseCase>();
        // services.AddScoped<UpdateTaskUseCase>();
        return services;
    }
}