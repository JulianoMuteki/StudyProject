using FluentAssertions;
using StudyProject.Domain.Entities;
using StudyProject.Infra.Repository.Common;
using Xunit;

namespace StudyProject.Infra.Data.Tests;

public class GenericRepositoryTests
{
    private static Client NewClient(string name = "Juliano", string email = "j@example.com")
        => new() { Name = name, LastName = "Pestili", Email = email };

    [Fact]
    public void Add_Should_Return_Entity_And_Persist_After_Save()
    {
        using var context = TestContextFactory.CreateContext();
        var repo = new GenericRepository<Client>(context);

        var added = repo.Add(NewClient());

        added.Should().NotBeNull();
        context.SaveChanges();
        repo.GetAll().Should().ContainSingle(c => c.Name == "Juliano");
    }

    [Fact]
    public void GetAll_Should_Be_Empty_Initially()
    {
        using var context = TestContextFactory.CreateContext();
        var repo = new GenericRepository<Client>(context);

        repo.GetAll().Should().BeEmpty();
    }

    [Fact]
    public async Task GetAllAsync_Should_Return_All_Entities()
    {
        using var context = TestContextFactory.CreateContext();
        var repo = new GenericRepository<Client>(context);
        repo.Add(NewClient("A"));
        repo.Add(NewClient("B"));
        context.SaveChanges();

        var result = await repo.GetAllAsync();

        result.Should().HaveCount(2);
    }

    [Fact]
    public void Find_Should_Return_Matching_Entity()
    {
        using var context = TestContextFactory.CreateContext();
        var repo = new GenericRepository<Client>(context);
        repo.Add(NewClient("Juliano"));
        context.SaveChanges();

        var found = repo.Find(c => c.Name == "Juliano");

        found.Should().NotBeNull();
        found!.Name.Should().Be("Juliano");
    }

    [Fact]
    public void FindAll_Should_Return_All_Matching()
    {
        using var context = TestContextFactory.CreateContext();
        var repo = new GenericRepository<Client>(context);
        repo.Add(NewClient("Juliano", "a@x.com"));
        repo.Add(NewClient("Juliano", "b@x.com"));
        repo.Add(NewClient("Other"));
        context.SaveChanges();

        var results = repo.FindAll(c => c.Name == "Juliano");

        results.Should().HaveCount(2);
    }

    [Fact]
    public void Exist_Should_Return_True_When_Match()
    {
        using var context = TestContextFactory.CreateContext();
        var repo = new GenericRepository<Client>(context);
        repo.Add(NewClient());
        context.SaveChanges();

        repo.Exist(c => c.Name == "Juliano").Should().BeTrue();
        repo.Exist(c => c.Name == "Missing").Should().BeFalse();
    }

    [Fact]
    public void Update_Should_Mark_Entity_As_Modified()
    {
        using var context = TestContextFactory.CreateContext();
        var repo = new GenericRepository<Client>(context);
        var client = repo.Add(NewClient("Before"));
        context.SaveChanges();

        client.Name = "After";
        var updated = repo.Update(client);
        context.SaveChanges();

        updated.Should().NotBeNull();
        repo.Find(c => c.Name == "After").Should().NotBeNull();
    }

    [Fact]
    public void Delete_Should_Remove_Entity()
    {
        using var context = TestContextFactory.CreateContext();
        var repo = new GenericRepository<Client>(context);
        var client = repo.Add(NewClient());
        context.SaveChanges();

        repo.Delete(client);

        repo.GetAll().Should().BeEmpty();
    }

    [Fact]
    public async Task CountAsync_Should_Return_Entity_Count()
    {
        using var context = TestContextFactory.CreateContext();
        var repo = new GenericRepository<Client>(context);
        repo.Add(NewClient("A"));
        repo.Add(NewClient("B"));
        context.SaveChanges();

        (await repo.CountAsync()).Should().Be(2);
    }

    [Fact]
    public void Filter_Should_Apply_Pagination()
    {
        using var context = TestContextFactory.CreateContext();
        var repo = new GenericRepository<Client>(context);
        repo.Add(NewClient("A"));
        repo.Add(NewClient("B"));
        repo.Add(NewClient("C"));
        context.SaveChanges();

        var firstPage = repo.Filter(page: 1, pageSize: 2).ToList();

        firstPage.Should().HaveCount(2);
    }
}