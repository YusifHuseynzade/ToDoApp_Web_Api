using DTO.SprintDtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
