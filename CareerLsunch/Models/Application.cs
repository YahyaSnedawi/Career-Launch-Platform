using CareerLaunch.Models.ViewModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CareerLaunch.Models
{
    public class Application
    {
        [Key]
        public int ApplicationId { get; set; }

        [Required]
        public int JobPostId { get; set; }
        [ForeignKey(nameof(JobPostId))]
        public JobPost? Job { get; set; }

        [Required]
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        [DataType(DataType.MultilineText)]
        public string CoverLetter { get; set; }

        public string? ResumePath { get; set; }

     
        [Required]
        public DateTime ApplicationDate { get; set; } = DateTime.Now;

        [NotMapped]
        public IFormFile ResumeFile { get; set; }
    }
}

