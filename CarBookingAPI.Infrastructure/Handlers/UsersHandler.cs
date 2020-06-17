using CarBookingAPI.Core.Contracts.Handlers;
using CarBookingAPI.Core.Contracts.Mappers;
using CarBookingAPI.Core.Contracts.Utilities;
using CarBookingAPI.Core.Contracts.ViewModels;
using CarBookingAPI.Infrastructure.Context;
using CarBookingAPI.Infrastructure.Models;
using CarBookingAPI.Infrastructure.Utilities.Security;
using CarBookingAPI.Infrastructure.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarBookingAPI.Infrastructure.Handlers
{
    public class UsersHandler : IUsersHandler<UsersViewModel, CredentialsViewModel>, IPaginated<UsersViewModel>
    {
        private CarBookingContext _context;
        private IMapper<CredentialsViewModel, Users> _credentialsMapper;
        private IMapper<UsersViewModel, Users> _usersMapper;
        private IHasher _hasher;
        private string _hashSecret;

        public UsersHandler(CarBookingContext context, string hashSecret, IMapper<CredentialsViewModel, Users> credentialsMapper,
            IMapper<UsersViewModel, Users> usersMapper)
        {
            _context = context;
            _hashSecret = hashSecret;
            _credentialsMapper = credentialsMapper;
            _usersMapper = usersMapper;
            _hasher = new PasswordHasher(_hashSecret);
        }

        public async Task<UsersViewModel> Add(CredentialsViewModel credentials)
        {
            var search = await _context.Users
                .FirstOrDefaultAsync(x => x.Username == credentials.Username.ToLowerInvariant());

            if(search == null)
            {
                var salt = _hasher.CreateSalt();

                var user = _credentialsMapper.ToModel(credentials);
                user.Id = Guid.NewGuid();
                user.HashedPassword = _hasher.HashPassword(credentials.Password, salt);
                user.Salt = salt;

                _context.Users.Add(user);

                await _context.SaveChangesAsync();

                return _usersMapper.ToViewModel(user);
            }

            throw new ArgumentException();
        }

        public async Task<int?> Delete(Guid id)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Id == id);

            if(user != null)
            {
                _context.Remove(user);
                return await _context.SaveChangesAsync();
            }

            return null;
        }

        public async Task<UsersViewModel> GetById(Guid id)
        {
            var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (user != null)
            {
                return new UsersViewModel()
                {
                    Id = user.Id,
                    Username = user.Username
                };
            }

            return null;
        }

        public async Task<ICollection<UsersViewModel>> GetPaginated(IPaginationViewModel<UsersViewModel> paginationData)
        {
            var queryResult = await _context.Users
                .Where(x => x.Username.Contains(paginationData.QueryData.Username))
                .Skip(paginationData.Skip * paginationData.Take)
                .Take(paginationData.Take)
                .ToListAsync();

            if (queryResult.Count == 0)
            {
                return null;
            }

            List<UsersViewModel> result = new List<UsersViewModel>();

            foreach (var item in queryResult)
            {
                result.Add(new UsersViewModel()
                {
                    Id = item.Id,
                    Username = item.Username
                });
            }

            return result;
        }

        public async Task<UsersViewModel> Update(CredentialsViewModel credentials)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Username == credentials.Username.ToLowerInvariant());

            if(user != null)
            {
                user.Username = credentials.Username.ToLowerInvariant();

                if(!string.IsNullOrEmpty(credentials.Password))
                {
                    user.HashedPassword = _hasher.HashPassword(credentials.Password, user.Salt);
                }

                _context.Users.Update(user);

                if(await _context.SaveChangesAsync() > 0)
                {
                    return new UsersViewModel()
                    {
                        Id = user.Id,
                        Username = user.Username
                    };
                }

                return null;
            }

            return null;
        }
    }
}
