
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Peasant.Console;
using Peasant.Core.Helpers;

class Program 
{
    static void Main(string[] args) 
    {
        if (args.Length > 0) 
        {
            Program.Process(args);
            return;
        }

        IHost host = Host.CreateDefaultBuilder(args)
            .ConfigureServices(services =>
            {
                services.AddHostedService<Worker>();
            })
            .Build();

        host.Run();
    }

    protected static void Process(string[] strings) 
    {
        if( strings.Length > 0 && strings[0] == "hello" ) {
            Console.WriteLine("Hello, how can I help you!");
        }
        
        if( strings.Length > 0 && strings[0] == "speedtest" ) {
            SpeedTest.Start();
        }
    }
}