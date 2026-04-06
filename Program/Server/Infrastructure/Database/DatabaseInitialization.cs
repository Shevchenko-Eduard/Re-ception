using System.Reflection;
using Application.Interfaces;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database;

public class DatabaseInitialization(
    ProgramContext context,
    ILogger<DatabaseInitialization>? logger,
    IHostEnvironment hostEnvironment)
{
	private readonly ProgramContext _context = context;
	private readonly ILogger<DatabaseInitialization>? _logger = logger;
	private readonly IHostEnvironment _hostEnvironment = hostEnvironment;

    public async Task InitializeAsync(CancellationToken cancellationToken = default)
	{
		try
		{
			// Всегда используем миграции в Production
			if (IsProductionEnvironment())
			{
				await _context.Database.MigrateAsync(cancellationToken);
				await SeedDataIfNeededAsync(cancellationToken);
			}
			else
			{
				// Для Development можно использовать EnsureCreatedAsync для скорости
				var created = await _context.Database.EnsureCreatedAsync(cancellationToken);
				if (created)
				{
					var seedData = ExtractHasData(_context);
					if (!seedData.Any())
					{
						_logger?.LogInformation("No seed data found in the model.");
						return;
					}
					else
					{
						foreach (var entity in seedData)
						{
							_context.Add(entity);
						}
						await _context.SaveChangesAsync(cancellationToken);
						_logger?.LogInformation($"Seeding database with {seedData.Count()} entities.");
					}
					await SeedDataIfNeededAsync(cancellationToken);
				}
				else if (_context.Database.HasPendingModelChanges())
				{
					_logger?.LogWarning("Database has pending model changes. Consider creating a migration.");
				}
			}
		}
		catch (Exception ex)
		{
			_logger?.LogError(ex, "An error occurred while initializing the database");
			throw;
		}
	}

	private async Task SeedDataIfNeededAsync(CancellationToken cancellationToken)
	{
		// Здесь добавляем данные, которые НЕ в HasData
		// Например, данные из конфигурации, API и т.д.
	}

	private bool IsProductionEnvironment()
	{
		return _hostEnvironment.IsProduction();
	}
	public static IEnumerable<object> ExtractHasData(DbContext context)
	{
		var model = context.Model;
		var entitiesData = new List<object>();

		foreach (var entityType in model.GetEntityTypes())
		{
			IEnumerable<IDictionary<string, object?>> seedData = entityType.GetSeedData();
			foreach (var data in seedData)
			{
				object? entity = entityType.ClrType.GetConstructor(Type.EmptyTypes)?.Invoke(null);
				foreach (var property in data.Keys)
				{
					object? value = data[property];
					PropertyInfo? clrProperty = entityType.ClrType.GetProperty(property);
					clrProperty?.SetValue(entity, value);
				}
				if (entity is not null)
				{
					entitiesData.Add(entity);
				}
			}
		}

		return entitiesData;
	}
}
