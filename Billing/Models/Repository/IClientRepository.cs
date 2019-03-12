using System.Collections.Generic;
using System.Threading.Tasks;
using Billing.Models.DataModel;

namespace Billing.Models.Repository
{
	public interface IClientRepository
	{
		Task<int> Create(string name);

		Task Delete(int id);

		Task<IEnumerable<Client>> Get();

		Task<IEnumerable<Client>> GetClient(int id);

		Task Update(Client client);
	}
}