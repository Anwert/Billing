using System.ComponentModel.DataAnnotations;

namespace Billing.Models.ViewModels
{
	public class RegisterModel
	{
		[Required(ErrorMessage ="Не указан Email")]
		[EmailAddress(ErrorMessage = "Неверный формат Email")]
		public string Email { get; set; }
         
		[Required(ErrorMessage = "Не указан пароль")]
		public string Password { get; set; }
		
		[Required(ErrorMessage = "Не указано имя")]
		public string Name { get; set; }
		
		[Required(ErrorMessage = "Не указан телефонный номер")]
		[Phone(ErrorMessage = "Неверный формат телефонного номера")]
		public string Contacts { get; set; }
         
		[Compare("Password", ErrorMessage = "Пароль введен неверно")]
		public string ConfirmPassword { get; set; }
		
		public string GlobalError { get; set;  }
	}
}