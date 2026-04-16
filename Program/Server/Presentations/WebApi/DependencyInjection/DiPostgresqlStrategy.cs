using Infrastructure.Database.Strategy;

namespace WebApi.DependencyInjection;

public class DiPostgresqlStrategy : PostgresqlStrategy
{
    public DiPostgresqlStrategy() : base($"")
    {
    }
}
