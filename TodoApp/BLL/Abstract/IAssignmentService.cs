using DTO.AssignmentDtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        Task<IActionResult> UpdateExpiredAssignmentsStatus();
        Task<IActionResult> Delete(int id);
    }
}
