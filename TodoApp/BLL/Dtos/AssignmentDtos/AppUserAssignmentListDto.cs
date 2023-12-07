using BLL.Dtos.SprintDtos;
using BLL.Dtos.StatusDto;

namespace BLL.Dtos.AssignmentDtos
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
