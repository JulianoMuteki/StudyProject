using Microsoft.EntityFrameworkCore;
using StudyProject.Infra.Context;

namespace StudyProject.WebApi.Extensions
{
    public static class EFMigration
    {
        public static void MigrateOfContext<T>(this IApplicationBuilder app, StudyProjectContext context) where T : StudyProjectContext
        {           
            var logger = app.ApplicationServices.GetService<ILogger<IApplicationBuilder>>();
            logger.LogWarning("Connectionstring: {0}", context.Database.GetConnectionString());

            if (!context.Database.CanConnect())
            {
                bool isOk = context.Database.EnsureCreated();
                if (!isOk)
                {

                    logger.LogWarning("EnsureCreated failed");
                }
            }
            else if (context.Database.GetPendingMigrations().Count() > 0)
                context.Database.Migrate();
        }
    }
}
