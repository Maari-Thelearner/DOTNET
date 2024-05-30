// Data/TicketContext.cs
using Microsoft.EntityFrameworkCore;
using TicketNotificationAPI.Models;

namespace TicketNotificationAPI.Data
{
    public class TicketContext : DbContext
    {
        public TicketContext(DbContextOptions<TicketContext> options) : base(options) { }
        public DbSet<Ticket> Tickets { get; set; }
    }
}
