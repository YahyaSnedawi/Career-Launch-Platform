using CareerLaunch.Models;
using CareerLaunch.Models.ViewModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CareerLaunch.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<JobPost> JobPosts { get; set; }
     
        public DbSet<BlogPost> Blogs { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Application> Applications { get; set; }


    }
}
  


