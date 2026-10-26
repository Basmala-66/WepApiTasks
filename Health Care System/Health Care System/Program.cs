
using Health_Care_System.Data;
using Health_Care_System.Data.Seeding;
using Health_Care_System.Extensions;
using Health_Care_System.Filters;
using Health_Care_System.Middlewares;
using Microsoft.EntityFrameworkCore;

namespace Health_Care_System
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            builder.Services.AddServices(builder.Configuration);



            builder.Services.AddControllers(options =>
            {
                options.Filters.AddService<ExceptionHandlingFilter>();
                options.Filters.AddService<ActionExecutionLoggingFilter>();
                options.Filters.AddService<ActionTimingFilter>();
            });

            var app = builder.Build();

            app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseMiddleware<RequestTimingMiddleware>();
            app.UseMiddleware<RequestIdHeaderMiddleware>();
            app.UseMiddleware<RateLimitingMiddleware>();


            using (var scop = app.Services.CreateScope())
            {
                var services = scop.ServiceProvider;
                var loogerFactory = services.GetRequiredService<ILoggerFactory>();

                try
                {
                    var dbContext = services.GetRequiredService<AppDbContext>();
                    await dbContext.Database.MigrateAsync();
                    await DoctorContextSeeding.SeedAsync(dbContext);
                }
                catch (Exception ex)
                {
                    var logger = loogerFactory.CreateLogger<Program>();
                    logger.LogError(ex, ex.Message);
                }
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors("ConfigurationOrigins");

            app.UseAuthorization();

            app.MapControllers();


            app.Run();
        }
    }
}
