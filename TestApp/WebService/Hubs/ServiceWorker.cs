using Common.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WebService.Hubs
{
    public class ServiceWorker : BackgroundService
    {
        private readonly IHubContext<ServiceHub, IMessage> _serviceHub;
        public ServiceWorker(IHubContext<ServiceHub, IMessage> serviceHub)
        {
            _serviceHub = serviceHub;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                Console.WriteLine("Ping!");
                await _serviceHub.Clients.All.Ping();
                await Task.Delay(3000);
            }
        }
    }
}
