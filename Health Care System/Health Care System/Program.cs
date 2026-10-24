
using Health_Care_System.Data;
using Health_Care_System.Data.Seeding;
using Health_Care_System.Helper;
using Microsoft.EntityFrameworkCore;
using System;

namespace Health_Care_System
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSqlConnection"))
            );

            builder.Services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<PatientProfile>();
                cfg.AddProfile<DoctorProfile>();
                cfg.AddProfile<AppointmentProfile>();
            });

            var app = builder.Build();

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

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
