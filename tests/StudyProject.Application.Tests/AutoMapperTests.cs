using AutoMapper;
using FluentAssertions;
using StudyProject.Application.AutoMapper;
using StudyProject.Application.ViewModels;
using StudyProject.Domain.Entities;
using Xunit;

namespace StudyProject.Application.Tests;

public class AutoMapperTests
{
    private readonly IMapper _mapper = AutoMapperConfig.RegisterMappings().CreateMapper();

    [Fact]
    public void Configuration_Should_BeValid()
    {
        AutoMapperConfig.RegisterMappings().AssertConfigurationIsValid();
    }

    [Fact]
    public void Should_Map_Client_To_ClientVM()
    {
        var client = new Client
        {
            Name = "Juliano",
            LastName = "Pestili",
            Email = "j@example.com",
            IsActive = true
        };

        var vm = _mapper.Map<ClientVM>(client);

        vm.Name.Should().Be("Juliano");
        vm.LastName.Should().Be("Pestili");
        vm.Email.Should().Be("j@example.com");
        vm.IsActive.Should().BeTrue();
    }

    [Fact]
    public void Should_Map_ClientVM_To_Client()
    {
        var vm = new ClientVM { Name = "Juliano", LastName = "Pestili", Email = "j@example.com" };

        var client = _mapper.Map<Client>(vm);

        client.Name.Should().Be("Juliano");
        client.LastName.Should().Be("Pestili");
        client.Email.Should().Be("j@example.com");
    }

    [Fact]
    public void Should_Map_Product_To_ProductVM()
    {
        var product = new Product { Name = "Notebook", Description = "A laptop", Weight = 1.5f };

        var vm = _mapper.Map<ProductVM>(product);

        vm.Name.Should().Be("Notebook");
        vm.Description.Should().Be("A laptop");
        vm.Weight.Should().Be(1.5f);
    }

    [Fact]
    public void ProductVM_To_Product_Should_Initialize_IdVia_AfterMap()
    {
        var vm = new ProductVM { Name = "Notebook", Description = "A laptop", Weight = 1.5f };

        var product = _mapper.Map<Product>(vm);

        product.Name.Should().Be("Notebook");
        product.ID.Should().NotBe(Guid.Empty);
    }
}