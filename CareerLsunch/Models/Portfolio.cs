using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareerLaunch.Models
{
    public class Portfolio
    {
        public int PortfolioId { get; set; }
        [Required(ErrorMessage = "Full Name is required.")]
        [StringLength(100, ErrorMessage = "Full Name cannot exceed 100 characters.")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Job Title is required.")]
        [StringLength(50, ErrorMessage = "Job Title cannot exceed 50 characters.")]
        public string JobTitle { get; set; }
        [Required(ErrorMessage = "Biography is required.")]
        [StringLength(1000, ErrorMessage = "Biography cannot exceed 1000 characters.")]
        public string Biography { get; set; }
        [Required(ErrorMessage = "Skills is requierd")]
        public List<string> Skills { get; set; } = new List<string>();
      
        public string? ProfilePicture { get; set; }
       
        public string? ResumeUrl { get; set; }
        [Required(ErrorMessage = "Contact Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string ContactEmail { get; set; }
        [Required(ErrorMessage = "Contact Phone is required.")]
        [Phone(ErrorMessage = "Invalid phone number format.")]
        public string ContactPhone { get; set; }
        public string WorkType { get; set; } // Freelancer أو Worker
        public PortfolioStatus Status { get; set; }

        [NotMapped]
        public IFormFile ProfilePictureFile { get; set; }
        [NotMapped]
        public IFormFile ResumeFile { get; set; }
        
    
    }
    public enum PortfolioStatus
    {
        Pending,
        Accepted,
        Rejected
    }

}

