using System.Collections.Generic;
using System.Threading.Tasks;
using Billing.Models.DataModel;
using Billing.Models.Repository;

namespace Billing.Models.Service
{
	public class FavourService : IFavourService
	{
		private readonly IFavourRepository _favourRepository;
		
		public FavourService(IFavourRepository favour_repository)
		{
			_favourRepository = favour_repository;
		}
		
		public async Task<Favour> GetFavourById(int id) =>  await _favourRepository.GetFavourById(id);

		public async Task<IEnumerable<Favour>> GetFavours() => await _favourRepository.GetFavours();
	}
}