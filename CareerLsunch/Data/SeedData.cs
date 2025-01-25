using CareerLaunch.Models.ViewModel;
using Microsoft.AspNetCore.Identity;

namespace CareerLaunch.Data
{
    public class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            
            var roleNames = new[] { "Admin", "Employer", "JobSeeker" };

           
            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            
            var adminEmail = "yahya@careerlaunch.com"; 
            var adminPassword = "1234@Admin.Yahya";  
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FirstName = "Yahya",
                    LastName = "Admin",
                };

                await userManager.CreateAsync(adminUser, adminPassword);

               
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }

            var employerEmail = "employer@careerlaunch.com";  
            var employerPassword = "1234@Employer.Yahya"; 
            var employerUser = await userManager.FindByEmailAsync(employerEmail);
            if (employerUser == null)
            {
                employerUser = new ApplicationUser
                {
                    UserName = employerEmail,
                    Email = employerEmail,
                    FirstName = "Employer",
                    LastName = "User",
                };

                await userManager.CreateAsync(employerUser, employerPassword);

                await userManager.AddToRoleAsync(employerUser, "Employer");
            }

           
            var jobSeekerEmail = "seeker@careerlaunch.com"; 
            var jobSeekerPassword = "1234@JobSeeker.Yahya";  
            var jobSeekerUser = await userManager.FindByEmailAsync(jobSeekerEmail);
            if (jobSeekerUser == null)
            {
                jobSeekerUser = new ApplicationUser
                {
                    UserName = jobSeekerEmail,
                    Email = jobSeekerEmail,
                    FirstName = "Job",
                    LastName = "Seeker",
                };

                await userManager.CreateAsync(jobSeekerUser, jobSeekerPassword);

                await userManager.AddToRoleAsync(jobSeekerUser, "JobSeeker");
            }
        }
    
    }
}
