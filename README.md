# Urban System Web Application

## Overview

The **Urban System Web Application** is a modular and scalable project designed for efficient urban management. It provides a robust architecture with a well-defined separation of concerns, making it easy to maintain, extend, and customize. It integrates ASP.NET Core Identity for user authentication and authorization, with a focus on enhancing collaboration and organization.

---

## Project Structure

### Top-Level Directories
- **`ServiceTests/`**: Contains unit tests for verifying the functionality of service classes.
- **`UrbanSystem.Data.Models/`**: Defines the core data models used across the application.
- **`UrbanSystem.Common/`**: Houses shared utilities and constants.
- **`UrbanSystem.Data/`**: Contains database-related configurations, repositories, and migration files.
- **`UrbanSystem.Services.Data/`**: Implements the business logic and core services of the application.
- **`UrbanSystem.Web/`**: The main web application, including controllers, views, and other web-specific configurations.
- **`UrbanSystem.Web.Helpers/`**: Provides helper classes and utilities for web-related functionalities.
- **`UrbanSystem.Web.Infrastructure/`**: Includes middleware and other infrastructure-level components.
- **`UrbanSystem.Web.ViewModels/`**: Contains view models for passing data between the controllers and views.

---

## UrbanSystem.Web

### Key Components

#### 1. **Controllers**
Located in `UrbanSystem.Web/Controllers`, this folder defines the routing and logic for handling HTTP requests.

- Each controller corresponds to a specific domain (e.g., `Location`, `Meeting`, or `Project`) and manages the interaction between the user interface and the underlying services.

#### 2. **Views**
The `UrbanSystem.Web/Views` directory contains Razor views for rendering the user interface. These views work closely with controllers to display data dynamically. Views are organized by feature to ensure maintainability.

#### 3. **Middleware**
Custom middleware components are placed under `UrbanSystem.Web.Infrastructure`. These include:
- Exception handling middleware.
- Custom authorization handlers.

#### 4. **Static Files**
Static resources such as CSS, JavaScript, and images are stored in `UrbanSystem.Web/wwwroot`.

#### 5. **Program.cs**
The entry point of the application, configuring the middleware pipeline, routing, dependency injection, and services.

---

## Data Models

The `UrbanSystem.Data.Models` folder contains the following entities:

1. **ApplicationUser**
   - Extends `IdentityUser<Guid>` for user management.
   - Includes additional properties for suggestions, meetings, and comments.
   - Supports relationships with entities such as `Suggestion`, `Meeting`, and `Comment`.

2. **Comment**
   - Represents user comments associated with suggestions or meetings.

3. **Meeting**
   - Tracks meeting details, attendees, and organizers.

4. **Suggestion**
   - Captures user-submitted ideas and suggestions for urban development.

5. **ApplicationUserSuggestion**
   - Manages many-to-many relationships between users and suggestions.

---

## Configuration

### Application Settings
- **`appsettings.json`**: Default configuration for the application, including database connection strings and application-specific settings.
- **`appsettings.Development.json`**: Environment-specific configurations for local development.

### Identity Configuration
ASP.NET Core Identity is configured to use `ApplicationUser` and `Guid` as the key type. Example setup in `Program.cs`:
```csharp
services.AddIdentity<ApplicationUser, IdentityRole<Guid>>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();
```

---

# Database
## Entity Framework Core
- **Database Context**: **`ApplicationDbContext`** defines the DB schema and handles relationships between entities.
- **Migrations**: Managed using EF Core migrations.
-- Add a migration:
  ```bash
  Add-Migration {MigrationName}
-- Apply migrations:
  ```bash
  Update-Database
```

---

# Testing
The ServiceTests directory includes comprehensive test cases for the service layer. Each service has its corresponding test file to ensure accuracy and reliability.

Example tests:
- **`LocationServiceTests.cs`**
- **`MeetingServiceTests.cs`**
- **`ProjectServiceTests.cs`**

---

# Development
## Prerequisites
- **.NET SDK**: Ensure the latest version of .NET SDK is installed.
- **Database**: Configure your database connection string in **`appsettings.json`**

---

# Additional Features
## User Roles
The application supports role-based access control using custom roles:
- Roles are defined by extending **`IdentityRole<Guid>`**.
- Users can be assigned roles for enhanced permissions management.

## Lazy Loading
Navigation properties in **`ApplicationUser`** and other models are virtual, supporting lazy loading when configured:
```csharp
services.AddDbContext<ApplicationDbContext>(options =>
    options.UseLazyLoadingProxies().UseSqlServer(connectionString));
```

## DTOs
Data transfer objects are used for API communication to prevent circular references and ensure secure data handling. They are found in the **`UrbanSystem.Web.ViewModels`** class library.
