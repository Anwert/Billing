using System.ComponentModel.DataAnnotations;

namespace Billing.Models.ViewModels
{
	public class RegisterModel
	{
		[Required(ErrorMessage = "Не указано имя")]
		public string Name { get; set; }
         
		[Required(ErrorMessage = "Не указан пароль")]
		public string Password { get; set; }
		
		[Required(ErrorMessage = "Не указана контактная информация")]
		public string Contacts { get; set; }
         
		[Compare("Password", ErrorMessage = "Пароль введен неверно")]
		public string ConfirmPassword { get; set; }
		
		public string GlobalError { get; set;  }
	}
}