using Health_Care_System.Data;
using Health_Care_System.Data.Interceptors;
using Health_Care_System.Helper;
using Health_Care_System.Repositories;
using Health_Care_System.Services;
using Microsoft.EntityFrameworkCore;

namespace Health_Care_System.Extensions
{
    public static class ApplicationServiceExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddDbContext<AppDbContext>(option =>
            {
                option.UseSqlServer(configuration.GetConnectionString("DefaultSqlConnection"));


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
