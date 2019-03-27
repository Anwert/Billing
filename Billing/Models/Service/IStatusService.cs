using System.Threading.Tasks;
using Billing.Models.DataModel;

namespace Billing.Models.Service
{
	public interface IStatusService
	{
		Task<Status> GetStatusById(int id);
	}
}