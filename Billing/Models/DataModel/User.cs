namespace Billing.Models.DataModel
{
	public class User
	{
		public int Id { get; set; }
		
		public string Email { get; set; }
		
		public string Password { get; set; }
		
		public int Role { get; set; }
	}
}