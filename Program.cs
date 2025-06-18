using ContactManager.Services;
using ContactManager.Data;

namespace ContactManager
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            SQLitePCL.Batteries.Init(); // required for SQLite

            // Add MVC services (with views)
            builder.Services.AddControllersWithViews();

            // Dependency injection
            builder.Services.AddScoped<IContactRepository, ContactRepository>();
            builder.Services.AddScoped<IContactService, ContactService>();

            var app = builder.Build();

            // Seed testing data if DB is empty
            using (var scope = app.Services.CreateScope())
            {
                var seeder = new TestingDataSeeder(
                    scope.ServiceProvider.GetRequiredService<IConfiguration>());

                await seeder.EnsureDatabaseSeededAsync();
            }

            app.UseStaticFiles(); // Enables serving wwwroot
            app.UseHttpsRedirection();           

            app.UseRouting();

            app.UseAuthorization(); // optional, in case you add auth later

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Contact}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
