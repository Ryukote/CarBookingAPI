using CarBookingAPI.Core.Contracts.Mappers;
using CarBookingAPI.Infrastructure.Models;
using CarBookingAPI.Infrastructure.ViewModels;
using System.Collections.Generic;

namespace CarBookingAPI.Infrastructure.Utilities.Mappers
{
    public class CredentialsMapper : IMapper<CredentialsViewModel, Users>
    {
        public Users ToModel(CredentialsViewModel viewModel)
        {
            return new Users()
            {
                HashedPassword = "",
                Id = default,
                Salt = default,
                Username = viewModel.Username
            };
        }

        public CredentialsViewModel ToViewModel(Users model)
        {
            throw new System.NotImplementedException();
        }

        public ICollection<CredentialsViewModel> ToViewModelCollection(ICollection<Users> modelCollection)
        {
            throw new System.NotImplementedException();
        }
    }
}
