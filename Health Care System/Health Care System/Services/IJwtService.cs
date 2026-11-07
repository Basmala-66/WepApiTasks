using Health_Care_System.Entities;

namespace Health_Care_System.Services
{
    public interface IJwtService
    {
        string GenerateToken(ApplicationUser user);
    }
}
