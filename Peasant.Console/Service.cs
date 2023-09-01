using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Peasant.Console
{
    public class Service
    {
        public IHost iHost { get; set; }
        public bool isRunning { get; set; }

        public Service(string[] args) 
        {
            this.Build(args);
        }

        private void Build(string[] args)
        {
            this.iHost = Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services.AddHostedService<Worker>();
                })
                .Build();
        }

        public async void Start()
        {
            this.Build(new string[0]); //Build first.

            if (this.iHost != null && !isRunning)
            {
                await this.iHost.StartAsync();
            }
            isRunning = true;
        }

        public async void Stop()
        {
            if (this.iHost != null && isRunning)
            {
                await this.iHost.StopAsync(new System.Threading.CancellationToken());
            }
            isRunning = false;
        }
    }
}
