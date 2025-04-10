
# ⚽ Football League App

A modular and scalable ASP.NET Core 5 Web API for managing football teams, matches, and rankings using Entity Framework Core, Repository pattern, FluentValidation, and proper error handling middleware.

---

## 📁 Project Structure

```
FootballLeagueApp/
│
├── FootballLeagueApp.API            # Main API project
├── FootballLeagueApp.Common         # Shared logic: exceptions, validation, helpers
├── FootballLeagueApp.DataAccess     # EF Core DbContext, Repositories
├── FootballLeagueApp.Domain         # Business logic, DTOs, Services, Validators
└── FootballLeagueApp.Tests          # Unit tests
```

---

## 🚀 Getting Started

### ✅ Prerequisites

- [.NET 5 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/5.0)
- SQL Server
- Visual Studio 2019+ or VS Code

### 🛠️ Setup Instructions

1. **Clone the Repository**

```bash
git clone https://github.com/CvetoslavYanachkov/FootballLeagueApp.git
cd FootballLeagueApp
```

2. **Configure Database**

Update the `appsettings.json` in `FootballLeagueApp.API`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=FootballLeagueDB;Trusted_Connection=True;"
}
```

3. **Apply Migrations & Run App**

```bash
cd FootballLeagueApp.API
dotnet ef database update
dotnet run
```

API runs at `https://localhost:5001` or `http://localhost:5000`.

---

## 📡 API Endpoints

### 🏟️ Match

| Method | Endpoint                        | Description                         |
|--------|---------------------------------|-------------------------------------|
| GET    | `/api/Match/get-match`          | Retrieves a match by its ID         |
| GET    | `/api/Match/get-matches`        | Retrieves all played matches        |
| POST   | `/api/Match/create-match`       | Creates a new match                 |
| PUT    | `/api/Match/update-match`       | Updates an existing match           |
| DELETE | `/api/Match/delete-match`       | Deletes a match by its ID           |

### 📈 Ranking

| Method | Endpoint                             | Description                                    |
|--------|--------------------------------------|------------------------------------------------|
| GET    | `/api/Ranking/get-ranking`           | Get ranking of a specific team by team ID      |
| GET    | `/api/Ranking/get-list-ranking`      | Get current ranking list of all teams          |

### 🏆 Team

| Method | Endpoint                        | Description                         |
|--------|----------------------------------|-------------------------------------|
| GET    | `/api/Team/get-team`            | Get team by ID                      |
| GET    | `/api/Team/get-teams`           | Get list of all teams               |
| POST   | `/api/Team/create-team`         | Create a new team                   |
| PUT    | `/api/Team/update-team`         | Update a team                       |
| DELETE | `/api/Team/delete-team`         | Delete a team by ID                 |

---

## ✅ Features

- Team and Match management via RESTful endpoints
- Automatic Ranking system update after matches
- Repository & Service pattern architecture
- Request validation via FluentValidation
- Centralized error handling with custom middleware
- Unit testing with xUnit

---

## 🧪 Run Tests

```bash
dotnet test FootballLeagueApp.Tests
```

---

## 📄 Technologies Used

- ASP.NET Core 5
- Entity Framework Core 5
- MS SQL Server
- FluentValidation
- Swagger (OpenAPI 3)
- xUnit for testing

---

## 📝 License

This project is licensed under the MIT License.
