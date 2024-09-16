using System.ComponentModel.DataAnnotations;

namespace MyDemo.PL.ViewModels
{
    public class ForgetPasswordViewModel
    {

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email!")]
        public string Email { get; set; }
    }
}
