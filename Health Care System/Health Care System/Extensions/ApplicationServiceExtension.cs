
using Health_Care_System.Data;
using Health_Care_System.Data.Interceptors;
using Health_Care_System.Filters;
using Health_Care_System.Helper;
using Health_Care_System.Middlewares;
using Health_Care_System.Repositories;
using Health_Care_System.Services;
using Microsoft.EntityFrameworkCore;

namespace Health_Care_System.Extensions
{
    public static class ApplicationServiceExtension
    {
        public static IServiceCollection AddServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(options =>
            {
                var xmlFile = $"{typeof(ApplicationServiceExtension).Assembly.GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });

            services.AddDbContext<AppDbContext>(option =>
            {
                option.UseSqlServer(
                    configuration.GetConnectionString("DefaultSqlConnection"));

                option.AddInterceptors(
                    new SoftDeleteInterceptor(),
                    new CreatedAtInterceptor(),
                    new ModifiedAtInterceptor()
                );
            });

            services.AddScoped(
                typeof(IGenericRepository<>),
                typeof(GenericRepository<>)
            );

            services.AddScoped<IPatientService, PatientService>();
            services.AddScoped<IDoctorService, DoctorService>();
            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();

            services.AddScoped<ActionExecutionLoggingFilter>();
            services.AddScoped<ActionTimingFilter>();
            services.AddScoped<ExceptionHandlingFilter>();

            services.AddTransient<RequestIdHeaderMiddleware>();
            services.AddTransient<RequestTimingMiddleware>();

            #region CORS

            var corsOrigins = configuration
                .GetSection("Cors:AllowedOrigins")
                .Get<string[]>() ?? Array.Empty<string>();

            const string corsPolicyName = "ConfigurationOrigins";

            services.AddCors(options =>
                options.AddPolicy(corsPolicyName, policy =>
                {
                    if (corsOrigins.Length > 0)
                    {
                        policy.WithOrigins(corsOrigins);
                    }

                    policy.AllowAnyHeader().AllowAnyMethod();
                }));

            #endregion

            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<PatientProfile>();
                cfg.AddProfile<DoctorProfile>();
                cfg.AddProfile<AppointmentProfile>();
            });

            return services;
        }
    }
}
