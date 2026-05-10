# Анализ архитектуры Re-ception

## 1. Архитектурный паттерн

**Clean Architecture (Чистая архитектура)**
Приложение следует классическому паттерну Clean Architecture с четырьмя основными слоями:

- **Domain** - ядро бизнес-логики (не зависит от других слоев)
- **Application** - UseCases и бизнес-процессы
- **Infrastructure** - реализация технических деталей (БД, репозитории)
- **Presentation** - клиентские приложения (Web API)

Направление зависимостей: **Presentation → Application → Domain** ← **Infrastructure** (к Domain)

**Философия:** Бизнес-логика полностью изолирована от технических деталей; легко тестируется и переносима между фреймворками.

---

## 2. Слои приложения

### 2.1 Domain Layer (Домен)

**Назначение:** Содержит всю бизнес-логику, не зависит от других слоев

**Структура:**

```txt
Domain/
├── Abstract/              # Абстрактные базовые классы для типизирования
├── Entity/               # Доменные сущности
├── Exception/            # Доменные исключения
├── Interfaces/           # Контракты (репозитории, сервисы)
└── Service/             # Доменные сервисы
```

**Ключевые компоненты:**

#### Abstract классы (паттерн: Type-Safe Enum)

1. **EnumObjectAbstract\<T>** - Базовый enum-подобный объект
   - Использует рефлексию для автоматического сбора всех статических полей
   - Каждому объекту автоматически присваивается уникальный ID
   - Поддерживает сравнение по ID и поиск по ID
   - Пример использования: `UserGender` (Indeterminate, Female, Male)

2. **StatusObjectAbstract\<T>** - Enum с именем (расширение EnumObjectAbstract)
   - Добавляет свойство `Name`
   - Позволяет поиск по названию
   - Примеры: `RoomStatus`, `ReservationStatus`, `PaymentStatus`, `PaymentMethod`

3. **StatusWithParentsObjectsAbstract\<T>** - Enum со статусом наследования
   - Добавляет свойство `Parents` - список возможных родительских статусов
   - Переопределяет `Equals` для проверки наследования (может быть равен родителю)
   - Используется для иерархических статусов

#### Entity классы (доменные сущности)

**Hotel** (отель)

- ID: byte (макс 255 отелей)
- Поля: Name, Description, Email (Value Object), Phone (Value Object)
- Координаты: Latitude (-90...90), Longitude (-180...180) с валидацией
- Методы обновления: UpdateEmail, UpdatePhone, UpdateName и т.д.
- Отношения: Rooms, Employees, HotelTags (many-to-many)

**Room** (комната)

- ID: ushort
- Связь: HotelId, RoomTypeId, RoomStatusId
- Поля: RoomNumber, Floor, PricePerDay
- Отношения: RoomTags (many-to-many), Reservations

**Reservation** (бронирование)

- ID: ulong
- Связь: GuestId, RoomId, ReservationStatusId
- Поля: CheckIn, CheckOut, TotalPrice, Discount, CreateAt
- Методы: UpdateCheckIn, UpdateCheckOut, UpdateTotalPrice (async)
- Использует: `ICalculatorReservationPrice` для расчета цены

**Payment** (платеж)

- ID: uint
- Связь: ReservationId (может быть null), PaymentStatusId, PaymentMethodId
- Поля: Amount, PaymentDate
- Отношения: Reservation, PaymentStatus, PaymentMethod

**User** (пользователь)

- ID: Guid
- Поля: UserName, DateOfBirth, CreateAt, GenderId
- Отношения: Employee или Guest, UserRoles, UserPermissions
- Использует: `IClock` для временных меток

#### Value Objects (объекты-значения)

Immutable объекты с валидацией в свойстве:

1. **Email**
   - Regex валидация: `^[^@\s]+@[^@\s]+\.[^@\s]+$`
   - Свойство: Value, IsVerified
   - Методы: Verified(), Unverified()

2. **Phone**
   - Regex валидация: `^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$`
   - Свойство: Value, IsVerified
   - Поддерживает российские номера

#### Interfaces (контракты)

```txt
Domain/Interfaces/
├── Repositories/        # Репозиториевые интерфейсы
│   ├── BaseRepository/  # Базовые CRUD интерфейсы
│   ├── HotelRepository/
│   ├── RoomRepository/
│   ├── ReservationRepository/
│   └── PaymentRepository/
├── IClock              # Получение текущего времени
└── ICalculatorReservationPrice  # Расчет цены бронирования
```

#### Exception классы

**Иерархия:**

```txt
DomainException (base)
├── DomainExternalException  # Ошибки входных данных (валидация)
└── DomainInnerException     # Внутренние ошибки (логика)
```

---

### 2.2 Application Layer (Приложение)

**Назначение:** Бизнес-процессы (UsesCases), не содержит UI или БД логику

**Структура:**

```txt
Application/
├── DTOs/               # Data Transfer Objects
├── Interfaces/         # Контракты (IUnitOfWork, ICurrentUser, IAction, IQuestion)
├── UseCases/          # Бизнес-процессы
│   ├── HotelUseCases/
│   ├── RoomUseCases/
│   ├── ReservationUseCases/
│   └── ...
└── Directory.Build.props
```

#### IUnitOfWork - Управление транзакциями

```csharp
public interface IUnitOfWork
{
    Task BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);
    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
}
```

Используется для управления ACID транзакциями в БД.

#### ICurrentUser - Контекст текущего пользователя

```csharp
public interface ICurrentUser
{
    string? Id { get; }
    bool IsAuthenticated { get; }
}
```

Предоставляет информацию о текущем аутентифицированном пользователе.

#### IAction<TInput, TOutput> - Команда с результатом

```csharp
public interface IAction<TInput, TOutput> : IAction
{
    Task<TOutput> Execute(TInput input);
}
```

**Варианты:**

- `IAction<TInput>` - команда без результата
- `IAction<TInput, TOutput>` - команда с результатом

Каждый UseCase реализует этот интерфейс.

#### IQuestion<TOutput, TInput> - Запрос данных

```csharp
public interface IQuestion<TOutput, TInput> : IQuestion
{
    Task<TOutput> Ask(TInput input);
}
```

**Варианты:**

- `IQuestion<TOutput>` - запрос без параметров
- `IQuestion<TOutput, TInput>` - запрос с параметром

#### UseCase пример

```csharp
public class CreateHotelUseCase(
    IHotelRepository hotelRepository,
    IUnitOfWork unitOfWork) : IAction<HotelDTOs.Create, Hotel>
{
    public async Task<Hotel> Execute(HotelDTOs.Create input)
    {
        var hotel = input.GetHotel();
        await _hotelRepository.AddAsync(hotel);
        await _unitOfWork.SaveChangesAsync();
        return hotel;
    }
}
```

#### DTOs (Data Transfer Objects)

Используются для обмена данными между слоями:

- **Create** DTO - содержит поля для создания
- **Update** DTO - содержит опциональные поля для обновления
- **Delete** DTO - ID сущности для удаления

DTOs содержат методы преобразования в Domain объекты:

```csharp
public record Create(string Name, string Email, string Phone, ...)
{
    public Hotel GetHotel() => new(
        name: Name,
        email: new(Email),
        phone: new(Phone),
        ...);
}
```

---

### 2.3 Infrastructure Layer (Инфраструктура)

**Назначение:** Реализация технических деталей (БД, файловое хранилище)

**Структура:**

```txt
Infrastructure/
├── Clock.cs                    # Реализация IClock
├── Database/
│   ├── ProgramContext.cs       # DbContext Entity Framework
│   ├── ProgramContextDbSets.cs # DbSets для каждой сущности
│   ├── DatabaseInitialization.cs
│   ├── Configs/               # Entity Type Configurations
│   ├── Converter/             # Конвертеры для Value Objects
│   └── Interfaces/
├── EfRepository/              # Repository реализации через EF
│   ├── EfUnitOfWork.cs
│   ├── HotelRepository/
│   ├── RoomRepository/
│   ├── ReservationRepository/
│   └── PaymentRepository/
└── MinioRepository/           # Repository для S3 (Minio) хранилища
    ├── MinioRepository.cs
    ├── MinioHotelImageRepository.cs
    └── MinioRoomImageRepository.cs
```

#### Database Configuration

**ProgramContext** - DbContext для Entity Framework

- Использует Fluent API конфигурацию через `IEntityTypeConfiguration<T>`
- Применяет глобальные конвертеры через `FactoryConverter.UseConverter()`
- Поддерживает SQLite и PostgreSQL

**Entity Type Configurations** - Fluent API конфигурации

```csharp
public class HotelConf : IEntityTypeConfiguration<Hotel>
{
    public void Configure(EntityTypeBuilder<Hotel> builder)
    {
        builder.ToTable("hotels");
        builder.HasKey(h => h.Id);
        builder.Property(h => h.Name)
            .HasColumnName("name")
            .HasMaxLength(100)
            .IsRequired();
        // Индексы, foreign keys, т.д.
    }
}
```

#### Repository Pattern

**Иерархия интерфейсов:**

```txt
IBaseCrudRepository<TValue, TValueId>
├── IBaseCreateRepository<TValue>
├── IBaseReadRepository<TValue, TValueId>
├── IBaseUpdateRepository<TValue>
└── IBaseDeleteRepository<TValueId>

IBaseStatusRepository<T> extends IBaseEnumObjectAbstract<T>
  └── Для работы со StatusObjectAbstract сущностями
      └── Добавляет GetByNameAsync()
```

**Пример реализации:**

```csharp
public class EfHotelRepository(ProgramContext context) : IHotelRepository
{
    public async Task AddAsync(Hotel entity) 
        => await _context.Hotels.AddAsync(entity);
    
    public async Task UpdateAsync(Hotel entity) 
        => _context.Hotels.Update(entity);
    
    public async Task DeleteAsync(int id) 
        => await _context.Hotels.Where(h => h.Id == id).ExecuteDeleteAsync();
    
    public async Task<Hotel?> GetByIdAsync(int id) 
        => await _context.Hotels.FirstOrDefaultAsync(h => h.Id == id);
    
    public IQueryable<Hotel> GetQueryable() 
        => _context.Hotels.AsQueryable();
}
```

#### EfUnitOfWork - Управление транзакциями

```csharp
public class EfUnitOfWork : IUnitOfWork
{
    private readonly ProgramContext _context;
    private IDbContextTransaction? _currentTransaction;

    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_currentTransaction != null)
            throw new InvalidOperationException("Transaction already started");
        _currentTransaction = await _context.Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw new InvalidOperationException("Concurrency conflict", ex);
        }
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_currentTransaction == null)
            throw new InvalidOperationException("No active transaction");
        await _currentTransaction.CommitAsync(cancellationToken);
        _currentTransaction = null;
    }
}
```

#### Minio Repository - Работа с S3 хранилищем

**IS3Repository** интерфейс:

```csharp
public interface IS3Repository
{
    Task UploadAsync(Stream fileStream, string fileName, string bucket);
    Task<Stream> DownloadAsync(string fileName, string bucket);
    Task DeleteAsync(string fileName, string bucket);
}
```

**Реализация через Minio:**

- `MinioHotelImageRepository` - изображения отелей
- `MinioRoomImageRepository` - изображения комнат
- Использует Minio.NET клиент для работы с S3-совместимым хранилищем

#### Clock - Получение текущего времени

```csharp
public class Clock : IClock
{
    DateTimeOffset IClock.Now => Now();
    public static DateTimeOffset Now() => DateTimeOffset.Now;
}
```

Позволяет подменять время в тестах через DI.

---

### 2.4 Presentation Layer (Представление)

**Назначение:** API слой для клиентов

**Структура:**

```txt
Presentations/
├── CustomerWeb/  # API для клиентов-гостей
├── EmployeeWeb/  # API для сотрудников
└── LibWeb/       # Общие библиотеки/компоненты
```

Каждое приложение использует UseCase'ы через Dependency Injection.

---

## 3. Проектные паттерны

### 3.1 Repository Pattern

- **Использование:** Абстрактный доступ к данным через интерфейсы
- **Реализация:** EfRepository (Entity Framework), MinioRepository (S3)
- **Преимущество:** Легко подменять реализацию, тестировать

### 3.2 Unit of Work Pattern

- **Использование:** Управление транзакциями
- **Интерфейс:** `IUnitOfWork`
- **Реализация:** `EfUnitOfWork` на основе DbContextTransaction
- **Методы:** BeginTransaction, SaveChanges, CommitTransaction, RollbackTransaction

### 3.3 DTO Pattern (Data Transfer Objects)

- **Использование:** Обмен данными между слоями
- **Реализация:** Record типы в C#
- **Содержит методы:** Преобразование в Domain объекты (`GetHotel()`, `GetRoom()` и т.д.)

### 3.4 Type-Safe Enum Pattern

- **Использование:** Enum-подобные объекты с типизацией
- **Реализация:** `EnumObjectAbstract<T>`, `StatusObjectAbstract<T>`
- **Преимущество:** Type-safe, легко расширяемо, хранится в БД

### 3.5 Value Object Pattern

- **Использование:** Immutable объекты с встроенной валидацией
- **Примеры:** `Email`, `Phone`
- **Особенность:** Валидация в свойствах через init accessors

### 3.6 Dependency Injection

- **Использование:** Внедрение зависимостей через конструкторы
- **Примеры:**

  ```csharp
  public CreateHotelUseCase(
      IHotelRepository hotelRepository,
      IUnitOfWork unitOfWork) { }
  
  public class Hotel
  {
      public Hotel(
          ICalculatorReservationPrice calculator,
          IClock clock) { }
  }
  ```

### 3.7 CQRS-подобный паттерн

- **Commands (Actions):** IAction для операций записи (Create, Update, Delete)
- **Queries (Questions):** IQuestion для операций чтения
- **Разделение:** Четкое разделение между читающими и пишущими операциями

### 3.8 Strategy Pattern

- **Использование:** `ICalculatorReservationPrice` - различные стратегии расчета цены
- **Реализация:** Внедряется в Entity для использования в методах

### 3.9 Factory Pattern

- **Использование:** `FactoryConverter` - создание конвертеров для Value Objects
- **Использование:** DTOs содержат factory методы (`GetHotel()`)

### 3.10 Entity Type Configuration Pattern

- **Использование:** Fluent API конфигурация через `IEntityTypeConfiguration<T>`
- **Преимущество:** Конфигурация отделена от DbContext, DDD-соответствие

---

## 4. Технологии и зависимости

### 4.1 Framework & Runtime

- **.NET 10.0** - целевой фреймворк
- **C# 13.0** - современный C# с nullable reference types, records
- **Implicit Usings** - автоматическое подключение глобальных using'ов

### 4.2 Data Access

- **Entity Framework Core 10.0.5** - ORM, управление БД
  - `Microsoft.EntityFrameworkCore.Design`
  - `Microsoft.EntityFrameworkCore.Tools`
  - `Microsoft.EntityFrameworkCore.Sqlite` - поддержка SQLite
  - `Npgsql.EntityFrameworkCore.PostgreSQL` - поддержка PostgreSQL
- **Database Migrations** - через EF Core Tools

### 4.3 Object Mapping

- **Mapster 10.0.6** - быстрый mapper для преобразования объектов
  - `Mapster.DependencyInjection` - интеграция с DI контейнером

### 4.4 Authentication & Security

- **Microsoft.AspNetCore.Identity.EntityFrameworkCore 10.0.5** - управление пользователями
- **BCrypt.Net-Next 4.1.0** - хеширование паролей

### 4.5 File Storage

- **Minio 7.0.0** - S3-совместимый клиент для работы с объектным хранилищем
- **MinioRepository** - обертка над Minio для управления изображениями

### 4.6 Code Analysis & Quality

- **Roslynator.Analyzers 4.15.0** - анализатор кода на основе Roslyn
- **AnalysisLevel: latest** - проверка с самыми свежими анализаторами

### 4.7 Target Environment

- **RuntimeIdentifier: linux-x64** - целевая платформа Linux
- **InvariantGlobalization: false** - поддержка локализации

---

## 5. Особенности реализации

### 5.1 Value Objects (Email, Phone)

Immutable объекты с встроенной валидацией:

```csharp
public sealed partial class Email
{
    public string Value
    {
        get;
        init
        {
            // Валидация прямо в init accessor
            ArgumentException.ThrowIfNullOrWhiteSpace(value);
            if (!RegexEmail().IsMatch(value))
                throw new DomainExternalException("Invalid email format");
            field = value;
        }
    }
}
```

**Преимущества:**

- Валидация на уровне конструктора
- Невозможно создать невалидный объект
- Type-safe по сравнению со строками
- Поддержка `IsVerified` флага

### 5.2 Type-Safe Enum Hierarchy

**EnumObjectAbstract:**

- Автоматический сбор всех static полей через рефлексию
- Автоматическое присваивание ID
- Сравнение по ID
- Поиск по ID: `FromId(byte id)`

**StatusObjectAbstract:**

- Расширяет EnumObjectAbstract с добавлением Name
- Поиск по названию: `FromName(string name)`
- Примеры: RoomStatus, ReservationStatus, PaymentStatus

**StatusWithParentsObjectsAbstract:**

- Иерархические статусы с возможностью наследования
- Переопределенный Equals проверяет также родительские статусы
- Поддержка: `Parents` - список возможных родителей
- Для иерархических бизнес-правил

### 5.3 Асинхронность везде

```csharp
public async Task Execute(HotelDTOs.Create input)
{
    var hotel = input.GetHotel();
    await _hotelRepository.AddAsync(hotel);
    await _unitOfWork.SaveChangesAsync();
    return hotel;
}
```

Все операции ввода-вывода асинхронны с поддержкой CancellationToken.

### 5.4 CancellationToken всюду

```csharp
Task BeginTransactionAsync(CancellationToken cancellationToken = default);
Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
```

Позволяет корректно отменять долгие операции.

### 5.5 Immutable Entity Properties

```csharp
public int Id { get; private set; }
public string Name { get; private set; }
```

Использование `private set` для защиты от случайных изменений. Изменения только через методы.

### 5.6 Domain Service Injection

Сущности могут иметь инъецированные сервисы через конструктор:

```csharp
public class Reservation
{
    public Reservation(
        ICalculatorReservationPrice calculator,
        IClock clock) { }
    
    public async Task UpdateTotalPrice()
    {
        TotalPrice = await _calculator.Calculator(this);
    }
}
```

### 5.7 Queryable Pattern

Repositories возвращают `IQueryable<T>` для поддержки LINQ-запросов:

```csharp
public IQueryable<Hotel> GetQueryable() => _context.Hotels.AsQueryable();
```

Позволяет Application слою писать сложные LINQ запросы без знания БД.

### 5.8 Конвертеры Value Objects в БД

Через `FactoryConverter` Value Objects (Email, Phone) преобразуются в строки при сохранении в БД:

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    var entityTypes = modelBuilder.Model.GetEntityTypes();
    FactoryConverter.UseConverter(entityTypes);
}
```

### 5.9 Multi-Database Support

Поддерживает как SQLite (для разработки), так и PostgreSQL (для production):

- Конфигурация в `Directory.Build.props`
- Выбор БД через конфигурацию

### 5.10 Index Strategy

Entity Type Configurations содержат оптимизированные индексы:

```csharp
builder.HasIndex(h => new { h.Latitude, h.Longitude })
    .HasDatabaseName("idx_hotels_lati_long");
builder.HasIndex(h => h.Email)
    .IsUnique();
```

---

## 6. Диаграмма зависимостей слоев

```txt
┌─────────────────────────────────────────────┐
│          Presentation Layer                 │
│  (CustomerWeb, EmployeeWeb, LibWeb)         │
└────────────────────┬────────────────────────┘
                     │ использует
                     ▼
┌─────────────────────────────────────────────┐
│        Application Layer                    │
│  (UseCases, DTOs, Interfaces)               │
└────────────────────┬────────────────────────┘
                     │ использует
                     ▼
┌─────────────────────────────────────────────┐
│           Domain Layer                      │
│  (Entities, ValueObjects, Services,         │
│   Abstract, Interfaces, Exceptions)         │
└─────────────────────────────────────────────┘
         ▲                           ▲
         │                           │
      использует               использует
         │                           │
         │                           │
┌─────────────────────┐  ┌──────────────────┐
│ Infrastructure      │  │  (EF Core)       │
│ (Repositories,      │──│  (DbContext)     │
│  UnitOfWork,        │  │  (Database)      │
│  Clock,             │  └──────────────────┘
│  MinioRepository)   │
└─────────────────────┘
```

**Направление зависимостей:** Внешние слои зависят от внутренних, но не наоборот.

---

## 7. Ключевые интерфейсы и их роли

| Интерфейс | Слой | Назначение |
| ----------- | ----------------- |
| `IUnitOfWork` | Application | Управление транзакциями БД |
| `ICurrentUser` | Application | Контекст текущего пользователя |
| `IAction<TIn, TOut>` | Application | Команда с результатом (Write) |
| `IQuestion<TOut, TIn>` | Application | Запрос данных (Read) |
| `IClock` | Domain | Получение текущего времени |
| `ICalculatorReservationPrice` | Domain | Расчет цены бронирования |
| `IHotelRepository` | Domain | CRUD операции для Hotel |
| `IBaseCrudRepository<T, TId>` | Domain | Базовые CRUD операции |
| `IBaseStatusRepository<T>` | Domain | Операции со Status объектами |
| `IS3Repository` | Infrastructure | Работа с S3 хранилищем |
| `IEntityTypeConfiguration<T>` | Infrastructure | Конфигурация Entity в БД |

---

## 8. Примеры реализации паттернов

### 8.1 Create UseCase с DTOs

```csharp
// UseCase
public class CreateHotelUseCase(
    IHotelRepository hotelRepository,
    IUnitOfWork unitOfWork) : IAction<HotelDTOs.Create, Hotel>
{
    public async Task<Hotel> Execute(HotelDTOs.Create input)
    {
        var hotel = input.GetHotel();  // DTO → Domain Entity
        await _hotelRepository.AddAsync(hotel);
        await _unitOfWork.SaveChangesAsync();
        return hotel;
    }
}

// DTO с factory методом
public record Create(
    string Name,
    string Email,
    string Phone,
    double Latitude,
    double Longitude,
    string? Description = null
)
{
    public Hotel GetHotel() => new(
        name: Name,
        email: new(Email),
        phone: new(Phone),
        latitude: Latitude,
        longitude: Longitude,
        description: Description);
}
```

### 8.2 Repository с IQueryable

```csharp
public class EfHotelRepository(ProgramContext context) : IHotelRepository
{
    public async Task<Hotel?> GetByIdAsync(int id) 
        => await _context.Hotels.FirstOrDefaultAsync(h => h.Id == id);
    
    public IQueryable<Hotel> GetQueryable() 
        => _context.Hotels.AsQueryable();  // Позволяет строить сложные LINQ запросы
}

// Использование в Application
var query = hotelRepository.GetQueryable()
    .Where(h => h.Latitude > 50 && h.Longitude < 40)
    .OrderBy(h => h.Name);
```

### 8.3 Type-Safe Enum

```csharp
// Domain
public class RoomStatus : StatusObjectAbstract<RoomStatus>
{
    public static readonly RoomStatus Vacant = new("Vacant");
    public static readonly RoomStatus CheckOut = new("CheckOut");
    public static readonly RoomStatus OutOfOrder = new("OutOfOrder");
    public static readonly RoomStatus Occupied = new("Occupied");
    public static readonly RoomStatus Reserved = new("Reserved");

    protected RoomStatus(string name) : base(name) { }
}

// Использование
var vacant = RoomStatus.Vacant;
var byId = RoomStatus.FromId(0);
var byName = RoomStatus.FromName("Vacant");
if (status == RoomStatus.Occupied) { /* ... */ }
```

### 8.4 Value Object с валидацией

```csharp
// Создание с валидацией
var email = new Email("test@example.com");  // ✓ Валидно
var email = new Email("invalid");            // ✗ Выбросит DomainExternalException

// Использование
Console.WriteLine(email.Value);  // "test@example.com"
var verified = email.Verified();  // Флаг IsVerified = true
```

### 8.5 UnitOfWork с транзакциями

```csharp
var unitOfWork = /* from DI */;

try
{
    await unitOfWork.BeginTransactionAsync();
    
    // Множество операций
    await hotelRepository.AddAsync(hotel1);
    await hotelRepository.AddAsync(hotel2);
    
    await unitOfWork.SaveChangesAsync();
    await unitOfWork.CommitTransactionAsync();
}
catch (Exception)
{
    await unitOfWork.RollbackTransactionAsync();
    throw;
}
```

---

## 9. Сводная таблица компонентов

| Компонент | Паттерн | Слой | Назначение |
| ----------- | -------- | ------ | ----------- |
| Entity (Hotel, Room, ...) | DDD Entity | Domain | Доменная сущность с бизнес-логикой |
| ValueObject (Email, Phone) | Value Object | Domain | Immutable с встроенной валидацией |
| EnumObjectAbstract | Type-Safe Enum | Domain | Type-safe enum-подобные объекты |
| Repository | Repository | Domain/Inf | Абстрактный доступ к данным |
| UnitOfWork | Unit of Work | Application | Управление транзакциями |
| UseCase (IAction) | Command | Application | Команда для записи данных |
| Query (IQuestion) | Query | Application | Запрос для чтения данных |
| DTO | Transfer Object | Application | Обмен данными между слоями |
| DbContext | ORM | Infrastructure | Управление БД через EF Core |
| Configuration | Fluent API | Infrastructure | Конфигурация Entity в БД |
| Service (Calculator, Clock) | Strategy | Domain | Бизнес-сервис |

---

## 10. Выводы

**Re-ception** - это **Clean Architecture приложение** с:

✓ **Четкой слоистостью:** Domain → Application → Infrastructure ← Presentation  
✓ **Полной изоляцией бизнеса:** Domain не зависит от технологий  
✓ **Type-safe enum реализацией:** Вместо строк используются типизированные объекты  
✓ **Immutable Value Objects:** Email, Phone с встроенной валидацией  
✓ **CQRS-подобным разделением:** IAction (Write) vs IQuestion (Read)  
✓ **Repository Pattern:** Абстрактный доступ к данным  
✓ **Асинхронностью везде:** Все операции async/await  
✓ **Современным C# 13:** Records, nullable, implicit usings  
✓ **Поддержкой нескольких БД:** SQLite + PostgreSQL  
✓ **S3 хранилищем:** Через Minio для изображений  

Архитектура позволяет **легко тестировать**, **переносить между платформами**, и **масштабировать** без изменения бизнес-логики.
