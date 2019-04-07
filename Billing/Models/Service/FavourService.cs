using System.Collections.Generic;
using System.Threading.Tasks;
using Billing.Models.DataModel;
using Billing.Models.Repository;
using Billing.Models.ViewModel;

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
		
		public Task<Favour> GetFavourByName(string name) => _favourRepository.GetFavourByName(name);
		
		public async Task CreateFavourWithFavourModel(FavourModel model)
		{
			var favour  = GetFavourByModel(model);
			
			await _favourRepository.Create(favour);
		}

		public async Task<FavourModel> GetFavourModelById(int id)
		{
			var favour_model = await _favourRepository.GetFavourById(id);
			
			return GetModelByFavour(favour_model);
		}

		public async Task UpdateFavourWithFavourModel(FavourModel model)
		{
			var favour = GetFavourByModel(model);
			
			await _favourRepository.Update(favour);
		}

		public Task Delete(int id) => _favourRepository.Delete(id);

		private Favour GetFavourByModel(FavourModel model)
		{
			return new Favour
			{
				Id		= model.Id,
				Name	= model.Name
			};
		}
		
		private FavourModel GetModelByFavour(Favour favour)
		{
			return new FavourModel
			{
				Id		= favour.Id,
				Name	= favour.Name
			};
		}
	}
}