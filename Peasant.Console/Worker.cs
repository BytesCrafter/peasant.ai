
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Peasant.Console;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;

    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            await Task.Delay(1000, stoppingToken);
        }
    }

    public override void Dispose()
    {
        _logger.LogInformation("Peasant HostedService is Disposed");
    }

    public override Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Peasant HostedService is Starting");
        return base.StartAsync(cancellationToken);
    }

    public override Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Peasant HostedService is Stopping");
        return base.StopAsync(cancellationToken);
    }
}
