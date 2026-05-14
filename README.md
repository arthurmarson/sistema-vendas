# Sales System (ASP.NET Core MVC)

Study web application for a sales system, built during a Brazilian C# DDD course. The project uses ASP.NET Core MVC with Entity Framework Core and MySQL to manage customers, products, categories, and sales, plus a simple sales report. It is a learning repository focused on practicing CRUD, DDD, relationships, and sales flow.

---

## Features

- ✅ Simple session-based authentication (login with user/password)
- ✅ Category CRUD
- ✅ Customer CRUD
- ✅ Product CRUD
- ✅ Sales registration and editing with items
- ✅ Sales report by product (chart)
- 🚧 Full DDD layers (e.g., Application/Domain) — still monolithic

---

## Tech Stack

- **.NET 8 / ASP.NET Core MVC**
- **Entity Framework Core**
- **MySQL** (via `Pomelo.EntityFrameworkCore.MySql`)
- **Razor Views**
- **Session-based auth**
- **Bootstrap/JS** (assets in `wwwroot`)

---

## Project Structure

```text
/Controllers        -> MVC controllers (routes and actions)
/Services           -> Business rules and EF Core access
/Entities           -> Domain entities (Category, Customer, Product, Sale, User)
/Models             -> ViewModels and helper models
/DAL                -> DbContext and entity configuration
/Views              -> Razor Views (UI screens)
/wwwroot            -> Static assets (css/js/img)
```

---

## Getting Started

### Prerequisites

- .NET SDK **8.0**
- MySQL **8.x**
- EF Core tool (optional):
  ```bash
  dotnet tool install --global dotnet-ef
  ```

### Database setup

The default connection string is in `appsettings.json`:

```json
"ConnectionStrings": {
  "ApplicationDbContext": "server=localhost;userid=root;password=12345;database=estoque"
}
```

> Update user, password, and database name to match your environment.

### Migrations

There are no migrations in the repository yet. If you want to generate them:

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### Run the project

```bash
dotnet restore
dotnet run
```

Default URLs:

- http://localhost:5238
- https://localhost:7198

(Defined in `Properties/launchSettings.json`)

---

## Configuration

- **appsettings.json**: logging, connection string
- **appsettings.Development.json**: development logging
- **Session** enabled in `Program.cs`
- **Culture** forced to `en-US` (number formatting)

---

## API / UI

This is a **traditional MVC** project, not a Web API. Main screens:

- `/Login`
- `/Categoria`
- `/Cliente`
- `/Produto`
- `/Venda`
- `/Relatorio`

Auxiliary JSON endpoint:

- `GET /Venda/LerValorProduto/{id}` → returns product price

---

## Architecture Notes

- **Monolithic MVC** architecture
- Service layer (`Services`) for business rules
- EF Core with a **single DbContext**
- Many-to-many relationship between Sales and Products via `VendaProdutos`
- Simple **Session-based** authentication, no Identity/Claims
- Password hashing via **MD5** (educational use, not recommended for production)

---

## Contributing

Contributions are welcome. Open an issue or send a pull request with improvements, fixes, or suggestions.

---

## License

This repository currently does not specify a license.
