using System.ComponentModel.DataAnnotations;

namespace MyDemo.PL.ViewModels
{
    public class ResetPasswordViewModel
    {

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Password Confirmation is required")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Password Confirmation does not match Password!")]
        public string ConfirmPassword { get; set; }
    }
}
