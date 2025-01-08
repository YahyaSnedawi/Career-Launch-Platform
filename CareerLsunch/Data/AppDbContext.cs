using CareerLaunch.Models;
using CareerLaunch.Models.ViewModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CareerLaunch.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Application>()
                .HasOne(a => a.Job)
                .WithMany()
                .HasForeignKey(a => a.JobId)
                .OnDelete(DeleteBehavior.Restrict);  

            modelBuilder.Entity<Application>()
                .HasOne(a => a.JobSeeker)
                .WithMany()
                .HasForeignKey(a => a.JobSeekerId)
                .OnDelete(DeleteBehavior.Restrict);  

            modelBuilder.Entity<Application>()
                .HasOne(a => a.Employer)
                .WithMany()
                .HasForeignKey(a => a.EmployerId)
                .OnDelete(DeleteBehavior.Restrict);  
        }


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<JobPost> JobPosts { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<BlogPost> Blogs { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }


    }
}
  


