using Microsoft.EntityFrameworkCore;
using StudyProject.Infra.Context;

namespace StudyProject.Infra.Data.Tests;

public static class TestContextFactory
{
    public static StudyProjectContext CreateContext()
        => new(new DbContextOptionsBuilder<StudyProjectContext>()
            .UseInMemoryDatabase($"db-{Guid.NewGuid():N}")
            .Options);
}