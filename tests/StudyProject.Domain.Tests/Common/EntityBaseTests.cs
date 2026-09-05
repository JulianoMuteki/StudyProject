using FluentAssertions;
using StudyProject.Domain.Common;
using StudyProject.Domain.Entities;
using Xunit;

namespace StudyProject.Domain.Tests.Common;

public class EntityBaseTests
{
    private static void SetId(EntityBase entity, Guid id)
        => typeof(EntityBase).GetProperty(nameof(EntityBase.ID))!.SetValue(entity, id);

    [Fact]
    public void Should_Equal_When_SameId()
    {
        var id = Guid.NewGuid();
        var a = new Client();
        var b = new Client();
        SetId(a, id);
        SetId(b, id);

        a.Equals(b).Should().BeTrue();
        (a == b).Should().BeTrue();
    }

    [Fact]
    public void Should_NotEqual_When_DifferentId()
    {
        var a = new Client();
        var b = new Client();

        a.Equals(b).Should().BeFalse();
        (a != b).Should().BeTrue();
    }

    [Fact]
    public void Should_NotEqual_When_ComparedToNull()
    {
        var a = new Client();

        a.Equals(null).Should().BeFalse();
        (a == null).Should().BeFalse();
        (a != null).Should().BeTrue();
    }

    [Fact]
    public void Should_Equal_Itself_ByReference()
    {
        var a = new Client();

        a.Equals(a).Should().BeTrue();
    }

    [Fact]
    public void Should_Generate_NonEmptyId_OnConstruction()
    {
        new Client().ID.Should().NotBe(Guid.Empty);
        new Product().ID.Should().NotBe(Guid.Empty);
    }

    [Fact]
    public void Should_Initialize_AuditDates_OnConstruction()
    {
        var entity = new Product();

        entity.CreationDate.Should().NotBe(default);
        entity.DateModified.Should().NotBe(default);
    }

    [Fact]
    public void Should_Have_Consistent_HashCode()
    {
        var a = new Client();
        var b = new Client();
        SetId(b, a.ID);

        a.GetHashCode().Should().Be(b.GetHashCode());
    }

    [Fact]
    public void ToString_Should_Contain_TypeName_And_Id()
    {
        var a = new Client();

        var text = a.ToString();

        text.Should().Contain(nameof(Client));
        text.Should().Contain(a.ID.ToString());
    }
}