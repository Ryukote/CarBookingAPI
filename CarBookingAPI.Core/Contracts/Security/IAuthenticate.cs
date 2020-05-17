using CarBookingAPI.Core.Contracts.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CarBookingAPI.Core.Contracts.Security
{
    /// <summary>
    /// Authenticate object.
    /// </summary>
    /// <typeparam name="TLogin">Valid TLogin class.</typeparam>
    public interface IAuthenticate<TLogin>
        where TLogin : IViewModel
    {
        /// <summary>
        /// Authenticate user.
        /// </summary>
        /// <param name="login">Valid TLogin data.</param>
        /// <returns>IActionResult.</returns>
        Task<IActionResult> Authenticate(TLogin login);
    }
}
