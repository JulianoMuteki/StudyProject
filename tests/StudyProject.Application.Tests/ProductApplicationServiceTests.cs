using FluentAssertions;
using NSubstitute;
using StudyProject.Domain.Entities;
using StudyProject.Domain.Interfaces.Base;
using Xunit;

namespace StudyProject.Application.Tests;

public class ProductApplicationServiceTests
{
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
    private readonly IGenericRepository<Product> _repository = Substitute.For<IGenericRepository<Product>>();
    private readonly ProductApplicationService _service;

    public ProductApplicationServiceTests()
    {
        _unitOfWork.Repository<Product>().Returns(_repository);
        _service = new ProductApplicationService(_unitOfWork);
    }

    [Fact]
    public async Task GetAllProducts_Should_Return_RepositoryProducts()
    {
        ICollection<Product> products = new List<Product>
        {
            new() { Name = "Notebook" },
            new() { Name = "Mouse" }
        };
        _repository.GetAllAsync().Returns(Task.FromResult(products));

        var result = await _service.GetAllProducts();

        result.Should().HaveCount(2);
        await _repository.Received(1).GetAllAsync();
    }

    [Fact]
    public void GetAll_Should_Return_RepositoryProducts()
    {
        var products = new List<Product> { new() { Name = "Notebook" } };
        _repository.GetAll().Returns(products);

        var result = _service.GetAll();

        result.Should().ContainSingle(p => p.Name == "Notebook");
        _repository.Received(1).GetAll();
    }

    [Fact]
    public void GetAll_Should_ReturnEmpty_When_NoProducts()
    {
        _repository.GetAll().Returns(new List<Product>());

        _service.GetAll().Should().BeEmpty();
    }
}