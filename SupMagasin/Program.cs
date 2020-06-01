using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SupMagasin.Dal;
using SupMagasin.Utils;

namespace SupMagasin
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
            Console.WriteLine("Bonjour");
        }

        public static IHostBuilder CreateWebHostBuilder(string[] args) {

                    return Host.CreateDefaultBuilder(args)
                    .ConfigureWebHostDefaults(webBuilder =>
                    {
                        webBuilder.UseStartup<Startup>();
                    })
                    .ConfigureServices(services =>
                    {
                        services.AddHostedService<AlertStockService>();
                    })
                    .ConfigureWebHost(option => 
                    {
                        option.UseUrls("http://*:15403");
                    }
                    );
        }
            

        //Alerte Stock
        public class AlertStockService : IHostedService, IDisposable
        {
            private readonly ILogger<AlertStockService> _logger;
            private Timer _timer;

            public AlertStockService(ILogger<AlertStockService> logger)
            {
                _logger = logger;
            }

            public Task StartAsync(CancellationToken cancellationToken)
            {
                _logger.LogInformation("Service StockAlert Start : {0}", DateTime.Now);

                _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromHours(8));
                return Task.CompletedTask;
            }

            private void DoWork(object state)
            {
                DalProduit dal = new DalProduit();
                var StockAle = dal.GetProduitToOrder().Result;
                Mailling mailling = new Mailling();
                mailling.SendAlertWithDocCSV(StockAle);
            }

            public Task StopAsync(CancellationToken cancellationToken)
            {
                Console.WriteLine("Le service StockAlert est eteint:{0}", DateTime.Now);
                _timer?.Change(Timeout.Infinite, 0);
                return Task.CompletedTask;
            }

            public void Dispose()
            {
                _timer.Dispose();
                Console.WriteLine("Le service StockAlert working : {0}", DateTime.Now);
            }
        }
    }
}
