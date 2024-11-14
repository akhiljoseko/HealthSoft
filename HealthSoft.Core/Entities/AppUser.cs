using Microsoft.AspNetCore.Identity;

namespace HealthSoft.Core.Entities
{
    public class AppUser : IdentityUser
    {
        public required string FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
