namespace Billing.Models.DataModel
{
	public class Contract
	{
		public int Id { get; set; }
		
		public User Client { get; set; }
		
		public User Manager { get; set; }
		
		public Favour Favour { get; set; }
		
		public Status Status { get; set; }
	}
}
