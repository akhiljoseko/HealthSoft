using Microsoft.AspNetCore.Identity;

namespace HealthSoft.Core.Entities
{
    public class AppUser : IdentityUser
    {
        public virtual Doctor? Doctor { get; set; }
        public virtual Patient? Patient { get; set; }
    }
}
