---

description: "Task list for implementing MVP RSS subscription management"
---

# Tasks: MVP RSS Subscription List

**Input**: Design documents from `/specs/001-rss-subscriptions-mvp/`
**Prerequisites**: plan.md (required), spec.md (required), research.md, data-model.md, contracts/, quickstart.md

**Tests**: Tests are OPTIONAL unless explicitly requested in the feature specification or required by the repository constitution (e.g., when adding non-trivial logic such as parsing/network/serialization).

**Organization**: Tasks are grouped by user story to enable independent implementation and testing of each story.

## Format: `[ID] [P?] [Story] Description`

- **[P]**: Can run in parallel (different files, no dependencies)
- **[Story]**: Which user story this task belongs to (e.g., US1, US2, US3)
- Include exact file paths in descriptions

## Phase 1: Setup (Shared Infrastructure)

**Purpose**: Create the solution + project scaffolding for backend and frontend.

- [ ] T001 Create feature doc structure checkpoint in specs/001-rss-subscriptions-mvp/ (verify plan/spec/research/data-model/contracts/quickstart exist)
- [ ] T002 Create repository directories backend/ and frontend/ at repo root
- [ ] T003 Create .NET solution file RSSFeedReader.sln at repo root
- [ ] T004 Initialize backend API project in backend/RSSFeedReader.Api/ using `dotnet new webapi`
- [ ] T005 Initialize frontend Blazor WebAssembly project in frontend/RSSFeedReader.UI/ using `dotnet new blazorwasm`
- [ ] T006 Add backend/RSSFeedReader.Api/RSSFeedReader.Api.csproj to RSSFeedReader.sln
- [ ] T007 Add frontend/RSSFeedReader.UI/RSSFeedReader.UI.csproj to RSSFeedReader.sln
- [ ] T008 [P] Verify backend runs locally via backend/RSSFeedReader.Api/Properties/launchSettings.json
- [ ] T009 [P] Verify frontend runs locally via frontend/RSSFeedReader.UI/Properties/launchSettings.json

---

## Phase 2: Foundational (Blocking Prerequisites)

**Purpose**: Shared plumbing required before implementing any user story behavior.

**⚠️ CRITICAL**: No user story work can begin until this phase is complete.

- [ ] T010 Remove template demo pages in frontend/RSSFeedReader.UI/Pages/ (delete Home.razor, Counter.razor, Weather.razor)
- [ ] T011 Update navigation menu in frontend/RSSFeedReader.UI/Layout/NavMenu.razor to remove links to deleted pages and point to the MVP subscriptions page
- [ ] T012 Create a single MVP root page file frontend/RSSFeedReader.UI/Pages/Subscriptions.razor with `@page "/"`
- [ ] T013 Configure API base URL in frontend/RSSFeedReader.UI/wwwroot/appsettings.json (add `ApiBaseUrl` pointing to backend `/api/`)
- [ ] T014 Read ApiBaseUrl from configuration in frontend/RSSFeedReader.UI/Program.cs and register `HttpClient` with BaseAddress
- [ ] T015 Configure backend CORS in backend/RSSFeedReader.Api/Program.cs to allow the frontend origin from frontend launch settings
- [ ] T016 Define API DTOs in backend/RSSFeedReader.Api/Models/SubscriptionDto.cs and backend/RSSFeedReader.Api/Models/CreateSubscriptionRequest.cs
- [ ] T017 Create domain model backend/RSSFeedReader.Api/Models/Subscription.cs
- [ ] T018 Create in-memory store/service interface backend/RSSFeedReader.Api/Services/ISubscriptionStore.cs
- [ ] T019 Implement in-memory store backend/RSSFeedReader.Api/Services/InMemorySubscriptionStore.cs
- [ ] T020 Register store/service in backend/RSSFeedReader.Api/Program.cs (DI container)
- [ ] T021 Create API controller backend/RSSFeedReader.Api/Controllers/SubscriptionsController.cs with route prefix `/api/subscriptions`

**Checkpoint**: Foundation ready (solution builds, UI routes clean, API reachable, CORS configured)

---

## Phase 3: User Story 1 - Add a subscription (Priority: P1) 🎯 MVP

**Goal**: User can paste a URL, submit, and see it added.

**Independent Test**: Run backend + frontend, submit a non-empty URL, confirm it appears in the list immediately.

### Implementation for User Story 1

- [ ] T022 [US1] Implement POST `/api/subscriptions` in backend/RSSFeedReader.Api/Controllers/SubscriptionsController.cs (returns `201 Created` with `SubscriptionDto`)
- [ ] T023 [US1] Implement store add behavior in backend/RSSFeedReader.Api/Services/InMemorySubscriptionStore.cs (create ID, set createdAt, store url)
- [ ] T024 [P] [US1] Create frontend API client wrapper in frontend/RSSFeedReader.UI/Services/SubscriptionsApiClient.cs (method: CreateSubscriptionAsync)
- [ ] T025 [US1] Wire API client in frontend/RSSFeedReader.UI/Program.cs (DI registration for SubscriptionsApiClient)
- [ ] T026 [US1] Implement add form UI in frontend/RSSFeedReader.UI/Pages/Subscriptions.razor (input + add button)
- [ ] T027 [US1] On successful POST, update the UI list state in frontend/RSSFeedReader.UI/Pages/Subscriptions.razor

**Checkpoint**: US1 complete (Add works end-to-end)

---

## Phase 4: User Story 2 - View the subscription list (Priority: P2)

**Goal**: User can see the current session’s subscription list (including on first load).

**Independent Test**: Launch frontend and confirm an empty state appears; after adding items, reload the page and confirm the list is retrieved from the backend (while backend is still running).

### Implementation for User Story 2

- [ ] T028 [US2] Implement GET `/api/subscriptions` in backend/RSSFeedReader.Api/Controllers/SubscriptionsController.cs (returns `200 OK` with list of `SubscriptionDto`)
- [ ] T029 [US2] Implement store list behavior in backend/RSSFeedReader.Api/Services/InMemorySubscriptionStore.cs
- [ ] T030 [P] [US2] Add list method to frontend/RSSFeedReader.UI/Services/SubscriptionsApiClient.cs (method: GetSubscriptionsAsync)
- [ ] T031 [US2] Load subscriptions on page init in frontend/RSSFeedReader.UI/Pages/Subscriptions.razor (call GET and render list)
- [ ] T032 [US2] Add an explicit empty state message in frontend/RSSFeedReader.UI/Pages/Subscriptions.razor when the list is empty

**Checkpoint**: US2 complete (List renders correctly on load and after reload)

---

## Phase 5: User Story 3 - Prevent accidental empty submissions (Priority: P3)

**Goal**: Empty/whitespace submissions are rejected.

**Independent Test**: Attempt to submit empty input; confirm no new subscription is added and the UI remains stable.

### Implementation for User Story 3

- [ ] T033 [US3] Add backend validation for POST request in backend/RSSFeedReader.Api/Controllers/SubscriptionsController.cs (empty/whitespace url → `400 Bad Request` with `ErrorResponse`)
- [ ] T034 [P] [US3] Add ErrorResponse model in backend/RSSFeedReader.Api/Models/ErrorResponse.cs
- [ ] T035 [US3] Add frontend client-side guard in frontend/RSSFeedReader.UI/Pages/Subscriptions.razor (trim and block empty submissions)
- [ ] T036 [US3] Add user-friendly UI feedback in frontend/RSSFeedReader.UI/Pages/Subscriptions.razor when submission is rejected (no stack traces)

**Checkpoint**: US3 complete (Both client and server reject empty input)

---

## Phase 6: Polish & Cross-Cutting Concerns

**Purpose**: Ensure a clean developer experience and alignment with constitution quality gates.

- [ ] T037 [P] Confirm CORS origins align with launch settings in backend/RSSFeedReader.Api/Program.cs and frontend/RSSFeedReader.UI/Properties/launchSettings.json
- [ ] T038 Update quickstart verification steps (if needed) in specs/001-rss-subscriptions-mvp/quickstart.md to reflect final ports and config keys
- [ ] T039 Run a clean build of the full solution (document any warnings) via RSSFeedReader.sln
- [ ] T040 [P] Optional: add a minimal backend unit test project backend/RSSFeedReader.Api.Tests/ and a test for empty-url rejection in backend/RSSFeedReader.Api.Tests/SubscriptionsControllerTests.cs

---

## Dependencies & Execution Order

### Phase Dependencies

- **Setup (Phase 1)**: No dependencies; can start immediately.
- **Foundational (Phase 2)**: Depends on Setup; blocks all user stories.
- **User Story 1 (Phase 3)**: Depends on Foundational.
- **User Story 2 (Phase 4)**: Depends on Foundational (can be done in parallel with US1 after foundation if staffed).
- **User Story 3 (Phase 5)**: Depends on US1 (because it hardens the add flow) and benefits from US2 (for consistent UX), but can be done as soon as POST exists.
- **Polish (Phase 6)**: Depends on all desired user stories being complete.

### User Story Completion Order (Dependency Graph)

- US1 (P1) → US3 (P3 hardening of add flow)
- US2 (P2) is independent after Foundational; it improves first-load and refresh behavior.

### Parallel Opportunities

- Phase 1: backend and frontend scaffolding/verification tasks can be split across developers.
- Phase 2: backend foundational tasks (DTOs/store/controller shell) can proceed in parallel with frontend cleanup/config tasks.
- After Phase 2: US1 and US2 can proceed in parallel with careful coordination on API contract.

---

## Parallel Example: User Story 1

After Foundational (Phase 2) is complete, these can be worked in parallel by different developers:

```text
Backend:  T022 (POST /api/subscriptions) + T023 (store add)
Frontend: T024 (client create) + T026 (add form UI)
Glue:     T027 (update UI list after POST)
```

---

## Parallel Example: User Story 2

After Foundational (Phase 2) is complete, these can be worked in parallel:

```text
Backend:  T028 (GET /api/subscriptions) + T029 (store list)
Frontend: T030 (client list) + T031 (load on init) + T032 (empty state)
```

---

## Implementation Strategy

### MVP First (User Story 1 Only)

1. Complete Phase 1 + Phase 2
2. Complete Phase 3 (US1)
3. **STOP and validate**: verify add flow works end-to-end

### Incremental Delivery

1. Setup + Foundational → foundation ready
2. US1 → demoable MVP
3. US2 → better first-load experience and refresh behavior
4. US3 → harden validation + UX feedback
5. Polish tasks
