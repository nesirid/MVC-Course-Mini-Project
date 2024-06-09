using Microsoft.AspNetCore.Identity;

namespace CourseManagement.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
