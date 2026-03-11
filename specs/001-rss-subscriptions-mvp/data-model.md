# Data Model: MVP RSS Subscription List

**Feature**: [spec.md](spec.md)  
**Date**: 2026-03-11

## Entities

### Subscription

Represents a single user-provided feed URL stored for the current session.

**Fields**

- `id`: string (GUID) — stable identifier for UI rendering keys and future evolution
- `url`: string — the submitted URL, stored exactly as provided after trimming
- `createdAt`: datetime (UTC) — timestamp for ordering/debugging (optional for MVP UI, but low-cost)

**Validation rules (MVP)**

- `url` MUST be non-empty after trimming whitespace.
- No additional URL validation is required for MVP.
- Duplicate `url` values are allowed.

**Relationships**

- None in MVP.

## State & transitions

- Initial state: empty list.
- Transition: Add Subscription → list size increases by 1 if validation passes.
- No delete/update in MVP.

## Notes for Extended-MVP

If feed fetching is added later, avoid mixing feed item storage into the Subscription entity; introduce a separate `FeedItem` entity and keep parsing/network constraints in the plan.
