using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace SmartfaceSolution.Services
{
    public class BackBroundService : BackgroundService
    {
        private readonly ILogger<BackBroundService> _logger;
        private readonly IMatchService _matchService;
        public BackBroundService(IMatchService matchService,ILogger<BackBroundService> logger)
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