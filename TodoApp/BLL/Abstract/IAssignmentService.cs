using BLL.Dtos.AssignmentDtos;
using Microsoft.AspNetCore.Mvc;

namespace BLL.Abstract
{
    public interface IAssignmentService
    {
        Task<IActionResult> Create(AssignmentPostDto postDto);
        Task<IActionResult> Update(AssignmentPutDto putDto);
        Task<IActionResult> GetAll(int page);
        Task<IActionResult> Get(int id);
        Task<IActionResult> GetAssignmentBySprintId(int sprintId, int page);
        Task<IActionResult> GetAssignmentByAppUserId(int appUserId, int page);
        Task<IActionResult> UpdateAssignmentStatus(UpdateAssignmentStatusDto updateDto);
        Task<IActionResult> AllowReviewForAssignmentAsync(AssignmentReviewDto assignmentReviewDTO);
        Task<IActionResult> UpdateExpiredAssignmentsStatus();
        Task<IActionResult> Delete(int id);
    }
}
