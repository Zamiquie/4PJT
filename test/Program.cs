using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RestSharp;
using SupMagasin.Dal;

namespace test
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureServices(services =>
                {
                    services.AddHostedService<ServiceTest>();
                    services.AddHostedService<ServiceTestDeux>();

                });

        public class ServiceTest : IHostedService, IDisposable
        {
            private readonly ILogger<ServiceTest> _logger;
            private Timer _timer;

            public ServiceTest(ILogger<ServiceTest> logger)
            {
                _logger = logger;
            }
 
            public Task StartAsync(CancellationToken cancellationToken)
            {
                _logger.LogInformation("Service Start : {0}", DateTime.Now);

                _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
                return Task.CompletedTask;
            }

            private void DoWork(object state)
            {
                DalProduit dal = new DalProduit();

            }

            public Task StopAsync(CancellationToken cancellationToken)
            {
                Console.WriteLine("Le service est eteint:{0}", DateTime.Now);
                _timer?.Change(Timeout.Infinite, 0);
                return Task.CompletedTask;
            }

            public void Dispose()
            {
                _timer.Dispose();
                Console.WriteLine("Le service working : {0}", DateTime.Now);
            }
        } 

    }
}
