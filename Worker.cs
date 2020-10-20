using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;

namespace DotNetWorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }
     
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
        // 120000
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                // TODO.
                CheckConnection();
                await Task.Delay(3000, stoppingToken);
            }   
        }

        private void CheckConnection() {
            using (var conn = new SqlConnection("data source=127.0.0.1;uid=user;password=password;database=db")) {
                try {
                    conn.Open();
                    _logger.LogInformation("SQL Connected: {time}", DateTimeOffset.Now);
                } catch(Exception ex) {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}