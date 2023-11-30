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
    public class RelatedAssignmentGetDto
    {
        public int SprintId {  get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public StatusInAssignmentGetDto Status { get; set; }
        public List<AppUserInAssignmentGetDto>? AppUsers { get; set; }
        public DateTime StartedDate { get; set; }
        public DateTime ExpirationDate { get; set; }

    }
}
