using FluentAssertions;
using StudyProject.Secutity.Auth;
using Xunit;

namespace StudyProject.Secutity.Tests;

public class AuthorizeEnumTests
{
    [Fact]
    public void Should_Set_Roles_From_Enum_Values()
    {
        var attribute = new AuthorizeEnum(RoleAuthorize.Admin, RoleAuthorize.Manager);

        attribute.Roles.Should().Be("Admin, Manager");
    }

    [Fact]
    public void Should_Throw_When_Argument_IsNot_AnEnum()
    {
        var act = () => new AuthorizeEnum("Admin");

        act.Should().Throw<ArgumentException>().WithMessage("*roles*");
    }

    [Fact]
    public void Should_Allow_Empty_RoleList()
    {
        var attribute = new AuthorizeEnum();

        attribute.Roles.Should().BeEmpty();
    }
}