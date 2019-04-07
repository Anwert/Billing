using System.Collections.Generic;
using System.Threading.Tasks;
using Billing.Models.DataModel;

namespace Billing.Models.Service
{
	public interface IContractService
	{
		Task<IEnumerable<Contract>> GetContracts();
		
		Task Create(Contract contract);
		
		Task UpdateStatusForContract(int new_status_id, int contract_id);
		
		Task<IEnumerable<Contract>> GetContractsForClient(int client_id);
	}
}