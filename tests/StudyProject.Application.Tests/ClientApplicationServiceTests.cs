using FluentAssertions;
using NSubstitute;
using StudyProject.Domain.Entities;
using StudyProject.Domain.Interfaces.Base;
using Xunit;

namespace StudyProject.Application.Tests;

public class ClientApplicationServiceTests
{
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
    private readonly IGenericRepository<Client> _repository = Substitute.For<IGenericRepository<Client>>();
    private readonly ClientApplicationService _service;

    public ClientApplicationServiceTests()
    {
        _unitOfWork.Repository<Client>().Returns(_repository);
        _service = new ClientApplicationService(_unitOfWork);
    }

    [Fact]
    public void GetAll_Should_Return_RepositoryClients()
    {
        var clients = new List<Client>
        {
            new() { Name = "A" },
            new() { Name = "B" }
        };
        _repository.GetAll().Returns(clients);

        var result = _service.GetAll();

        result.Should().HaveCount(2);
        _unitOfWork.Received(1).Repository<Client>();
        _repository.Received(1).GetAll();
    }

    [Fact]
    public void GetAll_Should_ReturnEmpty_When_NoClients()
    {
        _repository.GetAll().Returns(new List<Client>());

        _service.GetAll().Should().BeEmpty();
    }

    [Fact]
    public async Task GetAllClient_Should_Return_RepositoryClientsAsync()
    {
        var clients = new List<Client>
        {
            new() { Name = "A" },
            new() { Name = "B" }
        };
        _repository.GetAllAsync().Returns(Task.FromResult<ICollection<Client>>(clients));

        var result = await _service.GetAllClient();

        result.Should().HaveCount(2);
        _unitOfWork.Received(1).Repository<Client>();
        await _repository.Received(1).GetAllAsync();
    }
}