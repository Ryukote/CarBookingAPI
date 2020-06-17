using CarBookingAPI.Core.Contracts.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CarBookingAPI.Contracts
{
    public interface IController<T>
        where T : IViewModel
    {
        Task<IActionResult> GetById(Guid id);

        Task<IActionResult> Post([FromBody] T data);

        Task<IActionResult> Put([FromBody] T data);

        Task<IActionResult> Delete(Guid id);
    }
}
