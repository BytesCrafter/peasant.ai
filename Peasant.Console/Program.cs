
using Peasant.Console;
using Peasant.Core.Controllers;
using Peasant.Core.Helpers;

class Program 
{
    public static Service service = null;

    static void Main(string[] args) 
    {
        if (args.Length > 0) 
        {
            Program.Process(args);
            return;
        }

        Program.service = new Service(args);
        Program.service.Start();
    }

    protected static void Process(string[] strings) 
    {
        if( strings.Length > 0 && strings[0] == "hello" ) {
            Console.WriteLine("Hello, how can I help you!");
        }
        
        if( strings.Length > 0 && strings[0] == "speedtest" ) {
            SpeedTest.Start();
        }

        if (strings.Length > 0 && strings[0] == "netmap")
        {
            new NetworkMapper();
        }
    }
}