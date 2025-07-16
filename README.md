# TODO.Task-Service

This is the **Task Management Microservice** for the TODO application. It handles task list operations, user-specific task management, and supports secure access through JWT authentication.

---

## 🧩 Tech Stack

- [.NET 9](https://dotnet.microsoft.com/)
- Minimal API + Clean Architecture
- Entity Framework Core + PostgreSQL
- JWT Authentication

---

## 📁 Project Structure

```
src/
├── API           # Minimal API entry point
├── Data          # EF Core DbContext, Repositories, Migrations
├── Extensions    # Service registration helpers
├── Managers      # Application logic
├── Models        # Domain + DTO models
├── Services      # Interfaces + infrastructure helpers (auth, hashing)
tests/            # Unit and integration tests
```

---

## 🔐 Authentication

This service enforces JWT bearer authentication. Requests must include a valid bearer token in the `Authorization` header:

```
Authorization: Bearer <your-token>
```

Tokens are validated using the configured `JWT_ISSUER`, `JWT_AUDIENCE`, and `JWT_KEY`.

---

## 📘 API Overview

All routes are prefixed by `/api` and typically routed through the API Gateway.

### Task List Endpoints (`/api/tasklist`)

- `GET /api/tasklist` — Get all task lists for the authenticated user
- `GET /api/tasklist/{listId}` — Retrieve a specific task list
- `POST /api/tasklist` — Create a new task list
- `PUT /api/tasklist/{listId}` — Update an existing task list
- `DELETE /api/tasklist/{listId}` — Delete a task list

### Task Item Endpoints (`/api/taskitem`)

- `GET /api/taskitem/{taskListId}` — Get all tasks in a specific list
- `POST /api/taskitem` — Add a new task item to a list
- `PUT /api/taskitem/{itemId}` — Update a task item
- `DELETE /api/taskitem/{itemId}` — Delete a task item

---

## 📄 License

MIT — see `LICENSE` file.
