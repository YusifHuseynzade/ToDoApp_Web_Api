using BLL.Abstract;
using BLL.Concrete;
using DTO.AccountDtos;
using DTO.AssignmentDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ToDo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignmentsController : ControllerBase
    {
        private readonly IAssignmentService _assignmentService;

        public AssignmentsController(IAssignmentService assignmentService)
        {
            _assignmentService = assignmentService;
        }

        [Authorize(Roles = "Project Manager")]
        [HttpPost("")]
        public async Task<IActionResult> Create([FromForm] AssignmentPostDto postDto)
        {
            return await _assignmentService.Create(postDto);

        }

        [Authorize(Roles = "Project Manager")]
        [HttpPut]
        public async Task<IActionResult> UpdateAssignment([FromForm] AssignmentPutDto putDto)
        {
            return await _assignmentService.Update(putDto);

        }

        [Authorize(Roles = "Project Manager, Developer")]
        [HttpGet("all")]
        public async Task<IActionResult> GetAll(int page = 1)
        {
            return await _assignmentService.GetAll(page);
        }

        [Authorize(Roles = "Project Manager")]
        [HttpGet("RelatedAssignments")]
        public async Task<IActionResult> GetAssignmentBySprintId(int sprintId, int page = 1)
        {
            return await _assignmentService.GetAssignmentBySprintId(sprintId, page);
        }

        [Authorize(Roles = "Project Manager, Developer")]
        [HttpGet("AppUserAssignments")]
        public async Task<IActionResult> GetAssignmentByAppUserId(int appUserId, int page = 1)
        {
            return await _assignmentService.GetAssignmentByAppUserId(appUserId, page);
        }

        [Authorize(Roles = "Project Manager, Developer")]
        [HttpPut("UpdateAssignmentStatus")]
        public async Task<IActionResult> UpdateAssignmentStatus([FromQuery] UpdateAssignmentStatusDto putDto)
        {
            return await _assignmentService.UpdateAssignmentStatus(putDto);
        }

        [Authorize(Roles = "Project Manager, Developer")]
        [HttpPost("AssignmentReview")]
        public async Task<IActionResult> AllowReviewsForAssignmentAsync([FromQuery] AssignmentReviewDto postDto)
        {
            return await _assignmentService.AllowReviewForAssignmentAsync(postDto);
        }

        [Authorize(Roles = "Project Manager")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return await _assignmentService.Get(id);
        }

        [Authorize(Roles = "Project Manager")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAssignment(int id)
        {
            return await _assignmentService.Delete(id);
        }
    }
}
