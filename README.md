# NareshLearn — Fullstack LMS (Angular + .NET)

## 🚀 Overview

NareshLearn is a fullstack Learning Management System (LMS) built using:

- **Backend:** ASP.NET Core (.NET) with Clean Architecture
- **Frontend:** Angular (modern standalone components)

The project demonstrates how to build a **production-style SaaS application** with:

- JWT authentication
- Role-based authorization
- Feature-based vertical slices
- Clean separation of concerns

---

## 🧱 Project Structure

```
NareshLearn/
├─ backend/      → ASP.NET Core Web API
├─ frontend/     → Angular application
├─ README.md     → Fullstack overview
```

### Backend

```
backend/
 ├─ src/
 ├─ tests/
 └─ NareshLearn.sln
```

- Clean Architecture
- Domain-driven design
- EF Core + SQLite
- JWT authentication

### Frontend

```
frontend/
 ├─ src/app/
 │   ├─ core/
 │   ├─ shared/
 │   └─ features/
```

- Feature-based structure
- Standalone components
- HTTP interceptor for JWT
- Route guards for auth/roles

---

## 🔐 Features Implemented

### Authentication
- User registration
- Login with JWT
- Token storage in frontend
- Auth interceptor (Angular)

### Authorization
- Role-based access (Student / Instructor / Admin)
- Protected API endpoints
- Frontend route guards

### Courses
- Public course listing (`GET /api/courses`)
- Instructor/Admin course creation (`POST /api/courses`)

---

## 🔄 Fullstack Flow

```
Angular UI
   ↓
HTTP Request
   ↓
ASP.NET API
   ↓
Application Layer (use case)
   ↓
Domain Entity
   ↓
EF Core
   ↓
SQLite DB
```

---

## ▶️ Running the Project

### Backend

```bash
dotnet run --project backend/src/NareshLearn.Api
```

Swagger:

```
http://localhost:5149/swagger
```

### Frontend

```bash
cd frontend
ng serve
```

App:

```
http://localhost:4200
```

---

## ⚙️ Tech Stack

### Backend
- .NET (ASP.NET Core Web API)
- Entity Framework Core
- SQLite
- JWT Authentication
- Swagger (Swashbuckle)

### Frontend
- Angular
- TypeScript
- Reactive Forms
- HTTP Interceptors

---

## 🧠 Architecture Highlights

### Clean Architecture
- Domain isolated from frameworks
- Application contains use cases
- Infrastructure handles persistence

### Vertical Slice Design
- Features built end-to-end
- Minimal coupling between features
- Easy to extend and maintain

### Custom Authentication
- No ASP.NET Identity
- Full control over JWT and claims

---

## 🔭 Roadmap

- Angular UI enhancements (dashboard, UX polish)
- Course publishing workflow
- Instructor ownership rules
- Enrollment system
- Integration tests
- Docker support
- CI/CD pipeline
- Cloud deployment (Azure)

---

## 🎯 Purpose

This project was built to demonstrate:

- Fullstack engineering capability
- Modern .NET backend architecture
- Angular frontend integration
- Real-world authentication and authorization patterns

It is designed as a **portfolio-quality system** suitable for senior fullstack and product engineering roles.

