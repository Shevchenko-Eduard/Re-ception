# Re-ception: Практические примеры и рекомендации

## 1. Примеры создания новых UseCase'ов

### 1.1 Простой UseCase: Создание отеля

```csharp
// Domain/Entity/Hotel/Hotel.cs
public class Hotel
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public Email Email { get; private set; }
    public Phone Phone { get; private set; }
    public double Latitude { get; private set; }
    public double Longitude { get; private set; }
    public string? Description { get; private set; }
    
    // Methods for updating
    public void UpdateName(string name) => Name = name;
    public void UpdateEmail(Email email) => Email = email;
    public void UpdatePhone(Phone phone) => Phone = phone;
    public void UpdateLatitude(double latitude) => Latitude = latitude;
    public void UpdateLongitude(double longitude) => Longitude = longitude;
}

// Application/DTOs/HotelDTOs.cs
public static class HotelDTOs
{
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
}

// Application/UseCases/HotelUseCases/CreateHotelUseCase.cs
public class CreateHotelUseCase(
    IHotelRepository hotelRepository,
    IUnitOfWork unitOfWork) : IAction<HotelDTOs.Create, Hotel>
{
    public async Task<Hotel> Execute(HotelDTOs.Create input)
    {
        // 1. Validate input (can use FluentValidation)
        // 2. Create domain entity (Entity constructor validates)
        var hotel = input.GetHotel();
        
        // 3. Persist to repository
        await hotelRepository.AddAsync(hotel);
        
        // 4. Save changes (one atomic operation)
        await unitOfWork.SaveChangesAsync();
        
        // 5. Return created entity
        return hotel;
    }
}

// Usage in Presentation Layer (Controller)
[HttpPost("hotels")]
public async Task<ActionResult<HotelResponse>> CreateHotel(
    CreateHotelRequest request,
    [FromServices] IAction<HotelDTOs.Create, Hotel> createHotelAction)
{
    var hotelDto = new HotelDTOs.Create(
        request.Name,
        request.Email,
        request.Phone,
        request.Latitude,
        request.Longitude,
        request.Description);
    
    var hotel = await createHotelAction.Execute(hotelDto);
    
    return CreatedAtAction(
        nameof(GetHotel),
        new { id = hotel.Id },
        MapToResponse(hotel));
}
```

### 1.2 UseCase с бизнес-логикой: Расчет цены бронирования

```csharp
// Domain/Service/CalculatorReservationPrice.cs
public class CalculatorReservationPrice : ICalculatorReservationPrice
{
    private const decimal _basePriceMultiplier = 1.0m;
    private const decimal _weekendMultiplier = 1.2m; // +20% выходные
    
    public async Task<decimal> Calculator(Reservation reservation)
    {
        if (reservation.CheckOut <= reservation.CheckIn)
            throw new DomainInnerException("Invalid dates");
        
        var nights = (int)(reservation.CheckOut.Date - reservation.CheckIn.Date).TotalDays;
        var room = reservation.Room 
            ?? throw new DomainInnerException("Room not loaded");
        
        decimal totalPrice = 0;
        var currentDate = reservation.CheckIn.Date;
        
        while (currentDate < reservation.CheckOut.Date)
        {
            var dayOfWeek = currentDate.DayOfWeek;
            var isWeekend = dayOfWeek == DayOfWeek.Saturday || dayOfWeek == DayOfWeek.Sunday;
            
            decimal multiplier = isWeekend ? _weekendMultiplier : _basePriceMultiplier;
            totalPrice += (room.PricePerDay ?? 0) * multiplier;
            
            currentDate = currentDate.AddDays(1);
        }
        
        return totalPrice;
    }
}

// Application/UseCases/ReservationUseCases/UpdateReservationPriceUseCase.cs
public class UpdateReservationPriceUseCase(
    IReservationRepository reservationRepository,
    ICalculatorReservationPrice calculator,
    IUnitOfWork unitOfWork) : IAction<ReservationDTOs.UpdatePrice>
{
    public async Task Execute(ReservationDTOs.UpdatePrice input)
    {
        var reservation = await reservationRepository.GetByIdAsync(input.ReservationId)
            ?? throw new DomainExternalException("Reservation not found");
        
        // Инъецированный сервис вычисляет цену
        var newPrice = await calculator.Calculator(reservation);
        
        reservation.UpdateTotalPrice(newPrice);
        
        await reservationRepository.UpdateAsync(reservation);
        await unitOfWork.SaveChangesAsync();
    }
}
```

### 1.3 UseCase с транзакциями: Создание бронирования

```csharp
// Application/UseCases/ReservationUseCases/CreateReservationUseCase.cs
public class CreateReservationUseCase(
    IReservationRepository reservationRepository,
    IRoomRepository roomRepository,
    IGuestRepository guestRepository,
    ICalculatorReservationPrice calculator,
    IUnitOfWork unitOfWork) : IAction<ReservationDTOs.Create, Reservation>
{
    public async Task<Reservation> Execute(ReservationDTOs.Create input)
    {
        try
        {
            // 1. Начало транзакции
            await unitOfWork.BeginTransactionAsync();
            
            // 2. Проверка существования комнаты и гостя
            var room = await roomRepository.GetByIdAsync(input.RoomId)
                ?? throw new DomainExternalException("Room not found");
            
            var guest = await guestRepository.GetByIdAsync(input.GuestId)
                ?? throw new DomainExternalException("Guest not found");
            
            // 3. Проверка доступности комнаты в период
            var conflictingReservations = await reservationRepository
                .GetQueryable()
                .Where(r => r.RoomId == input.RoomId
                    && r.CheckIn < input.CheckOut
                    && r.CheckOut > input.CheckIn
                    && r.ReservationStatus != ReservationStatus.Cancelled)
                .AnyAsync();
            
            if (conflictingReservations)
                throw new DomainExternalException("Room not available for dates");
            
            // 4. Создание бронирования
            var reservation = input.GetReservation(guest, room);
            
            // 5. Расчет цены через бизнес-сервис
            var price = await calculator.Calculator(reservation);
            reservation.UpdateTotalPrice(price);
            
            // 6. Сохранение
            await reservationRepository.AddAsync(reservation);
            
            // 7. Обновление статуса комнаты
            room.UpdateRoomStatusId(RoomStatus.Reserved.Id);
            await roomRepository.UpdateAsync(room);
            
            // 8. Сохранение всех изменений в одной транзакции
            await unitOfWork.SaveChangesAsync();
            
            // 9. Коммит транзакции
            await unitOfWork.CommitTransactionAsync();
            
            return reservation;
        }
        catch (Exception)
        {
            // Откат транзакции при ошибке
            await unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}
```

---

## 2. Примеры работы с Value Objects

### 2.1 Создание Email с валидацией

```csharp
// ✓ Валидный email
var validEmail = new Email("user@example.com");
Console.WriteLine(validEmail.Value);  // "user@example.com"

// ✗ Невалидный email - выбросит DomainExternalException
try
{
    var invalidEmail = new Email("invalid-email");  // No @ sign
}
catch (DomainExternalException ex)
{
    Console.WriteLine($"Email error: {ex.Message}");
}

// ✗ Null или пусто - выбросит ArgumentException
try
{
    var emptyEmail = new Email("");  // Empty string
}
catch (ArgumentException)
{
    // Handled by ArgumentException.ThrowIfNullOrWhiteSpace
}
```

### 2.2 Создание Phone с валидацией русского номера

```csharp
// ✓ Различные валидные форматы
var phone1 = new Phone("+79991234567");      // +7 prefix
var phone2 = new Phone("89991234567");       // 8 prefix
var phone3 = new Phone("+7 999 123-45-67");  // With spaces/dashes
var phone4 = new Phone("(999) 123-4567");    // With parentheses

// ✗ Невалидный номер
try
{
    var invalidPhone = new Phone("123");  // Too short
}
catch (DomainExternalException ex)
{
    Console.WriteLine("Phone validation failed");
}
```

### 2.3 Использование Value Objects в Entity

```csharp
// Domain Entity создает Value Objects автоматически
public class Hotel
{
    public string Name { get; private set; }
    public Email Email { get; private set; }
    public Phone Phone { get; private set; }
    
    public Hotel(string name, Email email, Phone phone, ...)
    {
        Name = name;
        Email = email;  // уже валидирован при создании Email
        Phone = phone;  // уже валидирован при создании Phone
    }
    
    public void UpdateEmail(Email newEmail)
    {
        Email = newEmail;  // Замена целого объекта, а не строки
    }
}

// Использование
var hotel = new Hotel(
    name: "Grand Hotel",
    email: new Email("hotel@example.com"),  // Валидация здесь
    phone: new Phone("+79991234567"));      // Валидация здесь

// Обновление
hotel.UpdateEmail(new Email("new@example.com"));

// Type-safety: можем быть уверены, что Email всегда валиден
```

---

## 3. Примеры работы с Type-Safe Enum

### 3.1 Создание Type-Safe Enum для статуса

```csharp
// Domain/Entity/Room/RoomStatus.cs
public class RoomStatus : StatusObjectAbstract<RoomStatus>
{
    // Статические инстансы - это наши "enum значения"
    public static readonly RoomStatus Vacant = new("Vacant");
    public static readonly RoomStatus Occupied = new("Occupied");
    public static readonly RoomStatus CheckOut = new("CheckOut");
    public static readonly RoomStatus OutOfOrder = new("OutOfOrder");
    public static readonly RoomStatus Reserved = new("Reserved");
    
    // Защищенный конструктор (может быть вызван только выше)
    protected RoomStatus(string name) : base(name) { }
}

// Использование
var currentStatus = RoomStatus.Occupied;
var newStatus = RoomStatus.Vacant;

// Type-safety
if (currentStatus == RoomStatus.Occupied)
{
    Console.WriteLine("Room is occupied");
}

// Поиск по ID (автоматическая нумерация)
var statusById = RoomStatus.FromId(0);  // Первый created status

// Поиск по названию
var statusByName = RoomStatus.FromName("Vacant");

// Вычисление всех доступных статусов
var allStatuses = RoomStatus.All;  // ReadOnlyCollection<RoomStatus>
foreach (var status in allStatuses)
{
    Console.WriteLine($"{status.Name} (ID: {status.Id})");
}

// Сравнение
var isVacant = currentStatus == RoomStatus.Vacant;  // false
currentStatus = RoomStatus.Vacant;
isVacant = currentStatus == RoomStatus.Vacant;      // true
```

### 3.2 Иерархический статус (StatusWithParents)

```csharp
// Domain/Entity/Reservation/ReservationStatus.cs
public class ReservationStatus : StatusWithParentsObjectsAbstract<ReservationStatus>
{
    public static readonly ReservationStatus New = new("New");
    public static readonly ReservationStatus Confirmed = new("Confirmed", New);
    public static readonly ReservationStatus Guaranteed = new("Guaranteed", Confirmed);
    public static readonly ReservationStatus CheckedIn = new("CheckedIn", Guaranteed);
    public static readonly ReservationStatus Cancelled = new("Cancelled");
    public static readonly ReservationStatus Rejected = new("Rejected", New);
    
    protected ReservationStatus(string name) : base(name) { }
    protected ReservationStatus(string name, params IEnumerable<ReservationStatus> parents) 
        : base(name, parents) { }
}

// Иерархия:
// New ──> Confirmed ──> Guaranteed ──> CheckedIn
//     \──> Rejected
// Cancelled (отдельная ветка)

// Использование иерархии
var currentStatus = ReservationStatus.CheckedIn;

// Прямое сравнение
if (currentStatus == ReservationStatus.CheckedIn) { }  // true

// Сравнение с parent (благодаря переопределенному Equals)
if (currentStatus == ReservationStatus.Guaranteed) { }  // может быть true!
if (currentStatus == ReservationStatus.Confirmed) { }   // может быть true!

// Это полезно для правил типа:
// "Если статус бронирования подтвержден (или его наследник), то..."
public bool IsConfirmed(ReservationStatus status)
{
    return status == ReservationStatus.Confirmed;  // Проверит иерархию!
}

// Список всех статусов со статусом
var allStatuses = ReservationStatus.All;  
// New, Confirmed, Guaranteed, CheckedIn, Cancelled, Rejected
```

---

## 4. Примеры работы с Repository Pattern

### 4.1 Базовые операции Repository

```csharp
// Injection в UseCase
public class GetHotelByIdUseCase(
    IHotelRepository hotelRepository) : IQuestion<Hotel?, int>
{
    public async Task<Hotel?> Ask(int hotelId)
    {
        // Простой запрос по ID
        return await hotelRepository.GetByIdAsync(hotelId);
    }
}

// Использование GetQueryable для сложных запросов
public class SearchHotelsUseCase(
    IHotelRepository hotelRepository) : IQuestion<List<Hotel>, string>
{
    public async Task<List<Hotel>> Ask(string searchTerm)
    {
        var query = hotelRepository.GetQueryable()
            .Where(h => h.Name.Contains(searchTerm)
                || h.Description.Contains(searchTerm))
            .OrderBy(h => h.Name);
        
        return await query.ToListAsync();
    }
}

// Использование GetQueryable для фильтрации по координатам
public async Task<List<Hotel>> GetHotelsNearby(double lat, double lng, double radiusKm)
{
    // Простая формула расстояния (для примера)
    var maxLat = lat + (radiusKm / 111.0);  // ~111 км на 1 градус широты
    var minLat = lat - (radiusKm / 111.0);
    var maxLng = lng + (radiusKm / 111.0);
    var minLng = lng - (radiusKm / 111.0);
    
    return await hotelRepository.GetQueryable()
        .Where(h => h.Latitude >= minLat && h.Latitude <= maxLat
            && h.Longitude >= minLng && h.Longitude <= maxLng)
        .ToListAsync();
}
```

### 4.2 CRUD операции

```csharp
// CREATE
public class CreateHotelUseCase(
    IHotelRepository hotelRepository,
    IUnitOfWork unitOfWork) : IAction<HotelDTOs.Create, Hotel>
{
    public async Task<Hotel> Execute(HotelDTOs.Create input)
    {
        var hotel = input.GetHotel();
        await hotelRepository.AddAsync(hotel);  // Add to context
        await unitOfWork.SaveChangesAsync();     // Flush to DB
        return hotel;
    }
}

// READ
public class GetHotelUseCase(
    IHotelRepository hotelRepository) : IQuestion<Hotel?, int>
{
    public async Task<Hotel?> Ask(int id)
    {
        return await hotelRepository.GetByIdAsync(id);
    }
}

// UPDATE
public class UpdateHotelUseCase(
    IHotelRepository hotelRepository,
    IUnitOfWork unitOfWork) : IAction<HotelDTOs.Update>
{
    public async Task Execute(HotelDTOs.Update input)
    {
        var hotel = await hotelRepository.GetByIdAsync(input.Id)
            ?? throw new DomainExternalException("Hotel not found");
        
        // Modify entity
        hotel.UpdateName(input.Name ?? hotel.Name);
        hotel.UpdateEmail(new(input.Email ?? hotel.Email.Value));
        
        // Save changes
        await hotelRepository.UpdateAsync(hotel);
        await unitOfWork.SaveChangesAsync();
    }
}

// DELETE
public class DeleteHotelUseCase(
    IHotelRepository hotelRepository,
    IUnitOfWork unitOfWork) : IAction<int>
{
    public async Task Execute(int id)
    {
        await hotelRepository.DeleteAsync(id);
        await unitOfWork.SaveChangesAsync();
    }
}
```

---

## 5. Примеры работы с Unit of Work

### 5.1 Простая транзакция

```csharp
public class TransferMoneyUseCase(
    IPaymentRepository paymentRepository,
    IAccountRepository accountRepository,
    IUnitOfWork unitOfWork) : IAction<TransferMoneyRequest>
{
    public async Task Execute(TransferMoneyRequest input)
    {
        try
        {
            // Начало транзакции
            await unitOfWork.BeginTransactionAsync();
            
            // Операция 1: Проверка баланса отправителя
            var fromAccount = await accountRepository.GetByIdAsync(input.FromAccountId);
            if (fromAccount.Balance < input.Amount)
                throw new DomainExternalException("Insufficient funds");
            
            // Операция 2: Уменьшение баланса отправителя
            fromAccount.Debit(input.Amount);
            await accountRepository.UpdateAsync(fromAccount);
            
            // Операция 3: Увеличение баланса получателя
            var toAccount = await accountRepository.GetByIdAsync(input.ToAccountId);
            toAccount.Credit(input.Amount);
            await accountRepository.UpdateAsync(toAccount);
            
            // Операция 4: Создание записи платежа
            var payment = new Payment(
                fromAccountId: input.FromAccountId,
                toAccountId: input.ToAccountId,
                amount: input.Amount);
            await paymentRepository.AddAsync(payment);
            
            // Все четыре операции сохраняются в одной транзакции
            await unitOfWork.SaveChangesAsync();
            
            // Завершение транзакции
            await unitOfWork.CommitTransactionAsync();
        }
        catch (Exception ex)
        {
            // Откат всех изменений при любой ошибке
            await unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}
```

### 5.2 Вложенные транзакции (вспомогательные операции)

```csharp
public class CreateCompleteReservationUseCase(
    IReservationRepository reservationRepository,
    IRoomRepository roomRepository,
    IPaymentRepository paymentRepository,
    IUnitOfWork unitOfWork) : IAction<CreateReservationRequest, Reservation>
{
    public async Task<Reservation> Execute(CreateReservationRequest input)
    {
        try
        {
            await unitOfWork.BeginTransactionAsync();
            
            // Вспомогательный метод для создания платежа
            var payment = await CreateInitialPayment(input);
            
            // Основная логика
            var room = await roomRepository.GetByIdAsync(input.RoomId);
            var reservation = new Reservation(room, input.CheckIn, input.CheckOut);
            
            await reservationRepository.AddAsync(reservation);
            await unitOfWork.SaveChangesAsync();
            
            await unitOfWork.CommitTransactionAsync();
            return reservation;
        }
        catch (Exception)
        {
            await unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
    
    private async Task<Payment> CreateInitialPayment(CreateReservationRequest input)
    {
        // Подметод выполняется в контексте текущей транзакции
        var payment = new Payment(
            amount: input.DepositAmount,
            method: PaymentMethod.Card);
        
        await paymentRepository.AddAsync(payment);
        // SaveChanges будет вызван в основном методе
        return payment;
    }
}
```

---

## 6. Примеры конфигурации Entity в БД

### 6.1 Полная конфигурация Hotel

```csharp
public class HotelConf : IEntityTypeConfiguration<Hotel>
{
    public void Configure(EntityTypeBuilder<Hotel> builder)
    {
        // Таблица
        builder.ToTable("hotels");
        
        // Primary Key
        builder.HasKey(h => h.Id).HasName("hotel_id");
        
        // Свойства
        builder.Property(h => h.Name)
            .HasColumnName("name")
            .HasMaxLength(100)
            .IsRequired();
        
        builder.Property(h => h.Description)
            .HasColumnName("description")
            .HasMaxLength(1000);
        
        // Value Objects преобразуются в строки
        builder.Property(h => h.Email)
            .HasColumnName("email")
            .HasMaxLength(254)
            .IsRequired();
        
        builder.Property(h => h.Phone)
            .HasColumnName("phone")
            .HasMaxLength(50)
            .IsRequired();
        
        // Координаты с precision
        builder.Property(h => h.Latitude)
            .HasColumnName("latitude")
            .HasPrecision(9, 6)  // 9 digits, 6 decimal places
            .IsRequired();
        
        builder.Property(h => h.Longitude)
            .HasColumnName("longitude")
            .HasPrecision(9, 6)
            .IsRequired();
        
        // Индексы для поиска по координатам
        builder.HasIndex(h => new { h.Latitude, h.Longitude })
            .HasDatabaseName("idx_hotels_lati_long");
        
        // Уникальный индекс на Email
        builder.HasIndex(h => h.Email)
            .IsUnique()
            .HasDatabaseName("idx_hotels_email_unique");
    }
}
```

### 6.2 Конфигурация с Foreign Key и navigation properties

```csharp
public class ReservationConf : IEntityTypeConfiguration<Reservation>
{
    public void Configure(EntityTypeBuilder<Reservation> builder)
    {
        builder.ToTable("reservations");
        builder.HasKey(r => r.Id);
        
        // Скалярные свойства
        builder.Property(r => r.CheckIn).IsRequired();
        builder.Property(r => r.CheckOut).IsRequired();
        builder.Property(r => r.TotalPrice).HasPrecision(12, 2);
        builder.Property(r => r.Discount).HasPrecision(12, 2);
        
        // Foreign Keys
        builder.HasOne(r => r.Guest)
            .WithMany(g => g.Reservations)
            .HasForeignKey(r => r.GuestId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(r => r.Room)
            .WithMany(rm => rm.Reservations)
            .HasForeignKey(r => r.RoomId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
        
        // Status (Type-Safe Enum) stored as byte
        builder.Property(r => r.ReservationStatusId)
            .HasColumnName("status_id");
        
        // Индексы для часто используемых запросов
        builder.HasIndex(r => new { r.RoomId, r.CheckIn, r.CheckOut })
            .HasDatabaseName("idx_reservations_room_dates");
        
        builder.HasIndex(r => r.GuestId)
            .HasDatabaseName("idx_reservations_guest");
    }
}
```

---

## 7. Рекомендации по добавлению новых функций

### 7.1 Процесс добавления новой Entity

```
1. Создать Entity класс в Domain/Entity/
   ├─ Добавить Interfaces для репозитория в Domain/Interfaces/Repositories/
   └─ Если статус → наследовать StatusObjectAbstract<T>
   
2. Создать DTO в Application/DTOs/
   ├─ Create (с GetXXX() factory методом)
   ├─ Update (с GetXXX(entity) методом обновления)
   └─ Delete (если нужно)
   
3. Создать UseCase'ы в Application/UseCases/
   ├─ CreateXXXUseCase : IAction<DTOs.Create, Entity>
   ├─ UpdateXXXUseCase : IAction<DTOs.Update>
   ├─ DeleteXXXUseCase : IAction<int>
   └─ GetXXXUseCase : IQuestion<Entity?, int>
   
4. Создать Repository реализацию в Infrastructure/EfRepository/
   ├─ EfXXXRepository : IXXXRepository
   └─ Реализовать CRUD методы
   
5. Создать Entity Configuration в Infrastructure/Database/Configs/
   ├─ XXXConf : IEntityTypeConfiguration<Entity>
   └─ Настроить таблицу, индексы, foreign keys
   
6. Зарегистрировать в DbContext
   └─ public DbSet<Entity> XXXs { get; set; }
   
7. Добавить в Presentation слой
   └─ API Endpoints (Controllers)
```

### 7.2 Пример: добавление Entity "Comment"

```
STEP 1: Domain Entity
────────────────────────────────────────
// Domain/Entity/Comment/Comment.cs
public class Comment
{
    public int Id { get; private set; }
    public string Text { get; private set; }
    public Guid AuthorId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public int ReservationId { get; private set; }
    
    public User Author { get; private set; }
    public Reservation Reservation { get; private set; }
    
    public Comment(string text, Guid authorId, int reservationId)
    {
        if (string.IsNullOrWhiteSpace(text) || text.Length > 500)
            throw new DomainExternalException("Invalid comment text");
        
        Text = text;
        AuthorId = authorId;
        ReservationId = reservationId;
        CreatedAt = DateTimeOffset.Now;
    }
    
    public void UpdateText(string newText)
    {
        if (string.IsNullOrWhiteSpace(newText) || newText.Length > 500)
            throw new DomainExternalException("Invalid comment text");
        Text = newText;
    }
}

STEP 2: Repository Interface
────────────────────────────────────────
// Domain/Interfaces/Repositories/ICommentRepository.cs
public interface ICommentRepository : IBaseCrudRepository<Comment, int>
{
    Task<List<Comment>> GetByReservationIdAsync(int reservationId);
}

STEP 3: DTOs
────────────────────────────────────────
// Application/DTOs/CommentDTOs.cs
public static class CommentDTOs
{
    public record Create(string Text, int ReservationId)
    {
        public Comment GetComment(Guid authorId) =>
            new(Text, authorId, ReservationId);
    }
    
    public record Update(int Id, string Text);
}

STEP 4: UseCases
────────────────────────────────────────
// Application/UseCases/CommentUseCases/CreateCommentUseCase.cs
public class CreateCommentUseCase(
    ICommentRepository repository,
    ICurrentUser currentUser,
    IUnitOfWork unitOfWork) : IAction<CommentDTOs.Create, Comment>
{
    public async Task<Comment> Execute(CommentDTOs.Create input)
    {
        var authorId = currentUser.Id ?? throw new DomainExternalException("User not authenticated");
        
        var comment = input.GetComment(Guid.Parse(authorId));
        await repository.AddAsync(comment);
        await unitOfWork.SaveChangesAsync();
        
        return comment;
    }
}

// Application/UseCases/CommentUseCases/GetReservationCommentsUseCase.cs
public class GetReservationCommentsUseCase(
    ICommentRepository repository) : IQuestion<List<Comment>, int>
{
    public async Task<List<Comment>> Ask(int reservationId)
    {
        return await repository.GetByReservationIdAsync(reservationId);
    }
}

STEP 5: Repository Implementation
────────────────────────────────────────
// Infrastructure/EfRepository/CommentRepository/EfCommentRepository.cs
public class EfCommentRepository(ProgramContext context) : ICommentRepository
{
    public async Task AddAsync(Comment entity) =>
        await context.Comments.AddAsync(entity);
    
    public async Task UpdateAsync(Comment entity) =>
        context.Comments.Update(entity);
    
    public async Task DeleteAsync(int id) =>
        await context.Comments.Where(c => c.Id == id).ExecuteDeleteAsync();
    
    public async Task<Comment?> GetByIdAsync(int id) =>
        await context.Comments.FirstOrDefaultAsync(c => c.Id == id);
    
    public IQueryable<Comment> GetQueryable() =>
        context.Comments.AsQueryable();
    
    public async Task<List<Comment>> GetByReservationIdAsync(int reservationId) =>
        await context.Comments
            .Where(c => c.ReservationId == reservationId)
            .OrderByDescending(c => c.CreatedAt)
            .ToListAsync();
}

STEP 6: Entity Configuration
────────────────────────────────────────
// Infrastructure/Database/Configs/CommentConf.cs
public class CommentConf : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.ToTable("comments");
        builder.HasKey(c => c.Id);
        
        builder.Property(c => c.Text)
            .HasColumnName("text")
            .HasMaxLength(500)
            .IsRequired();
        
        builder.Property(c => c.CreatedAt)
            .HasColumnName("created_at");
        
        builder.HasOne(c => c.Author)
            .WithMany()
            .HasForeignKey(c => c.AuthorId);
        
        builder.HasOne(c => c.Reservation)
            .WithMany(r => r.Comments)
            .HasForeignKey(c => c.ReservationId);
        
        builder.HasIndex(c => c.ReservationId);
    }
}

STEP 7: Add to DbContext
────────────────────────────────────────
// Infrastructure/Database/ProgramContext.cs
public partial class ProgramContext : DbContext
{
    public DbSet<Comment> Comments { get; set; }  // Add this
    // ...
}

STEP 8: Presentation (Controller)
────────────────────────────────────────
// In Presentation project (CustomerWeb or EmployeeWeb)
[ApiController]
[Route("api/[controller]")]
public class CommentsController : ControllerBase
{
    [HttpPost("{reservationId}/comments")]
    public async Task<ActionResult<CommentResponse>> CreateComment(
        int reservationId,
        [FromBody] CreateCommentRequest request,
        [FromServices] IAction<CommentDTOs.Create, Comment> createAction)
    {
        var comment = await createAction.Execute(
            new(request.Text, reservationId));
        
        return CreatedAtAction(
            nameof(GetComments),
            new { reservationId },
            MapToResponse(comment));
    }
    
    [HttpGet("{reservationId}/comments")]
    public async Task<ActionResult<List<CommentResponse>>> GetComments(
        int reservationId,
        [FromServices] IQuestion<List<Comment>, int> getCommentsQuestion)
    {
        var comments = await getCommentsQuestion.Ask(reservationId);
        return Ok(comments.Select(MapToResponse).ToList());
    }
}
```

---

## 8. Лучшие практики

### ✓ DO

- **Используйте Value Objects** для повторяющихся паттернов (Email, Phone, Address)
- **Создавайте методы обновления в Entity** вместо public setters
- **Используйте IQueryable** для сложных запросов в Repository
- **Регистрируйте зависимости** в DI контейнере, а не создавайте через new
- **Используйте CancellationToken** во всех async методах
- **Создавайте отдельные UseCase'ы** для каждого действия (Single Responsibility)
- **Используйте DTOs** для обмена данными между слоями
- **Валидируйте данные** в Value Objects и Entity конструкторах
- **Используйте транзакции** для множественных операций
- **Логируйте ошибки** перед выбросом исключений

### ✗ DON'T

- ❌ Не передавайте Entity напрямую в Presentation (используйте DTOs/Response Models)
- ❌ Не создавайте Database access код в Domain слое
- ❌ Не используйте static методы для бизнес-логики
- ❌ Не обходите Repository для прямого доступа к DbContext
- ❌ Не создавайте слишком большие UseCase'ы (>50 строк кода - признак что нужно разбить)
- ❌ Не используйте ORM-specific типы (IQueryable) в возвращаемых значениях API
- ❌ Не забывайте о транзакциях при множественных операциях
- ❌ Не оставляйте свойства Entity с public setters (используйте private set + методы)
- ❌ Не валидируйте только на Presentation слое
- ❌ Не создавайте циклические зависимости между слоями

---

## 9. Тестирование

### 9.1 Unit Test для Value Object

```csharp
public class EmailTests
{
    [Fact]
    public void ValidEmail_ShouldBeCreated()
    {
        // Arrange & Act
        var email = new Email("test@example.com");
        
        // Assert
        Assert.Equal("test@example.com", email.Value);
    }
    
    [Fact]
    public void InvalidEmail_ShouldThrowException()
    {
        // Arrange & Act & Assert
        Assert.Throws<DomainExternalException>(
            () => new Email("invalid-email"));
    }
}
```

### 9.2 Unit Test для UseCase (с Mocks)

```csharp
public class CreateHotelUseCaseTests
{
    [Fact]
    public async Task Execute_WithValidInput_ShouldCreateHotel()
    {
        // Arrange
        var repositoryMock = new Mock<IHotelRepository>();
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        
        var useCase = new CreateHotelUseCase(
            repositoryMock.Object,
            unitOfWorkMock.Object);
        
        var input = new HotelDTOs.Create(
            "Test Hotel",
            "test@hotel.com",
            "+79991234567",
            55.75,
            37.62);
        
        // Act
        var result = await useCase.Execute(input);
        
        // Assert
        Assert.NotNull(result);
        Assert.Equal("Test Hotel", result.Name);
        repositoryMock.Verify(r => r.AddAsync(It.IsAny<Hotel>()), Times.Once);
        unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
```

---

Документ содержит практические примеры для быстрого старта с архитектурой Re-ception.
