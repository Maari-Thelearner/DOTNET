using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TicketProcessor;

Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddDbContext<TicketContext>(options =>
            options.UseSqlite(hostContext.Configuration.GetConnectionString("DefaultConnection")));
        services.AddHostedService<TicketProcessorService>();
        services.AddTransient<EmailSender>();
    })
    .Build()
    .Run();
