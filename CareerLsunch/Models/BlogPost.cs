using System.ComponentModel.DataAnnotations.Schema;

namespace CareerLaunch.Models
{
    public class BlogPost
    {
        public int BlogPostId { get; set; }
        public string AuthorEmail {  get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime? PublishedDate { get; set; } = DateTime.Now;
        public string Author { get; set; } 
        public string? AuthorImageUrl { get; set; }
        public string? AuthorBio { get; set; }
        public List<string> Categories { get; set; } = new List<string>();
        public List<string> Tags { get; set; } = new List<string>();
        public BlogPostStatus Status { get; set; }
        public string SectionTitle { get; set; }
        public string SectionContent { get; set; }

        [NotMapped]
        public IFormFile File { get; set; }
    }
    public enum BlogPostStatus
    {
        Pending,
        Accepted,
        Rejected
    }
}
