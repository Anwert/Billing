using System.Collections.Generic;
using System.Threading.Tasks;
using Billing.Models.DataModel;

namespace Billing.Models.Service
{
	public interface IContractService
	{
		Task<IEnumerable<Contract>> GetContracts();
	}
}