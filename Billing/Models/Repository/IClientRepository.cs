using System.Collections.Generic;
using System.Threading.Tasks;
using Billing.Models.DataModel;

namespace Billing.Models.Repository
{
	public interface IClientRepository
	{
		Task<int> Create(Client client);

		Task Delete(int id);

		Task<IEnumerable<Client>> Get();

		Task<Client> GetClient(int id);

		Task Update(Client client);
	}
}