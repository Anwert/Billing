﻿using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Billing.Models.DataModel;
using Billing.Models.ViewModel;

namespace Billing.Models.Service
{
	public interface IUserService
	{
		Task<int> Create(User user);
		
		Task<int> CreateUserByRegisterModel(RegisterModel model, string role);

		Task Delete(int id);

		Task<IEnumerable<User>> GetUsers();

		Task<User> GetUserById(int id);

		Task Update(User user);

		Task<IEnumerable<User>>GetClients();
		
		Task<User> GetUserByLoginModel(LoginModel model);
		
		Task<User> GetUserByName(string name);

		ClaimsPrincipal GetClaimsPrincipal(string email, string role);
		
		Task<int> CreateClient(ClientModel model);
	}
}