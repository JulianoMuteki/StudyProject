using FluentAssertions;
using StudyProject.Domain.Common;
using StudyProject.Domain.Entities;
using StudyProject.Domain.Validations;
using Xunit;

namespace StudyProject.Domain.Tests.Common;

public class ValidateBaseTests
{
    private readonly ValidateBase _validate = new();

    [Fact]
    public void Should_ReturnTrue_When_ModelIsValid()
    {
        var client = new Client { Name = "Juliano", Email = "j@example.com" };

        var ok = _validate.Validate(client, new ClientValidator());

        ok.Should().BeTrue();
        _validate.IsValid.Should().BeTrue();
        _validate.ValidationResult.Errors.Should().BeEmpty();
    }

    [Fact]
    public void Should_ReturnFalse_When_ModelIsInvalid()
    {
        var client = new Client { Name = null, Email = null };

        var ok = _validate.Validate(client, new ClientValidator());

        ok.Should().BeFalse();
        _validate.IsValid.Should().BeFalse();
        _validate.ValidationResult.Errors.Should().NotBeEmpty();
    }
}