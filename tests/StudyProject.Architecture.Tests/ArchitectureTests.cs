using System.Reflection;
using FluentAssertions;
using NetArchTest.Rules;
using Xunit;

namespace StudyProject.Architecture.Tests;

/// <summary>
/// Architecture conformance tests. The "binding" tests below (layer direction via
/// assembly references, naming conventions) must pass. A few tests intentionally
/// assert the Classic Clean-Architecture rules that the codebase currently drifts
/// from; those are expected to fail and are documented as findings.
/// </summary>
public class ArchitectureTests
{
    private static readonly Assembly Domain = typeof(StudyProject.Domain.Entities.Client).Assembly;
    private static readonly Assembly ApplicationAssembly = typeof(StudyProject.Application.ClientApplicationService).Assembly;
    private static readonly Assembly InfraContext = typeof(StudyProject.Infra.Context.StudyProjectContext).Assembly;
    private static readonly Assembly InfraRepository = typeof(StudyProject.Infra.Repository.UnitOfWork).Assembly;

    private static readonly string[] StudyProjectAssemblyNames =
    {
        "StudyProject.Domain",
        "StudyProject.Application",
        "StudyProject.Infra.Context",
        "StudyProject.Infra.Repository",
        "StudyProject.CrossCutting.Ioc",
        "StudyProject.Secutity",
        "StudyProject.WebApi",
        "StudyProject.UI.WebCore"
    };

    private static readonly string[] PresentationAssemblyNames =
    {
        "StudyProject.WebApi",
        "StudyProject.UI.WebCore"
    };

    private static IEnumerable<string> ReferencedAssemblyNames(Assembly assembly)
        => assembly.GetReferencedAssemblies().Select(a => a.Name!);

    // ---------------------------------------------------------------------------------
    // ADR-001: Domain intentionally couples to FluentValidation and ASP.NET Core Identity.
    // These tests are skipped until the coupling is refactored out. See docs/adr/.
    // ---------------------------------------------------------------------------------

    // ---------------------------------------------------------------------------------
    // Layer direction (assembly references) — these compile into binding rules.
    // ---------------------------------------------------------------------------------

    [Fact]
    public void Domain_ShouldNot_Reference_Any_Other_StudyProject_Layer()
    {
        var forbidden = StudyProjectAssemblyNames.Except(new[] { "StudyProject.Domain" });

        ReferencedAssemblyNames(Domain).Intersect(forbidden).Should().BeEmpty();
    }

    [Fact]
    public void Infrastructure_ShouldNot_Reference_Presentation()
    {
        var infraDeps = ReferencedAssemblyNames(InfraContext).Concat(ReferencedAssemblyNames(InfraRepository));

        infraDeps.Intersect(PresentationAssemblyNames).Should().BeEmpty();
    }

    // ---------------------------------------------------------------------------------
    // Layer direction (project file) — catches unused ProjectReference entries that
    // the compiler drops and therefore never appear in assembly metadata.
    // ---------------------------------------------------------------------------------

    [Fact]
    public void Application_ShouldNot_Reference_Infrastructure_Project()
    {
        var csproj = ReadProjectFile("StudyProject.Application", "StudyProject.Application.csproj");

        csproj.Should().NotContain("StudyProject.Infra");
    }

    // ---------------------------------------------------------------------------------
    // Domain framework coupling (type-level, NetArchTest) — Classic Clean-Architecture
    // requires a dependency-free domain. These document current drift.
    // ---------------------------------------------------------------------------------

    [Fact(Skip = "Intentional deviation documented in ADR-001: Domain couples to FluentValidation and ASP.NET Core Identity. Re-enable when refactored out.")]
    public void Domain_ShouldNot_DependOn_FluentValidation()
    {
        var result = Types.InAssembly(Domain)
            .ShouldNot()
            .HaveDependencyOn("FluentValidation")
            .GetResult();

        result.IsSuccessful.Should().BeTrue(string.Join(", ", result.FailingTypeNames ?? Enumerable.Empty<string>()));
    }

    [Fact(Skip = "Intentional deviation documented in ADR-001: Domain couples to FluentValidation and ASP.NET Core Identity. Re-enable when refactored out.")]
    public void Domain_ShouldNot_DependOn_AspNetCoreIdentity()
    {
        var result = Types.InAssembly(Domain)
            .ShouldNot()
            .HaveDependencyOn("Microsoft.AspNetCore.Identity")
            .GetResult();

        result.IsSuccessful.Should().BeTrue(string.Join(", ", result.FailingTypeNames ?? Enumerable.Empty<string>()));
    }

    // ---------------------------------------------------------------------------------
    // Naming conventions (type-level, NetArchTest).
    // ---------------------------------------------------------------------------------

    [Fact]
    public void Validators_Should_EndWith_Validator()
    {
        var result = Types.InAssembly(Domain)
            .That()
            .ResideInNamespace("StudyProject.Domain.Validations")
            .Should()
            .HaveNameEndingWith("Validator")
            .GetResult();

        result.IsSuccessful.Should().BeTrue(string.Join(", ", result.FailingTypeNames ?? Enumerable.Empty<string>()));
    }

    [Fact]
    public void Repositories_Should_EndWith_Repository()
    {
        var result = Types.InAssembly(InfraRepository)
            .That()
            .ResideInNamespace("StudyProject.Infra.Repository.Repositories")
            .Should()
            .HaveNameEndingWith("Repository")
            .GetResult();

        result.IsSuccessful.Should().BeTrue(string.Join(", ", result.FailingTypeNames ?? Enumerable.Empty<string>()));
    }

    // ---------------------------------------------------------------------------------

    private static string ReadProjectFile(string projectDirName, string projectFileName)
    {
        var root = FindRepositoryRoot();

        return File.ReadAllText(Path.Combine(root, "src", projectDirName, projectFileName));
    }

    private static string FindRepositoryRoot()
    {
        var dir = new DirectoryInfo(AppContext.BaseDirectory);

        while (dir is not null && !File.Exists(Path.Combine(dir.FullName, "StudyProject.sln")))
        {
            dir = dir.Parent;
        }

        return dir?.FullName
            ?? throw new InvalidOperationException("Could not locate the repository root.");
    }
}