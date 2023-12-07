using AutoMapper;
using BLL.Dtos.AccountDtos;
using BLL.Dtos.AssignmentDtos;
using BLL.Dtos.RoleDto;
using BLL.Dtos.SprintDtos;
using BLL.Dtos.StatusDto;
using Domain.Entities;
using DTO.AccountDtos;
using Microsoft.AspNetCore.Http;
using System.Data;

namespace BLL.Mapper
{
    public class AdminMapper : Profile
    {
        private readonly IHttpContextAccessor _httpAccessor;

        public AdminMapper(IHttpContextAccessor httpAccessor)
        {
            _httpAccessor = httpAccessor;


            CreateMap<AppUserPostDto, AppUser>();
            CreateMap<AppUserPutDto, AppUser>();
            CreateMap<AppUser, AppUserGetDto>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.AppUserRole))
            .ForMember(dest => dest.Assignments, opt => opt.MapFrom(src => src.AssignmentUsers.Select(pt => new AssignmentInAppUserGetDto { Id = pt.Assignment.Id, Title = pt.Assignment.Title }).ToList()));
            CreateMap<AppUser, AppUserListItemDto>().ForMember(dest => dest.Assignments, opt => opt.MapFrom(src => src.AssignmentUsers.Select(pt => new AssignmentInAppUserGetDto { Id = pt.Assignment.Id, Title = pt.Assignment.Title }).ToList()));

            CreateMap<AppUserRole, RoleInAppUserGetDto>();
            CreateMap<Status, StatusInAssignmentGetDto>();
            CreateMap<Sprint, SprintInAssignmentGetDto>();
            CreateMap<AppUser, AppUserInAssignmentGetDto>();
            CreateMap<Assignment, AssignmentInAppUserGetDto>();
            CreateMap<Assignment, AppUserAssignmentListDto>();

            CreateMap<Assignment, AssignmentGetDto>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.Sprint, opt => opt.MapFrom(src => src.Sprint))
            .ForMember(dest => dest.AppUsers, opt => opt.MapFrom(src => src.AssignmentUsers.Select(pt => new AppUserInAssignmentGetDto { Id = pt.AppUser.Id, FullName = pt.AppUser.FullName }).ToList()));
            CreateMap<AssignmentPostDto, Assignment>();
            CreateMap<AssignmentPutDto, Assignment>();
            CreateMap<Assignment, AssignmentListItemDto>()
            .ForMember(dest => dest.AppUsers, opt => opt.MapFrom(src => src.AssignmentUsers.Select(pt => new AppUserInAssignmentGetDto { Id = pt.AppUser.Id, FullName = pt.AppUser.FullName }).ToList()));
            CreateMap<Assignment, RelatedAssignmentGetDto>()
           .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
           .ForMember(dest => dest.AppUsers, opt => opt.MapFrom(src => src.AssignmentUsers.Select(pt => new AppUserInAssignmentGetDto { Id = pt.AppUser.Id, FullName = pt.AppUser.FullName }).ToList()));

            CreateMap<UpdateAssignmentStatusDto, Assignment>();

            CreateMap<Sprint, SprintGetDto>();
            CreateMap<SprintPostDto, Sprint>();
            CreateMap<SprintPutDto, Sprint>();
            CreateMap<Sprint, SprintListItemDto>();
        }

    }
}
