# InvoiceManagementMVC

### ASP.NET Core MVC invoice management application with EF Core Database-First, SQL Server, relational invoice data, and computed totals

InvoiceManagementMVC is a **.NET 10 / ASP.NET Core MVC** application built around an existing SQL Server invoice schema using Entity Framework Core Database-First.

The application provides invoice CRUD workflows, works with a one-to-many relationship between invoices and invoice items, and calculates invoice totals through custom partial-class extensions while keeping scaffolded persistence models separate from developer-authored logic.

---

## Application Flow

```text
Browser
   ↓
ASP.NET Core MVC
   ↓
InvoicesController
   ↓
InvoicesContext
   ↓
Entity Framework Core
   ↓
SQL Server
```

The application follows the standard MVC request flow, with Razor views using strongly typed invoice models and the controller accessing the Database-First EF Core context through dependency injection.

---

## Database-First Data Model

The persistence model is based on an existing SQL Server schema and represented through scaffolded Entity Framework Core entities and `InvoicesContext`.

```text
Invoice
├── InvoiceNumber
├── DateOfIssue
└── InvoiceItems
        │
        └── 1 : many
                ↓
            InvoiceItem
            ├── Id
            ├── InvoiceNumber
            ├── Title
            ├── Quantity
            └── Price
```

An invoice can contain multiple invoice items through the `InvoiceNumber` foreign key.

The project keeps database-generated entity definitions separate from custom application behavior by using C# partial classes.

---

## Invoice Workflows

The MVC application provides workflows for:

- listing invoices
- creating invoices
- viewing invoice details
- editing invoices
- deleting invoices

The application opens directly on the invoice list rather than the default ASP.NET Core template page.

Invoice deletion also removes associated invoice items before deleting the invoice, preserving the existing foreign-key relationship without changing the Database-First schema to use cascade deletion.

---

## Data Loading & Totals

The invoice list displays totals calculated from related invoice items.

Related data is loaded through eager loading:

```csharp
_context.Invoices
    .Include(invoice => invoice.InvoiceItems)
    .AsNoTracking()
```

Using `Include()` avoids issuing a separate query for the invoice items of every invoice.

For read-only queries, `AsNoTracking()` is used where entity tracking is not required.

The resulting flow is:

```text
Invoices
   ↓
Include(InvoiceItems)
   ↓
EF Core
   ↓
Invoice + related items
   ↓
computed total
```

---

## Partial-Class Business Extensions

The scaffolded entities are declared as partial classes, allowing custom logic to remain outside the generated model files.

`InvoiceItem` calculates the total for an individual line:

```csharp
public decimal? ItemTotal()
{
    return Quantity * Price;
}
```

`Invoice` then aggregates its related items:

```csharp
public decimal? Total()
{
    decimal? total = 0;

    foreach (var item in InvoiceItems)
    {
        total += item.ItemTotal();
    }

    return total;
}
```

This keeps custom calculation logic separate from code that may be regenerated from the database schema.

---

## Configuration

The SQL Server connection string is provided through ASP.NET Core configuration:

```text
ConnectionStrings:InvoicesDB
```

Database configuration is registered in `Program.cs` and passed to `InvoicesContext` through `DbContextOptions`.

The connection string is therefore no longer hardcoded inside the scaffolded `DbContext`.

The default local configuration uses:

```text
Server: .\SQLEXPRESS
Database: invoices
Authentication: Windows Integrated Security
```

---

## Database Setup

A reproducible local SQL Server schema is provided through:

```text
database/setup.sql
```

The script creates the `invoices` database and the required tables when they do not already exist:

```text
Invoices
   1
   │
   └─────── *
          InvoiceItem
```

The setup preserves the same one-to-many relationship represented by the Database-First EF Core model.

---

## Running Locally

### Prerequisites

- .NET 10 SDK
- SQL Server or SQL Server Express

### 1. Prepare the database

Run:

```text
database/setup.sql
```

against the local SQL Server instance.

### 2. Restore dependencies

```bash
dotnet restore
```

### 3. Run the application

```bash
dotnet run --project InvoiceManagementMVC/InvoiceManagementMVC.csproj
```

Default development HTTPS address:

```text
https://localhost:7197
```

The application opens directly on the invoice overview.

---

## Technology Stack

**Backend**  
C# · .NET 10 · ASP.NET Core MVC

**Data**  
Entity Framework Core · SQL Server · Database-First · LINQ

**UI**  
Razor Views · Bootstrap

**Data Modeling**  
One-to-Many Relationships · EF Core Scaffolding · Partial Classes

---

## Design Scope

InvoiceManagementMVC is intentionally a **focused Database-First MVC application** rather than a layered enterprise system.

Key design choices include:

- EF Core models reverse-engineered from an existing SQL Server schema;
- direct `DbContext` usage from the MVC controller;
- eager loading of invoice items for invoice totals;
- `AsNoTracking()` for read-only queries;
- partial classes for custom calculations outside scaffolded entity files;
- explicit dependent-item deletion that preserves the existing database relationship;
- a SQL setup script for reproducible local development.

The current UI provides CRUD operations for invoices while invoice items primarily represent the related data used for invoice totals.

A larger invoice system could introduce dedicated invoice-item workflows, service and repository layers, validation rules, authentication and authorization, automated testing, migrations or independent database deployment, and additional accounting or payment functionality as those requirements become justified.

---

## License

This project is licensed under the [MIT License](LICENSE).
