# Accessibility Checklist

Date: 2026-05-29

Scope: Angular auth and course UI only. This pass improves accessibility toward WCAG 2.1 AA, but does not claim full compliance because manual keyboard, screen-reader, colour contrast, and automated axe evidence has not yet been completed.

## Pages Reviewed

### Navigation/Header

- Issues identified: primary navigation had no explicit accessible label; focus styling for nav links/buttons was not defined; no skip link was available.
- Changes implemented: added labelled primary navigation, a skip link to main content, explicit logout button type, and visible focus styles.
- Manual verification still required: keyboard tab order across all routes, skip-link behaviour in browser, focus visibility at 200% zoom.
- Related WCAG principle: Operable, Robust.

### Login Page

- Issues identified: validation messages were visible but not connected to inputs; invalid state was not exposed to assistive technologies; API error was not announced; focus styles were not explicit.
- Changes implemented: added `aria-describedby`, `aria-invalid`, `role="alert"`, `aria-live="assertive"`, autocomplete hints, `novalidate`, and visible focus/error styles.
- Manual verification still required: screen-reader announcement of invalid fields and API errors; colour contrast check for error red and focus outline.
- Related WCAG principle: Perceivable, Understandable, Robust.

### Register Page

- Issues identified: most form labels existed, but invalid state and error descriptions needed stable assistive-tech wiring; API error needed a live alert region.
- Changes implemented: added conditional `aria-describedby`, `aria-invalid`, assertive alert region, and invalid field styling while preserving Student/Instructor-only role options.
- Manual verification still required: keyboard-only registration flow, screen-reader role select announcement, colour contrast check.
- Related WCAG principle: Perceivable, Operable, Understandable, Robust.

### Course List Page

- Issues identified: loading text contained debug copy; loading, empty, and error states were not exposed as status/alert regions; course items used generic divs.
- Changes implemented: added a labelled page section, accessible loading/empty status messages, assertive error alert, labelled course grid, and semantic `article` elements for courses.
- Manual verification still required: screen-reader announcement of async loading/empty/error states; heading order review when more course content is added.
- Related WCAG principle: Perceivable, Robust.

### Create Course Page

- Issues identified: title validation was visible but not connected to the field; invalid state was not exposed; API error was not announced; focus styles were not explicit.
- Changes implemented: added `aria-describedby`, `aria-invalid`, `role="alert"`, `aria-live="assertive"`, `novalidate`, and visible focus/error styles.
- Manual verification still required: keyboard-only submit/error flow, screen-reader announcement of title validation, colour contrast check.
- Related WCAG principle: Perceivable, Operable, Understandable, Robust.

## Not Yet Addressed

- Full colour contrast audit across all states.
- Screen-reader testing with NVDA, JAWS, VoiceOver, or Narrator.
- Browser-based keyboard walkthrough evidence.
- Focus management after route navigation and failed submissions.
- Accessible page titles per route.
- Removal of old commented Angular starter markup in `app.html`.
- Broader responsive/zoom testing at 200% and 400%.

## Future Automated Testing

- Add Playwright E2E coverage for login, register, course list, and create course.
- Add axe-core checks to the Playwright suite for critical pages.
- Run accessibility checks in CI as a lightweight smoke job once routes can be served reliably in CI.
- Keep manual testing alongside axe because automated tools cannot prove full WCAG 2.1 AA compliance.
