using Health_Care_System.Entities;
using System.Text.Json;

namespace Health_Care_System.Data.Seeding
{
    public static class DoctorContextSeeding
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            if (!context.Doctors.Any())
            {
                var jsonData = File.ReadAllText("C:\\Users\\basma\\Desktop\\depi tasks\\c#\\task 16\\Health Care System\\Health Care System\\Doctors.json");

                var doctors = JsonSerializer.Deserialize<List<Doctor>>(jsonData);

                if (doctors is null)
                    return;

                await context.Doctors.AddRangeAsync(doctors);
                await context.SaveChangesAsync();
            }
        }
    }
}
