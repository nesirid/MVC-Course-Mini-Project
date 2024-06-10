namespace CourseManagement.Models
{
    public class InstructorSocialMedia
    {
        public int InstructorId { get; set; }
        public Instructor Instructor { get; set; }
        public int SocialMediaId { get; set; }
        public SocialMedia SocialMedia { get; set; }
        public string Url { get; set; }
    }
}
