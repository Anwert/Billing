using System.ComponentModel.DataAnnotations;

namespace Billing.Models.ViewModel
{
	public class FavourModel
	{
		public int Id { get; set; }
		
		[Required(ErrorMessage = "Не указано имя")]
		public string Name { get; set; }
		
		[Required(ErrorMessage = "Не указана цена")]
		public decimal? Cost { get; set; }
		
		public string GlobalError { get; set;  }
	}
}