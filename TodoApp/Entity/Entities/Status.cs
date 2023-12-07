namespace Domain.Entities
{
    public class Status : BaseEntity
    {
        public string Name { get; set; }
        public List<Assignment> Assignments { get; set; }
    }
}