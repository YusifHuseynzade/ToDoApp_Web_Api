namespace Entity.Entities
{
    public class Assignment : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int SprintId { get; set; }
        public Sprint Sprint { get; set; }
        public int StatusId { get; set; }
        public Status Status { get; set; }
        public List<AssignmentUser> AssignmentUsers { get; set; }
        public List<Review>? Reviews { get; set; }
        public DateTime StartedDate { get; set; } = DateTime.UtcNow.AddHours(4);
        public DateTime ExpirationDate { get; set; }

    }
}