namespace Identity.Domain.Entities
{
    public class Role:BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}