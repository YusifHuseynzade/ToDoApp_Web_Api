using DTO.AccountDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.AssignmentDtos
{
    public class AssignmentPostDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int SprintId { get; set; }
        public int StatusId { get; set; }
        public List<int>? AppUserIds { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
