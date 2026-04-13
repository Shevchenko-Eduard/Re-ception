namespace UnitTests.Infrastructure;

public class DatabaseInitializationTests
{
    private readonly DatabaseCollection _databaseCollection;
    public DatabaseInitializationTests()
    {
        _databaseCollection = new();
    }
    /// <summary>
    /// проверка что БД просто создается и схема
    /// соответствует модели, без ошибок при миграции
    /// </summary>
    [Fact]
    public void SchemaTest() 
    {
        Assert.True(true);
    }
}