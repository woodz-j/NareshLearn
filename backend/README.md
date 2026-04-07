# NareshLearn LMS — Backend (Clean Architecture + Vertical Slices)

## 🚀 Overview

NareshLearn is a SaaS Learning Management System (LMS) backend built with **ASP.NET Core**, demonstrating **Clean Architecture combined with feature-based vertical slices**.

It is designed as a **production-grade, portfolio-ready system** that showcases modern backend engineering practices used in real-world products.

### Current Capabilities
- JWT authentication (custom implementation)
- Role-based authorization (Student / Instructor / Admin)
- Public course listing
- Instructor-only course creation
- EF Core persistence with SQLite
- Unit-tested application layer

### 🔭 Roadmap (visible future direction)
- Angular frontend integration
- Integration tests (end-to-end)
- Docker containerization
- CI/CD pipeline (GitHub Actions / Azure DevOps)
- Course publishing workflow
- Instructor ownership rules
- Enrollment system

---

## 🧱 Architecture

The project follows **Clean Architecture** with strict separation of concerns:

```
NareshLearn/
 ├─ Domain           → business rules and entities
 ├─ Application      → use cases (vertical slices)
 ├─ Infrastructure   → EF Core, persistence, external services
 ├─ Api              → HTTP layer, authentication, DI
 └─ Tests            → unit and integration tests
```

### Dependency Rule

```
Domain
↑
Application
↑
Infrastructure
↑
API
```

- Domain has **zero framework dependencies**
- Application depends only on Domain
- Infrastructure implements abstractions
- API orchestrates requests

---

## 🧩 Vertical Slice Implementation

Within Clean Architecture, the system is implemented using **feature-based vertical slices**.

Instead of grouping by technical layers, features are grouped by behavior:

```
Application/
  Auth/
    Register/
    Login/
  Courses/
    Create/
    List/
```

Each slice includes:
- Request models
- Application logic
- Domain interaction
- Persistence
- API endpoint
- Tests

---

## 🔄 Example Flow — Create Course

```
HTTP Request (POST /api/courses)
        ↓
CoursesController (Authorization + JWT extraction)
        ↓
CreateCourseService (Application logic)
        ↓
Course (Domain entity validation)
        ↓
ICourseRepository (abstraction)
        ↓
EF Core (Infrastructure)
        ↓
SQLite Database
```

---

## 🔐 Authentication & Authorization

### Authentication
- Custom JWT implementation (no ASP.NET Identity)
- Claims include:
  - `sub` (UserId)
  - `email`
  - `role`

### Authorization
- Role-based:

```csharp
[Authorize(Roles = "Instructor,Admin")]
```

- Policy-based (extensible):

```csharp
[Authorize(Policy = "InstructorOnly")]
```

---

## 📦 Features Implemented

### Authentication
- User registration
- Login with JWT token generation
- Password hashing abstraction

### Authorization
- Role-based access control
- Protected endpoints

### Courses
- Public listing: `GET /api/courses`
- Instructor/Admin creation: `POST /api/courses`
- Course persistence via EF Core

---

## 🗄️ Database

SQLite via EF Core.

### Users Table
- Id (GUID)
- FirstName
- LastName
- Email (Unique)
- PasswordHash
- Role
- CreatedAtUtc

### Courses Table
- Id (GUID)
- Title
- Description
- InstructorId
- IsPublished
- CreatedAtUtc

---

## 🧪 Testing

Unit testing with:
- xUnit
- FluentAssertions
- Moq

Covers:
- Domain validation
- Application use cases
- Repository interactions

---

## ⚙️ Tech Stack

- .NET (ASP.NET Core Web API)
- Entity Framework Core
- SQLite
- JWT Authentication
- Swashbuckle (Swagger)

---

## 🧠 Key Design Decisions

### Why NOT ASP.NET Identity?
- Keeps Domain layer clean
- Full control over authentication
- Better alignment with API-first SaaS systems

### Why Clean Architecture?
- Separation of concerns
- Testability
- Scalability

### Why Vertical Slices?
- Feature isolation
- Easier maintenance
- Clear business logic boundaries

### Why SQLite?
- Lightweight for development
- Easy to migrate to SQL Server/Postgres later

---

## ▶️ Running the Project

```bash
dotnet restore
dotnet build
dotnet run --project src/NareshLearn.Api
```

Open Swagger:

```
http://localhost:xxxx/swagger
```

---

## 🧭 Future Direction

The project is intentionally evolving toward a **full-stack SaaS LMS platform** with:

- Angular frontend
- Secure authentication flows
- Course lifecycle management
- Enrollment and progress tracking
- Cloud deployment and CI/CD

---

## 🎯 Purpose

This project was built as part of a professional reskilling effort to demonstrate:

- Modern .NET backend engineering
- Clean Architecture + vertical slices
- Real-world API design
- Production-ready authentication systems

It represents a **portfolio-quality backend system** suitable for roles in full-stack development and AI-enabled product engineering.

