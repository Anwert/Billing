using System.Collections.Generic;
using System.Threading.Tasks;
using Billing.Models.DataModel;

namespace Billing.Models.Repository
{
	public interface IContractRepository
	{
		Task<IEnumerable<Contract>> GetContracts();
	}
}