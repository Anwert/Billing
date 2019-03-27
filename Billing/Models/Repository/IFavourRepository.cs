using System.Threading.Tasks;
using Billing.Models.DataModel;

namespace Billing.Models.Repository
{
	public interface IFavourRepository
	{
		Task<Favour> GetFavourById(int id);
	}
}