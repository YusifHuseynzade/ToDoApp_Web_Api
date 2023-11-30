using DTO.AssignmentDtos;
using DTO.RoleDto;

namespace DTO.AccountDtos
{
    public class AppUserGetDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public RoleInAppUserGetDto Role { get; set; }
        public List<AssignmentInAppUserGetDto>? Assignments { get; set; }
    }
}
