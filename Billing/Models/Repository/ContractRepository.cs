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
select	cn.contract	{nameof(Contract.Id)},
		mn.manager	{nameof(Contract.Manager.Id)},
		cl.client	{nameof(Contract.Client.Id)},
		fv.favour	{nameof(Contract.Favour.Id)},
		st.status	{nameof(Contract.Status.Id)}
from	contract	cn
	join manager	mn on m.manager	= cn.manager
	join client		cl on cl.client	= cn.client
	join favour		fv on d.favour	= cn.favour
	join status		st on s.status	= cn.status
", (contract, manager, client, favour, status) =>
				{
					contract.Manager.Id	= manager.Id;
					contract.Client.Id	= client.Id;
					contract.Favour.Id	= favour.Id;
					contract.Status.Id	= status.Id;
					return contract;
				},
				splitOn: $"{nameof(User.Id)}, {nameof(User.Id)}, {nameof(Favour.Id)}, {nameof(Status.Id)}");
			}
		}
	}
}