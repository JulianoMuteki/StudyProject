using FluentAssertions;
using StudyProject.Domain.Entities;
using Xunit;

namespace StudyProject.Domain.Tests.Entities;

public class ClientProductValueTests
{
    private static ClientProductValue NewValue(Guid clientId, Guid productId, float price)
        => new()
        {
            ClientID = clientId,
            ProductID = productId,
            Price = price
        };

    [Fact]
    public void Should_Equal_When_SameValues()
    {
        var clientId = Guid.NewGuid();
        var productId = Guid.NewGuid();

        var a = NewValue(clientId, productId, 10f);
        var b = NewValue(clientId, productId, 10f);

        a.Equals(b).Should().BeTrue();
        (a == b).Should().BeTrue();
        a.GetHashCode().Should().Be(b.GetHashCode());
    }

    [Fact]
    public void Should_NotEqual_When_PriceDiffers()
    {
        var clientId = Guid.NewGuid();
        var productId = Guid.NewGuid();

        var a = NewValue(clientId, productId, 10f);
        var b = NewValue(clientId, productId, 20f);

        a.Equals(b).Should().BeFalse();
        (a != b).Should().BeTrue();
    }

    [Fact]
    public void Should_NotEqual_When_KeyDiffers()
    {
        var productId = Guid.NewGuid();

        var a = NewValue(Guid.NewGuid(), productId, 10f);
        var b = NewValue(Guid.NewGuid(), productId, 10f);

        a.Equals(b).Should().BeFalse();
    }

    [Fact]
    public void Should_NotEqual_When_ComparedToNull()
    {
        var a = NewValue(Guid.NewGuid(), Guid.NewGuid(), 10f);

        a.Equals(null).Should().BeFalse();
        (a == null).Should().BeFalse();
        (a != null).Should().BeTrue();
    }
}