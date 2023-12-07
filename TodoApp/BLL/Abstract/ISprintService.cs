using BLL.Dtos.SprintDtos;
using Microsoft.AspNetCore.Mvc;

namespace BLL.Abstract
{
    public interface ISprintService
    {
        Task<IActionResult> Create(SprintPostDto postDto);
        Task<IActionResult> Edit(SprintPutDto putDto);
        Task<IActionResult> GetAll(int page);
        Task<IActionResult> Get(int id);
        Task<IActionResult> Delete(int id);
    }
}
