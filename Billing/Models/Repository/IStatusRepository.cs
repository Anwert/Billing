using System.Collections.Generic;
using System.Threading.Tasks;
using Billing.Models.DataModel;

namespace Billing.Models.Repository
{
	public interface IStatusRepository
	{
		Task<Status> GetStatusById(int id);
		
		Task<IEnumerable<Status>> GetStatuses();
	}
}