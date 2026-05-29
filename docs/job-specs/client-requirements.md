# Client Requirements Reference

## Purpose

NareshLearn should evolve into a secure, accessible, scalable web platform that demonstrates enterprise-grade engineering practices suitable for public sector and education-related systems.

This document summarizes the key non-functional and technical requirements derived from the reference client specification.

---

# Priority Requirements

## Accessibility (WCAG 2.1 AA)

The application should:

- Meet WCAG 2.1 AA standards.
- Support keyboard-only navigation.
- Provide appropriate labels for all form controls.
- Use semantic HTML where possible.
- Maintain sufficient colour contrast.
- Provide visible focus indicators.
- Support screen-reader accessibility.
- Display meaningful validation and error messages.

### Evidence

- Accessibility review checklist.
- Accessibility testing results.
- Accessibility-focused UI components.

---

## Security and Authentication

The application should:

- Use secure authentication mechanisms.
- Implement role-based access control.
- Protect sensitive endpoints using authorization policies.
- Prevent unauthorized access to resources.
- Follow secure API development practices.
- Support secure password storage.
- Support JWT-based authentication.

### Evidence

- JWT authentication implementation.
- Angular route guards.
- HTTP interceptor for token handling.
- Protected API endpoints.

---

## GDPR and Data Protection

The application should:

- Demonstrate GDPR-aware data handling.
- Minimize storage of personal data.
- Record user consent where appropriate.
- Support future implementation of:
  - data export
  - data deletion
  - retention policies
- Avoid storing sensitive information unnecessarily.

### Future Enhancements

- User data export.
- User account deletion.
- Data retention policies.

---

## Audit Logging

The platform should support auditability.

Examples:

- User login events.
- User registration events.
- Course creation events.
- Course modification events.
- Administrative actions.

### Future Enhancements

- Central audit log table.
- Audit reporting.
- User activity history.

---

## Testing Requirements

### Unit Testing

Current stack:

- xUnit
- FluentAssertions
- Moq

### Integration Testing

Suggested:

- WebApplicationFactory
- Testcontainers (optional)

### End-to-End Testing

Suggested:

- Playwright

### Accessibility Testing

Suggested:

- axe-core
- Playwright accessibility checks

### Performance Testing

Suggested:

- k6

Scenarios:

- Login traffic
- Course browsing
- Course creation
- Concurrent users

---

## Cloud and DevOps Requirements

### CI

Current platform:

- GitHub Actions

### CD

Current platform:

- GitHub Actions
- Azure App Service

### Containerisation

Current platform:

- Docker

---

## Current Roadmap Priorities

1. Angular Registration Page
2. Course Publishing Workflow
3. Published Course Visibility Rules
4. Instructor Ownership Checks
5. Student Enrollment
6. Audit Logging
7. Integration Testing
8. Playwright End-to-End Testing
9. Accessibility Review
10. Performance Testing
11. Azure Hardening (Key Vault, App Insights, Deployment Slots)