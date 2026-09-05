using FluentAssertions;
using StudyProject.Domain.Entities;
using Xunit;

namespace StudyProject.Domain.Tests.Entities;

public class ProductTests
{
    [Fact]
    public void Should_Initialize_On_Construction()
    {
        var product = new Product();

        product.ID.Should().NotBe(Guid.Empty);
        product.CreationDate.Should().NotBe(default);
        product.DateModified.Should().NotBe(default);
        product.ClientsProductsValues.Should().NotBeNull();
        product.ClientsProductsValues.Should().BeEmpty();
    }

    [Fact]
    public void Should_Set_Properties()
    {
        var product = new Product
        {
            Name = "Notebook",
            Description = "A laptop",
            Weight = 1.5f
        };

        product.Name.Should().Be("Notebook");
        product.Description.Should().Be("A laptop");
        product.Weight.Should().Be(1.5f);
    }
}