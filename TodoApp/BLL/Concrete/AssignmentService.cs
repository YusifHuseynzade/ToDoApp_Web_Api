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


        public AssignmentService(IMapper mapper, IAssignmentRepository assignmentRepository, ISprintRepository sprintRepository, IStatusRepository statusRepository)
        {
            _mapper = mapper;
            _assignmentRepository = assignmentRepository;
            _sprintRepository = sprintRepository;
            _statusRepository = statusRepository;
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

                // Başarılı bir işlem durumunda StatusCode 200 dönebilirsiniz.
                return new OkObjectResult(new { message = "Expired assignments updated successfully." });
            }
            catch (Exception ex)
            {
                // Hata durumunda StatusCode 500 dönebilirsiniz.
                Console.WriteLine($"Error during assignment update: {ex.Message}");
                return new StatusCodeResult(500);
            }
        }

        private int GetFailedStatusId()
        {
            // Örneğin, "Failed" adındaki bir durumun ID'sini almak için bir repository kullanabilirsiniz.
            var failedStatus = _statusRepository.FirstOrDefaultAsync(s => s.Name == "Failed").Result;

            // Eğer "Failed" adında bir durum bulunamazsa, hata durumu için varsayılan bir değer döndürebilirsiniz.
            if (failedStatus == null)
            {
                // Varsayılan olarak 1 değerini kullanıyoruz, ancak sizin durumunuza bağlı olarak değiştirilebilir.
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
    }
}
