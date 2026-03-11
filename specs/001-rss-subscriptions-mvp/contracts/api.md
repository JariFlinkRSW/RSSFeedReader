# API Contract: Subscriptions (MVP)

**Feature**: [spec.md](../spec.md)  
**Date**: 2026-03-11

This contract describes the MVP API used by the Blazor UI to add and list subscriptions.

## Conventions

- Base path: `/api`
- Content type: `application/json`
- Authentication: none (MVP)

## Models

### SubscriptionDto

```json
{
  "id": "string",
  "url": "string",
  "createdAt": "2026-03-11T12:34:56Z"
}
```

### CreateSubscriptionRequest

```json
{
  "url": "string"
}
```

### ErrorResponse (optional)

```json
{
  "message": "string"
}
```

## Endpoints

### List subscriptions

- Method: `GET`
- Path: `/api/subscriptions`
- Success: `200 OK`

Response body:

```json
[
  {
    "id": "string",
    "url": "https://example.com/feed.xml",
    "createdAt": "2026-03-11T12:34:56Z"
  }
]
```

### Add subscription

- Method: `POST`
- Path: `/api/subscriptions`

Request body:

```json
{
  "url": "https://example.com/feed.xml"
}
```

Success:

- `201 Created`
- Response body: `SubscriptionDto`

Validation failures (MVP):

- If `url` is missing or whitespace-only: `400 Bad Request` with an `ErrorResponse`

Notes:

- Duplicate URLs are allowed.
- No attempt is made to validate that the URL points to a real feed.
