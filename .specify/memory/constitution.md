<!--
Sync Impact Report

- Version change: N/A (template placeholders) 26 1.0.0
- Modified principles: N/A 26 Added 5 project-specific principles
- Added sections: Security & Safety Requirements; Development Workflow & Quality Gates
- Removed sections: None
- Templates requiring updates:
	- 5f9 updated: .specify/templates/plan-template.md
	- 5f9 updated: .specify/templates/spec-template.md
	- 5f9 updated: .specify/templates/tasks-template.md
	- 6ab not present: .specify/templates/commands/*.md (no sync needed)
- Deferred placeholders: None
-->

# RSS Feed Reader Constitution

## Core Principles

### I. MVP Scope Discipline (NON-NEGOTIABLE)
The MVP MUST implement only subscription list management:

- Users MUST be able to add a feed subscription by URL.
- Users MUST be able to see the list of subscriptions.

The MVP MUST NOT include: feed fetching, feed parsing, persistence, background polling,
or URL validation beyond preventing obvious UI/runtime breakage (e.g., empty string).

When a change request is outside MVP scope, it MUST be explicitly deferred to
Extended-MVP/post-MVP and documented in the spec/plan.

### II. Secure-by-Default Data Handling
All user input and all content from feeds (when Extended-MVP begins) MUST be treated as
untrusted.

- The UI MUST render subscription URLs as plain text (no raw HTML rendering).
- Backend logs MUST NOT include secrets or large payloads; avoid logging full feed
	content.
- If network fetching is introduced (Extended-MVP+), the implementation MUST include:
	timeouts, size limits, safe XML parsing defaults, and protections against SSRF-style
	access (at minimum: restrict to `http`/`https`, no local-file schemes).

Security work MUST be proportional to scope: the MVP remains safest by avoiding
network operations entirely.

### III. Maintainable, Boring Architecture
Prefer the simplest design that preserves clear boundaries between frontend and backend.

- The backend API MUST own subscription storage and provide add/list endpoints.
- The frontend MUST treat the backend as the source of truth.
- Use in-memory storage for MVP; introduce persistence only after MVP is complete.
- Avoid speculative abstractions (repository/unit-of-work patterns, generic frameworks)
	unless a concrete need is documented in the plan.

### IV. Quality Gates Are Cheap and Mandatory
Every change MUST meet these minimum quality gates:

- Builds successfully on a clean checkout.
- No new compiler warnings introduced.
- Formatting/linting rules (if configured) are followed.

Tests are required when risk justifies them:

- MVP-only UI wiring may be delivered without tests if it is trivial.
- Any non-trivial logic (parsing, deduplication, validation, network, serialization)
	MUST include automated tests.

### V. Operational Clarity (Even for Local Apps)
When failures occur, the system MUST fail clearly and safely.

- Backend errors MUST return appropriate HTTP status codes.
- Error messages shown in the UI MUST be user-comprehensible and MUST NOT leak
	implementation details.
- Logging MUST be sufficient to diagnose issues during development (request path,
	status code, correlation where practical) without recording sensitive data.

## Security & Safety Requirements

These requirements apply to MVP and all future phases.

- MVP MUST not perform feed fetching/parsing (reduces security surface area).
- CORS (when applicable) MUST be restricted to the local frontend origin(s) used in
	development.
- Configuration MUST be read from configuration files/environment (no hard-coded URLs
	for API base addresses outside of safe defaults).

Extended-MVP+ requirements (when feed fetching exists):

- HTTP fetching MUST use explicit timeouts and reasonable response size limits.
- XML/feed parsing MUST use safe defaults; reject obviously malformed inputs.
- Any HTML content displayed from feeds MUST be sanitized (or displayed as plain text).

## Development Workflow & Quality Gates

- Feature work MUST be described via specs/plans/tasks under `specs/` when using Speckit.
- Every plan MUST include a "Constitution Check" section and explicitly call out scope
	alignment (MVP vs Extended-MVP) and any security implications.
- If a principle is violated, the plan MUST document the rationale and the simplest
	alternative that was rejected.

## Governance
<!-- Example: Constitution supersedes all other practices; Amendments require documentation, approval, migration plan -->

This constitution supersedes other development conventions in this repository.

- Amendments MUST be made via a documented change to this file with:
	(1) a rationale, (2) a version bump, and (3) updates to dependent templates.
- Versioning policy:
	- MAJOR: removes/weakens a principle or changes governance in a breaking way
	- MINOR: adds a new principle/section or materially expands guidance
	- PATCH: clarifies wording without changing intent
- Compliance expectations:
	- Feature plans MUST include a Constitution Check.
	- Reviews MUST verify that the implementation stays within MVP scope unless the
		feature spec explicitly targets Extended-MVP.
	- Security requirements MUST be revisited before introducing any network fetching or
		HTML rendering.

**Version**: 1.0.0 | **Ratified**: 2026-03-11 | **Last Amended**: 2026-03-11
