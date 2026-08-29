# CQRS

A minimal **CQRS + Clean Architecture** demo built with **ASP.NET Core 9**, **MediatR**, and **EF Core (SQLite)**. It implements a simple Todo API to showcase how commands, queries, validation, and cross-cutting concerns fit together in a layered .NET solution.

## Overview

This project is a learning/reference implementation of the **CQRS (Command Query Responsibility Segregation)** pattern using [MediatR](https://github.com/LuckyPennySoftware/MediatR) as the in-process mediator, with each use case (e.g. `CreateTodo`, `GetTodoById`) organized as a self-contained vertical slice — its command/query, handler, and validator living together.

## Architecture

The solution follows a four-layer Clean Architecture split:

```
CQRS.sln
├── API              → ASP.NET Core Web API (controllers, DI wiring, exception handling)
├── Application      → Use cases: commands, queries, handlers, validators, pipeline behaviors
├── Domain           → Entities and core domain models (no external dependencies)
└── Infrastructure   → EF Core DbContext, persistence, external concerns
```

**Dependency direction:** `API → Infrastructure → Application → Domain`
`Domain` has no dependencies on any other project — it's the innermost layer.

### Key building blocks

| Concern | How it's handled |
|---|---|
| Commands & Queries | [MediatR](https://mediatr.io) `IRequest<T>` / `IRequestHandler<T, TResponse>`, one folder per use case (vertical slice) |
| Validation | [FluentValidation](https://docs.fluentvalidation.net/), run automatically via a MediatR `ValidationBehavior<TRequest, TResponse>` pipeline behavior before every handler executes |
| Persistence | EF Core with SQLite, accessed through an `IAppDbContext` abstraction |
| Error handling | A centralized `GlobalExceptionHandler` using ASP.NET Core's `IExceptionHandler` + `ProblemDetails` |
| Data access boundary | `Application` depends only on `IAppDbContext` (an interface), never on EF Core directly — `Infrastructure` provides the implementation |

## Tech Stack

- **.NET 9** / ASP.NET Core Web API
- **MediatR** — in-process mediator for CQRS command/query dispatch
- **FluentValidation** — request validation
- **Entity Framework Core** (SQLite provider) — persistence
- **ProblemDetails (RFC 9110)** — standardized error responses

## Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download)

### Run locally

```bash
git clone https://github.com/Kerollos-Saad/CQRS.git
cd CQRS

dotnet restore
dotnet build
dotnet run --project API
```

The API will start on the URL configured in `API/Properties/launchSettings.json` (see console output for the exact port). A SQLite database file (`app.db`) is created automatically on first run.

### Trying the endpoints

A ready-to-use [`request.http`](./request.http) file is included at the repo root — open it in an editor with an HTTP client extension (e.g. the VS Code REST Client, or Rider/Visual Studio's built-in HTTP file support) and run the requests directly.

## API Endpoints

| Method | Route | Description |
|---|---|---|
| `GET` | `/api/todos` | Get all todos |
| `GET` | `/api/todos/{id}` | Get a todo by id |
| `POST` | `/api/todos` | Create a new todo |
| `PUT` | `/api/todos/{id}` | Update an existing todo |
| `DELETE` | `/api/todos/{id}` | Delete a todo |

### Example: Create a Todo

```http
POST /api/todos
Content-Type: application/json

{
    "title": "first todo for Aug"
}
```

Returns the new todo's `Guid` id. Sending an empty `title` returns a `400` with validation details, handled by the `ValidationBehavior` pipeline before the request ever reaches the handler.

## Project Structure (example: the `CreateTodo` use case)

```
Application/
└── Features/
    └── CreateTodo/
        ├── CreateTodoCommand.cs           # public sealed record : IRequest<Guid>
        ├── CreateTodoCommandHandler.cs    # IRequestHandler<CreateTodoCommand, Guid>
        └── CreateTodoCommandValidator.cs  # AbstractValidator<CreateTodoCommand>
```

Each feature folder is self-contained: adding a new use case means adding a new folder, not touching existing code — a core benefit of the vertical-slice approach to CQRS.

## Why CQRS here?

This repo intentionally keeps the domain simple (a Todo list) so the *pattern* stays the focus rather than business complexity. It's meant as a reference for:

- Wiring up MediatR with ASP.NET Core dependency injection
- Adding a validation pipeline behavior that runs before every command/query handler
- Keeping `Application` free of infrastructure concerns via the `IAppDbContext` interface
- Centralizing exception-to-HTTP-response translation with `IExceptionHandler`

## License

No license specified yet — add one (e.g. MIT) if you intend for others to reuse this code.
