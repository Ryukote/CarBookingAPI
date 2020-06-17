using CarBookingAPI.Core.Contracts.ViewModels;
using CarBookingAPI.Infrastructure.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CarBookingAPI.Contracts
{
    public interface IPaginatedController<T>
        where T : IViewModel
    {
        Task<IActionResult> GetPaginated([FromBody] QueryViewModel<T> data);
    }
}
