# Study Project

[![.NET](https://github.com/JulianoMuteki/StudyProject/actions/workflows/dotnet.yml/badge.svg?branch=master)](https://github.com/JulianoMuteki/StudyProject/actions/workflows/dotnet.yml)

A study project for improving technical skills in .NET, modeling DDD + CQRS + SOLID patterns.

| Language | Description |
|----------|-------------|
| 🇺🇸 **English** | This project is used as a study base to improve my technical skills. I'm modeling according to tutorials, videos and books. It has still a lot of bugs but that's part of it. I'm open to suggestions, tips. |
| 🇧🇷 **Português** | Este projeto é usado como base de estudo para aperfeiçoar minhas habilidades técnicas. Estou modelando conforme tutoriais, vídeos e demos. Ainda muitos bugs mas faz parte. Estou aberto a sugestões, dicas 😁 |
| 🇮🇹 **Italiano** | Questo progetto viene utilizzato come base di studio per migliorare le mie capacità tecniche. Sto modellando secondo tutorial, video e libri. Ha ancora molti bug, ma ne fa parte. Sono aperto a suggerimenti, consigli. |

---

## 🛠️ Technology Stack

| Category | Technologies |
|----------|--------------|
| **API** | ASP.NET Core 6 Web API, Minimal APIs |
| **Web UI** | ASP.NET MVC Core 6, Razor Views |
| **Architecture** | DDD (Domain-Driven Design), CQRS patterns, SOLID principles |
| **Data Access** | Entity Framework Core 6, SQL Server, Repository + Unit of Work |
| **Validation** | FluentValidation 11 |
| **Mapping** | AutoMapper |
| **Authentication** | ASP.NET Core Identity, JWT Bearer tokens |
| **Authorization** | Policy-based, Role-based, Custom claims |
| **Documentation** | Swagger/OpenAPI (Swashbuckle) |
| **Messaging** | RabbitMQ (planned/configured) |
| **Database** | SQL Server 2019 (Docker) |
| **Containerization** | Docker, Docker Compose |
| **Cloud** | Azure (deployment target) |
| **Testing** | xUnit, FluentAssertions, Architecture Tests |
| **CI/CD** | GitHub Actions |

---

## 📁 Project Structure

```
StudyProject/
├── .claude/                    # Symlink to SDD .NET Platform (sdd-net-plugin)
├── .github/
│   ├── workflows/
│   │   └── dotnet.yml          # CI/CD pipeline
│   └── ISSUE_TEMPLATE/
│       └── net10-migration.md  # Issue template for .NET 10 migration
├── docker/
│   ├── docker-compose.yaml     # Base compose (production-ready)
│   ├── docker-compose.dev.yaml # Development overrides
│   ├── .env.dev                # Dev environment variables
│   └── secret.json             # User secrets template
├── src/
│   ├── StudyProject.Domain/           # Domain Layer (Entities, VOs, Interfaces, Validations)
│   ├── StudyProject.Application/      # Application Layer (Services, ViewModels, AutoMapper)
│   ├── StudyProject.Infra.Data/       # Data Layer (Repositories, UnitOfWork, GenericRepository)
│   ├── StudyProject.Infra.Context/    # EF Core Context, Mappings, Migrations
│   ├── StudyProject.CrossCutting.Ioc/ # Dependency Injection Configuration
│   ├── StudyProject.Secutity/         # Security Layer (JWT, Policies, Custom Claims)
│   ├── StudyProject.UI.Web/           # MVC Web Application (Controllers, Views, Services)
│   └── StudyProject.WebApi/           # REST API (Controllers, Program.cs, Health Checks)
├── tests/
│   ├── StudyProject.Domain.Tests/         # Domain entity & validation tests
│   ├── StudyProject.Application.Tests/    # Application service & AutoMapper tests
│   ├── StudyProject.Infra.Data.Tests/     # Repository & UnitOfWork tests
│   ├── StudyProject.Architecture.Tests/   # NetArchTest architecture compliance tests
│   └── StudyProject.Secutity.Tests/       # Security/Authorization tests
├── docs/                              # Documentation (currently empty)
├── Dockerfile                         # Multi-stage Dockerfile
├── StudyProject.sln                   # Solution file
└── README.md                          # This file
```

### Solution Folder Organization

The solution uses folders for logical grouping:

| Folder | Projects |
|--------|----------|
| **0 - Presentations** | StudyProject.UI.Web, StudyProject.WebApi |
| **1 - Services** | (empty - placeholder for future services) |
| **2 - Application** | StudyProject.Application |
| **3 - Domain** | StudyProject.Domain |
| **4 - Infra** | StudyProject.Infra.Context, StudyProject.CrossCutting.Ioc |
| **4.1 - CrossCutting** | StudyProject.CrossCutting.Ioc |
| **4.2 - Data** | StudyProject.Infra.Data |
| **5 - Docker** | (docker compose files) |
| **tests** | All test projects |

---

## 🏗️ Architecture Overview

### Layer Dependencies

```
┌─────────────────────────────────────────────────────────────┐
│                    Presentation Layer                        │
│  StudyProject.WebApi  │  StudyProject.UI.Web                │
└─────────────────────┬───────────────────────────────────────┘
                      │
                      ▼
┌─────────────────────────────────────────────────────────────┐
│                    Application Layer                         │
│                  StudyProject.Application                    │
│  Services: ProductApplicationService, ClientApplicationService│
│  ViewModels: ProductVM, ClientVM, Identity ViewModels        │
│  AutoMapper Profiles                                         │
└─────────────────────┬───────────────────────────────────────┘
                      │
                      ▼
┌─────────────────────────────────────────────────────────────┐
│                      Domain Layer                            │
│                    StudyProject.Domain                       │
│  Entities: Product, Client, ClientProductValue              │
│  Identity: ApplicationUser, ApplicationRole, Claims         │
│  Interfaces: IUnitOfWork, IGenericRepository, I*AppService  │
│  Validations: ClientValidator, ProductValidator (FluentVal) │
│  Common: EntityBase, ValueObject, ValidateBase              │
└─────────────────────┬───────────────────────────────────────┘
                      │
                      ▼
┌─────────────────────────────────────────────────────────────┐
│                   Infrastructure Layer                       │
│  StudyProject.Infra.Context  │  StudyProject.Infra.Data      │
│  StudyProject.CrossCutting.Ioc  │  StudyProject.Secutity     │
│  EF Core Context, Mappings    │  Repositories, UnitOfWork    │
│  Migrations                   │  GenericRepository<T>        │
└─────────────────────────────────────────────────────────────┘
```

### Key Architectural Patterns

| Pattern | Implementation |
|---------|----------------|
| **Domain-Driven Design** | Entities inherit `EntityBase`, Value Objects, Domain Validations |
| **Repository + Unit of Work** | `IGenericRepository<T>`, `IUnitOfWork`, `GenericRepository<T>` |
| **CQRS (Partial)** | Separate read/write via Application Services + ViewModels |
| **Dependency Injection** | `CrossCutting.Ioc` bootstrappers for Application/Infrastructure |
| **AutoMapper** | Profiles for Entity ↔ ViewModel mapping |
| **FluentValidation** | Validators for domain entities |
| **Policy-Based Auth** | Custom `AuthorizePolicyEnum` attribute with `PERMISSIONS` enum |

---

## 🚀 Getting Started

### Prerequisites

- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/)
- [Git](https://git-scm.com/)
- SQL Server 2019+ (or use Docker)

### Quick Start with Docker

```bash
# 1. Clone the repository
git clone https://github.com/JulianoMuteki/StudyProject.git
cd StudyProject

# 2. Configure User Secrets (required for JWT & DB connection)
# See: docker/secret.json for template
# Create folder at: %APPDATA%\Microsoft\UserSecrets\$USER_SECRETS_ID
# Copy docker/secret.json there and update values
# Add USER_SECRETS_ID to docker/.env.dev

# 3. Build and run with Docker Compose
docker-compose -f docker/docker-compose.yaml -f docker/docker-compose.dev.yaml --env-file docker/.env.dev build
docker-compose -f docker/docker-compose.yaml -f docker/docker-compose.dev.yaml --env-file docker/.env.dev up -d

# 4. Access the application
# Web API: http://localhost:8003
# Swagger: http://localhost:8003/swagger
# Web UI:  http://localhost:8003 (if configured)
```

### Local Development (without Docker)

```bash
# 1. Restore dependencies
dotnet restore

# 2. Update database (run migrations)
dotnet ef database update --project src/StudyProject.Infra.Context --startup-project src/StudyProject.WebApi

# 3. Run Web API
dotnet run --project src/StudyProject.WebApi

# 4. Run Web UI (in separate terminal)
dotnet run --project src/StudyProject.UI.Web
```

### User Secrets Configuration

Create `secrets.json` in your User Secrets folder:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=StudyProject;User=sa;Password=YourPassword;TrustServerCertificate=True;"
  },
  "Jwt": {
    "key": "your-super-secret-jwt-key-min-32-chars",
    "Issuer": "StudyProject",
    "Audience": "StudyProjectUsers"
  },
  "TokenConfiguration": {
    "Issuer": "StudyProject",
    "Audience": "StudyProjectUsers",
    "ExpireHours": 2
  },
  "EmailSettings": {
    "SmtpServer": "smtp.gmail.com",
    "SmtpPort": 587,
    "SenderEmail": "your-email@gmail.com",
    "SenderPassword": "your-app-password"
  }
}
```

---

## 📚 API Endpoints

### Authentication (`/api/Account`)

| Method | Endpoint | Description | Auth |
|--------|----------|-------------|------|
| `POST` | `/api/Account/Login` | User login, returns JWT | ❌ Anonymous |
| `POST` | `/api/Account/Register` | Register new user | ❌ Anonymous |
| `POST` | `/api/Account/ValidateRegister` | Confirm email with token | ❌ Anonymous |
| `POST` | `/api/Account/SendTokenEmail` | Resend confirmation email | ❌ Anonymous |

### Products (`/api/Product`)

| Method | Endpoint | Description | Auth | Policy |
|--------|----------|-------------|------|--------|
| `GET` | `/api/Product` | List all products | ✅ JWT | - |
| `DELETE` | `/api/Product/{id}` | Delete product | ✅ JWT | `PERMISSIONS.Delete` |

### Authorization (`/api/Authorization`)

| Method | Endpoint | Description | Auth | Policy |
|--------|----------|-------------|------|--------|
| `GET` | `/api/Authorization` | Admin/Manager index | ✅ JWT | `RoleAuthorize.Admin/Manager`, `PERMISSIONS.Index` |
| `DELETE` | `/api/Authorization/{id}` | Delete (admin) | ✅ JWT | `PERMISSIONS.Delete` |

### User Management (`/UserManagement`)

| Method | Endpoint | Description | Auth | Roles |
|--------|----------|-------------|------|-------|
| `GET` | `/UserManagement/Users` | List all users | ✅ JWT | Admin |
| `POST` | `/UserManagement/ResetPassword` | Reset user password | ✅ JWT | Admin |
| `PUT` | `/UserManagement/ChangePassword` | Change own password | ✅ JWT | Admin |
| `DELETE` | `/UserManagement/{email}` | Delete user | ✅ JWT | Admin |

### Health Check

| Method | Endpoint | Description |
|--------|----------|-------------|
| `GET` | `/Health` | Health check endpoint |

---

## 🧪 Testing

### Run All Tests

```bash
dotnet test --verbosity normal
```

### Run Specific Test Project

```bash
# Domain tests (entities, validations)
dotnet test tests/StudyProject.Domain.Tests/

# Application tests (services, AutoMapper)
dotnet test tests/StudyProject.Application.Tests/

# Infrastructure data tests (repositories, UoW)
dotnet test tests/StudyProject.Infra.Data.Tests/

# Architecture tests (NetArchTest)
dotnet test tests/StudyProject.Architecture.Tests/

# Security tests (policies, tokens)
dotnet test tests/StudyProject.Secutity.Tests/
```

### Test Coverage

```bash
dotnet test --collect:"XPlat Code Coverage"
# Report: TestResults/<guid>/coverage.cobertura.xml
```

### Test Structure

| Project | Focus | Key Tests |
|---------|-------|-----------|
| `Domain.Tests` | Entities, Value Objects, Validators | `ClientTests`, `ProductTests`, `ClientValidatorTests`, `ProductValidatorTests` |
| `Application.Tests` | Services, AutoMapper | `ProductApplicationServiceTests`, `ClientApplicationServiceTests`, `AutoMapperTests` |
| `Infra.Data.Tests` | Repositories, UnitOfWork | `GenericRepositoryTests`, `UnitOfWorkTests` |
| `Architecture.Tests` | Layer boundaries, naming | `ArchitectureTests` (NetArchTest) |
| `Secutity.Tests` | Auth policies, tokens | `AuthorizeEnumTests`, `CustomTokenTests`, `PolicyTypesTests` |

---

## 🐳 Docker Configuration

### Files

| File | Purpose |
|------|---------|
| `docker/Dockerfile` | Multi-stage build (build → runtime) |
| `docker/docker-compose.yaml` | Base services (API + SQL Server) |
| `docker/docker-compose.dev.yaml` | Dev overrides (ports, volumes, env) |
| `docker/.env.dev` | Environment variables for dev |
| `docker/secret.json` | User secrets template |

### Key Docker Features

- **Multi-stage build** for smaller runtime image
- **Health checks** for SQL Server dependency
- **User Secrets** mounted as volume for local development
- **Non-root user** in container (security best practice)
- **Environment-based configuration** via `.env.dev`

---

## 🔄 CI/CD Pipeline (GitHub Actions)

### Workflow: `.github/workflows/dotnet.yml`

Triggers: Push to `master`, Pull Requests to `master`

| Step | Command | Purpose |
|------|---------|---------|
| Checkout | `actions/checkout@v3` | Clone repository |
| Setup .NET | `actions/setup-dotnet@v2` | Install .NET 6.x |
| Restore | `dotnet restore` | Restore NuGet packages |
| Build | `dotnet build --no-restore` | Compile all projects |
| Test | `dotnet test --no-build --verbosity normal` | Run all tests |

### Adding Security Scans (Recommended)

```yaml
# Add to dotnet.yml
- name: Security Scan
  run: |
    dotnet tool install --global dotnet-scan
    dotnet scan StudyProject.sln --format sarif --output security.sarif
- name: Upload SARIF
  uses: github/codeql-action/upload-sarif@v3
  with:
    sarif_file: security.sarif

- name: Dependency Check
  uses: dependency-check/Dependency-Check_Action@main

- name: Container Scan
  uses: aquasecurity/trivy-action@master
  with:
    severity: 'CRITICAL,HIGH'
```

---

## 🔐 Security Features

| Feature | Implementation |
|---------|----------------|
| **Authentication** | JWT Bearer tokens via `Microsoft.AspNetCore.Authentication.JwtBearer` |
| **Authorization** | Policy-based (`AuthorizePolicyEnum`), Role-based (`AuthorizeEnum`) |
| **Identity** | ASP.NET Core Identity with custom `ApplicationUser`/`ApplicationRole` |
| **Password Hashing** | Built-in Identity password hashing (PBKDF2) |
| **Token Validation** | Issuer, Audience, Lifetime, Signing Key validation |
| **Custom Claims** | `CustomClaimTypes`, `PolicyTypes`, `RoleAuthorize` |
| **Rate Limiting** | Not yet implemented (planned) |
| **CORS** | Not yet configured (planned) |

---

## 📦 Key NuGet Packages

| Package | Version | Project(s) |
|---------|---------|------------|
| `Microsoft.AspNetCore.Authentication.JwtBearer` | 6.0.4 | WebApi, Security |
| `Microsoft.AspNetCore.Identity.EntityFrameworkCore` | 6.0.4 | Domain, Infra.Context |
| `Microsoft.EntityFrameworkCore.SqlServer` | 6.0.4 | Infra.Context |
| `Microsoft.EntityFrameworkCore.Design` | 6.0.4 | Infra.Context |
| `FluentValidation` | 11.0.0 | Domain |
| `AutoMapper` / `AutoMapper.Extensions.Microsoft.DependencyInjection` | Latest | Application, UI.Web, CrossCutting.Ioc |
| `Swashbuckle.AspNetCore.SwaggerGen` | 6.3.1 | WebApi |
| `Swashbuckle.AspNetCore.SwaggerUI` | 6.4.0 | WebApi |
| `Microsoft.Extensions.Diagnostics.HealthChecks.EntityFrameworkCore` | 6.0.5 | WebApi |
| `xunit` / `xunit.runner.visualstudio` | Latest | All test projects |
| `FluentAssertions` | Latest | All test projects |
| `NetArchTest.Rules` | Latest | Architecture.Tests |

---

## 🎯 Learning Goals

This project targets learning and practicing:

- [ ] **DDD** - Domain entities, value objects, aggregates, domain events
- [ ] **CQRS** - Command/Query separation with MediatR (planned)
- [ ] **SOLID** - Single responsibility, open/closed, Liskov substitution, interface segregation, dependency inversion
- [ ] **Clean Architecture** - Layer separation, dependency rule
- [ ] **Testing Strategies** - Unit, integration, architecture, contract tests
- [ ] **Containerization** - Docker multi-stage, compose, health checks
- [ ] **CI/CD** - GitHub Actions, security scanning, quality gates
- [ ] **Observability** - Health checks, logging, metrics (planned)

---

## 📋 Current Limitations / Known Issues

| Area | Issue |
|------|-------|
| **ProductController** | `Delete` returns hardcoded string, doesn't actually delete |
| **AuthorizationController** | `Get`/`Delete` return hardcoded strings |
| **Error Handling** | Generic `catch` blocks returning `BadRequest` without details |
| **Logging** | Minimal structured logging |
| **Validation** | Some controllers lack FluentValidation integration |
| **CQRS** | Not fully implemented (commands/queries separated) |
| **MediatR** | Not yet integrated |
| **API Versioning** | Not implemented |
| **Rate Limiting** | Not implemented |
| **Integration Tests** | No Testcontainers-based integration tests yet |

---

## 🛣️ Roadmap

- [ ] Migrate to .NET 8 / .NET 10 (see issue template)
- [ ] Implement MediatR for CQRS
- [ ] Add FluentValidation pipeline behavior
- [ ] Implement proper error handling with `ProblemDetails`
- [ ] Add integration tests with Testcontainers
- [ ] Add contract tests (Pact)
- [ ] Implement refresh token rotation
- [ ] Add rate limiting middleware
- [ ] Configure Serilog + Seq/Elasticsearch
- [ ] Add OpenTelemetry distributed tracing
- [ ] Implement RabbitMQ event publishing
- [ ] Add API versioning (`/api/v1/`, `/api/v2/`)
- [ ] Improve test coverage to >80%
- [ ] Add architecture decision records (ADRs)

---

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch: `git checkout -b feature/my-feature`
3. Follow the existing code style and patterns
4. Write tests for new functionality
5. Ensure all tests pass: `dotnet test`
6. Submit a Pull Request

### Code Style

- Follow [.NET Coding Conventions](https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions)
- Use `dotnet format` before committing
- Enable `Nullable` and `ImplicitUsings` in new projects
- Prefer `record` types for DTOs/ViewModels
- Use `Result<T>` pattern for error handling (avoid exceptions for control flow)

---

## 📄 License

This project is for **educational/study purposes only**. No license specified — treat as personal learning repository.

---

## 🔗 References & Resources

- [.NET Documentation](https://docs.microsoft.com/en-us/dotnet/)
- [ASP.NET Core Documentation](https://docs.microsoft.com/en-us/aspnet/core/)
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)
- [FluentValidation](https://fluentvalidation.net/)
- [AutoMapper](https://automapper.org/)
- [DDD Reference](https://domainlanguage.com/ddd/)
- [Clean Architecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
- [GitHub Actions for .NET](https://docs.github.com/en/actions/guides/building-and-testing-net)

---

## 📝 Changelog

| Date | Version | Changes |
|------|---------|---------|
| 2026-09-08 | — | README restructured and expanded |
| 2022-12-21 | — | Dockerfile added |
| 2022-04-30 | — | Initial commit |

---

*Last updated: 2026-09-08*