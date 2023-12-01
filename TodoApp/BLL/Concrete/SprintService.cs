using AutoMapper;
using BLL.Abstract;
using DAL.Abstract;
using DTO.PaginationDto;
using DTO.SprintDtos;
using Entity.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Concrete
{
    public class SprintService : ISprintService
    {
        private readonly IMapper _mapper;
        private readonly ISprintRepository _sprintRepository;

        public SprintService(IMapper mapper, ISprintRepository sprintRepository)
        {
            _mapper = mapper;
            _sprintRepository = sprintRepository;
        }

        public async Task<IActionResult> Create(SprintPostDto postDto)
        {
            if (await _sprintRepository.IsExistAsync(x => x.Title == postDto.Title))
                return new BadRequestObjectResult(new { error = new { field = "Title", message = "Title already created!" } });

            Sprint sprint = _mapper.Map<Sprint>(postDto);

            if (sprint.ExpirationDate < sprint.StartedDate)
                return new BadRequestObjectResult(new { error = new { field = "ExpirationDate", message = "ExpirationDate cannot be earlier than StartedDate!" } });

            await _sprintRepository.AddAsync(sprint);
            await _sprintRepository.CommitAsync();

            return new StatusCodeResult(201);
        }

        public async Task<IActionResult> Edit(SprintPutDto putDto)
        {
            Sprint sprint = await _sprintRepository.GetAsync(x => x.Id == putDto.Id);

            if (sprint == null) return new NotFoundResult();
            if (await _sprintRepository.IsExistAsync(x => x.Id != putDto.Id && x.Title == putDto.Title))
                return new BadRequestObjectResult(new { error = "Bad Request" });

            _mapper.Map(putDto, sprint);

            if (sprint.ExpirationDate < sprint.StartedDate)
                return new BadRequestObjectResult(new { error = "ExpirationDate cannot be earlier than StartedDate" });

            await _sprintRepository.UpdateAsync(sprint);
            return new NoContentResult();
        }

        public async Task<IActionResult> GetAll(int page)
        {
            var query = _sprintRepository.GetAll(x => true);
            var sprintDtos = _mapper.Map<List<SprintListItemDto>>(query.Skip((page - 1) * 4).Take(4));

            PaginationListDto<SprintListItemDto> model =
            new PaginationListDto<SprintListItemDto>(sprintDtos, page, 4, query.Count());
            return new OkObjectResult(model);
        }

        public async Task<IActionResult> Get(int id)
        {
            Sprint sprint = await _sprintRepository.GetAsync(x => x.Id == id);
            if (sprint == null)
                return new NotFoundResult();

            SprintGetDto assignmentDto = _mapper.Map<SprintGetDto>(sprint);
            return new OkObjectResult(assignmentDto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            Sprint sprint = await _sprintRepository.GetAsync(x => x.Id == id);
            if (sprint == null)
                return new NotFoundResult();

            _sprintRepository.Remove(sprint);
            _sprintRepository.Commit();
            return new NoContentResult();
        }

    }
}
