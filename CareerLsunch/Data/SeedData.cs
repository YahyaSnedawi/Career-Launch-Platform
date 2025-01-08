using CareerLaunch.Models.ViewModel;
using Microsoft.AspNetCore.Identity;

namespace CareerLaunch.Data
{
    public class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            
            var roleNames = new[] { "Employer", "JobSeeker" };

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            var user = await userManager.FindByEmailAsync("admin@domain.com");
            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = "admin@domain.com",
                    Email = "admin@domain.com",
                    FirstName = "Admin",
                    LastName = "User"
                };

                await userManager.CreateAsync(user, "Password@123");

                await userManager.AddToRoleAsync(user, "Employer");
            }

            var jobSeekerUser = await userManager.FindByEmailAsync("seeker@domain.com");
            if (jobSeekerUser == null)
            {
                jobSeekerUser = new ApplicationUser
                {
                    UserName = "seeker@domain.com",
                    Email = "seeker@domain.com",
                    FirstName = "Job",
                    LastName = "Seeker"
                };

                await userManager.CreateAsync(jobSeekerUser, "Password@123");

                await userManager.AddToRoleAsync(jobSeekerUser, "JobSeeker");
            }
        }
    }
}
