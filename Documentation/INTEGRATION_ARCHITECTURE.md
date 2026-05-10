# Интеграционная архитектура Re-ception

## 1. Обзор системы интеграции

Re-ception представляет собой многоуровневую распределённую систему управления отелем с следующими основными компонентами:

```
┌─────────────────────────────────────────────────────────────────────┐
│                          CLIENT LAYER                               │
│              (Web Frontend приложения)                              │
│    ┌────────────────┐    ┌────────────────┐    ┌─────────────┐      │
│    │  CustomerWeb   │    │  EmployeeWeb   │    │   LibWeb    │      │
│    │  (React/SPA)   │    │  (React/SPA)   │    │  (Shared)   │      │
│    └────────┬───────┘    └────────┬───────┘    └─────────────┘      │
└─────────────┼─────────────────────┼─────────────────────────────────┘
              │                     │
              └──────────────┬──────┘
                             │
              ┌──────────────┴──────────────┐
              │  HTTPS/REST API calls       │
              │  + JWT Token Authentication  │
              │                             │
┌─────────────▼─────────────────────────────▼──────────────────────────┐
│                    API GATEWAY (Nginx) LAYER                         │
│                                                                      │
│  ┌────────────────────────────────────────────────────────────────┐  │
│  │                     Nginx Reverse Proxy                        │  │
│  │                                                                │  │
│  │  • SSL/TLS Termination (HTTPS)                                 │  │
│  │  • Request Routing (path-based routing)                        │  │
│  │  • Load Balancing (if needed)                                  │  │
│  │  • Security Headers                                            │  │
│  │  • CORS Configuration                                          │  │
│  │                                                                │  │
│  │  Routes:                                                       │  │
│  │  • /auth/* → Keycloak (Single Sign-On)                         │  │
│  │  • /api/employee/* → Employee-API (Port 5001)                  │  │
│  │  • /api/customer/* → Customer-API (Port 5002)                  │  │
│  └────────────────────────────────────────────────────────────────┘  │
└─────────────┬────────────────────────────────────────────────────────┘
              │
      ┌───────┼────────┬─────────────┐
      │       │        │             │
      ▼       ▼        ▼             ▼
┌─────────┬──────────┬───────────┬────────────────────────────────────┐
│         │          │           │  MICROSERVICES & INFRASTRUCTURE    │
│         │          │           │                                    │
│  ┌──────▼───┐  ┌──▼──────┐  ┌─▼──────────┐  ┌──────────┐            │
│  │ Keycloak │  │Employee │  │ Customer   │  │  Redis   │            │
│  │ (Auth)   │  │  API    │  │   API      │  │ (Cache)  │            │
│  │          │  │         │  │            │  │          │            │
│  │ Port     │  │ Port    │  │  Port      │  │ Port     │            │
│  │ 8080     │  │ 5001    │  │  5002      │  │ 6379     │            │
│  └────┬─────┘  └────┬────┘  └─────┬──────┘  └─────┬────┘            │
│       │             │             │              │                  │
│       │             │             │              │                  │
│  ┌────▼─────────────▼─────────────▼──────────────▼────────────────┐ │
│  │              PostgreSQL (Main Database)                        │ │
│  │                                                                │ │
│  │  Databases:                                                    │ │
│  │  • keycloak_db  (Keycloak Auth State)                          │ │
│  │  • reception_db (Business Data)                                │ │
│  │                                                                │ │
│  └────────────────────────────────────────────────────────────────┘ │
│                                                                     │
│  ┌──────────────────────────────────────────────────────────────┐   │
│  │           MinIO (S3-Compatible Object Storage)               │   │
│  │                                                              │   │
│  │  Storage buckets:                                            │   │
│  │  • hotel-images (Hotel images)                               │   │
│  │  • room-images  (Room images)                                │   │
│  │                                                              │   │
│  │  Port: 9000 (API) / 9001 (Console)                           │   │
│  └──────────────────────────────────────────────────────────────┘   │
└─────────────────────────────────────────────────────────────────────┘
```

## 2. Компоненты интеграции

### 2.1 Nginx - API Gateway

**Назначение:** Точка входа для всех клиентских запросов, управление маршрутизацией и безопасностью.

**Интеграции:**
- **Входящие:** HTTPS запросы от клиентских приложений
- **Исходящие:** 
  - Маршрутизация на Keycloak (`:8080`)
  - Маршрутизация на Employee API (`:5001`)
  - Маршрутизация на Customer API (`:5002`)

**Ключевые параметры конфигурации:**
```
NGINX_HTTP_PORT=80
NGINX_HTTPS_PORT=443
NGINX_DOMAIN=your-domain.com

PORT_EMPLOYEE=5000
HOST_EMPLOYEE=employee-api
PORT_CUSTOMER=5000
HOST_CUSTOMER=customer-api
PORT_AUTH=8080
HOST_AUTH=keycloak
```

**Функции:**
- SSL/TLS завершение (HTTPS)
- Path-based маршрутизация
- Переписывание заголовков (X-Forwarded-*)
- Кеширование статических ресурсов
- Rate limiting (при необходимости)

---

### 2.2 Keycloak - Authentication & Authorization

**Назначение:** Централизованное управление аутентификацией, авторизацией и ролями пользователей.

**Интеграции:**
- **Входящие:**
  - Запросы аутентификации от Nginx и API
  - Токен валидация от обоих API
- **Исходящие:**
  - Доступ к keycloak-db (PostgreSQL) для хранения конфигурации
  - Предоставление JWT токенов клиентам

**Ключевые параметры:**
```
KEYCLOAK_HOST=keycloak
KEYCLOAK_HTTP_PORT=8080
KEYCLOAK_AUTH_SERVER_URL=http://keycloak:8080

# Realms
KEYCLOAK_EMPLOYEE_REALM=employee
KEYCLOAK_EMPLOYEE_RESOURCE=employee-api
KEYCLOAK_EMPLOYEE_SECRET=<secret>

KEYCLOAK_CUSTOMER_REALM=customer
KEYCLOAK_CUSTOMER_RESOURCE=customer-api
KEYCLOAK_CUSTOMER_SECRET=<secret>

# Database
KC_POSTGRES_DB=keycloak
KC_POSTGRES_USER=keycloak_user
KC_POSTGRES_PASSWORD=<password>
KC_POSTGRES_PORT=5433
```

**Структура:**
- **Realms:** Отдельные области для Employee и Customer приложений
- **Roles:** Управление ролями (Admin, Manager, Customer, etc.)
- **Clients:** Конфигурация приложений (Employee API, Customer API)
- **User Federation:** Интеграция с внешними источниками (при необходимости)

**Аутентификационный поток:**
```
1. Пользователь входит в приложение
2. Nginx перенаправляет на Keycloak (/auth/...)
3. Keycloak проверяет учетные данные
4. После успешной аутентификации выдается JWT токен
5. JWT токен отправляется клиенту
6. Клиент использует токен для запросов к API
7. API валидирует токен с помощью Keycloak
```

---

### 2.3 Employee API - Микросервис для сотрудников

**Назначение:** REST API для управления данными сотрудников, комнат, отелей и платежей.

**Интеграции:**
- **Входящие:**
  - HTTP запросы через Nginx (`:5001`)
  - Запросы от фронтенда EmployeeWeb
- **Исходящие:**
  - Подключение к PostgreSQL (backend-db) для данных
  - Подключение к MinIO для загрузки/скачивания изображений
  - Подключение к Redis для кеширования
  - Валидация токенов с Keycloak

**Ключевые параметры:**
```
ASPNETCORE_ENVIRONMENT=Development
ASPNETCORE_URLS=http://+:5000
ASPNETCORE_PORT=5000
EMPLOYEE_API_PORT=5001

DB__Host=backend-db
DB__Port=5432
DB__Database=reception
DB__Username=reception_user
DB__Password=<password>

Keycloak__AuthServerUrl=http://keycloak:8080
Keycloak__Realm=employee
Keycloak__Resource=employee-api

Minio__Endpoint=minio:9000
Minio__Username=minioadmin
Minio__Password=<password>

Redis__Endpoint=redis:6379
Redis__InstanceName=reception
Redis__Password=<password>
```

**Основные операции:**
- CRUD операции для Hotels, Rooms, Reservations, Payments
- Работа с изображениями через MinIO
- Кеширование часто используемых данных в Redis
- Валидация авторизации через Keycloak

---

### 2.4 Customer API - Микросервис для клиентов

**Назначение:** REST API для клиентов, предоставляет урезанный набор операций (просмотр отелей, комнат, создание бронирований).

**Интеграции:**
- **Входящие:**
  - HTTP запросы через Nginx (`:5002`)
  - Запросы от фронтенда CustomerWeb
- **Исходящие:**
  - Подключение к PostgreSQL (backend-db) для данных
  - Подключение к MinIO для получения изображений
  - Подключение к Redis для кеширования
  - Валидация токенов с Keycloak

**Ключевые отличия от Employee API:**
- Ограниченный доступ к данным (только публичная информация)
- Возможность создания резервирований и платежей
- Просмотр своих заказов и истории платежей
- Отсутствие доступа к управлению отелями и сотрудниками

---

### 2.5 PostgreSQL - Основная база данных

**Назначение:** Хранение всех бизнес-данных приложения.

**Интеграции:**
- **Входящие:**
  - Запросы от Employee API
  - Запросы от Customer API
  - Запросы от Keycloak (для keycloak-db)
- **Исходящие:** Возврат результатов запросов

**Структура баз данных:**
```
├── keycloak_db (Keycloak state database)
│   ├── users
│   ├── roles
│   ├── client_*
│   └── ...
│
└── reception (Business data)
    ├── users
    ├── employees
    ├── guests
    ├── hotels
    ├── rooms
    ├── room_types
    ├── reservations
    ├── payments
    ├── payment_methods
    ├── payment_statuses
    └── ... (связующие таблицы)
```

**Ключевые параметры:**
```
# Keycloak DB
KC_POSTGRES_DB=keycloak
KC_POSTGRES_USER=keycloak_user
KC_POSTGRES_PASSWORD=<password>
KC_POSTGRES_PORT=5433

# Application DB
SERVER_POSTGRES_DB=reception
SERVER_POSTGRES_USER=reception_user
SERVER_POSTGRES_PASSWORD=<password>
SERVER_POSTGRES_PORT=5432
```

**Здоровье и надежность:**
- Health checks для обоих API перед запуском сервиса
- Поддержка транзакций в Application через EfUnitOfWork
- Автоматические миграции схемы (Entity Framework)

---

### 2.6 Redis - Кеширование

**Назначение:** Быстрое кеширование часто используемых данных для снижения нагрузки на БД.

**Интеграции:**
- **Входящие:**
  - Запросы на чтение/запись от Employee API
  - Запросы на чтение/запись от Customer API
- **Исходящие:** Возврат закешированных данных

**Типы кешируемых данных:**
- Списки отелей
- Информация о комнатах
- Доступность комнат
- Данные о статусах и типах
- Пользовательские сессии (если используется)

**Ключевые параметры:**
```
Redis__Endpoint=redis:6379
Redis__InstanceName=reception
Redis__Password=<password>
CACHE_PORT=6379
CACHE_PASSWORD=<password>
```

**Политика кеширования:**
- TTL (Time-To-Live) различается в зависимости от типа данных
- Инвалидация кеша при обновлении данных
- Резервные стратегии при отсутствии кеша

---

### 2.7 MinIO - Хранилище объектов

**Назначение:** S3-совместимое хранилище для изображений отелей и комнат.

**Интеграции:**
- **Входящие:**
  - Запросы на загрузку/скачивание изображений от API
- **Исходящие:** Предоставление изображений клиентам через API

**Структура хранилища:**
```
minio/
├── hotel-images/
│   ├── {hotelId}/
│   │   └── {imageId}.jpg
│   └── ...
│
└── room-images/
    ├── {roomId}/
    │   └── {imageId}.jpg
    └── ...
```

**Ключевые параметры:**
```
S3_HOST=minio
S3_PORT=9000
MINIO_ROOT_USER=minioadmin
MINIO_ROOT_PASSWORD=<password>

Minio__Endpoint=minio:9000
Minio__Username=minioadmin
Minio__Password=<password>
Minio__HTTPS=false
```

**API Integration:**
- Repository Pattern (IS3Repository)
  - MinioHotelImageRepository
  - MinioRoomImageRepository
- Операции:
  - UploadImage(stream, bucketName, objectName)
  - DownloadImage(bucketName, objectName)
  - DeleteImage(bucketName, objectName)
  - GetImageUrl(bucketName, objectName)

---

## 3. Сетевая архитектура (Docker Networks)

### Docker Networks структура:

```
┌──────────────────────────────────────────────────────┐
│ Docker Networks                                      │
│                                                      │
│ 1. server-network (backend services)                 │
│    └─ Employee API ←→ Customer API ←→ PostgreSQL     │
│       Redis, MinIO                                   │
│                                                      │
│ 2. keycloak-network (auth services)                  │
│    └─ Keycloak ←→ Keycloak-DB                        │
│       (подключение к server-network)                 │
│                                                      │
│ 3. nginx-network (gateway)                           │
│    └─ Nginx ←→ Employee API, Customer API            │
│       Keycloak                                       │
│                                                      │
└──────────────────────────────────────────────────────┘
```

**Сервисы в каждой сети:**
- **server-network:** employee-api, customer-api, backend-db, redis, minio
- **keycloak-network:** keycloak, keycloak-db
- **nginx-network:** nginx, keycloak, employee-api, customer-api

---

## 4. Процессы интеграции

### 4.1 Запуск системы (Bootstrap)

```
1. PostgreSQL + Redis + MinIO (base services)
   ↓
2. Keycloak-DB (Keycloak state)
   ↓
3. Keycloak (Auth server) [depends_on: keycloak-db]
   ↓
4. Employee API [depends_on: PostgreSQL, Redis, MinIO, Keycloak]
   ↓
5. Customer API [depends_on: PostgreSQL, Redis, MinIO, Keycloak]
   ↓
6. Nginx [depends_on: Keycloak, Employee API, Customer API]
   ↓
7. System ready for client connections
```

**Health Checks:**
- Keycloak: HTTP /health/ready (порт 9000)
- Employee API: HTTP /health (порт 5000)
- Customer API: HTTP /health (порт 5000)
- Nginx: HTTP /health (порт 80)

### 4.2 Аутентификационный поток

```
CLIENT
  │
  ├─→ [1] POST /auth/login
  │   (username + password)
  │
  ↓
NGINX (API Gateway)
  │
  ├─→ [2] Forward to Keycloak
  │   /auth/realms/{realm}/protocol/openid-connect/token
  │
  ↓
KEYCLOAK
  │
  ├─→ [3] Query keycloak-db
  │   SELECT users WHERE username = ?
  │
  ├─→ [4] Validate password
  │
  ├─→ [5] Generate JWT token
  │   (signed with realm key)
  │
  ↓
NGINX
  │
  ├─→ [6] Return token to client
  │
  ↓
CLIENT
  │
  ├─→ [7] Store JWT token in localStorage/sessionStorage
```

### 4.3 API Request Flow с авторизацией

```
CLIENT
  │
  ├─→ [1] GET /api/employee/hotels
  │   Headers: Authorization: Bearer {JWT_TOKEN}
  │
  ↓
NGINX (API Gateway)
  │
  ├─→ [2] Check SSL/TLS certificate
  │
  ├─→ [3] Route to /api/employee → employee-api:5000
  │
  ├─→ [4] Pass Authorization header
  │
  ↓
EMPLOYEE API
  │
  ├─→ [5] Check Authorization header
  │
  ├─→ [6] Validate JWT token with Keycloak
  │   POST /auth/realms/employee/protocol/openid-connect/token/introspect
  │
  ↓
KEYCLOAK
  │
  ├─→ [7] Verify token signature
  │
  ├─→ [8] Check expiration
  │
  ├─→ [9] Check user roles/permissions
  │
  ├─→ [10] Return token validity + claims
  │
  ↓
EMPLOYEE API
  │
  ├─→ [11] If valid: Continue to handler
  │        If invalid: Return 401 Unauthorized
  │
  ├─→ [12] Authorize by role/permission
  │        If allowed: Continue
  │        If denied: Return 403 Forbidden
  │
  ├─→ [13] Query database
  │   SELECT * FROM hotels WHERE owner_id = {user_id}
  │
  ↓
PostgreSQL
  │
  ├─→ [14] Execute query
  │
  ├─→ [15] Return results
  │
  ↓
EMPLOYEE API
  │
  ├─→ [16] Check cache (Redis)
  │   GET reception:hotels:{user_id}
  │
  ├─→ [17] Cache hit/miss handling
  │
  ├─→ [18] Serialize response (JSON)
  │
  ├─→ [19] Return 200 OK + data
  │
  ↓
NGINX
  │
  ├─→ [20] Return response to client
  │
  ↓
CLIENT
  │
  ├─→ [21] Receive JSON data
  │
  ├─→ [22] Update UI
```

### 4.4 Data Flow для изображений

```
CLIENT
  │
  ├─→ [1] POST /api/employee/hotels/{id}/images
  │   Form: {file: File}
  │   Headers: Authorization: Bearer {JWT}
  │
  ↓
NGINX → EMPLOYEE API → VALIDATE TOKEN → AUTHORIZE
  │
  ├─→ [2] Save to temporary location
  │
  ├─→ [3] Upload to MinIO
  │   PUT /hotel-images/{hotelId}/{imageId}.jpg
  │
  ↓
MinIO
  │
  ├─→ [4] Store image
  │
  ├─→ [5] Return S3 URL
  │
  ↓
EMPLOYEE API
  │
  ├─→ [6] Save image metadata to PostgreSQL
  │   INSERT INTO hotel_images
  │   (hotel_id, image_id, s3_url, created_at)
  │
  ├─→ [7] Invalidate cache
  │   DEL reception:hotel_images:{hotel_id}
  │
  ├─→ [8] Return 200 Created + image metadata
  │
  ↓
CLIENT
  │
  ├─→ [9] Render image using URL from response
  │   <img src="http://minio:9000/hotel-images/.../img.jpg" />
```

---

## 5. Обработка ошибок и восстановление

### 5.1 Стратегии обработки сбоев

**БД недоступна:**
```
API → PostgreSQL (timeout/connection refused)
  ↓
→ Retry logic (3 попытки с backoff)
  ↓
→ Try Redis cache
  ↓
→ Return cached data OR
→ Return 503 Service Unavailable
```

**Keycloak недоступен:**
```
API → Keycloak (timeout/connection refused)
  ↓
→ Token validation fails
  ↓
→ Return 401 Unauthorized
  ↓
→ Client redirects to login
```

**MinIO недоступен:**
```
API → MinIO (timeout/connection refused)
  ↓
→ Image upload fails
  ↓
→ Return 503 Service Unavailable
  ↓
→ Don't save image metadata to DB
```

**Redis недоступен:**
```
API → Redis (timeout/connection refused)
  ↓
→ Continue without cache
  ↓
→ Query directly from PostgreSQL
  ↓
→ Return data normally
```

### 5.2 Health Checks и Restart Policies

```
┌─────────────┬──────────┬──────────────┬─────────────────────┐
│ Service     │ Interval │ Timeout      │ Restart Policy      │
├─────────────┼──────────┼──────────────┼─────────────────────┤
│ Keycloak    │ 5s       │ 10s          │ always              │
│ PostgreSQL  │ 10s      │ 5s           │ unless-stopped      │
│ Redis       │ 5s       │ 3s           │ unless-stopped      │
│ MinIO       │ 5s       │ 3s           │ unless-stopped      │
│ Employee API│ 10s      │ 5s           │ unless-stopped      │
│ Customer API│ 10s      │ 5s           │ unless-stopped      │
│ Nginx       │ 5s       │ 10s          │ unless-stopped      │
└─────────────┴──────────┴──────────────┴─────────────────────┘
```

---

## 6. Масштабируемость и производительность

### 6.1 Возможные точки масштабирования

**Горизонтальное масштабирование:**
```
┌──────────────────────────────────────────────┐
│ Nginx (Load Balancer)                        │
└──────────┬───────────────────────────────────┘
           │
    ┌──────┴───────┐
    ↓              ↓
┌─────────┐    ┌─────────┐
│ API 1   │    │ API 2   │  → Can scale to N replicas
│ :5001   │    │ :5001   │
└─────────┘    └─────────┘
    ↓              ↓
    └──────┬───────┘
           ↓
    ┌──────────────┐
    │ PostgreSQL   │  → Shared database
    │ (Read replicas)
    └──────────────┘
```

**Кеширование:**
- Redis используется для снижения нагрузки на БД
- Redis может быть масштабирован через Cluster или Sentinel

**БД оптимизация:**
- Индексы на часто используемых полях
- Партиционирование больших таблиц (reservations, payments)
- Read replicas для read-heavy операций

### 6.2 Оптимизация производительности

**На уровне API:**
- Pagination для больших наборов данных
- Filtering и sorting на уровне БД
- Lazy loading связанных сущностей
- DTOs для передачи только необходимых данных

**На уровне сетевой архитектуры:**
- HTTP/2 в Nginx
- Gzip compression для ответов
- Browser caching headers
- CDN для статических ресурсов

**На уровне кеширования:**
- Стратегия LRU (Least Recently Used) в Redis
- Инвалидация кеша при записи
- Prefetching часто используемых данных

---

## 7. Конфигурация переменных окружения

### 7.1 .env файл (корневой уровень)

```env
# General Configuration
ASPNETCORE_ENVIRONMENT=Development
NGINX_DOMAIN=localhost

# Ports
NGINX_HTTP_PORT=80
NGINX_HTTPS_PORT=443
EMPLOYEE_API_PORT=5001
CUSTOMER_API_PORT=5002
KEYCLOAK_HTTP_PORT=8080
S3_PORT=9000
CACHE_PORT=6379

# Database - PostgreSQL (Server)
SERVER_POSTGRES_USER=reception_user
SERVER_POSTGRES_PASSWORD=reception_password
SERVER_POSTGRES_DB=reception
SERVER_POSTGRES_PORT=5432
DB_HOST=backend-db

# Database - PostgreSQL (Keycloak)
KC_POSTGRES_USER=keycloak_user
KC_POSTGRES_PASSWORD=keycloak_password
KC_POSTGRES_DB=keycloak
KC_POSTGRES_PORT=5433
KEYCLOAK_HOST=keycloak

# Keycloak
ADMIN_NAME=admin
ADMIN_PASSWORD=admin_password
KEYCLOAK_AUTH_SERVER_URL=http://keycloak:8080

# Keycloak - Employee Realm
KEYCLOAK_EMPLOYEE_REALM=employee
KEYCLOAK_EMPLOYEE_RESOURCE=employee-api
KEYCLOAK_EMPLOYEE_SECRET=employee_secret

# Keycloak - Customer Realm
KEYCLOAK_CUSTOMER_REALM=customer
KEYCLOAK_CUSTOMER_RESOURCE=customer-api
KEYCLOAK_CUSTOMER_SECRET=customer_secret

# MinIO (S3)
MINIO_ROOT_USER=minioadmin
MINIO_ROOT_PASSWORD=minioadmin_password
S3_HOST=minio

# Redis
REDIS_INSTANCE_NAME=reception
CACHE_PASSWORD=redis_password

# ASP.NET Core
ASPNETCORE_PORT=5000
ASPNETCORE_URLS=http://+:5000

# Logging
LOG_LEVEL=debug
```

---

## 8. Диаграмма взаимодействия компонентов

### 8.1 Sequence Diagram: User Login

```
Client          Nginx       Keycloak     Keycloak-DB   PostgreSQL
  │               │            │             │             │
  ├──POST login──→│             │             │             │
  │               ├─forward────→│             │             │
  │               │             ├─query creds─→             │
  │               │             │             ├─SELECT users──→
  │               │             │             │←──user data──┤
  │               │             ├─verify pwd──│             │
  │               │             ├─generate JWT─┤             │
  │               │←──token────┤             │             │
  │←────token────┤             │             │             │
  │               │             │             │             │
```

### 8.2 Sequence Diagram: Get Hotels

```
Client          Nginx      Employee-API   Redis    PostgreSQL  Keycloak
  │               │            │           │           │          │
  ├──GET hotels──→│             │           │           │          │
  │ (+ JWT token) │             │           │           │          │
  │               ├──request───→│           │           │          │
  │               │             ├─validate token─────────────────→│
  │               │             │           │           │←─response─┤
  │               │             ├─check cache─→        │          │
  │               │             │←─cache miss─┤        │          │
  │               │             ├──query────────────────→           │
  │               │             │           │←─hotels──┤           │
  │               │             ├─set cache──→         │          │
  │               │             │←──done────┤          │          │
  │               │←──response──┤           │           │          │
  │←──response────┤             │           │           │          │
  │               │             │           │           │          │
```

---

## 9. Команды для развертывания и мониторинга

### 9.1 Docker Compose команды

```bash
# Запуск всей системы
docker-compose -f Program/Nginx/docker-compose.yml up -d

# Просмотр статуса сервисов
docker-compose -f Program/Nginx/docker-compose.yml ps

# Просмотр логов конкретного сервиса
docker-compose -f Program/Nginx/docker-compose.yml logs -f employee-api
docker-compose -f Program/Nginx/docker-compose.yml logs -f keycloak
docker-compose -f Program/Nginx/docker-compose.yml logs -f nginx

# Перезапуск сервиса
docker-compose -f Program/Nginx/docker-compose.yml restart employee-api

# Остановка системы
docker-compose -f Program/Nginx/docker-compose.yml down

# Удаление всех данных
docker-compose -f Program/Nginx/docker-compose.yml down -v
```

### 9.2 Проверка здоровья сервисов

```bash
# Keycloak
curl -f http://localhost:8080/health/ready

# Employee API
curl -f http://localhost:5001/health

# Customer API
curl -f http://localhost:5002/health

# Nginx
curl -f http://localhost/health

# PostgreSQL
docker-compose exec backend-db psql -U reception_user -d reception -c "SELECT 1"

# Redis
docker-compose exec redis redis-cli ping

# MinIO
curl -f http://localhost:9000/minio/health/live
```

---

## 10. Документация по подсистемам

Дополнительная информация:
- [Clean Architecture](ARCHITECTURE_ANALYSIS.md) - детальный разбор слоев приложения
- [Диаграммы компонентов](ARCHITECTURE_DIAGRAMS.md) - визуальные представления
- [Практические примеры](PRACTICAL_EXAMPLES.md) - примеры использования API
- [Технологический стек](libs.md) - список используемых библиотек

---

## 11. Заключение

Re-ception использует:
- **Микросервисную архитектуру** для разделения ответственности (Employee vs Customer)
- **API Gateway** (Nginx) для единой точки входа и маршрутизации
- **Централизованную аутентификацию** (Keycloak) для управления доступом
- **Масштабируемую БД** (PostgreSQL) с кешированием (Redis)
- **Объектное хранилище** (MinIO) для медиа-файлов
- **Docker Compose** для оркестрации сервисов

Эта архитектура обеспечивает гибкость, масштабируемость и безопасность при управлении сложными системами бронирования в отелях.
