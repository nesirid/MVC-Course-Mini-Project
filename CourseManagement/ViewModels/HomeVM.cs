using CourseManagement.Models;

namespace CourseManagement.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Course> Courses { get; set; }
        public IEnumerable<InstructorVM> Instructors { get; set; }
        public IEnumerable<Slider> Sliders { get; set; }

    }
}
