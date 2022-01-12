using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SmartfaceSolution.MatchScop;

namespace SmartfaceSolution.Services
{
    public class BackBroundS : BackgroundService
    {
        private readonly ILogger<BackBroundS> _logger;
        private readonly IMatchService _matchService;
        public BackBroundS(IMatchService matchService,ILogger<BackBroundS> logger)
        {
            _logger = logger;
            _matchService = matchService;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
              _logger.LogInformation("BackBround service running", DateTime.Now);
                await Task.Delay(TimeSpan.FromMilliseconds(3000), stoppingToken);
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