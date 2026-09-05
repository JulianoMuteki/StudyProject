using FluentAssertions;
using StudyProject.Secutity.Auth;
using Xunit;

namespace StudyProject.Secutity.Tests;

public class PolicyTypesTests
{
    [Fact]
    public void ListAllRoles_Should_Contain_All_Declared_Roles()
    {
        PolicyTypes.ListAllRoles.Should().HaveCount(6);
        PolicyTypes.ListAllRoles.Should().Contain(new[] { "Admin", "Manager", "Staff", "Client", "Employee", "Driver" });
    }

    [Fact]
    public void ListAllClaims_Should_Contain_All_Permissions()
    {
        PolicyTypes.ListAllClaims.Keys.Should().HaveCount(5);
        PolicyTypes.ListAllClaims.Keys.Should().BeEquivalentTo(new[]
        {
            PERMISSIONS.Create, PERMISSIONS.Read, PERMISSIONS.Update, PERMISSIONS.Delete, PERMISSIONS.Index
        });
    }

    [Fact]
    public void ClaimValues_Should_Follow_DefaultPolicy_Naming()
    {
        PolicyTypes.ListAllClaims[PERMISSIONS.Create].Value.Should().Be("default.policy.create");
        PolicyTypes.ListAllClaims[PERMISSIONS.Delete].Value.Should().Be("default.policy.delete");
    }
}