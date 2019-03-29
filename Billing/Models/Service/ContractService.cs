using System.Collections.Generic;
using System.Threading.Tasks;
using Billing.Models.DataModel;
using Billing.Models.Repository;

namespace Billing.Models.Service
{
	public class ContractService : IContractService
	{
		private readonly IContractRepository	_contractRepository;
		private readonly IUserService			_userService;
		private readonly IFavourService			_favourService;
		private readonly IStatusService			_statusService;

		public ContractService(IContractRepository contract_repository,
			IUserService user_service,
			IFavourService favour_service,
			IStatusService status_service)
		{
			_contractRepository	= contract_repository;
			_userService		= user_service;
			_favourService		= favour_service;
			_statusService		= status_service;
		}
		
		public async Task<IEnumerable<Contract>> GetContracts()
		{
			var contracts = await _contractRepository.GetContracts();
			
			foreach (var contract in contracts)
			{
				var manager_task	= _userService.GetUserById(contract.Manager.Id);
				var client_task		= _userService.GetUserById(contract.Client.Id);
				var favour_task		= _favourService.GetFavourById(contract.Favour.Id);
				var status_task		= _statusService.GetStatusById(contract.Status.Id);
				
				await Task.WhenAll(manager_task, client_task, favour_task, status_task);
				
				contract.Manager	= manager_task.Result;
				contract.Client		= client_task.Result;
				contract.Favour		= favour_task.Result;
				contract.Status		= status_task.Result;
			}
			
			return contracts;
		}

		public async Task Create(Contract contract)
		{
			await _contractRepository.Create(contract);
		}

		public async Task UpdateStatusForContract(int new_status_id, int contract_id)
		{
			await _contractRepository.UpdateStatusForContract(new_status_id, contract_id);
		}
	}
}