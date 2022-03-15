using Microsoft.EntityFrameworkCore;

namespace PlatformService.Data
{
    public static class PrepDb
    {
        public static void PrepPopuldation(IApplicationBuilder app, bool isProd)
        {
            using(var serviceScoped = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScoped.ServiceProvider.GetService<AppDbContext>(), isProd);
            }
        }

        private static void SeedData(AppDbContext context, bool isProd)
        {
            if (isProd)
            {
                Console.WriteLine("--> Attemping to apply migrations...");
                try
                {
                    context.Database.Migrate();
                }
                catch (Exception ex)
                {

                    Console.WriteLine($"--> Could not run migrations: {ex.Message}");
                }
            }

            if (!context.Platforms.Any())
            {
                Console.WriteLine("--> Seeding Data...");

                context.Platforms.AddRange(
                    new Models.Platform() { Name = "Dotnet", Publisher = "Microsoft", Cost = "Free" },
                    new Models.Platform() { Name = "Sql Server Express", Publisher="Microsoft", Cost = "Free"},
                    new Models.Platform() { Name = "Kubernetes", Publisher = "Cloud Native Computing Foundation", Cost = "Free"}
                    );
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("--> We already have data");
            }
        }
    }
}
