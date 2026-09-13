# Enhanzer Purchase Bill System

A full-stack Purchase Bill Management System developed as part of the Enhanzer Full Stack Developer Assignment.

The application provides user authentication, user-location synchronization, purchase bill item management, automatic calculations, validation, and database persistence.

The system is developed using Angular for the frontend, ASP.NET Core Web API for the backend, and Microsoft SQL Server Express for data storage.

---

## Technologies Used

### Frontend

- Angular
- TypeScript
- HTML5
- SCSS
- Angular Forms
- Angular HttpClient
- Angular Router
- Angular Route Guards

### Backend

- ASP.NET Core Web API
- .NET 8
- C#
- Entity Framework Core
- Entity Framework Core Migrations

### Database

- Microsoft SQL Server Express
- SQL Server Management Studio (SSMS)

---

## Project Structure

```text
EnhanzerPurchaseBill/
│
├── backend/
│   └── PurchaseBill.Api/
│       ├── Controllers/
│       │   ├── LocationDetailsController.cs
│       │   └── PurchaseBillItemsController.cs
│       │
│       ├── Data/
│       │   └── ApplicationDbContext.cs
│       │
│       ├── DTOs/
│       │   ├── ExternalLocation.cs
│       │   ├── ExternalLoginResponse.cs
│       │   ├── ExternalUser.cs
│       │   └── LoginRequest.cs
│       │
│       ├── Migrations/
│       │
│       ├── Models/
│       │   ├── LocationDetail.cs
│       │   └── PurchaseBillItem.cs
│       │
│       ├── Services/
│       │
│       ├── Program.cs
│       ├── appsettings.json
│       └── PurchaseBill.Api.csproj
│
├── database/
│   └── PurchaseBillDb.sql
│
├── frontend/
│   ├── src/
│   │   └── app/
│   │       ├── guards/
│   │       │   └── auth-guard.ts
│   │       │
│   │       ├── location-details/
│   │       │
│   │       ├── login/
│   │       │
│   │       ├── models/
│   │       │   ├── location-detail.ts
│   │       │   └── purchase-bill-item.ts
│   │       │
│   │       ├── purchase-bill/
│   │       │
│   │       ├── services/
│   │       │   ├── auth.ts
│   │       │   ├── location-detail.service.ts
│   │       │   └── purchase-bill-item.service.ts
│   │       │
│   │       ├── app.config.ts
│   │       ├── app.html
│   │       ├── app.routes.ts
│   │       └── app.ts
│   │
│   ├── package.json
│   └── README.md
│
└── README.md