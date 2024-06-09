using System.ComponentModel.DataAnnotations;

namespace CourseManagement.ViewModels.Accounts
{
    public class LoginVM
    {
        [Required]
        public string EmailOrUserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

}
