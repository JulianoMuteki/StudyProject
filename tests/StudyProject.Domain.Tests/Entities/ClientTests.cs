using FluentAssertions;
using StudyProject.Domain.Entities;
using Xunit;

namespace StudyProject.Domain.Tests.Entities;

public class ClientTests
{
    [Fact]
    public void Should_Initialize_On_Construction()
    {
        var client = new Client();

        client.ID.Should().NotBe(Guid.Empty);
        client.CreationDate.Should().NotBe(default);
        client.DateModified.Should().NotBe(default);
        client.ClientsProductsValues.Should().NotBeNull();
        client.ClientsProductsValues.Should().BeEmpty();
    }

    [Fact]
    public void Should_Set_Properties()
    {
        var client = new Client
        {
            Name = "Juliano",
            LastName = "Pestili",
            Email = "j@example.com",
            IsActive = true
        };

        client.Name.Should().Be("Juliano");
        client.LastName.Should().Be("Pestili");
        client.Email.Should().Be("j@example.com");
        client.IsActive.Should().BeTrue();
    }
}