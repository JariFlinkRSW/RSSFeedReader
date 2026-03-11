# Research: MVP RSS Subscription List

**Feature**: [spec.md](spec.md)  
**Plan**: [plan.md](plan.md)  
**Date**: 2026-03-11

This research file captures decisions made to remove ambiguity from the plan.

## Decision 1: Target runtime and language

- Decision: Use .NET 8 LTS (C#) for both backend and frontend.
- Rationale: Matches the chosen ASP.NET Core + Blazor WebAssembly tech stack and provides an LTS baseline for maintainability.
- Alternatives considered:
  - .NET 6 LTS: older baseline; less desirable for new work.
  - .NET 9: not LTS (at time of writing); higher churn risk.

## Decision 2: API style

- Decision: ASP.NET Core Web API using controller-based endpoints for clarity.
- Rationale: Controller + DTO patterns make contracts explicit and easy to evolve when Extended-MVP adds more endpoints.
- Alternatives considered:
  - Minimal APIs: fine, but controller conventions fit a teaching/POC repo and improve discoverability for new developers.

## Decision 3: MVP storage and validation

- Decision: Store subscriptions in-memory in the backend process; validate only empty/whitespace.
- Rationale: Aligns with MVP scope and keeps security surface minimal.
- Alternatives considered:
  - Persistence (SQLite/EF Core): explicitly out of scope for MVP.
  - Full URL validation: deferred; MVP assumes user provides valid URLs.

## Decision 4: Duplicate subscriptions

- Decision: Allow duplicates in MVP.
- Rationale: The spec explicitly allows duplicates unless changed later; keeps MVP logic minimal.
- Alternatives considered:
  - De-duplication by normalized URL: useful later, but adds logic and edge cases (normalization rules) that are not required for MVP.

## Decision 5: Frontend configuration and ports

- Decision: Frontend reads API base URL from `wwwroot/appsettings.json`; backend CORS allows the frontend origin(s) used by launch settings.
- Rationale: Matches the TechStack guidance and prevents hard-coded environment assumptions.
- Alternatives considered:
  - Hard-coded API base URL: faster initially but brittle across machines/ports.

## Constitution alignment summary

- MVP scope discipline: PASS (no fetching/parsing/persistence).
- Secure-by-default: PASS (no untrusted feed content processed in MVP; input treated as plain text).
- Maintainability: PASS (clear API/UI boundary).
