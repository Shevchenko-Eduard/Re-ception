using Infrastructure.Database.Strategy;

namespace WebApi.DependencyInjection;

public class DiPostgresqlStrategy : PostgresqlStrategy
{
    public DiPostgresqlStrategy() : base(Environment.GetEnvironmentVariable("DATABASE_URL") ?? throw new SystemException())
    {
    }
}
