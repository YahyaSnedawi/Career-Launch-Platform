using CareerLaunch.Models.ViewModel;
using Microsoft.AspNetCore.Builder;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareerLaunch.Models
{
    public class JobPost
    {

        [Key]
        public int JobPostId { get; set; }
        public string? EmployerId { get; set; }
        [ForeignKey(nameof(EmployerId))]
        public ApplicationUser? Employer { get; set; }

        [Required(ErrorMessage = "The job title is required.")]
        [StringLength(100, ErrorMessage = "The title must be less than 100 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "The job description is required.")]
        [StringLength(1000, ErrorMessage = "The description must be less than 1000 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "The company name is required.")]
        [StringLength(100, ErrorMessage = "The company name must be less than 100 characters.")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "The location is required.")]
        [StringLength(200, ErrorMessage = "The location must be less than 200 characters.")]
        public string Location { get; set; }

        [Required(ErrorMessage = "The job type is required.")]
        public string JobType { get; set; }

        [Required(ErrorMessage = "The email is required.")]
        [EmailAddress(ErrorMessage = "The email is not a valid email address.")]
        public string Email { get; set; }
        public string? UploadedFilePath { get; set; }
       
       
        public JobPostStatus Status { get; set; }
        public DateTime? PostDate { get; set; } = DateTime.UtcNow;


        [NotMapped]
        public IFormFile File { get; set; }


    }
    public enum JobPostStatus
    {
        Pending,
        Accepted,
        Rejected
    }

}