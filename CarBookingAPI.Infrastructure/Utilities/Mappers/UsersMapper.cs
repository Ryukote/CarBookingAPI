using CarBookingAPI.Core.Contracts.Mappers;
using CarBookingAPI.Infrastructure.Models;
using CarBookingAPI.Infrastructure.ViewModels;
using System.Collections.Generic;

namespace CarBookingAPI.Infrastructure.Utilities.Mappers
{
    public class UsersMapper : IMapper<UsersViewModel, Users>
    {
        public Users ToModel(UsersViewModel viewModel)
        {
            return new Users()
            {
                HashedPassword = "",
                Id = viewModel.Id,
                Username = viewModel.Username,
                Salt = new byte[16]
            };
        }

        public UsersViewModel ToViewModel(Users model)
        {
            return new UsersViewModel()
            {
                Id = model.Id,
                Username = model.Username.ToLowerInvariant()
            };
        }

        public ICollection<UsersViewModel> ToViewModelCollection(ICollection<Users> modelCollection)
        {
            var usersCollection = new List<UsersViewModel>();

            foreach(var item in modelCollection)
            {
                usersCollection.Add(new UsersViewModel()
                {
                    Id = item.Id,
                    Username = item.Username
                });
            }

            return usersCollection;
        }
    }
}
