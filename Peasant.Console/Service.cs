using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Peasant.Console
{
    public class Service
    {
        public IHost iHost { get; set; }
        public bool isRunning { get; set; }

        public Service(string[] args) 
        {
            //Initialize something...
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

        public void Start(bool isBackground = true)
        {
            this.Build(new string[0]); //Build first.

            if (this.iHost != null && !isRunning)
            {
                if(isBackground)
                {
                    this.iHost.Run();
                }

                else
                {
                    this.iHost.RunAsync();
                }
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
