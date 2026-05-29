# NareshLearn Gap Analysis - May 2026

Date: 2026-05-29

Scope: analysis against `docs/job-specs/client-requirements.md`. No source, test, workflow, package, or project files were changed.

## Repository Inventory

### Current backend features

- Clean Architecture projects are present under `backend/src/NareshLearn.Domain`, `backend/src/NareshLearn.Application`, `backend/src/NareshLearn.Infrastructure`, and `backend/src/NareshLearn.Api`.
- Custom JWT authentication is configured in `backend/src/NareshLearn.Api/Program.cs`, with token generation in `backend/src/NareshLearn.Infrastructure/Auth/JwtTokenGenerator.cs`.
- Register and login API endpoints are in `backend/src/NareshLearn.Api/Controllers/AuthController.cs`.
- Register/login application services are in `backend/src/NareshLearn.Application/Auth/Register/RegisterUserService.cs` and `backend/src/NareshLearn.Application/Auth/Login/LoginUserService.cs`.
- Role-based authorization is used in `backend/src/NareshLearn.Api/Controllers/CoursesController.cs`, especially `[Authorize(Roles = "Instructor,Admin")]` for course creation and `[Authorize(Roles = "Student")]` for the placeholder enroll endpoint.
- Course domain and persistence are in `backend/src/NareshLearn.Domain/Courses/Course.cs`, `backend/src/NareshLearn.Application/Courses`, and `backend/src/NareshLearn.Infrastructure/Courses/CourseRepository.cs`.
- EF Core SQLite persistence is configured in `backend/src/NareshLearn.Infrastructure/Data/AppDbContext.cs`, entity configurations, migrations, and `backend/src/NareshLearn.Api/appsettings.json`.
- Basic profile endpoint exists at `backend/src/NareshLearn.Api/Controllers/ProfileController.cs`.
- Debug endpoint exists at `backend/src/NareshLearn.Api/Controllers/DebugController.cs`.
- Docker backend asset exists at `backend/Dockerfile`.

### Current frontend features

- Angular app lives under `frontend/src`.
- Routes are configured in `frontend/src/app/app.routes.ts`.
- Login page logic and template are in `frontend/src/app/features/auth/login`.
- Register component exists in `frontend/src/app/features/auth/register`, but the template is still placeholder text and the route is not currently active.
- Course list UI is in `frontend/src/app/features/courses/course-list`.
- Course creation UI is in `frontend/src/app/features/courses/course-create`.
- JWT token storage and role checks are in `frontend/src/app/core/auth/auth.service.ts`.
- JWT interceptor is in `frontend/src/app/core/interceptors/auth-interceptor.ts`.
- Auth and instructor guards are in `frontend/src/app/core/guards`.
- API base URL environment switching exists in `frontend/src/environments/environment.ts` and `frontend/src/environments/environment.development.ts`.

### Existing Azure deployment assets

- Backend deployment workflow: `.github/workflows/deploy-backend-azure.yml`.
- Azure publish profile reference: `backend/src/NareshLearn.Api/Properties/PublishProfiles/NareshLearnApi20260408172133 - Web Deploy.pubxml`.
- Production Angular API URL points at Azure App Service in `frontend/src/environments/environment.ts`.
- Backend Dockerfile exists at `backend/Dockerfile`, but deployment workflow publishes directly to App Service rather than building/pushing a container.

### Existing CI/CD assets

- CI workflow: `.github/workflows/ci.yml`.
- Backend Azure deployment workflow: `.github/workflows/deploy-backend-azure.yml`.
- Workflows use .NET SDK `10.0.x`.
- CI currently restores, builds, and runs backend unit tests only.

### Existing test assets

- Backend unit test project: `backend/tests/NareshLearn.UnitTests/NareshLearn.UnitTests.csproj`.
- Unit tests cover user validation, register service, and create course service in `backend/tests/NareshLearn.UnitTests/Users/UserTests.cs`, `backend/tests/NareshLearn.UnitTests/Auth/RegisterUserServiceTests.cs`, and `backend/tests/NareshLearn.UnitTests/Courses/CreateCourseServiceTests.cs`.
- Backend integration test project exists at `backend/tests/NareshLearn.IntegrationTests/NareshLearn.IntegrationTests.csproj`, but currently only contains the generated placeholder `UnitTest1.cs`.
- Angular unit/component specs exist beside core services, guards, interceptor, and feature components under `frontend/src/app/**/*.spec.ts`.
- No Playwright, axe, Lighthouse, k6, or load-test assets were found.

## 1. Current Implementation Summary

NareshLearn currently has a useful portfolio-grade baseline: a custom JWT backend, role-based API protection, Angular login, JWT interceptor, route guards, EF Core SQLite persistence, Dockerfile, and backend GitHub Actions workflows.

Key implemented areas:

- Authentication: `AuthController`, `RegisterUserService`, `LoginUserService`, `JwtTokenGenerator`, `AuthService`.
- Authorization: controller attributes in `CoursesController`, Angular guards in `frontend/src/app/core/guards`.
- Course creation and listing: `CreateCourseService`, `ListCoursesService`, `CourseRepository`, Angular course list/create pages.
- Persistence: EF Core SQLite via `AppDbContext`, configurations, migrations, and `appsettings.json`.
- CI/CD: backend build/test workflow and backend Azure App Service deployment workflow.
- Tests: focused backend unit tests plus Angular spec files.

Important current limitations:

- `CourseRepository.ListPublicAsync` returns all courses, including unpublished courses.
- `DevPasswordHasher` uses SHA-256 and is explicitly marked development-only.
- JWT signing key is present in `appsettings.json` as a placeholder and should be supplied through secure configuration.
- Swagger and debug authorization logging are enabled unconditionally in `Program.cs`.
- Register UI exists but is not implemented or routed.
- Enroll endpoint is a placeholder and does not persist enrollment.
- Audit logging, GDPR workflows, accessibility test evidence, integration tests, E2E tests, and load testing are missing.

## 2. Gap Analysis

### Implemented

- Clean Architecture and vertical slice direction are visible in `backend/src`.
- Custom JWT authentication is implemented without ASP.NET Identity.
- Role-based authorization exists for course creation and student enroll placeholder.
- Angular stores JWT in localStorage, attaches it through an interceptor, and protects instructor routes.
- EF Core SQLite is implemented with migrations and entity configurations.
- Dockerfile exists for backend API.
- GitHub Actions CI builds backend and runs backend unit tests.
- Azure App Service deployment workflow exists for backend.
- Basic frontend unit/component specs exist.

### Partially implemented

- WCAG 2.1 AA: some forms use labels and semantic inputs, for example login and course create templates, but there is no accessibility checklist, automated axe coverage, keyboard audit, focus review, contrast evidence, or screen-reader evidence. Register UI is placeholder-only.
- GDPR data handling: the domain stores limited personal data, but there is no consent tracking, privacy policy route, data export, account deletion, retention policy, or data processing audit trail.
- Secure authentication: JWT validation and role checks exist, but password hashing is not production-grade, no refresh-token/logout invalidation strategy exists, no rate limiting exists, and secrets are not externalized in local config.
- User access control: role checks exist, but instructor ownership checks are missing and course publish/modification rules are not exposed through API slices.
- Encrypted transmission: `UseHttpsRedirection` exists and Azure URL uses HTTPS, but there is no documented HTTPS-only production configuration, HSTS, secure cookie policy, or App Service TLS hardening evidence.
- Secure storage: SQLite persistence exists, but secrets and database encryption/key management are not hardened. For portfolio scope, keep SQLite now and document later Azure SQL/managed identity as production hardening.
- Audit capabilities: `AuditableEntity` tracks created/updated timestamps, but there is no audit log table or event capture for login, registration, course creation, or admin actions.
- Continuity of service: Dockerfile and Azure App Service deployment are a start, but there are no health checks, deployment slots, App Insights alerts, retry policies, load-test baselines, or scaling documentation.
- Integration testing: project exists, but meaningful WebApplicationFactory tests are missing and CI does not run the integration test project.
- Frontend testing: spec files exist, but CI does not install/build/test the Angular app.
- Azure readiness: backend deployment exists, but production configuration is not hardened and frontend deployment is not automated.

### Missing

- Implemented Angular registration page and active `/register` route.
- Published-course visibility rule in repository/API tests.
- Course publish workflow.
- Instructor ownership checks for course mutation.
- Persisted student enrollment flow.
- Production-grade password hasher such as BCrypt or Argon2 behind the existing `IPasswordHasher` abstraction.
- Rate limiting or lockout-style protection for login/register.
- Audit log model, repository, service, and API/admin retrieval path.
- GDPR consent/data export/account deletion/retention documentation and implementation.
- Security headers and production-only Swagger configuration.
- HSTS production configuration.
- CI job for frontend build/test.
- CI job for integration tests.
- Playwright E2E tests.
- axe-core accessibility tests.
- k6 or equivalent load-test scripts.
- App Insights setup and documented alerting.
- Azure Key Vault/App Service configuration guidance for JWT secret and connection string.
- Deployment slots and smoke tests after deploy.

### Recommended later

- Move from SQLite to Azure SQL only after feature and test coverage improve.
- Use Azure Key Vault with managed identity for production secrets.
- Add CDN/static hosting for Angular and an automated frontend deployment workflow.
- Add queue/background processing only if future LMS workflows require asynchronous jobs.
- Add OpenTelemetry/App Insights correlation once the core flows are stable.
- Add Testcontainers only when integration tests need realistic external dependencies; SQLite/WebApplicationFactory is enough initially.

## 3. Prioritised Roadmap

### Immediate next tasks

1. Finish Angular registration UI and route it from `frontend/src/app/app.routes.ts`.
2. Fix public course listing so `ListPublicAsync` returns only `IsPublished == true`.
3. Add focused unit tests for published-course visibility and admin-registration rejection.
4. Replace `DevPasswordHasher` with a production-suitable implementation behind `IPasswordHasher`.
5. Disable unconditional Swagger/debug logging in production.
6. Add frontend build/test to CI.

### Short-term hardening

1. Add course publish workflow with Instructor/Admin authorization.
2. Add instructor ownership checks before course update/publish operations.
3. Add meaningful WebApplicationFactory integration tests for auth, course list/create, and authorization failures.
4. Add accessible form validation patterns and a simple accessibility checklist under docs.
5. Add security headers, HSTS for production, and rate limiting for auth endpoints.
6. Add Azure App Service configuration notes for JWT key, connection string, and environment.

### Medium-term enhancements

1. Implement enrollment persistence and student enrollment UI.
2. Add audit log table and write audit events for register, login success/failure, course creation, publish, and enrollment.
3. Add GDPR support: consent timestamp, data export endpoint, account deletion/deactivation path, and retention notes.
4. Add Playwright E2E tests for register/login/course list/course creation.
5. Add axe accessibility checks to Playwright.
6. Add k6 smoke load tests for login and course browsing.

### Long-term production readiness

1. Add App Insights, dashboards, alerts, structured logging, and correlation IDs.
2. Add Azure deployment slots, health checks, smoke tests, and rollback notes.
3. Define high-volume event readiness: scaling settings, load-test targets, cache strategy, and incident playbook.
4. Move from SQLite to Azure SQL as a later production-hardening item, not now.
5. Add Key Vault and managed identity once Azure configuration is stable.

## 4. Suggested GitHub Issues

### Issue 1: Complete Angular registration flow

- Goal: Build a real registration page connected to `POST /api/auth/register`.
- Acceptance criteria:
  - `/register` route is active.
  - Form captures first name, last name, email, password, and allowed role.
  - Admin registration is not available from UI.
  - Validation and API errors are visible and accessible.
  - Component tests cover valid/invalid submit states.
- Suggested files/areas: `frontend/src/app/app.routes.ts`, `frontend/src/app/features/auth/register`, `frontend/src/app/core/auth/auth.service.ts`.
- Risk/complexity: Low.

### Issue 2: Show only published courses publicly

- Goal: Make public course browsing respect `IsPublished`.
- Acceptance criteria:
  - `GET /api/courses` returns only published courses.
  - Unit or integration tests prove unpublished courses are excluded.
  - Existing create-course behavior still creates unpublished courses by default.
- Suggested files/areas: `backend/src/NareshLearn.Infrastructure/Courses/CourseRepository.cs`, `backend/src/NareshLearn.Application/Courses/List`, backend tests.
- Risk/complexity: Low.

### Issue 3: Add course publish workflow

- Goal: Allow Instructor/Admin users to publish courses.
- Acceptance criteria:
  - Publish endpoint exists and requires Instructor/Admin.
  - Domain `Course.Publish()` is used.
  - Tests cover publish success and unauthorized/forbidden attempts.
- Suggested files/areas: `backend/src/NareshLearn.Domain/Courses/Course.cs`, `backend/src/NareshLearn.Application/Courses`, `backend/src/NareshLearn.Api/Controllers/CoursesController.cs`, Angular course management UI later.
- Risk/complexity: Medium.

### Issue 4: Enforce instructor ownership checks

- Goal: Prevent instructors from modifying or publishing courses they do not own.
- Acceptance criteria:
  - Instructor can mutate own course.
  - Instructor cannot mutate another instructor's course.
  - Admin can perform allowed administrative actions.
  - Tests cover ownership boundaries.
- Suggested files/areas: course application slices, `ICourseRepository`, `CourseRepository`, `CoursesController`.
- Risk/complexity: Medium.

### Issue 5: Replace development password hashing

- Goal: Replace SHA-256 password hashing while preserving the existing custom auth approach.
- Acceptance criteria:
  - `IPasswordHasher` uses BCrypt or Argon2.
  - Tests cover hash/verify behavior.
  - Existing auth services do not depend on framework identity.
  - Migration/backfill note is documented if needed.
- Suggested files/areas: `backend/src/NareshLearn.Infrastructure/Auth/DevPasswordHasher.cs`, `backend/src/NareshLearn.Application/Auth/IPasswordHasher.cs`, auth tests.
- Risk/complexity: Medium.

### Issue 6: Add audit logging foundation

- Goal: Record security and business events for auditability.
- Acceptance criteria:
  - Audit log entity/table exists.
  - Register, login success/failure, course create, publish, and enrollment write audit events.
  - User id, event type, timestamp, and metadata are captured without storing sensitive values.
  - Tests cover representative event writes.
- Suggested files/areas: `backend/src/NareshLearn.Domain`, `backend/src/NareshLearn.Application`, `backend/src/NareshLearn.Infrastructure/Data`, controllers/services.
- Risk/complexity: Medium.

### Issue 7: Add practical GDPR support

- Goal: Demonstrate GDPR-aware handling suitable for a portfolio LMS.
- Acceptance criteria:
  - Document personal data collected and purpose.
  - Add consent timestamp or privacy acknowledgement where appropriate.
  - Add export-my-data endpoint.
  - Add delete/deactivate-account path.
  - Add retention policy notes.
- Suggested files/areas: docs, user domain/application slices, profile/account controller, Angular account UI.
- Risk/complexity: Medium.

### Issue 8: Harden production API configuration

- Goal: Remove development-only behaviors from production runtime.
- Acceptance criteria:
  - Swagger is development-only or intentionally protected.
  - Authorization header debug logging is removed.
  - HSTS/security headers are enabled for production.
  - JWT key and connection string are read from secure environment/App Service config.
- Suggested files/areas: `backend/src/NareshLearn.Api/Program.cs`, `backend/src/NareshLearn.Api/appsettings*.json`, Azure docs/workflow notes.
- Risk/complexity: Medium.

### Issue 9: Expand CI to frontend and integration tests

- Goal: Make CI validate both stacks.
- Acceptance criteria:
  - CI runs backend unit tests.
  - CI runs meaningful backend integration tests when added.
  - CI runs `npm ci`, Angular build, and Angular tests.
  - CI caches NuGet/npm dependencies where useful.
- Suggested files/areas: `.github/workflows/ci.yml`, `frontend/package.json`, integration tests.
- Risk/complexity: Medium.

### Issue 10: Add E2E, accessibility, and load-test starter suites

- Goal: Add realistic portfolio evidence for national-scale readiness without overbuilding.
- Acceptance criteria:
  - Playwright covers login and course browsing.
  - axe-core checks key pages.
  - k6 smoke/load scripts cover login and course list endpoints.
  - CI runs lightweight smoke checks; heavier load tests remain manual/scheduled.
- Suggested files/areas: new `frontend/e2e` or `tests/e2e`, `tests/accessibility`, `tests/load`, GitHub Actions optional jobs.
- Risk/complexity: Medium to High.

## 5. YAML / CI-CD Review

### `.github/workflows/ci.yml`

- What it does:
  - Checks out code.
  - Installs .NET SDK `10.0.x`.
  - Restores `backend/NareshLearn.slnx`.
  - Builds backend in Release.
  - Runs `backend/tests/NareshLearn.UnitTests/NareshLearn.UnitTests.csproj`.
- Triggers:
  - `push` to `main`, `master`, `develop`.
  - `pull_request` targeting `main`, `master`, `develop`.
  - Manual `workflow_dispatch`.
- Secrets/settings:
  - No secrets required.
  - Relies on solution file `backend/NareshLearn.slnx` and hosted Ubuntu runner.
- Recommended improvements:
  - Add frontend job: setup Node, `npm ci`, `npm run build`, `npm test` in CI mode.
  - Add integration test job once real integration tests exist.
  - Add NuGet and npm caching.
  - Add coverage output as optional portfolio evidence.
  - Keep CI lightweight; do not add heavy load tests to normal PR CI.

### `.github/workflows/deploy-backend-azure.yml`

- What it does:
  - Runs on Ubuntu.
  - Restores, builds, and unit-tests backend.
  - Publishes `src/NareshLearn.Api/NareshLearn.Api.csproj`.
  - Deploys published output to Azure App Service using `azure/webapps-deploy@v3`.
- Triggers:
  - `push` to `main`.
  - Manual `workflow_dispatch`.
- Secrets/settings:
  - Requires `secrets.AZURE_WEBAPP_PUBLISH_PROFILE`.
  - Hard-codes App Service app name `NareshLearnApi20260408172133`.
  - Assumes App Service configuration provides production settings as needed.
- Recommended improvements:
  - Add environment protection for production deploys.
  - Add post-deploy smoke test against `/` and a health endpoint once added.
  - Move app name to workflow/env variable for readability.
  - Add deployment slot support before production-like usage.
  - Use App Service settings/Key Vault for JWT secret and connection string.
  - Add frontend deployment workflow later if Angular is hosted separately.

## 6. Testing Strategy

### Unit tests

- Keep backend unit tests focused on domain invariants and application services.
- Add tests for admin registration rejection, invalid role rejection, login success/failure, publish workflow, ownership checks, and published-only course listing.
- Remove or replace placeholder `UnitTest1.cs` files when meaningful coverage exists.

### Integration tests

- Use `Microsoft.AspNetCore.Mvc.Testing` already referenced by `backend/tests/NareshLearn.IntegrationTests`.
- Start with WebApplicationFactory tests for:
  - Register/login/token issuance.
  - `POST /api/courses` requires Instructor/Admin.
  - Student cannot create courses.
  - Public course list excludes unpublished courses after the visibility fix.
- Prefer in-memory or temporary SQLite for CI-safe tests.
- Add integration tests to CI only after they are deterministic.

### Angular component tests

- Expand current `.spec.ts` files beyond creation tests.
- Cover form validation, disabled submit states, error rendering, route guard redirects, token attachment, and role-based behavior.
- Add a CI frontend test command once tests can run headlessly.

### Playwright end-to-end tests

- Add a small E2E suite after backend/frontend startup is scripted.
- Cover:
  - Register/login happy path.
  - Login validation failure.
  - Public course browsing.
  - Instructor course creation.
  - Student blocked from instructor route.
- Keep these as smoke tests for portfolio CI; broader journeys can run manually.

### Accessibility tests

- Add manual checklist evidence first for login, register, course list, and course create.
- Add Playwright plus axe-core checks for critical pages.
- Validate labels, keyboard navigation, visible focus, semantic headings, error messages, and contrast.
- Treat automated checks as a floor; keep a short human review checklist in docs.

### Performance/load tests

- Add k6 scripts for API smoke and load scenarios:
  - Public course list.
  - Login.
  - Course creation with authenticated instructor.
- Use local/development thresholds initially, then record an Azure baseline.
- Do not run heavy load tests on every PR.
- For national-scale readiness, document target assumptions, peak request patterns, App Service scale settings, database bottlenecks, and rollback/incident steps.

## Commands run

- PASS: `Get-Content -Path AGENTS.md`
- PASS: `Get-ChildItem -Force`
- PASS: `rg --files`
- PASS: `Get-Content -Path README.md`
- PASS: `Get-Content -Path backend\README.md`
- PASS: `Get-Content -Path docs\job-specs\client-requirements.md`
- PASS: `Get-ChildItem -Path .github\workflows -Filter *.yml`
- PASS: `Get-Content -Path .github\workflows\ci.yml`
- PASS: `Get-Content -Path .github\workflows\deploy-backend-azure.yml`
- PASS: multiple `Get-Content` inspections under `backend/src`, `backend/tests`, and `frontend/src`
- PASS: `rg --files -g '!**/bin/**' -g '!**/obj/**' backend\src backend\tests frontend\src frontend\package.json frontend\angular.json backend\Dockerfile`
- PASS: `Test-Path -Path docs\roadmaps` returned `False`
- PASS: `git status --short` returned no changes before report creation
- FAIL then PASS: `New-Item -ItemType Directory -Path docs\roadmaps -Force | Out-Null`; first attempt was denied by sandbox, second attempt succeeded after approval
- FAIL: one exploratory `rg -n` search returned exit code 1 with no output; it was not needed for the final analysis

Builds/tests were not run because this task requested analysis only and restricted file changes to the roadmap report.
