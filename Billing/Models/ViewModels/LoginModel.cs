using System.ComponentModel.DataAnnotations;

namespace Billing.Models.ViewModels
{
	public class LoginModel
	{
		[Required(ErrorMessage = "Не указан Email")]
		public string Email { get; set; }
		 
		[Required(ErrorMessage = "Не указан пароль")]
		public string Password { get; set; }
	}
}