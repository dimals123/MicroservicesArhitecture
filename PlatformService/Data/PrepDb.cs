namespace PlatformService.Data
{
    public static class PrepDb
    {
        public static void PrepPopuldation(IApplicationBuilder app)
        {
            using(var serviceScoped = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScoped.ServiceProvider.GetService<AppDbContext>());
            }
        }

        private static void SeedData(AppDbContext context)
        {
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
