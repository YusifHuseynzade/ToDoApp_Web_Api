using DTO.AssignmentDtos;

namespace DTO.AccountDtos
{
    public class AppUserListItemDto
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public List<AssignmentInAppUserGetDto>? Assignments { get; set; }
    }
}
