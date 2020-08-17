using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EasyNetQ;
using EasyNetQ_ASPNETCore_Sample.Worker.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EasyNetQ_ASPNETCore_Sample.Worker
{
    public class Worker : BackgroundService
    {
        private readonly IBus _bus;
        private readonly ILogger _logger;

        public Worker(IBus bus, ILogger<Worker> logger)
        {
            _logger = logger;
            _bus = bus;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _bus.SubscribeAsync<SampleMessage>("sampleQueue", (message) =>
            {
                _logger.LogInformation($"Message received {message.Id}");

                return Task.CompletedTask;
            });


            await Task.Delay(Timeout.Infinite, stoppingToken);
        }
    }
}
