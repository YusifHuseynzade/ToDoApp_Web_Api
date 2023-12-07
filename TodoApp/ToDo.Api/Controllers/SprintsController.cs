using BLL.Abstract;
using BLL.Dtos.SprintDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ToDo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SprintsController : ControllerBase
    {
        private readonly ISprintService _sprintService;
        private readonly IConfiguration _configuration;

        public SprintsController(ISprintService sprintService)
        {
            _sprintService = sprintService;
        }

        [Authorize(Roles = "Project Manager")]
        [HttpPost("")]
        public async Task<IActionResult> Create(SprintPostDto postDto)
        {
            return await _sprintService.Create(postDto);

        }

        [Authorize(Roles = "Project Manager")]
        [HttpPut]
        public async Task<IActionResult> Edit(SprintPutDto putDto)
        {
            return await _sprintService.Edit(putDto);

        }

        [Authorize(Roles = "Project Manager")]
        [HttpGet("all")]
        public async Task<IActionResult> GetAll(int page = 1)
        {
            return await _sprintService.GetAll(page);
        }

        [Authorize(Roles = "Project Manager")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return await _sprintService.Get(id);

        }

        [Authorize(Roles = "Project Manager")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await _sprintService.Delete(id);
        }
    }
}
