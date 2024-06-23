namespace Ticket_Assigner_API.Model
{
    public class Assignee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
    }
}
