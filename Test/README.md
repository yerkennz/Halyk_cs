# Тестовое задание для C# разработчика

Этот проект представляет собой веб-API на .NET Core для хранения и получения курсов валют с использованием Entity Framework Core и PostgreSQL. Данные загружаются из публичного API Национального Банка Казахстана.

## Структура проекта

- `Controllers/CurrencyController.cs`: Содержит конечные точки API для сохранения и получения данных о валюте.
- `Models/Currency.cs`: Определяет модель валюты.
- `Data/TestDbContext.cs`: Настраивает контекст базы данных.
- `Startup.cs`: Настраивает сервисы и конвейер запросов приложения.
- `Program.cs`: Точка входа для приложения.

## Требования

- .NET Core SDK 6.0 или новее
- База данных PostgreSQL

## Настройка

### Шаг 1: Клонируйте репозиторий

```bash
git clone https://github.com/yerkennz/Halyk_cs.git
cd Test
```

### Шаг 2: Установите .NET Core SDK

Скачайте и установите .NET Core SDK с [официального сайта Microsoft](https://dotnet.microsoft.com/download).

### Шаг 3: Установите инструменты Entity Framework Core

```bash
dotnet tool install --global dotnet-ef
```

### Шаг 4: Настройка PostgreSQL

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=TEST;Username=username;Password=password"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

### Шаг 5: Примените миграции и создайте базу данных

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### Шаг 6: Запустите приложение
```bash
dotnet run
```

# Конечные точки API

## Сохранение данных о валюте

- URL: GET /currency/save/{date}

Загружает данные о валюте из Национального Банка Казахстана и сохраняет их в базу данных.

#### Параметры:

    date (обязательный): Дата, для которой загружаются данные о валюте (формат: dd.MM.yyyy).

#### Ответ:

    200 OK: Возвращает количество сохраненных записей.
    400 Bad Request: Если параметр даты отсутствует или недействителен.

### Пример запроса:

```bash
curl -X GET "http://localhost:5000/currency/save/15.03.2019"
```

### Пример ответа:

```json
{
  "count": 35
}
```

## Получение данных о валюте

- URL: GET /currency/{date}/{code}

Получает данные о валюте для указанной даты и кода валюты.

#### Параметры:

    date (обязательный): Дата, для которой получаются данные о валюте (формат: dd.MM.yyyy).
    code (необязательный): Код валюты для фильтрации данных.

#### Ответ:

    200 OK: Возвращает данные о валюте в формате JSON.
    400 Bad Request: Если параметр даты отсутствует или недействителен.

### Пример запроса:

```bash
curl -X GET "http://localhost:5000/currency/15.03.2019/AUD"
```

### Пример ответа:

```json
[
  {
    "id": 1,
    "title": "АВСТРАЛИЙСКИЙ ДОЛЛАР",
    "code": "AUD",
    "value": 267.39,
    "a_Date": "2019-03-15T00:00:00Z"
  }
]
```
