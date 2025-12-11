# AirlineCompany - Система управления авиакомпанией

Веб-приложение для управления рейсами, пассажирами и билетами авиакомпании.

## 🏗️ Архитектура

### Core (Ядро приложения)
- **Entities** - бизнес-сущности (AircraftFamily, AircraftModel, Flight, Passenger, Ticket)
- **Repositories** - интерфейсы паттерна Repository

### Infrastructure (Данные)
- **Data** - DbContext и DataSeeder с тестовыми данными
- **Migrations** - миграции базы данных
- **Repositories** - реализации репозиториев

### Application (Логика)
- **Services** - сервисы с бизнес-логикой (PassengerService, TicketService, AnalyticService)

### API (Веб-слой)
- **Controllers** - REST API контроллеры

### DTO (модели данных для запросов/ответов)

## 🚀 Функциональность

### Управление данными
- ✅ CRUD операции для всех сущностей
- ✅ Валидация и бизнес-правила
- ✅ Навигационные свойства и связи

### Аналитические отчеты
- ✅ Топ-5 рейсов по количеству пассажиров
- ✅ Рейсы с минимальной продолжительностью
- ✅ Пассажиры с нулевым багажом на рейсе
- ✅ Фильтрация рейсов по моделям и периодам
- ✅ Поиск рейсов по маршруту

## 📡 API Endpoints

### Passengers
GET /api/Passengers # Все пассажиры
POST /api/Passengers # Создать пассажира
GET /api/Passengers/{id} # Пассажир по ID
PUT /api/Passengers/{id} # Обновить пассажира
DELETE /api/Passengers/{id} # Удалить пассажира
GET /api/Passengers/{id}/tickets # Билеты пассажира

### Tickets
GET /api/Tickets # Все билеты
POST /api/Tickets # Создать билет
GET /api/Tickets/{id} # Билет по ID
PUT /api/Tickets/{id} # Обновить билет
DELETE /api/Tickets/{id} # Удалить билет
GET /api/Tickets/flight/{flightId} # Билеты рейса
GET /api/Tickets/passenger/{passengerId} # Билеты пассажира

### Analytics
GET /api/Analytics/top-five-flights # Топ-5 рейсов
GET /api/Analytics/min-duration-flights # Минимальная длительность
GET /api/Analytics/flight/{code}/zero-baggage-passengers # Пассажиры без багажа
GET /api/Analytics/model/{id}/flights # Рейсы модели
GET /api/Analytics/flights-by-route # Рейсы по маршруту

## 🛠️ Технологии

- **.NET 8** - основная платформа
- **Entity Framework Core 8** - ORM
- **ASP.NET Core Web API** - REST API
- **SQL Server** - база данных
- **Swagger** - документация API