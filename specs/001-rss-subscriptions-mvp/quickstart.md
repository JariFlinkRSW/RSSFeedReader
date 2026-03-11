# Quickstart: MVP RSS Subscription List

**Feature**: [spec.md](spec.md)  
**Date**: 2026-03-11

This quickstart describes the expected developer workflow once the backend and frontend projects exist.

## Prerequisites

- .NET SDK 8.x installed (`dotnet --version`)

## Project layout

Expected layout (see [plan.md](plan.md)):

- `backend/RSSFeedReader.Api/`
- `frontend/RSSFeedReader.UI/`

## Configure ports and API base URL

- Backend listens on a localhost port (from `backend/RSSFeedReader.Api/Properties/launchSettings.json`).
- Frontend runs on a different localhost port (from `frontend/RSSFeedReader.UI/Properties/launchSettings.json`).
- Frontend config in `frontend/RSSFeedReader.UI/wwwroot/appsettings.json` contains `ApiBaseUrl` pointing at the backend.

## Run backend

From repo root:

```powershell
dotnet run --project backend/RSSFeedReader.Api
```

## Run frontend

From repo root:

```powershell
dotnet run --project frontend/RSSFeedReader.UI
```

## Verify MVP behavior

1. Open the frontend URL in the browser.
2. Paste a feed URL into the subscription input.
3. Submit.
4. Confirm the subscription appears in the list.

## Notes (Blazor template cleanup)

If a new Blazor WebAssembly project is created from the template, remove demo pages (e.g., `Home`, `Counter`, `Weather`) and ensure only one page owns the root route (`@page "/"`) to avoid ambiguous routing.
