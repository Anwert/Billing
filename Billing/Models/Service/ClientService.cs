using Billing.Models.DataModel;
using Billing.Models.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Billing.Models.Service
{
	public class ClientService : IClientService
	{
		private readonly IClientRepository _clientRepository;

		public ClientService(IClientRepository client_repository)
		{
			_clientRepository = client_repository;
		}

		public async Task<int> Create(Client client)
		{
			return await _clientRepository.Create(client);
		}

		public async Task Delete(int id)
		{
			await _clientRepository.Delete(id);
		}

		public async Task<IEnumerable<Client>> Get()
		{
			return await _clientRepository.Get();
		}

		public async Task<Client> GetClient(int id)
		{
			return await _clientRepository.GetClient(id);
		}

		public async Task Update(Client client)
		{
			await _clientRepository.Update(client);
		}
	}
}
