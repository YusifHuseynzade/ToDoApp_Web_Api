using DTO.AccountDtos;
using DTO.SprintDtos;
using DTO.StatusDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.AssignmentDtos
{
    public class AssignmentListItemDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int SprintId { get; set; }
        public int StatusId { get; set; }
        public List<AppUserInAssignmentGetDto>? AppUsers { get; set; }
        public DateTime StartedDate { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
