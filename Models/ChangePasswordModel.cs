using System.ComponentModel.DataAnnotations;

namespace BlazorUserManagerApp.Models
{
    public class ChangePasswordModel
    {
        public bool ChangePaswword { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
        [MaxLength(16, ErrorMessage = "Password must be at most 16 characters")]
        [RegularExpression("^(?=.*[A-Za-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,16}", ErrorMessage = "Invalid Password")]
        public string PasswordHash { get; set; }
    }
}
