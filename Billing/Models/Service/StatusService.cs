using System.Collections.Generic;
using System.Threading.Tasks;
using Billing.Models.DataModel;
using Billing.Models.Repository;

namespace Billing.Models.Service
{
	public class StatusService : IStatusService
	{
		private readonly IStatusRepository _statusRepository;
		
		public StatusService(IStatusRepository status_repository)
		{
			_statusRepository = status_repository;
		}
		
		public async Task<Status> GetStatusById(int id) => await _statusRepository.GetStatusById(id);

		public async Task<IEnumerable<Status>> GetStatuses() => await _statusRepository.GetStatuses();
	}
}