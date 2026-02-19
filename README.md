# NareshLearn LMS --- Backend (Clean Architecture, .NET, EF Core, SQLite)

## Overview

NareshLearn is a public SaaS Learning Management System backend built
using ASP.NET Core and Clean Architecture principles. The project
demonstrates professional backend engineering practices including
domain-driven design, layered architecture, EF Core persistence, and
unit testing.

This repository represents a production-grade backend foundation capable
of supporting authentication, course management, enrollments, and full
frontend integration.

------------------------------------------------------------------------

## Architecture

The solution follows Clean Architecture with strict separation of
concerns:

    NareshLearn/
     ├─ Domain           → core business entities and rules
     ├─ Application      → use cases and interfaces
     ├─ Infrastructure   → database access and external implementations
     ├─ Api              → HTTP interface and dependency injection
     └─ Tests            → unit and integration tests

Benefits:

-   High maintainability
-   Testability
-   Scalability
-   Framework independence at core layers

------------------------------------------------------------------------

## Domain Layer

Implements core business logic and rules.

Features:

-   User entity with encapsulated state
-   Role enum (Student, Instructor, Admin)
-   Domain-level validation and invariant protection
-   AuditableEntity base class providing:
    -   GUID identifiers
    -   Creation timestamps
    -   Update tracking
-   DomainException for business rule enforcement

------------------------------------------------------------------------

## Application Layer

Implements business use cases and abstractions.

Implemented:

### RegisterUserService

Responsibilities:

-   Validates business rules
-   Prevents duplicate email registration
-   Hashes passwords via abstraction
-   Uses repository abstraction
-   Returns structured result types

Interfaces:

-   IUserRepository
-   IPasswordHasher

------------------------------------------------------------------------

## Infrastructure Layer

Implements persistence and external dependencies using EF Core and
SQLite.

Features:

-   AppDbContext configured with EF Core
-   SQLite database integration
-   User entity Fluent API configuration
-   UserRepository implementation using EF Core
-   Database migrations enabled
-   Persistent user storage

Database table:

    Users
     ├─ Id
     ├─ FirstName
     ├─ LastName
     ├─ Email (Unique)
     ├─ PasswordHash
     ├─ Role
     ├─ CreatedAtUtc
     └─ UpdatedAtUtc

------------------------------------------------------------------------

## API Layer

Implements RESTful HTTP endpoints.

Implemented endpoint:

    POST /api/auth/register

Capabilities:

-   Accepts registration requests
-   Validates business logic
-   Persists users
-   Returns structured responses

Swagger/OpenAPI enabled for documentation and testing.

------------------------------------------------------------------------

## Testing

Unit testing implemented using:

-   xUnit
-   FluentAssertions
-   Moq

Tests cover:

-   Domain validation
-   Business use cases
-   Service logic correctness

------------------------------------------------------------------------

## Technology Stack

Backend:

-   .NET 8
-   ASP.NET Core Web API
-   Entity Framework Core
-   SQLite
-   Clean Architecture

Testing:

-   xUnit
-   FluentAssertions
-   Moq

Documentation:

-   Swagger / OpenAPI

------------------------------------------------------------------------

## Current Capabilities

-   Persistent user registration
-   Role assignment
-   Duplicate email prevention
-   Domain validation
-   Database persistence
-   REST API endpoint
-   Swagger API documentation
-   Unit tested business logic

------------------------------------------------------------------------

## Future Roadmap

Planned features:

-   JWT authentication
-   Login endpoint
-   Role-based authorization
-   Course management
-   Enrollment system
-   Angular frontend integration
-   Integration tests
-   Docker support
-   CI/CD pipeline

------------------------------------------------------------------------

## Purpose

This project was built as part of a professional reskilling effort to
demonstrate enterprise-grade .NET backend development skills suitable
for full-stack .NET roles.

It showcases real-world architecture patterns used in modern production
systems.
