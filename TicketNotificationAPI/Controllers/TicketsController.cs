// Controllers/TicketsController.cs
using Microsoft.AspNetCore.Mvc;
using TicketNotificationAPI.Data;
using TicketNotificationAPI.Models;

namespace TicketNotificationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly TicketContext _context;

        public TicketsController(TicketContext context)
        {
            _context = context;
        }

        [HttpPost("RaiseTicket")]
        public async Task<IActionResult> RaiseTicket([FromBody] Ticket ticket)
        {
            if (ticket == null || ticket.Priority != "High")
            {
                return BadRequest(new { success = false, message = "Invalid ticket data or non-high priority ticket." });
            }

            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();

            return Ok(new { success = true, message = $"Ticket raised successfully. Ticket Reference #{ticket.Id}" });
        }
    }
}
