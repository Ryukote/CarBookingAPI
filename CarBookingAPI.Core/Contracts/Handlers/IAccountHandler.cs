using CarBookingAPI.Core.Contracts.ViewModels;
using System.Threading.Tasks;

namespace CarBookingAPI.Core.Contracts.Handlers
{
    /// <summary>
    /// Represents authentication contract.
    /// </summary>
    public interface IAccountHandler<TLogin, TJWT>
        where TLogin : IViewModel
        where TJWT : IViewModel
    {
        /// <summary>
        /// Authenticate user based on user data.
        /// </summary>
        /// <param name="login">Valid TLogin object.</param>
        /// <returns>Object that contains access token JWT and refresh token JWT with expiration information.</returns>
        Task<TJWT> Authentication(TLogin login);

        /// <summary>
        /// Creates new TJWT object for authorization based on active refresh token.
        /// </summary>
        /// <param name="refreshToken">Valid and active refresh token.</param>
        /// <returns>Object that contains access token JWT and refresh token JWT with expiration information.</returns>
        TJWT Refresh(string refreshToken);
    }
}
