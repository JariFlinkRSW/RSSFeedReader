# Quickstart: MVP RSS Subscription List

**Feature**: [spec.md](spec.md)  
**Date**: 2026-03-11

This quickstart describes the expected developer workflow once the backend and frontend projects exist.

## Prerequisites

- A .NET SDK that can build `net8.0` (8.0+)

## Project layout

Expected layout (see [plan.md](plan.md)):

- `backend/RSSFeedReader.Api/`
- `frontend/RSSFeedReader.UI/`

## Configure ports and API base URL

- Backend HTTP: `http://localhost:5151` (from `backend/RSSFeedReader.Api/Properties/launchSettings.json`)
- Frontend HTTP: `http://localhost:5213` (from `frontend/RSSFeedReader.UI/Properties/launchSettings.json`)
- Frontend config in `frontend/RSSFeedReader.UI/wwwroot/appsettings.json` contains `ApiBaseUrl` pointing at the backend `/api/` (example: `http://localhost:5151/api/`).

## Run backend

From repo root:

```powershell
dotnet run --project backend/RSSFeedReader.Api --launch-profile http
```

## Run frontend

From repo root:

```powershell
dotnet run --project frontend/RSSFeedReader.UI --launch-profile http
```

## Verify MVP behavior

1. Open the frontend URL in the browser.
2. Paste a feed URL into the subscription input.
3. Submit.
4. Confirm the subscription appears in the list.

## Notes (Blazor template cleanup)

If a new Blazor WebAssembly project is created from the template, remove demo pages (e.g., `Home`, `Counter`, `Weather`) and ensure only one page owns the root route (`@page "/"`) to avoid ambiguous routing.
