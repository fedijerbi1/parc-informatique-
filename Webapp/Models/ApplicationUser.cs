using Microsoft.AspNetCore.Identity;

namespace Webapp.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
    }

    public class AdminUser : ApplicationUser
    {
        public string AdminLevel { get; set; } = string.Empty;
    }

    public class EmployerUser : ApplicationUser
    {
        public string CompanyName { get; set; } = string.Empty;
    }
}
