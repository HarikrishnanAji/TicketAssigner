using Microsoft.EntityFrameworkCore;

namespace Ticket_Assigner_API.Model
{
    public class TicketAssignerDbContext : DbContext
    {
        public TicketAssignerDbContext(DbContextOptions<TicketAssignerDbContext> options) : base(options) { }

        public DbSet<Ticket> TicketSet { get;set; }
        public DbSet<Assignee> Assignees { get; set; }

    }
}
