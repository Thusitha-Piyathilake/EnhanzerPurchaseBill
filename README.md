# Enhanzer Purchase Bill System

A full-stack Purchase Bill Management System developed as part of the Enhanzer Full Stack Developer Assignment.

The application uses Angular for the frontend, ASP.NET Core Web API for the backend, and SQL Server for data storage.

---

## Technologies Used

### Frontend
- Angular
- TypeScript
- HTML
- SCSS
- Angular Forms
- Angular HttpClient
- Angular Route Guards

### Backend
- ASP.NET Core Web API
- .NET 8
- C#
- Entity Framework Core
- SQL Server

### Database
- Microsoft SQL Server Express

---

## Project Structure

```text
EnhanzerPurchaseBill/
│
├── frontend/
│   └── src/
│       └── app/
│           ├── guards/
│           ├── location-details/
│           ├── login/
│           ├── models/
│           ├── purchase-bill/
│           ├── services/
│           ├── app.config.ts
│           ├── app.routes.ts
│           └── app.ts
│
├── backend/
│   └── PurchaseBill.Api/
│       ├── Controllers/
│       ├── Data/
│       ├── Models/
│       ├── Migrations/
│       ├── Program.cs
│       └── appsettings.json
│
├── database/
│   └── PurchaseBillDb.sql
│
└── README.md