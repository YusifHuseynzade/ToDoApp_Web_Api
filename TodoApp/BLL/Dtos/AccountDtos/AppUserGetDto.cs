using BLL.Dtos.AssignmentDtos;
using BLL.Dtos.RoleDto;

namespace BLL.Dtos.AccountDtos
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
