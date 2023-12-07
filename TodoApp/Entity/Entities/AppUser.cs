namespace Domain.Entities
{
    public class AppUser : BaseEntity
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public AppUserRole AppUserRole { get; set; }
        public List<Review>? Reviews { get; set; }
        public List<AssignmentUser>? AssignmentUsers { get; set; }
        public int RoleId { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime TokenCreated { get; set; }
        public DateTime TokenExpires { get; set; }

    }
}
