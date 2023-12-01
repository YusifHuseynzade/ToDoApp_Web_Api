using AutoMapper;
using BLL.Abstract;
using DAL.Abstract;
using DAL.Concrete;
using DTO.AssignmentDtos;
using DTO.PaginationDto;
using DTO.SprintDtos;
using Entity.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Concrete
{
    public class AssignmentService : IAssignmentService
    {
        private readonly IMapper _mapper;
        private readonly IAssignmentRepository _assignmentRepository;
        private readonly ISprintRepository _sprintRepository;
        private readonly IStatusRepository _statusRepository;
        private readonly IAssignmentUserRepository _assignmentUserRepository;
        private readonly IReviewRepository _reviewRepository;


        public AssignmentService(IMapper mapper, IAssignmentRepository assignmentRepository, ISprintRepository sprintRepository, IStatusRepository statusRepository, IAssignmentUserRepository assignmentUserRepository, IReviewRepository reviewRepository)
        {
            _mapper = mapper;
            _assignmentRepository = assignmentRepository;
            _sprintRepository = sprintRepository;
            _statusRepository = statusRepository;
            _assignmentUserRepository = assignmentUserRepository;
            _reviewRepository = reviewRepository;
        }

        public async Task<IActionResult> Create(AssignmentPostDto postDto)
        {
            try
            {
                if (await _assignmentRepository.IsExistAsync(x => x.Title == postDto.Title))
                    return new BadRequestObjectResult(new { error = new { field = "Title", message = "Assignment with the same title already exists!" } });

                var existingSprint = await _sprintRepository
                    .GetAsync(s => s.Id == postDto.SprintId);

                if (existingSprint == null)
                {
                    return new BadRequestObjectResult(new { error = new { field = "SprintId", message = "Invalid SprintId specified!" } });
                }

                if (existingSprint.ExpirationDate < DateTime.UtcNow.AddHours(4))
                {
                    return new BadRequestObjectResult(new { error = new { field = "SprintId", message = "Sprint has expired! Cannot create assignment in an expired Sprint." } });
                }

                if (postDto.ExpirationDate > existingSprint.ExpirationDate)
                {
                    return new BadRequestObjectResult(new { error = new { field = "ExpirationDate", message = "Assignment's ExpirationDate cannot be later than or equal to Sprint's ExpirationDate." } });
                }

                Assignment assignment = _mapper.Map<Assignment>(postDto);

                if (assignment.ExpirationDate < assignment.StartedDate)
                {
                    return new BadRequestObjectResult(new { error = new { field = "ExpirationDate", message = "Assignment's ExpirationDate cannot be earlier than StartedDate." } });
                }

                if (postDto.AppUserIds != null && postDto.AppUserIds.Any())
                {
                    assignment.AssignmentUsers = postDto.AppUserIds.Select(appUserId => new AssignmentUser
                    {
                        AppUserId = appUserId
                    }).ToList();
                }

                await _assignmentRepository.AddAsync(assignment);
                await _assignmentRepository.CommitAsync();

                return new StatusCodeResult(201);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during assignment creation: {ex.Message}");
                return new StatusCodeResult(500); 
            }
        }

        public async Task<IActionResult> Update(AssignmentPutDto putDto)
        {
           
            var existingAssignment = _assignmentRepository.Get(x => x.Id == putDto.Id, "AssignmentUsers.AppUser");
            if (existingAssignment == null)
                return new NotFoundObjectResult(new { error = new { message = $"Assignment with id {putDto.Id} not found!" } });

           
            if (await _assignmentRepository.IsExistAsync(x => x.Title == putDto.Title && x.Id != putDto.Id))
                return new BadRequestObjectResult(new { error = new { field = "Title", message = "Assignment with the same title already exists!" } });
            var existingSprint = await _sprintRepository
            .GetAsync(s => s.Id == putDto.SprintId);

            if (existingSprint == null)
            {
                return new BadRequestObjectResult(new { error = new { field = "SprintId", message = "Invalid SprintId specified!" } });
            }

            if (putDto.ExpirationDate > existingSprint.ExpirationDate)
            {
                return new BadRequestObjectResult(new { error = new { field = "ExpirationDate", message = "Assignment's ExpirationDate cannot be later than or equal to Sprint's ExpirationDate." } });
            }

            _mapper.Map(putDto, existingAssignment);

            if (existingAssignment.ExpirationDate < existingAssignment.StartedDate)
            {
                return new BadRequestObjectResult(new { error = new { field = "ExpirationDate", message = "Assignment's ExpirationDate cannot be earlier than StartedDate." } });
            }

            existingAssignment.AssignmentUsers.Clear();
            if (putDto.AppUserIds != null && putDto.AppUserIds.Any())
            {
                existingAssignment.AssignmentUsers = putDto.AppUserIds.Select(appUserId => new AssignmentUser
                {
                    AppUserId = appUserId,
                }).ToList();
            }

            await _assignmentRepository.UpdateAsync(existingAssignment);
            await _assignmentRepository.CommitAsync();

            
            return new OkResult();
        }

        public async Task<IActionResult> UpdateAssignmentStatus(UpdateAssignmentStatusDto updateDto)
        {
            var assignment = await _assignmentRepository
                .GetAsync(a => a.Id == updateDto.AssignmentId, "AssignmentUsers");

            var assignmentUser = assignment?.AssignmentUsers
                .FirstOrDefault(au => au.AppUserId == updateDto.AppUserId);

            if (assignment == null || assignmentUser == null)
            {
                return new NotFoundResult();
            }

            if (assignment.ExpirationDate < DateTime.UtcNow.AddHours(4))
            {
                return new BadRequestObjectResult(new { error = new { message = "Assignment has expired. Status cannot be changed!" } });
            }

            assignment.StatusId = updateDto.NewStatusId;

            await _assignmentRepository.UpdateAsync(assignment);
            await _assignmentRepository.CommitAsync();

            return new OkResult();
        }

        public async Task<IActionResult> UpdateExpiredAssignmentsStatus()
        {
            try
            {
                var expiredAssignments = await _assignmentRepository.GetExpiredAssignmentsAsync();

                foreach (var assignment in expiredAssignments)
                {
                    assignment.StatusId = GetFailedStatusId();
                }
                await _assignmentRepository.CommitAsync();
               
                return new OkObjectResult(new { message = "Expired assignments updated successfully." });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during assignment update: {ex.Message}");
                return new StatusCodeResult(500);
            }
        }

        private int GetFailedStatusId()
        {
            var failedStatus = _statusRepository.FirstOrDefaultAsync(s => s.Name == "Failed").Result;
          
            if (failedStatus == null)
            {
                return 1;
            }

            return failedStatus.Id;
        }

        public async Task<IActionResult> GetAll(int page)
        {
            var query = _assignmentRepository.GetAll(x => true).Include(p => p.Sprint).Include(p => p.Status).Include(p => p.AssignmentUsers).ThenInclude(p => p.AppUser);
            var assignmentDtos = _mapper.Map<List<AssignmentListItemDto>>(query.Skip((page - 1) * 4).Take(4));

            PaginationListDto<AssignmentListItemDto> model =
            new PaginationListDto<AssignmentListItemDto>(assignmentDtos, page, 4, query.Count());
            return new OkObjectResult(model);
        }

        public async Task<IActionResult> GetAssignmentBySprintId(int sprintId, int page)
        {
            var query = _assignmentRepository.GetAll(x => x.SprintId == sprintId)
                                              .Include(p => p.Sprint)
                                              .Include(p => p.Status)
                                              .Include(p => p.AssignmentUsers)
                                              .ThenInclude(p => p.AppUser);
           
            var assignmentDtos = _mapper.Map<List<RelatedAssignmentGetDto>>(query.Skip((page - 1) * 4).Take(4));
            
            PaginationListDto<RelatedAssignmentGetDto> model =
                new PaginationListDto<RelatedAssignmentGetDto>(assignmentDtos, page, 4, query.Count());

            return new OkObjectResult(model);
        }

        public async Task<IActionResult> GetAssignmentByAppUserId(int appUserId, int page)
        {
            var query = _assignmentRepository.GetAll(x => x.AssignmentUsers.Any(au => au.AppUserId == appUserId))
                                      .Include(p => p.Sprint)
                                      .Include(p => p.Status)
                                      .Include(p => p.AssignmentUsers)
                                      .ThenInclude(p => p.AppUser);
            var assignmentDtos = _mapper.Map<List<AppUserAssignmentListDto>>(query.Skip((page - 1) * 4).Take(4));

            PaginationListDto<AppUserAssignmentListDto> model =
            new PaginationListDto<AppUserAssignmentListDto>(assignmentDtos, page, 4, query.Count());
            return new OkObjectResult(model);
        }

        public async Task<IActionResult> Get(int id)
        {
            Assignment assignment = await _assignmentRepository.GetAsync(x => x.Id == id, "Sprint", "Status", "AssignmentUsers.AppUser");
            if (assignment == null)
                return new NotFoundResult();

            AssignmentGetDto assignmentDto = _mapper.Map<AssignmentGetDto>(assignment);
            return new OkObjectResult(assignmentDto);
        }

        
        public async Task<IActionResult> Delete(int id)
        {
            Assignment assignment = await _assignmentRepository.GetAsync(x => x.Id == id);
            if (assignment == null)
                return new NotFoundResult();

            _assignmentRepository.Remove(assignment);
            _assignmentRepository.Commit();
            return new NoContentResult();
        }

        public async Task<IActionResult> AllowReviewForAssignmentAsync(AssignmentReviewDto assignmentReviewDTO)
        {
            try
            {
                var codeReviewStatus = await _statusRepository.GetAsync(s => s.Id == assignmentReviewDTO.StatusId);
                if (codeReviewStatus.Name.ToLower() != "code review")
                {
                    return new BadRequestObjectResult("Invalid status for allowing reviews.");
                }

                var assignment = await _assignmentRepository.GetAsync(a => a.Id == assignmentReviewDTO.AssignmentId, "AssignmentUsers");

                if (assignment.StatusId != assignmentReviewDTO.StatusId)
                {
                    return new BadRequestObjectResult("Assignment is not in the specified code review status.");
                }

                var validAppUserIds = assignment.AssignmentUsers.Select(au => au.AppUserId).ToList();
                if (!validAppUserIds.Contains(assignmentReviewDTO.AppUserId))
                {
                    return new BadRequestObjectResult($"AppUser with Id {assignmentReviewDTO.AppUserId} is not assigned to this assignment.");
                }

                var review = new Review
                {
                    AssignmentId = assignmentReviewDTO.AssignmentId,
                    AppUserId = assignmentReviewDTO.AppUserId,
                    Text = assignmentReviewDTO.ReviewText
                };
                await _reviewRepository.AddAsync(review);

                await _reviewRepository.CommitAsync();

                return new OkObjectResult("Review added successfully.");
            }
            catch (Exception ex)
            {
                // Log the exception
                return new StatusCodeResult(500);
            }
        }
    }
}
