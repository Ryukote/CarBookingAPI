using CarBookingAPI.Core.Contracts.Handlers;
using CarBookingAPI.Core.Contracts.Mappers;
using CarBookingAPI.Infrastructure.Context;
using CarBookingAPI.Infrastructure.Models;
using CarBookingAPI.Infrastructure.Utilities.Security;
using CarBookingAPI.Infrastructure.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace CarBookingAPI.Infrastructure.Handlers
{
    public class AccountHandler : IAccountHandler<CredentialsViewModel, AuthenticateViewModel>
    {
        private readonly CarBookingContext _context;
        private string _hashSecret;
        private string _jwtSecret;
        private IMapper<UsersViewModel, Users> _mapper;

        public AccountHandler(CarBookingContext context, string hashSecret, string jwtSecret, IMapper<UsersViewModel, Users> mapper)
        {
            _context = context;
            _hashSecret = hashSecret;
            _jwtSecret = jwtSecret;
            _mapper = mapper;
        }

        public async Task<AuthenticateViewModel> Authentication(CredentialsViewModel login)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Username == login.Username.ToLowerInvariant());

            if(user == null)
            {
                return null;
            }

            var salt = user.Salt;

            var hashedPassword = new PasswordHasher(_hashSecret)
                .HashPassword(login.Password, salt);

            if(user.HashedPassword != hashedPassword)
            {
                throw new ArgumentException();
            }

            var roles = new string[] { "RegisteredUser" };

            var jwt = new JWT(_jwtSecret);

            var accessToken = jwt.CreateAccessToken(_mapper.ToViewModel(user), roles);
            var refreshToken = jwt.CreateRefreshToken(_mapper.ToViewModel(user), roles);

            return new AuthenticateViewModel()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        public AuthenticateViewModel Refresh(string refreshToken)
        {
            var jwt = new JWT(_jwtSecret);

            var newAccessToken = jwt.RecreateAccessToken(refreshToken);
            var newRefreshToken = jwt.RecreateRefreshToken(refreshToken);

            return new AuthenticateViewModel()
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            };
        }
    }
}
