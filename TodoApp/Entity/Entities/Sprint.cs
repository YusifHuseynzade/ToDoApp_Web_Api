namespace Entity.Entities
{
    public class Sprint : BaseEntity
    {
        public string Title { get; set; }
        public DateTime StartedDate { get; set; } = DateTime.UtcNow.AddHours(4);
        public DateTime ExpirationDate { get; set; }
        public List<Assignment> Assignments { get; set; }
    }
}