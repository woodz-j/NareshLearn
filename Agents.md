# NareshLearn Agent Instructions

## Project Overview
NareshLearn is a fullstack LMS built with ASP.NET Core and Angular.

Backend:
- ASP.NET Core Web API
- Clean Architecture with feature-based vertical slices
- EF Core + SQLite
- JWT authentication
- Role-based authorization
- Docker support
- GitHub Actions CI/CD
- Azure App Service deployment

Frontend:
- Angular standalone components
- Feature-based structure
- AuthService, ApiService
- JWT stored in localStorage
- Auth interceptor
- Auth and instructor guards

## Repository Structure
- /backend - .NET API
- /frontend - Angular app

## Backend Commands
From repo root:

``bash
dotnet restore backend/NareshLearn.sln
dotnet build backend/NareshLearn.sln
dotnet test backend/tests/NareshLearn.UnitTests/NareshLearn.UnitTests.csproj
dotnet run --project backend/src/NareshLearn.Api

## Frontend Commands

From /frontend:
npm install
ng serve
ng build --configuration production

## Architecture Rules
Keep Domain framework-free.
Keep Application dependent only on Domain and abstractions.
Infrastructure implements repositories and EF Core persistence.
API handles HTTP, auth, DI, and controller routing.
Use feature folders for vertical slices.
Do not introduce ASP.NET Identity.
Keep JWT/custom auth approach.
Prefer small focused changes.

## Testing Rules
Backend unit tests use xUnit, FluentAssertions, and Moq.
CI currently runs unit tests only.
Do not add integration tests to CI unless they are CI-safe.

## Current Important Features
Register/Login JWT auth
Role-based authorization
Public course list
Instructor/Admin course creation
Angular login
Angular course list
Angular create course page
Environment switching local/Azure

## Next Recommended Tasks
Add Angular Register page.
Add Course publish workflow.
Only show published courses publicly.
Add Instructor ownership checks.
Add student enrollment flow.

## General instructions
Be concise and mindful of context/token usage. Avoid unnecessary explanations. If the conversation becomes long or context starts to degrade, explicitly recommend when to start a new chat and provide a handoff summary.

- Be concise.
- Minimise token usage where possible.
- Focus on implementation rather than lengthy explanations.
- If context becomes large or task history is no longer helping, tell me to start a new chat.
- When recommending a new chat, provide:
  1. Current status
  2. Files changed
  3. Outstanding tasks
  4. Suggested first prompt for the next chat

## Reference Requirements

Before proposing roadmap items or implementing new features, review:

docs/job-specs/client-requirements.md