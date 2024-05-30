using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TicketProcessor
{
    public class TicketProcessorService : BackgroundService
    {
        private readonly TicketContext _context;
        private readonly EmailSender _emailSender;
        private readonly ILogger<TicketProcessorService> _logger;

        public TicketProcessorService(TicketContext context, EmailSender emailSender, ILogger<TicketProcessorService> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _emailSender = emailSender ?? throw new ArgumentNullException(nameof(emailSender));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Checking for high priority tickets...");

                var highPriorityTickets = _context.Tickets.Where(t => t.Priority == "High" && !t.IsEmailSent).ToList();

                foreach (var ticket in highPriorityTickets)
                {
                    _emailSender.SendEmail(ticket);
                    ticket.IsEmailSent = true;
                    _context.Update(ticket);
                }

                await _context.SaveChangesAsync(stoppingToken);

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
    }
}
