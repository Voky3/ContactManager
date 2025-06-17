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

            // Add services to the container.
            builder.Services.AddControllers();

            builder.Services.AddScoped<IContactRepository, ContactRepository>();
            builder.Services.AddScoped<IContactService, ContactService>();

            // Enable Swagger UI
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Swagger for local testing
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // seed testing data if DB is empty
            using (var scope = app.Services.CreateScope())
            {
                var seeder = new TestingDataSeeder(
                    scope.ServiceProvider.GetRequiredService<IConfiguration>());

                await seeder.EnsureDatabaseSeededAsync(); 
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.MapControllers();

            app.Run();
        }
    }
}
