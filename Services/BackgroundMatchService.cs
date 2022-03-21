using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace SmartfaceSolution.Services
{
    /// <summary>
    /// <c>BackgroundMatchService</c> is a background service that are running as long as is running the IHostedService
    /// </summary>
    public class BackgroundMatchService : BackgroundService
    {
        private readonly ILogger<BackgroundMatchService> _logger;
        private readonly IMatchService _matchService;
        public BackgroundMatchService(IMatchService matchService,ILogger<BackgroundMatchService> logger)
        {
            _logger = logger;
            _matchService = matchService;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
              _logger.LogInformation("BackBround service running", DateTime.Now);
                await Task.Delay(TimeSpan.FromMilliseconds(1000), stoppingToken);
                _matchService.matchFaces();
            }
           
        }

        public override Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("BackBround service stop", DateTime.Now);
            return base.StopAsync(stoppingToken);
        }
    }
}