namespace Domain.Entities
{
    public class AssignmentUser : BaseEntity
    {
        public int AssignmentId { get; set; }
        public Assignment Assignment { get; set; }
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}