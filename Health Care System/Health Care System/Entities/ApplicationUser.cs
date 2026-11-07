using Microsoft.AspNetCore.Identity;

namespace Health_Care_System.Entities
{
    public class ApplicationUser:IdentityUser
    {
       public string Name { get; set; } = string.Empty;
    }
}
