namespace CareerLaunch.Models
{
    public class Company
    {
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public int JobsPosted { get; set; }
        public string LogoUrl { get; set; }
        public string SocialMediaLinks { get; set; }
    }
}
