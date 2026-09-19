# Invoice Management System

A web-based Invoice Management System built using ASP.NET Core MVC, C#, Entity Framework Core, and Microsoft SQL Server.

The application allows users to create and manage invoices, add invoice items, calculate totals automatically, search invoices, and manage invoice records through a simple web interface.

## Features

- Create invoices
- View invoice details
- Edit invoices
- Delete invoices
- Add invoice items
- Edit invoice items
- Delete invoice items
- Automatic item total calculation
- Automatic invoice total calculation
- Input validation
- Search invoices by invoice number
- Search invoices by item name
- Dashboard with invoice statistics
- SQL Server database integration
- Entity Framework Core ORM
- One-to-many Invoice → Invoice Items relationship
- Responsive Bootstrap-based UI

## Technologies Used

### Backend
- C#
- ASP.NET Core MVC
- .NET 10
- Entity Framework Core

### Database
- Microsoft SQL Server
- SQL
- Relational database design

### Frontend
- HTML5
- CSS3
- Razor Views
- Bootstrap
- JavaScript

### Development Tools
- Visual Studio Code
- SQL Server Management Studio
- Git
- GitHub

## Application Architecture

The application follows the MVC architecture:

```text
Browser
   |
   v
ASP.NET Core MVC
   |
   +-------------------+
   |                   |
   v                   v
Controller           View
   |
   v
Entity Framework Core
   |
   v
SQL Server

## Screenshots

### Dashboard
![Dashboard](screenshots/dashboard.png)

### Invoice Details
![Invoice Details](screenshots/invoice-details.png)

### Item Management
![Item Management](screenshots/item-management.png)