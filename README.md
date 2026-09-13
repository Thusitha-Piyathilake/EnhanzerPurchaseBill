# Enhanzer Purchase Bill

## 1. Technologies Used

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
- Swagger / OpenAPI

### Database
- Microsoft SQL Server Express
- SQL Server Management Studio (SSMS)

## 2. Prerequisites

Before running the application, install the following software:

- Node.js
- npm
- Angular CLI
- .NET 8 SDK
- Microsoft SQL Server Express
- SQL Server Management Studio (SSMS)
- Git

### Node.js and npm

Download and install Node.js from:

```
https://nodejs.org/
```

Verify the installation:

```bash
node --version
npm --version
```

### Angular CLI

Install Angular CLI globally:

```bash
npm install -g @angular/cli
```

Verify:

```bash
ng version
```

### .NET 8 SDK

Download and install the .NET 8 SDK from:

```
https://dotnet.microsoft.com/download/dotnet/8.0
```

Verify:

```bash
dotnet --version
```

### SQL Server Express

Install Microsoft SQL Server Express.

The application uses the SQL Server instance:

```
.\SQLEXPRESS
```

### SQL Server Management Studio

Install SQL Server Management Studio (SSMS) for database setup and management.

### Git

Download and install Git from:

```
https://git-scm.com/
```

Verify:

```bash
git --version
```

## 3. Clone the Repository

Clone the project from GitHub:

```bash
git clone https://github.com/Thusitha-Piyathilake/EnhanzerPurchaseBill.git
```

Navigate to the project:

```bash
cd EnhanzerPurchaseBill
```

## 4. Project Structure

```
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
│   │       ├── app.routes.ts
│   │       └── app.ts
│   │
│   └── package.json
│
├── README.md
└── .gitignore
```

## 5. Database Setup

The application uses Microsoft SQL Server Express.

The database setup script is included in:

```
database/PurchaseBillDb.sql
```

### Step 1: Start SQL Server Express

Make sure SQL Server Express is running.

The default SQL Server instance used by the application is:

```
.\SQLEXPRESS
```

### Step 2: Open SQL Server Management Studio

Open SQL Server Management Studio (SSMS).

Connect using:

- **Server Type:** Database Engine
- **Server Name:** `.\SQLEXPRESS`
- **Authentication:** Windows Authentication

### Step 3: Run the Database Script

Open:

```
database/PurchaseBillDb.sql
```

in SSMS.

Execute the complete script.

The script creates the database:

```
PurchaseBillDb
```

and the following tables:

- `Location_Details`
- `Purchase_Bill_Items`

### Step 4: Verify the Database

In SSMS, expand:

```
Databases
└── PurchaseBillDb
    └── Tables
```

The following tables should be available:

- `dbo.Location_Details`
- `dbo.Purchase_Bill_Items`

## 6. Backend Setup

The backend is an ASP.NET Core Web API application using .NET 8.

### Step 1: Navigate to the Backend

From the project root:

```bash
cd backend/PurchaseBill.Api
```

### Step 2: Restore Backend Dependencies

Run:

```bash
dotnet restore
```

This downloads and restores all NuGet packages required by the backend.

### Step 3: Build the Backend

Run:

```bash
dotnet build
```

### Step 4: Run the Backend

Run:

```bash
dotnet run
```

The backend API will run at:

```
http://localhost:5145
```

Swagger documentation is available at:

```
http://localhost:5145/swagger
```

Keep this terminal running.

## 7. Backend Database Configuration

The database connection string is configured in:

```
backend/PurchaseBill.Api/appsettings.json
```

The default connection string is:

```
Server=.\SQLEXPRESS;Database=PurchaseBillDb;Trusted_Connection=True;TrustServerCertificate=True;
```

If a different SQL Server instance is being used, update the connection string accordingly.

Example:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.\\SQLEXPRESS;Database=PurchaseBillDb;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

## 8. Entity Framework Core

The backend uses Entity Framework Core for database access.

The database context is located at:

```
backend/PurchaseBill.Api/Data/ApplicationDbContext.cs
```

Entity Framework Core migrations are located at:

```
backend/PurchaseBill.Api/Migrations/
```

If the Entity Framework Core CLI is required, install it using:

```bash
dotnet tool install --global dotnet-ef
```

Then migrations can be applied using:

```bash
dotnet ef database update
```

The repository also contains the SQL Server database script:

```
database/PurchaseBillDb.sql
```

## 9. Frontend Setup

The frontend is an Angular application.

Open a new terminal while keeping the backend running.

### Step 1: Navigate to the Frontend

From the project root:

```bash
cd frontend
```

### Step 2: Install Frontend Dependencies

Run:

```bash
npm install
```

This installs all dependencies defined in:

```
frontend/package.json
```

These include the Angular framework, Angular Router, Angular Forms, Angular HttpClient, RxJS, and other required packages.

### Step 3: Run the Angular Application

Run:

```bash
ng serve
```

The frontend will be available at:

```
http://localhost:4200
```

Open this URL in a web browser.

## 10. Installing All Dependencies

For a fresh project setup, install the dependencies as follows.

### Backend dependencies

Navigate to the backend:

```bash
cd backend/PurchaseBill.Api
```

Restore NuGet dependencies:

```bash
dotnet restore
```

Build the backend:

```bash
dotnet build
```

### Frontend dependencies

Open a new terminal and navigate to:

```bash
cd frontend
```

Install npm dependencies:

```bash
npm install
```

If Angular CLI is not installed:

```bash
npm install -g @angular/cli
```

Verify Angular CLI:

```bash
ng version
```

## 11. Running the Complete Application

The backend and frontend must be running at the same time.

### Terminal 1 — Backend

From the project root:

```bash
cd backend/PurchaseBill.Api
dotnet restore
dotnet build
dotnet run
```

Backend:

```
http://localhost:5145
```

Swagger:

```
http://localhost:5145/swagger
```

### Terminal 2 — Frontend

Open a new terminal:

```bash
cd frontend
npm install
ng serve
```

Frontend:

```
http://localhost:4200
```

Then open:

```
http://localhost:4200
```

in the browser.

## 12. Application Flow

The application follows this flow:

```
Open Application
       |
       v
Login Page
       |
       v
Enter Email and Password
       |
       v
Enhanzer External Authentication API
       |
       v
Authentication Successful
       |
       v
Retrieve User Locations
       |
       v
Synchronize Locations to SQL Server
       |
       v
Purchase Bill Page
       |
       v
Select Item and Batch
       |
       v
Enter Cost, Price, Quantity and Discount
       |
       v
Calculate Totals
       |
       v
Add Purchase Bill Item
       |
       v
Save to SQL Server
```

## 13. Login and Authentication

The application uses the Enhanzer external authentication API.

The external API endpoint is:

```
https://ez-staging-api.azurewebsites.net/api/External_Api/POS_Api/Invoke
```

The login request uses:

- Email as `Company_Code`
- Email as `Username`
- Password as `Pw`

After successful authentication, the application receives the user's locations.

The authenticated session is stored in the browser using `sessionStorage`.

## 14. Location Synchronization

After successful login, the application retrieves the locations assigned to the authenticated user.

The locations are synchronized into the SQL Server table:

```
Location_Details
```

The table stores:

- `Id`
- `Location_Code`
- `Location_Name`

The synchronization process prevents duplicate locations based on the location code and updates existing location names when required.

The Purchase Bill Batch dropdown uses the `Location_Name` values stored in this table.

## 15. Purchase Bill Management

The Purchase Bill page is accessible only after successful authentication.

The page provides:

- Item autocomplete
- Batch dropdown
- Standard Cost
- Standard Price
- Quantity
- Discount %
- Automatic Total Cost calculation
- Automatic Total Selling calculation
- Add Item
- Clear
- Delete Item
- Total Items summary
- Total Quantity summary

## 16. Available Items

The following items are available in the item autocomplete:

- Mango
- Apple
- Banana
- Orange
- Grapes
- Kiwi
- Strawberry

## 17. Batch Selection

The Batch dropdown is populated using the locations stored in:

```
Location_Details
```

The frontend retrieves the locations through:

```
GET /api/LocationDetails
```

## 18. Purchase Bill Calculations

The application automatically calculates the purchase bill totals.

### Total Cost

```
Total Cost =
(Standard Cost × Quantity)
-
((Standard Cost × Quantity) × Discount / 100)
```

### Total Selling

```
Total Selling =
Standard Price × Quantity
```

### Example

Given:

```
Standard Cost = 125
Standard Price = 170
Quantity = 7
Discount = 10%
```

Calculation:

```
Gross Cost = 125 × 7
           = 875

Discount = 875 × 10%
         = 87.50

Total Cost = 875 - 87.50
           = 787.50

Total Selling = 170 × 7
              = 1190.00
```

Final result:

```
Total Cost    = 787.50
Total Selling = 1190.00
```

## 19. Validation

The application performs validation on both the frontend and backend.

Validation includes:

- Email is required
- Password is required
- Item is required
- Batch is required
- Standard Cost cannot be negative
- Standard Price cannot be negative
- Quantity must be greater than 0
- Discount must be between 0 and 100

Meaningful error messages are displayed when invalid data is entered.

## 20. Authentication and Route Protection

The Purchase Bill page is protected using an Angular route guard.

The protected route is:

```
/purchase-bill
```

If an unauthenticated user attempts to access the Purchase Bill page, they are redirected to:

```
/login
```

The authenticated session is stored using:

```
sessionStorage
```

When the user logs out:

- The authentication session is removed.
- The user is redirected to the Login page.
- The protected Purchase Bill page cannot be accessed without logging in again.

## 21. Error Handling and Loading Indicators

The application handles errors related to:

- Invalid login credentials
- Missing login fields
- External authentication API failures
- Location synchronization failures
- Database/API failures
- Invalid purchase bill data
- Failed item creation
- Failed item deletion
- Missing location data

Loading indicators are displayed while asynchronous operations are being processed.

## 22. API Endpoints

### Location Details

```
GET     /api/LocationDetails
GET     /api/LocationDetails/{id}
POST    /api/LocationDetails
POST    /api/LocationDetails/sync
PUT     /api/LocationDetails/{id}
DELETE  /api/LocationDetails/{id}
```

### Purchase Bill Items

```
GET     /api/PurchaseBillItems
GET     /api/PurchaseBillItems/{id}
POST    /api/PurchaseBillItems
PUT     /api/PurchaseBillItems/{id}
DELETE  /api/PurchaseBillItems/{id}
```

## 23. Database Tables

### Location_Details

Stores locations retrieved from the external authentication API.

Main fields:

- `Id`
- `Location_Code`
- `Location_Name`

### Purchase_Bill_Items

Stores purchase bill items entered through the application.

Main fields:

- `Id`
- `Item`
- `Batch`
- `StandardCost`
- `StandardPrice`
- `Quantity`
- `Discount`
- `TotalCost`
- `TotalSelling`

## 24. Responsive Design

The frontend uses responsive SCSS and is designed for:

- Desktop
- Laptop
- Tablet
- Mobile-sized screens

The application follows a component-based Angular architecture with reusable services for API communication.

## 25. Build Verification

### Backend

Navigate to the backend:

```bash
cd backend/PurchaseBill.Api
```

Run:

```bash
dotnet build
```

The backend should build successfully without compilation errors.

### Frontend

Navigate to the frontend:

```bash
cd frontend
```

Run:

```bash
ng build
```

The Angular application should build successfully.

## 26. Troubleshooting

### Backend Does Not Start

Check the installed .NET version:

```bash
dotnet --version
```

Then run:

```bash
dotnet restore
dotnet build
dotnet run
```

### Database Connection Error

Make sure SQL Server Express is running.

Check that the SQL Server instance is:

```
.\SQLEXPRESS
```

Also check the connection string in:

```
backend/PurchaseBill.Api/appsettings.json
```

### Frontend Dependencies Are Missing

Navigate to:

```bash
cd frontend
```

Run:

```bash
npm install
```

Then:

```bash
ng serve
```



If the backend port is changed, update the Angular API service URLs to match the new backend port.

## 27. Database Deliverable

The SQL Server database script is included in:

```
database/PurchaseBillDb.sql
```

The script creates:

```
PurchaseBillDb
```

with the following tables:

- `Location_Details`
- `Purchase_Bill_Items`

The script also includes validation constraints for purchase bill data.
