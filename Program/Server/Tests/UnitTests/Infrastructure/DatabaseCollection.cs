using Infrastructure.Database;
using Infrastructure.Database.Strategy;

namespace UnitTests.Infrastructure;

public class DatabaseCollection : IDisposable
{
    public ProgramContext TodoContext { get; }
    private readonly SqliteInMemoryStrategy _sqliteInMemoryStrategy;
    public DatabaseCollection()
    {
        _sqliteInMemoryStrategy = new();
        TodoContext = new(_sqliteInMemoryStrategy);
        TodoContext.Database.EnsureCreated();
    }
    public void Dispose()
    {
        TodoContext.Dispose();
        _sqliteInMemoryStrategy.Dispose();
    }
}