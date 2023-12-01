using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.AssignmentDtos
{
    public class AssignmentReviewDto
    {
        public int AssignmentId { get; set; }
        public int StatusId { get; set; }
        public int AppUserId { get; set; }
        public string ReviewText { get; set; }
    }
}
