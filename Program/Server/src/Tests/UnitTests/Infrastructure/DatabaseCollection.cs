using Infrastructure.Database;
using Infrastructure.Database.Strategy;

namespace UnitTests.Infrastructure;

public class DatabaseCollection : IDisposable
{
    public ProgramContext Context { get; }
    private readonly SqliteInMemoryStrategy _sqliteInMemoryStrategy;
    public DatabaseCollection()
    {
        _sqliteInMemoryStrategy = new();
        Context = new(_sqliteInMemoryStrategy);
        Context.Database.EnsureCreated();
    }
    public void Dispose()
    {
        Context.Dispose();
        _sqliteInMemoryStrategy.Dispose();
    }
}