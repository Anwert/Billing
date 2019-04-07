using System.Collections.Generic;
using System.Threading.Tasks;
using Billing.Models.DataModel;

namespace Billing.Models.Repository
{
	public interface IFavourRepository
	{
		Task<Favour> GetFavourById(int id);
		
		Task<IEnumerable<Favour>> GetFavours();
		
		Task<Favour> GetFavourByName(string name);
		
		Task Create(Favour favour);
		
		Task Update(Favour favour);
		
		Task Delete(int id);
	}
}