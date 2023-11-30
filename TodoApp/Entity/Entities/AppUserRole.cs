namespace Entity.Entities
{
    public class AppUserRole : BaseEntity
    {
        public string RoleName { get; set; }
        public List<AppUser> AppUsers { get; set; }
    }
}
