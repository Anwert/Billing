using System.ComponentModel.DataAnnotations;

namespace Billing.Models.ViewModels
{
	public class LoginModel
	{
		[Required(ErrorMessage = "Не указано имя")]
		public string Name { get; set; }
		 
		[Required(ErrorMessage = "Не указан пароль")]
		public string Password { get; set; }
		
		public string GlobalError { get; set;  }
	}
}