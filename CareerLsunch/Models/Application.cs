using CareerLaunch.Models.ViewModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareerLaunch.Models
{
    public class Application
    {

        [Key]
        public int ApplicationId { get; set; }

        [Required]
        public int JobId { get; set; }
        public JobPost Job { get; set; }

        
        public string JobSeekerId { get; set; }
        public ApplicationUser JobSeeker { get; set; }

        public string CoverLetter { get; set; }
        public string ResumePath { get; set; }
        public string? EmployerId { get; set; }
        public ApplicationUser Employer { get; set; }

        public ApplicationStatus Status { get; set; }
       

        [Required]
        public DateTime ApplicationDate { get; set; } = DateTime.Now;
        [NotMapped]
        public IFormFile ResumeFile { get; set; }
    }
    public enum ApplicationStatus
    {
        Pending,
        Accepted,
        Rejected
    }
}