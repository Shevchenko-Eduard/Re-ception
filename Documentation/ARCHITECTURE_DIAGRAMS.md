# Re-ception: Визуальные диаграммы архитектуры

## 1. Архитектура в целом

```txt
┌───────────────────────────────────────────────────────────────────────┐
│                          PRESENTATION LAYER                           │
│                                                                       │
│  ┌──────────────────┐  ┌──────────────────┐  ┌──────────────────┐     │
│  │  CustomerWeb API │  │  EmployeeWeb API │  │    LibWeb        │     │
│  │                  │  │                  │  │  (Shared Libs)   │     │
│  └────────┬─────────┘  └────────┬─────────┘  └────────┬─────────┘     │
│           │                     │                    │                │
└───────────┼─────────────────────┼────────────────────┼────────────────┘
            │                     │                    │
            │  Dependency Injection (DI Container)     │
            │                     │                    │
┌───────────▼─────────────────────▼────────────────────▼────────────────┐
│                      APPLICATION LAYER                                │
│                                                                       │
│  ┌────────────────────────────────────────────────────────────────┐   │
│  │                       USE CASES (Business Logic)               │   │
│  │                                                                │   │
│  │  ┌──────────────┐ ┌──────────────┐ ┌──────────────┐            │   │
│  │  │HotelUseCases │ │RoomUseCases  │ │ReservationUs │ ...        │   │
│  │  │              │ │              │ │  eCases      │            │   │
│  │  │Create        │ │Create        │ │Create        │            │   │
│  │  │Update        │ │Update        │ │Update        │            │   │
│  │  │Delete        │ │Delete        │ │Delete        │            │   │
│  │  └──────────────┘ └──────────────┘ └──────────────┘            │   │
│  └────────────────────────────────────────────────────────────────┘   │
│                                                                       │
│  ┌────────────────────┐  ┌─────────────────────┐  ┌──────────────┐    │
│  │   DTOs             │  │   Interfaces        │  │              │    │
│  │                    │  │                     │  │              │    │
│  │ - Create<T>        │  │ - IUnitOfWork       │  │ - IAction    │    │
│  │ - Update<T>        │  │ - ICurrentUser      │  │ - IQuestion  │    │
│  │ - Delete<T>        │  │ - IRepository       │  │ - IClock     │    │
│  │                    │  │   (Abstractions)    │  │              │    │
│  └────────────────────┘  └─────────────────────┘  └──────────────┘    │
└───────────────────────────────────────────────────────────────────────┘
                                  ▲
                                  │
                    Dependency Injection
                                  │
┌─────────────────────────────────┼─────────────────────────────────────┐
│                      DOMAIN LAYER                                     │
│                   (Business Rules & Logic)                            │
│                                                                       │
│  ┌──────────────────────────────────────────────────────────────┐     │
│  │                      ENTITIES                                │     │
│  │  Hotel  │ Room  │ Reservation  │ Payment  │ User  │ etc.     │     │
│  │ (with business methods and validations)                      │     │
│  └──────────────────────────────────────────────────────────────┘     │
│                                                                       │
│  ┌──────────────────────┐  ┌──────────────────────────────────┐       │
│  │   VALUE OBJECTS      │  │    ABSTRACT BASE CLASSES         │       │
│  │                      │  │                                  │       │
│  │ Email (validated)    │  │ EnumObjectAbstract<T>            │       │
│  │ Phone (validated)    │  │   ↓                              │       │
│  │                      │  │ StatusObjectAbstract<T>          │       │
│  │ (Immutable)          │  │   ↓                              │       │
│  │                      │  │ StatusWithParentsObjectAbstract<T>       │
│  └──────────────────────┘  └──────────────────────────────────┘       │
│                                                                       │
│  ┌──────────────────┐  ┌──────────────────┐  ┌───────────────────┐    │
│  │    INTERFACES    │  │   SERVICES       │  │  EXCEPTIONS       │    │
│  │                  │  │                  │  │                   │    │
│  │ IRepository      │  │Calculator<T>     │  │ DomainException   │    │
│  │ IClock           │  │Clock             │  │  ├─External       │    │
│  │ ICalculator      │  │                  │  │  └─Inner          │    │
│  │                  │  │                  │  │                   │    │
│  └──────────────────┘  └──────────────────┘  └───────────────────┘    │
│                                                                       │
│                   (No Dependencies to Other Layers)                   │
└───────────────────────────────────────────────────────────────────────┘
         ▲                                             ▲
         │                                             │
         │         IMPLEMENTS & EXTENDS                │
         │                                             │
┌────────┴──────────────────────┐         ┌────────────┴──────────────┐
│    INFRASTRUCTURE LAYER       │         │  INFRASTRUCTURE LAYER     │
│   (Data Access Patterns)      │         │  (File Storage)           │
│                               │         │                           │
│  ┌─────────────────────────┐  │         │  ┌──────────────────────┐ │
│  │    Repositories         │  │         │  │  S3 Storage (Minio)  │ │
│  │                         │  │         │  │                      │ │
│  │ EfHotelRepository       │  │         │  │ IS3Repository        │ │
│  │ EfRoomRepository        │  │         │  │ MinioRepository      │ │
│  │ ... (implements IRepo)  │  │         │  │ MinioHotelImage      │ │
│  │                         │  │         │  │ MinioRoomImage       │ │
│  └────────────┬────────────┘  │         │  └──────────────────────┘ │
│               │               │         │                           │
│  ┌────────────▼────────────┐  │         └───────────────────────────┘
│  │    EfUnitOfWork         │  │
│  │   (IUnitOfWork impl)    │  │
│  │                         │  │
│  │ - BeginTransaction      │  │
│  │ - SaveChanges           │  │
│  │ - Commit/Rollback       │  │
│  └─────────────────────────┘  │
│                               │
│  ┌─────────────────────────┐  │
│  │   Entity Framework      │  │
│  │   DbContext             │  │
│  │   Configurations        │  │
│  │   Entity Type Config    │  │
│  │   Fluent API            │  │
│  └────────────┬────────────┘  │
│               │               │
└───────────────┼───────────────┘
                │
     ┌──────────▼──────────┐
     │   DATABASE          │
     │                     │
     │ PostgreSQL / SQLite │
     │ (Configurable)      │
     │                     │
     └─────────────────────┘
```

## 2. UseCase (IAction) Flow

```txt
HTTP REQUEST
    │
    ▼
┌─────────────────────────┐
│  Presentation Layer     │
│  (API Controller)       │
└────────────┬────────────┘
             │
             │ Deserialize Request
             │ + Validate Input
             ▼
┌─────────────────────────────────────┐
│  Application Layer - UseCase        │
│  (IAction<Input, Output>)           │
│                                     │
│  1. Create/Modify Entity from DTO   │
│  2. Call Repository methods         │
│  3. Call Domain Services            │
│  4. Call UnitOfWork.SaveChanges()   │
└────────────┬────────────────────────┘
             │
    ┌────────┴──────────────────────┐
    │                               │
    ▼                               ▼
┌──────────────────────┐   ┌────────────────────┐
│ Repository           │   │ UnitOfWork         │
│                      │   │                    │
│ AddAsync(entity)     │   │ SaveChangesAsync() │
│ UpdateAsync(entity)  │   │ CommitTransaction()│
│ DeleteAsync(id)      │   │                    │
└──────┬───────────────┘   └────────┬───────────┘
       │                            │
       └────────────────┬───────────┘
                        │
                        ▼
            ┌───────────────────────┐
            │ DbContext             │
            │ (Entity Framework)    │
            │                       │
            │ SaveChangesAsync()    │
            └───────────┬───────────┘
                        │
                        ▼
            ┌───────────────────────┐
            │ Database              │
            │ (PostgreSQL/SQLite)   │
            │                       │
            │ INSERT/UPDATE/DELETE  │
            └───────────────────────┘

        ◄─── Serialize Result ◄───
        │
        ▼
    HTTP RESPONSE
```

## 3. Entity TypeSafe Enum Hierarchy

```txt
┌────────────────────────────────────────┐
│    EnumObjectAbstract<T>               │
│                                        │
│  Properties:                           │
│  - Id: byte                            │
│  - All: ReadOnlyCollection<T>          │
│                                        │
│  Methods:                              │
│  - FromId(byte id): T                  │
│  - Equals(object): bool                │
│  - GetHashCode(): int                  │
└────────────┬───────────────────────────┘
             │ extends
             ▼
┌────────────────────────────────────────┐
│    StatusObjectAbstract<T>             │
│                                        │
│  Properties:                           │
│  - Name: string                        │
│                                        │
│  Methods:                              │
│  - FromName(string name): T            │
│  - Equals(StatusObjectAbstract<T>)     │
└────────────┬───────────────────────────┘
             │ extends
             ▼
┌────────────────────────────────────────┐
│  StatusWithParentsObjectsAbstract<T>   │
│                                        │
│  Properties:                           │
│  - Parents: IEnumerable<T>             │
│                                        │
│  Methods:                              │
│  - Equals (checks parent chain)        │
│  - operator == / !=                    │
└────────────────────────────────────────┘
             ▲
             │
    ┌────────┴───────────────┬────────────┐
    │                        │            │
    ▼                        ▼            ▼
┌─────────────────┐ ┌──────────────┐ ┌──────────────┐
│  RoomStatus     │ │PaymentStatus │ │ReservStatus  │
│                 │ │              │ │              │
│Vacant           │ │Paid          │ │New           │
│Occupied         │ │NotPaid       │ │Confirmed     │
│CheckOut         │ │Deleted       │ │Guaranteed    │
│OutOfOrder       │ │Terminated    │ │CheckedIn     │
│Reserved         │ │              │ │Rejected      │
│                 │ │              │ │Cancelled     │
└─────────────────┘ └──────────────┘ └──────────────┘
```

**Особенность:** `Equals()` может сравнивать по родителям. Например:

```txt
ReservationStatus.CheckedIn == ReservationStatus.Confirmed  // может быть true
                                              ▲
                                    если Confirmed - parent of CheckedIn
```

## 4. Value Object Pattern

```txt
┌─────────────────────────────────────────┐
│      Value Object (Email, Phone)        │
│                                         │
│  Characteristics:                       │
│  ├─ Immutable (init-only property)      │
│  ├─ Type-safe (not just a string)       │
│  ├─ Self-validating (validation in      │
│  │  property setter)                    │
│  └─ Equality by value (not by ref)      │
│                                         │
│  Example: Email                         │
│  ┌──────────────────────────────────┐   │
│  │ public sealed class Email        │   │
│  │ {                                │   │
│  │   public string Value { get; init│   │
│  │   {                              │   │
│  │     // Validation                │   │
│  │     if (!IsValidEmail(value))    │   │
│  │       throw DomainException();   │   │
│  │     field = value;               │   │
│  │   }}                             │   │
│  │                                  │   │
│  │   public Email(string value)     │   │
│  │   {                              │   │
│  │     Value = value;  // triggers  │   │
│  │                     // validation│   │
│  │   }                              │   │
│  │ }                                │   │
│  └──────────────────────────────────┘   │
│                                         │
│  Usage:                                 │
│  var email = new Email("test@ex.com");  │
│  // email.Value → "test@ex.com"         │
│  // Can't: email.Value = "new";         │
│  //        (no setter)                  │
└─────────────────────────────────────────┘
```

## 5. Repository Pattern Structure

```txt
┌────────────────────────────────────────────────────────────┐
│                  Repository Pattern                        │
└────────────────────────────────────────────────────────────┘

Domain Layer (Interfaces):
┌────────────────────────────────────────────┐
│  IBaseCrudRepository<T, TId>               │
│    extends                                 │
│    ├─ IBaseCreateRepository<T>             │
│    ├─ IBaseReadRepository<T, TId>          │
│    ├─ IBaseUpdateRepository<T>             │
│    └─ IBaseDeleteRepository<TId>           │
│                                            │
│  Methods:                                  │
│  - AddAsync(T entity)                      │
│  - GetByIdAsync(TId id): Task<T?>          │
│  - GetQueryable(): IQueryable<T>           │
│  - UpdateAsync(T entity)                   │
│  - DeleteAsync(TId id)                     │
└────────────────────────────────────────────┘
                ▲
                │ implemented by
                │
Infrastructure Layer (EF Implementations):
┌────────────────────────────────────────────┐
│  EfHotelRepository                         │
│  EfRoomRepository                          │
│  EfReservationRepository                   │
│  EfPaymentRepository                       │
│  ... (and more)                            │
│                                            │
│  Implementation:                           │
│  - Uses ProgramContext (DbContext)         │
│  - Executes LINQ to Entities queries       │
│  - Translates to SQL at runtime            │
└────────────────────────────────────────────┘
                ▲
                │ uses
                │
        ┌───────────────────┐
        │   DbContext       │
        │   (EF Core)       │
        │   ProgramContext  │
        └─────────┬─────────┘
                  │
          ┌───────▼────────┐
          │   Database     │
          │ SQL queries    │
          └────────────────┘
```

## 6. Unit of Work Pattern (Transaction Management)

```txt
┌──────────────────────────────────────────────────────────┐
│          Unit of Work (IUnitOfWork)                      │
│                                                          │
│  Manages:                                                │
│  - Database Transactions                                 │
│  - Change Tracking                                       │
│  - Atomic Operations                                     │
└──────────────────────────────────────────────────────────┘

Lifecycle:

1. BEGIN TRANSACTION
   ┌─────────────────────┐
   │ BeginTransactionAsync()
   │ ↓                   │
   │ IDbContextTransaction created
   └─────────────────────┘

2. EXECUTE OPERATIONS
   ┌──────────────────────────────┐
   │ repository.AddAsync(entity1) │
   │ repository.AddAsync(entity2) │
   │ repository.UpdateAsync(ent3) │
   │ ... multiple operations ...  │
   └──────────────────────────────┘

3. SAVE CHANGES
   ┌──────────────────────────┐
   │ SaveChangesAsync()       │
   │ ↓                        │
   │ Changes buffered in      │
   │ DbContext               │
   └──────────────────────────┘

4. COMMIT or ROLLBACK
   ┌─────────────────────────┐
   │ CommitTransactionAsync()│  ← All saved to DB
   │ or                      │
   │ RollbackTransactionAsync│  ← All rolled back
   └─────────────────────────┘

Error Handling:
┌────────────────────────────────────────┐
│  try {                                 │
│    await unitOfWork.BeginTransaction();│
│    // ... operations ...               │
│    await unitOfWork.SaveChanges();     │
│    await unitOfWork.CommitTransaction();
│  } catch (Exception) {                 │
│    await unitOfWork.RollbackTransaction();
│  }                                     │
└────────────────────────────────────────┘
```

## 7. Dependency Injection Flow

```txt
Application Startup:
┌────────────────────────────────┐
│  Program.cs / Startup.cs       │
│                                │
│  services.AddScoped<T>()       │
│  services.AddSingleton<T>()    │
│  services.AddTransient<T>()    │
└────────────┬───────────────────┘
             │
             ▼
┌────────────────────────────────────────┐
│        DI Container                    │
│                                        │
│  Registered Services:                  │
│  ├─ IUnitOfWork → EfUnitOfWork         │
│  ├─ IHotelRepository → EfHotelRepo     │
│  ├─ ICalculator → Calculator           │
│  ├─ IClock → Clock                     │
│  ├─ ICurrentUser → CurrentUser         │
│  └─ IAction<...> → UseCase             │
└────────────┬───────────────────────────┘
             │
             ▼
┌────────────────────────────────────────┐
│      Request Handling                  │
│                                        │
│  HTTP Request                          │
│    ↓                                   │
│  Resolve Dependencies:                 │
│  ├─ GetService(IAction<T,R>)           │
│  ├─  → Needs IRepository               │
│  ├─    → Needs ProgramContext          │
│  ├─  → Needs IUnitOfWork               │
│  └─    → Needs ProgramContext (same)   │
│    ↓                                   │
│  Create UseCase with all dependencies  │
│    ↓                                   │
│  Execute UseCase                       │
└────────────────────────────────────────┘
```

## 8. DTO Transformation Pipeline

```txt
HTTP Request
    │
    ▼ {"Name":"Hotel","Email":"..."}
┌──────────────────────────────────┐
│  Deserialize to DTO              │
│  HotelDTOs.Create                │
└────────────┬─────────────────────┘
             │
             ▼
┌──────────────────────────────────────┐
│  Validate DTO Fields                 │
│  (DataAnnotations, FluentValidation) │
└────────────┬─────────────────────────┘
             │
             ▼
┌────────────────────────────────────────────┐
│  DTO.GetHotel() - Factory method           │
│                                            │
│  HotelDTOs.Create ──► new Hotel(           │
│  {                    name: input.Name,    │
│    Name: "Hotel",     email: new Email(.), │
│    Email: "...",      phone: new Phone(.)  │
│    Phone: "..."  }    )                    │
│                                            │
└────────────┬───────────────────────────────┘
             │ (Value Objects created here)
             │ (Validation happens in Email/
             │  Phone constructors)
             ▼
┌────────────────────────────────────────┐
│  Domain Entity                         │
│  Hotel                                 │
│  ├─ Id: 0 (new)                        │
│  ├─ Name: "Hotel" (validated string)   │
│  ├─ Email: Email obj (validated)       │
│  ├─ Phone: Phone obj (validated)       │
│  └─ ...properties...                   │
└────────────┬───────────────────────────┘
             │
             ▼
┌──────────────────────────────────┐
│  Pass to Repository.AddAsync()   │
│  Pass to UnitOfWork.SaveChanges()│
└──────────────────────────────────┘
```

## 9. Database Layers (EF Core)

```txt
┌─────────────────────────────────────────────┐
│        Application Code                     │
│    repository.AddAsync(hotel)               │
└────────────┬────────────────────────────────┘
             │
             ▼
┌─────────────────────────────────────────────┐
│      DbContext (ProgramContext)             │
│                                             │
│  public DbSet<Hotel> Hotels { get; set; }   │
│  public DbSet<Room> Rooms { get; set; }     │
│  ...                                        │
│                                             │
│  OnModelCreating:                           │
│  - ApplyConfigurationsFromAssembly()        │
│  - FactoryConverter.UseConverter()          │
│  - Create Fluent API mappings               │
└────────────┬────────────────────────────────┘
             │
             ▼
┌─────────────────────────────────────────────┐
│  Entity Type Configurations                 │
│                                             │
│  HotelConf:                                 │
│  ├─ Table name: "hotels"                    │
│  ├─ Primary Key: Id                         │
│  ├─ Properties mapping                      │
│  ├─ Foreign Keys                            │
│  ├─ Indexes                                 │
│  └─ Value Object converters                 │
└────────────┬────────────────────────────────┘
             │
             ▼
┌─────────────────────────────────────────────┐
│      Linq to Entities / Query Translation   │
│                                             │
│  C# LINQ Query ──► SQL Query                │
│                                             │
│  dbContext.Hotels                           │
│    .Where(h => h.Name == "Hotel")           │
│    .FirstOrDefault()                        │
│        ↓                                    │
│  SELECT * FROM hotels WHERE name = 'Hotel'  │
│  LIMIT 1                                    │
└────────────┬────────────────────────────────┘
             │
             ▼
┌─────────────────────────────────────────────┐
│      Database Provider                      │
│  ├─ PostgreSQL (via Npgsql)                 │
│  ├─ SQLite (via Microsoft.EF.SQLite)        │
│  └─ Others                                  │
└────────────┬────────────────────────────────┘
             │
             ▼
┌─────────────────────────────────────────────┐
│         Database (Physical Storage)         │
│         PostgreSQL / SQLite                 │
│                                             │
│  Table: hotels                              │
│  ┌──────┬──────┬────────┬────────┐          │
│  │ id   │ name │ email  │ phone  │ ...      │
│  ├──────┼──────┼────────┼────────┼─────┐    │
│  │ 1    │ Hotel│ h@...  │ +7...  │ ... │    │
│  │ 2    │ Inn  │ i@...  │ +7...  │ ... │    │
│  └──────┴──────┴────────┴────────┴─────┘    │
└─────────────────────────────────────────────┘
```

## 10. CQRS-like Pattern (Commands vs Queries)

```txt
┌──────────────────────────────────────────┐
│       Application Commands & Queries     │
└──────────────────────────────────────────┘

WRITE Operations (Commands):
┌────────────────────────────────────────┐
│     IAction<Input, Output>             │
│                                        │
│  CreateHotelUseCase                    │
│    : IAction<HotelDTOs.Create, Hotel>  │
│  {                                     │
│    Execute(input) {                    │
│      // 1. Create entity               │
│      // 2. Add to repository           │
│      // 3. Save changes                │
│      // 4. Return created entity       │
│    }                                   │
│  }                                     │
│                                        │
│  Other Examples:                       │
│  - UpdateHotelUseCase                  │
│  - DeleteHotelUseCase                  │
│  - CreateReservationUseCase            │
│  - ...                                 │
└────────────────────────────────────────┘

READ Operations (Queries):
┌──────────────────────────────────────────┐
│    IQuestion<Output, Input>              │
│                                          │
│  GetHotelByIdQuestion                    │
│    : IQuestion<Hotel, int>               │
│  {                                       │
│    Ask(id) {                             │
│      // 1. Query repository              │
│      // 2. Return read-only data         │
│    }                                     │
│  }                                       │
│                                          │
│  Other Examples:                         │
│  - GetAllHotelsQuestion                  │
│  - GetHotelsByLocationQuestion           │
│  - SearchReservationsQuestion            │
│  - ...                                   │
└──────────────────────────────────────────┘

Benefits:
├─ Clear separation of concerns
├─ Optimized read queries (no unnecessary
│  change tracking)
├─ Separate scaling strategies
├─ Better testability
└─ Domain-driven design alignment
```

---

Документ демонстрирует **полный поток данных** через все слои архитектуры.
