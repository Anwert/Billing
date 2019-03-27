using System.Threading.Tasks;
using Billing.Models.DataModel;

namespace Billing.Models.Service
{
	public interface IFavourService
	{
		Task<Favour> GetFavourById(int id);
	}
}