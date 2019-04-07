using System.Collections.Generic;
using System.Threading.Tasks;
using Billing.Models.DataModel;
using Billing.Models.ViewModel;

namespace Billing.Models.Service
{
	public interface IFavourService
	{
		Task<Favour> GetFavourById(int id);
		
		Task<IEnumerable<Favour>> GetFavours();
		
		Task<Favour> GetFavourByName(string name);
		
		Task CreateFavourWithFavourModel(FavourModel model);
		
		Task<FavourModel> GetFavourModelById(int id);
		
		Task UpdateFavourWithFavourModel(FavourModel model);
	}
}