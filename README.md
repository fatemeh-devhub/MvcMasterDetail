# ASP.NET Core Web API Master Detail Project

A Master-Detail order management Web API built with ASP.NET Core.

## Technologies

* ASP.NET Core Web API (.NET 9)
* Entity Framework Core 9
* SQL Server
* RESTful API
* Swagger / OpenAPI
* Repository Pattern
* DTO Pattern
* Application Service Layer
* Dependency Injection
* ASP.NET Core Identity
* JWT Authentication

## Features

* Manage Orders
* Create orders with multiple order details
* Manage Customers
* Manage Sellers
* Manage Products
* Edit and update orders
* Calculate order total price
* Soft Delete
* JWT-based authentication and authorization
* Role-based authorization
* RESTful API endpoints

## Architecture

The project follows a layered architecture:

* Controllers
* Application Services
* DTOs
* Repository Layer
* Domain Models
* Entity Framework Core
* Infrastructure

## Database

SQL Server is used as the database engine.

Entity Framework Core is used for database access and entity configuration.

## Authentication & Authorization

The API uses ASP.NET Core Identity and JWT (JSON Web Token) authentication.

* User registration and authentication
* Role management
* JWT token generation
* Role-based authorization
* Protected API endpoints

## API Documentation

Swagger / OpenAPI is used to document and test the API endpoints.

After running the project, Swagger UI can be accessed from:

`/swagger`

## Project Structure

```text
MasterDetailSample01
│
├── ApplicationServices
├── Controllers
├── Infrastructure
│   └── Stored_Procedure
├── Migrations
├── Models
├── Properties
├── ResponseFrameworks
├── appsettings.json
├── appsettings.Development.json
├── Program.cs
└── MasterDetailSample01.csproj
```

## Database Operations

The project uses Entity Framework Core together with SQL Server.

Stored procedures are also used for selected database operations such as order insertion and soft deletion.

## Project Goal

The main goal of this project is to implement a scalable Master-Detail order management Web API using ASP.NET Core, Entity Framework Core, Repository and Application Service patterns, along with authentication and authorization.
