using FluentAssertions;
using StudyProject.Domain.Entities;
using StudyProject.Domain.Interfaces.Repository;
using StudyProject.Infra.Repository;
using StudyProject.Infra.Repository.Common;
using StudyProject.Infra.Repository.Repositories;
using Xunit;

namespace StudyProject.Infra.Data.Tests;

public class UnitOfWorkTests
{
    [Fact]
    public void Repository_Should_Return_GenericRepository()
    {
        using var context = TestContextFactory.CreateContext();
        var uow = new UnitOfWork(context);

        var repo = uow.Repository<Client>();

        repo.Should().BeOfType<GenericRepository<Client>>();
    }

    [Fact]
    public void Repository_Should_Reuse_Cached_Instance()
    {
        using var context = TestContextFactory.CreateContext();
        var uow = new UnitOfWork(context);

        var first = uow.Repository<Client>();
        var second = uow.Repository<Client>();

        first.Should().BeSameAs(second);
    }

    [Fact]
    public void RepositoryCustom_Should_Return_ClientRepository()
    {
        using var context = TestContextFactory.CreateContext();
        var uow = new UnitOfWork(context);

        var repo = uow.RepositoryCustom<IClientRepository>();

        repo.Should().BeOfType<ClientRepository>();
    }

    [Fact]
    public void RepositoryCustom_Should_Return_ProductRepository()
    {
        using var context = TestContextFactory.CreateContext();
        var uow = new UnitOfWork(context);

        var repo = uow.RepositoryCustom<IProductRepository>();

        repo.Should().BeOfType<ProductRepository>();
    }

    [Fact]
    public void Commit_Should_Persist_Added_Entities()
    {
        using var context = TestContextFactory.CreateContext();
        var uow = new UnitOfWork(context);
        var client = new Client { Name = "Juliano", LastName = "Pestili", Email = "j@example.com" };

        uow.Repository<Client>().Add(client);
        var changes = uow.Commit();

        changes.Should().Be(1);
        context.Clients.Count().Should().Be(1);
    }

    [Fact]
    public async Task CommitAsync_Should_Persist_Added_Entities()
    {
        using var context = TestContextFactory.CreateContext();
        var uow = new UnitOfWork(context);
        var client = new Client { Name = "Juliano", LastName = "Pestili", Email = "j@example.com" };

        uow.Repository<Client>().Add(client);
        var changes = await uow.CommitAsync();

        changes.Should().Be(1);
        context.Clients.Count().Should().Be(1);
    }
}