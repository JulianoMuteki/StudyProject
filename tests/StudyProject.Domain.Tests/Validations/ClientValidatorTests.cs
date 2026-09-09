using FluentAssertions;
using StudyProject.Domain.Entities;
using StudyProject.Domain.Validations;
using Xunit;

namespace StudyProject.Domain.Tests.Validations;

public class ClientValidatorTests
{
    private readonly ClientValidator _validator = new();

    [Fact]
    public void Should_Pass_When_NameLastNameAndEmailProvided()
    {
        var client = new Client { Name = "Juliano", LastName = "Pestili", Email = "j@example.com" };

        _validator.Validate(client).IsValid.Should().BeTrue();
    }

    [Fact]
    public void Should_Fail_When_LastNameIsNull()
    {
        var client = new Client { Name = "Juliano", Email = "j@example.com" };

        var result = _validator.Validate(client);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == nameof(Client.LastName));
    }

    [Fact]
    public void Should_Fail_When_NameIsNull()
    {
        var client = new Client { Name = null, Email = "j@example.com" };

        var result = _validator.Validate(client);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == nameof(Client.Name));
    }

    [Fact]
    public void Should_Fail_When_EmailIsNull()
    {
        var client = new Client { Name = "Juliano", Email = null };

        var result = _validator.Validate(client);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == nameof(Client.Email));
    }
}