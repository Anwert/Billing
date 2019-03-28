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
from contract cn
", (contract, manager, client, favour, status) =>
				{
					contract.Manager	= manager;
					contract.Client		= client;
					contract.Favour		= favour;
					contract.Status		= status;
					return contract;
				},
				splitOn: $"{nameof(User.Id)}, {nameof(User.Id)}, {nameof(Favour.Id)}, {nameof(Status.Id)}");
			}
		}
	}
}