namespace Domain.Entities
{
    public class Review : BaseEntity
    {
        public int AssignmentId { get; set; }
        public Assignment Assignment { get; set; }
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow.AddHours(4);
    }
}