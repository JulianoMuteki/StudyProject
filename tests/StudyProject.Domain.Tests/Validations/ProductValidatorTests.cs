using FluentAssertions;
using StudyProject.Domain.Entities;
using StudyProject.Domain.Validations;
using Xunit;

namespace StudyProject.Domain.Tests.Validations;

public class ProductValidatorTests
{
    private readonly ProductValidator _validator = new();

    [Fact]
    public void Should_Pass_When_NameAndDescriptionProvided()
    {
        var product = new Product { Name = "Notebook", Description = "A laptop" };

        _validator.Validate(product).IsValid.Should().BeTrue();
    }

    [Fact]
    public void Should_Fail_When_NameIsNull()
    {
        var product = new Product { Name = null, Description = "A laptop" };

        var result = _validator.Validate(product);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == nameof(Product.Name));
    }

    [Fact]
    public void Should_Fail_When_DescriptionIsNull()
    {
        var product = new Product { Name = "Notebook", Description = null };

        var result = _validator.Validate(product);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == nameof(Product.Description));
    }
}