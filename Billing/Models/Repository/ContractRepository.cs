using System.Collections.Generic;
using System.Threading.Tasks;
using Billing.Models.DataModel;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace Billing.Models.Repository
{
	public class ContractRepository : BaseRepository, IContractRepository
	{
		public ContractRepository(IConfiguration config) : base(config) {}

		public async Task<IEnumerable<Contract>> GetContracts()
		{
			using (var conn = Connection)
			{
				conn.Open();
				return await conn.QueryAsync<Contract, User, User, Favour, Status, Contract>($@"
select	contract	{nameof(Contract.Id)},
		manager		{nameof(Contract.Manager.Id)},
		client		{nameof(Contract.Client.Id)},
		favour		{nameof(Contract.Favour.Id)},
		status		{nameof(Contract.Status.Id)}
from	contract cn
", (contract, manager, client, favour, status) =>
				{
					contract.Manager	= manager;
					contract.Client		= client;
					contract.Favour		= favour;
					contract.Status		= status;
					return contract;
				},
				splitOn:	$"{nameof(Contract.Manager.Id)}," +
							$"{nameof(Contract.Client.Id)}," +
							$"{nameof(Contract.Favour.Id)}," +
							$"{nameof(Contract.Status.Id)}");
			}
		}

		public async Task Create(Contract contract)
		{
			using (var conn = Connection)
			{
				conn.Open();
				await conn.ExecuteAsync(@"
insert	contract (manager, client, favour, [status])
values	(@manager_id, @client_id, @favour_id, @status_id)
", new
				{
					manager_id	= contract.Manager.Id,
					client_id	= contract.Client.Id,
					favour_id	= contract.Favour.Id,
					status_id	= contract.Status.Id 
				});
			}
		}
		
		public async Task UpdateStatusForContract(int new_status_id, int contract_id)
		{
			using (var conn = Connection)
			{
				conn.Open();
				await conn.ExecuteAsync($@"
update	contract
set		[status]	= @{nameof(new_status_id)}
where 	contract	= @{nameof(contract_id)}
", new { new_status_id, contract_id });
			}
		}
		
		public async Task<IEnumerable<Contract>> GetContractsForClient(int client_id)
		{
			using (var conn = Connection)
			{
				conn.Open();
				return await conn.QueryAsync<Contract, User, User, Favour, Status, Contract>($@"
select	contract	{nameof(Contract.Id)},
		manager		{nameof(Contract.Manager.Id)},
		client		{nameof(Contract.Client.Id)},
		favour		{nameof(Contract.Favour.Id)},
		status		{nameof(Contract.Status.Id)}
from	contract cn
where	client = @{nameof(client_id)}
", param: new { client_id }, map: (contract, manager, client, favour, status) =>
					{
						contract.Manager	= manager;
						contract.Client		= client;
						contract.Favour		= favour;
						contract.Status		= status;
						return contract;
					},
					splitOn:	$"{nameof(Contract.Manager.Id)}," +
								$"{nameof(Contract.Client.Id)}," +
								$"{nameof(Contract.Favour.Id)}," +
								$"{nameof(Contract.Status.Id)}");
			}
		}

	}
}