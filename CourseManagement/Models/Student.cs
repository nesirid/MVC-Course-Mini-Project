namespace CourseManagement.Models
{
    public class Student : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
