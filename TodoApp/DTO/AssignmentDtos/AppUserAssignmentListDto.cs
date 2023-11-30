using DTO.SprintDtos;
using DTO.StatusDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.AssignmentDtos
{
    public class AppUserAssignmentListDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public SprintInAssignmentGetDto Sprint { get; set; }
        public StatusInAssignmentGetDto Status { get; set; }
        public DateTime StartedDate { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
