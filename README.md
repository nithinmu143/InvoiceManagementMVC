# InvoiceManagementMVC — ASP.NET Core MVC (Database-First)

## Overview
**InvoiceManagementMVC** is a business-style invoice management application built with **ASP.NET Core MVC**, **SQL Server**, and **Entity Framework Core (Database-First)**.  
It demonstrates practical CRUD workflows for **invoices and line items**, including **computed totals**, **LINQ-based filtering**, and a clean **Razor-based UI**—implemented with an emphasis on maintainability and clear separation between generated and custom code.

## Tech Stack
- **C# / .NET 10**
- **ASP.NET Core MVC**
- **SQL Server**
- **Entity Framework Core (Database-First / Scaffolding)**
- **LINQ**
- **Razor Views**

## Key Features
- **Database-First integration** with Entity Framework Core scaffolding from an existing SQL Server schema  
- **Invoice and line-item management** (one-to-many relationships)  
- Full **CRUD workflows** with **automatic totals calculation**  
- **LINQ queries** for filtering, sorting, summaries, and server-side calculations  
- **Razor UI** for invoice creation, editing, details, and overview screens  
- **Partial class extensions** to add business logic while keeping scaffolded code clean

## Architecture Notes
- **Database-First scaffolding** to generate models and DbContext from SQL Server  
- Clear separation between:
  - scaffolded (generated) code
  - developer-authored logic and extensions  
- **MVC pattern** with strongly typed models and views  
- LINQ for **projection**, **aggregation**, and **query composition**  
- Razor Views for structured, maintainable UI rendering

## Skills Demonstrated
ASP.NET Core MVC · C# · .NET 10 · SQL Server · Entity Framework Core · Database-First · LINQ · Razor Views · CRUD Workflows · Model Extensions

## Project Status
Educational, business-oriented project demonstrating **Database-First workflows** and **invoice-style domain logic**, refined for clarity, structure, and maintainability.
