using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dtos.SprintDtos
{
    public class SprintInAssignmentGetDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime StartedDate { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
