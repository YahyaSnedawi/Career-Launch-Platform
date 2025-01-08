using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;

namespace CareerLaunch.Models.ViewModel
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public ICollection<JobPost> JobPosts { get; set; }

        public ICollection<Application> Applications { get; set; }
    }
}
