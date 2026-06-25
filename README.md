# Re-ception

## Содержание

- [О проекте](#о-проекте)
- [Стек технологий](#стек-технологий)
- [Быстрый старт](#быстрый-старт)
  - [Предварительные требования](#предварительные-требования)
  - [Инструкция по развертыванию](#инструкция-по-развертыванию)
- [Важные переменные окружения](#важные-переменные-окружения)
- [Сервисы и доступ](#сервисы-и-доступ)
- [SSL-сертификат](#ssl-сертификат)
- [Настройка hosts](#настройка-hosts)
- [Проверка работоспособности](#проверка-работоспособности)
- [Полезные команды](#полезные-команды)
- [Структура проекта](#структура-проекта)

## О проекте

Re-ception — гостиничная система регистрации гостей с микросервисной инфраструктурой.

В проекте используются:

- ASP.NET Core backend для employee и customer API
- Keycloak для аутентификации и авторизации
- PostgreSQL для данных приложения и Keycloak
- Redis для кеширования
- MinIO для хранения файлов
- Nginx как HTTPS-прокси
- Terraform для настройки Keycloak realm'ов и клиентов

## Стек технологий

**Backend:**

- .NET 10.0
- ASP.NET Core
- Entity Framework Core
- HotChocolate

**Инфраструктура:**

- PostgreSQL 18
- MinIO
- Redis 8.6.3
- Keycloak 26.6.1
- Nginx 1.30
- Terraform 1.15.1

**Контейнеризация:**

- Docker
- Docker Compose

## Быстрый старт

### Предварительные требования

**Поддерживаемые ОС:**

- Linux (Ubuntu 20.04+, Debian 11+, CentOS 8+, RHEL 8+, Fedora 35+)
- macOS 11+ (Intel и Apple Silicon)
- Windows 10/11 (с WSL 2)

**Программное обеспечение:**

- Docker Engine 20.10+ (скачать с [docker.com](https://www.docker.com/products/docker-desktop))
- Docker Compose 2.0+ (обычно идет в комплекте с Docker Desktop)

Проверить установку:

```bash
docker --version
docker compose --version
```

**Системные требования:**

- **Минимум:** 4 CPU ядра, 8 GB RAM, 30 GB свободного дискового пространства
- **Рекомендуется:** 8 CPU ядер, 16 GB RAM, 50 GB свободного дискового пространства
*Примечание: Keycloak требует 2 CPU и 2 GB RAM, PostgreSQL базы данных требуют 512 MB каждая*

### Инструкция по развертыванию

1. **Создание файла окружения**

    ```bash
    ./create_env.sh
    ```

    Скрипт создаст `Program/.env` и создаст символические ссылки на него для сервисов.

2. **Настройте переменные окружения**

    Отредактируйте `Program/.env` и задайте параметры:

    - `SERVER_HOSTNAME` — домен для локального окружения, например `docker.local`
    - `ADMIN_NAME` / `ADMIN_PASSWORD` — администратор Keycloak
    - `SERVER_POSTGRES_PASSWORD` — пароль PostgreSQL приложения
    - `KC_POSTGRES_PASSWORD` — пароль PostgreSQL Keycloak
    - `MINIO_ROOT_USER` / `MINIO_ROOT_PASSWORD` — учетные данные MinIO
    - `CACHE_PASSWORD` — пароль Redis
    - `KC_SMPT_HOST`, `KC_SMPT_PORT`, `KC_SMPT_USERNAME`, `KC_SMPT_PASSWORD` — SMTP параметры для Keycloak

3. **Запустите все сервисы**:

   ```bash
   cd Program
   docker compose up -d
   ```

4. **Остановка сервисов**

   ```bash
   cd Program
   docker compose down
   ```

## Важные переменные окружения

Проект использует переменные из `Program/.env`.

- `SERVER_HOSTNAME` — основной домен, например `docker.local`
- `KEYCLOAK_HOST` — `auth.${SERVER_HOSTNAME}`
- `NGINX_DOMAIN` — `SERVER_HOSTNAME`
- `ADMIN_NAME`, `ADMIN_PASSWORD` — админ Keycloak
- `SERVER_POSTGRES_PASSWORD` — пароль к базе приложения
- `KC_POSTGRES_PASSWORD` — пароль к базе Keycloak
- `MINIO_ROOT_USER`, `MINIO_ROOT_PASSWORD` — MinIO credentials
- `CACHE_PASSWORD` — пароль Redis
- `EMPLOYEE_API_PORT` — порт employee API на хосте (`5001`)
- `CUSTOMER_API_PORT` — порт customer API на хосте (`5002`)
- `KEYCLOAK_HTTP_PORT` — порт Keycloak на хосте (`7080`)
- `ASPNETCORE_PORT` — внутренний порт ASP.NET Core сервисов (`8000`)

## Сервисы и доступ

Доступ к сервисам через Nginx и поддомены:

- `https://employee.${SERVER_HOSTNAME}` — employee API
- `https://customer.${SERVER_HOSTNAME}` — customer API
- `https://auth.${SERVER_HOSTNAME}` — Keycloak

> В текущей конфигурации фронтенд в основную `docker compose` конфигурацию напрямую не включён.

## SSL-сертификат

Перед запуском обязательно создайте директорию `./Program/Nginx/certs/` и положите туда сертификат и ключ:

- `./Program/Nginx/certs/cert.pem`
- `./Program/Nginx/certs/key.pem`

Если эти файлы отсутствуют или сертификат некорректен, Nginx не сможет запуститься и сервисы не будут работать из-за ошибки HTTPS/SSL.

## Настройка hosts

Добавьте в файл hosts:

**Linux / macOS:**

```bash
sudo nano /etc/hosts
```

**Windows:**

Откройте `C:\Windows\System32\drivers\etc\hosts` от имени администратора.

Добавьте строки:

```txt
127.0.0.1 docker.local employee.docker.local customer.docker.local auth.docker.local
```

Если используете другой домен, замените `docker.local` на значение `SERVER_HOSTNAME`.

## Проверка работоспособности

Проверьте резолвинг домена и доступность сервисов:

```bash
ping -c 3 docker.local # или ./test_host.sh
curl -k https://auth.docker.local
curl -k https://employee.docker.local
curl -k https://customer.docker.local
```

Если `./test_host.sh` сообщает ошибку, проверьте `Program/.env` и запись в `hosts`.

## Полезные команды

- Запустить сервисы: `cd Program && docker compose up -d`
- Остановить сервисы: `cd Program && docker compose down`
- Смотреть логи: `cd Program && docker compose logs -f`
- Перезапустить сервис: `cd Program && docker compose restart <service>`

## Структура проекта

- `Program/Server/` — backend API-сервисы, PostgreSQL, Redis, MinIO
- `Program/Keycloak/` — Keycloak, PostgreSQL для Keycloak, Terraform
- `Program/Nginx/` — Nginx прокси и SSL
- `Program/Frontend/` — frontend-часть (неактивирована в основном `docker compose`)
- `create_env.sh` — генерация шаблона `Program/.env`
- `test_host.sh` — проверка работы hostname`
