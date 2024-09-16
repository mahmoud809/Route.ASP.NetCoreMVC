using System.ComponentModel.DataAnnotations;

namespace MyDemo.PL.ViewModels
{
	public class RegisterViewModel
	{
        public string FName { get; set; }
        public string LName { get; set; }
      
        [Required(ErrorMessage ="Email is required")]
        [EmailAddress(ErrorMessage ="Invalid Email!")]
        public string Email { get; set; }

		[Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
		public string Password { get; set; }

		[Required(ErrorMessage = "Password Confirmation is required")]
		[DataType(DataType.Password)]
		[Compare("Password" , ErrorMessage = "Password Confirmation does not match Password!")]
		public string ConfirmPassword { get; set; }
        public bool IsAgree { get; set; }
    }
}
