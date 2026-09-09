# ADR-001: Domain Coupling to FluentValidation and ASP.NET Core Identity

## Status: Accepted

## Context
The Domain project references `FluentValidation` and `Microsoft.AspNetCore.Identity.EntityFrameworkCore`.
This violates the Clean Architecture principle that the Domain layer should have zero external dependencies.

These dependencies exist because:
- Domain validators (`ClientValidator`, `ProductValidator`) implement FluentValidation `AbstractValidator<T>`
- Domain entities (`ApplicationUser`, `ApplicationRole`) inherit from ASP.NET Core Identity EF Core types

## Decision
Document this as an intentional deviation. Keep the current coupling; plan to refactor later.

## Rationale
1. **FluentValidation in Domain**: Centralizes validation rules close to the entities they validate. Moving validators to the Application layer would require duplicating entity knowledge or leaking domain internals.
2. **Identity EF Core in Domain**: The `ApplicationUser` and `ApplicationRole` entities are domain concepts that map directly to ASP.NET Core Identity tables. Refactoring to remove this dependency would require a full rewrite of the identity model and is deferred to a future sprint.

## Consequences
### Positive
- Minimal disruption to existing working code
- Clear audit trail of the intentional deviation
- Tests continue to pass

### Negative
- Domain is not independently deployable/testable without referencing ASP.NET Core packages
- Domain cannot be reused in a non-web context without bringing in Identity dependencies

### Risks & Mitigations
- Risk: Future developers add new infrastructure dependencies to Domain
- Mitigation: Architecture tests (`Domain_ShouldNot_DependOn_FluentValidation`, `Domain_ShouldNot_DependOn_AspNetCoreIdentity`) serve as gatekeepers; any new coupling will be flagged in CI

## Alternatives Considered
1. **Move validators to Application layer**: Rejected — would require passing domain entities to Application for validation, breaking encapsulation
2. **Rewrite Identity model**: Rejected — too large a scope for this issue; deferred

## References
- Issue #16 T-02
- Architecture tests in `tests/StudyProject.Architecture.Tests/ArchitectureTests.cs`
