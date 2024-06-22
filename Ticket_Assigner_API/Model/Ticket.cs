namespace Ticket_Assigner_API.Model
{
    public class Ticket
    {
        public int Id { get; set; }
        public string TicketId { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public string Category { get; set; }
        public int TotalEstimate { get; set; }
        public int LoggedEstimate { get; set; }  
        public int AssigneeId { get; set; }
        public string Assignee { get; set; }
    }
}
