using CarBookingAPI.Core.Contracts.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CarBookingAPI.Contracts
{
    public interface IAccountController<TLogin>
        where TLogin : IViewModel
    {
        /// <summary>
        /// Authenticate user based on valid login data.
        /// </summary>
        /// <param name="loginData">Valid login data.</param>
        /// <returns>Returns access and refresh token if user is authenticated.</returns>
        Task<IActionResult> Authenticate([FromBody] TLogin loginData);

        /// <summary>
        /// Refresh access and refresh token based on valid refresh token.
        /// </summary>
        /// <param name="refreshToken">Valid refresh token.</param>
        /// <returns>Returns new access and refresh token.</returns>
        IActionResult Refresh([FromBody] string refreshToken);
    }
}
