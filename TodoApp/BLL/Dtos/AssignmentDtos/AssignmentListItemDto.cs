using BLL.Dtos.AccountDtos;

namespace BLL.Dtos.AssignmentDtos
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
