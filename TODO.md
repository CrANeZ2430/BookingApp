# BookingApp Roadmap:

## v1 — Core Production Release (Current Focus)

### Architecture & Core Backend
- [x] Add project's Core Entities
- [x] Set up Entity Framework Core and Migrations
- [x] Set up MediatR orchestration
- [x] Add Aggregate Root Base and Domain Events (complex integration deferred to v2)
- [x] Implement JWT Authentication/Authorization
- [x] Set up Docker Compose orchestration (API + DB)

### API Quality & Middleware Pipeline
- [x] Add Validation behavior pipeline
- [x] Implement global Exception Handling middleware
- [x] Add Performance-Logging behavior
- [x] Add Database Seeding (Seed Data for Rooms & RoomTypes)
- [x] Polish API Endpoint response contracts & status codes

### React Frontend (Core Booking Loop)
- [x] Set up React project (`BookingApp.UI`)
- [x] Implement Room & RoomType display (Search & List view)
- [x] Implement Booking creation UI & Form State
- [x] Add Basic Loading & Error UI Feedback
- [x] Add basic React app authorization
- [x] Set up Docker Compose orchestration (UI)

---

## v2 — Deployment & Enhancements

### Security & Advanced Auth
- [ ] Add Role-Based Authorization (RBAC UI Integration)
- [x] Implement Https into project

### Testing & Infrastructure
- [ ] Add Unit Tests (xUnit for CQRS Handlers & Domain Rules)
- [ ] Add Integration Tests (Testcontainers for PostgreSQL & EF Core)
- [ ] GitHub Actions CI/CD Pipeline

### Advanced API Polishing
- [ ] Integration of Domain Events