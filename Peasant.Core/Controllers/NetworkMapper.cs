using Peasant.Core.Helpers;
using Peasant.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Peasant.Core.Controllers
{
    public class NetworkMapper
    {
        class SupportRequest
        {
            public int RequestId { get; }
            public string Name { get; }
            public string Email { get; }
            public string Description { get; }
            public string Status { get; set; }

            public SupportRequest(int requestId, string name, string email, string description)
            {
                RequestId = requestId;
                Name = name;
                Email = email;
                Description = description;
                Status = "Open";
            }
        }

        static List<SupportRequest> supportRequests = new List<SupportRequest>();

        public NetworkMapper()
        {
            while (true)
            {
                Console.WriteLine("Peasant: Network Mapper");
                Console.WriteLine("1. Check for online Host");
                Console.WriteLine("2. Get Host's ports open");
                Console.WriteLine("3. Exit Network Mapper");
                Console.Write("Enter your choice: ");
                string pick = Console.ReadLine() ?? "0";

                if (int.TryParse(pick, out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            ScanNetworkHost();
                            break;
                        case 2:
                            ScanOpenPorts();
                            break;
                        case 3:
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }

                else
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                }

                Console.WriteLine();
            }
        }

        static Task<Port> IsPortOpen(string ipAddress, int port)
        {
            
            var tcs = new TaskCompletionSource<Port>();
            try
            {
                using (TcpClient tcpClient = new TcpClient())
                {
                    tcpClient.Connect(ipAddress, port);
                    tcs.SetResult(new Port(ipAddress, port, true) );
                }
            }
            catch (SocketException)
            {
                tcs.SetResult(new Port(ipAddress, port, false) );
            }

            return tcs.Task;
        }

        static void ScanOpenPorts()
        {
            Console.Write("Target IP: ");
            string target_ip = Console.ReadLine() ?? "192.168.1.1";

            Console.WriteLine("Scanning the ports open...");
            Top100Ports[] top100 = Top100Ports.GetAllPorts().ToArray();

            List<Task<Port>> portsOpen = new List<Task<Port>>();
            for (int i=1; i < top100.Length; i++)
            {
                portsOpen.Add(IsPortOpen(target_ip, top100[i].Port));
            }

            //Wait for all the tasks to complete
            Task.WaitAll(portsOpen.ToArray());

            //Now you can iterate over your list of pingTasks
            foreach (var pingTask in portsOpen)
            {
                if (pingTask.Result.isOpen != true)
                    continue;

                //pingTask.Result is whatever type T was declared in PingAsync
                Console.WriteLine($"{pingTask.Result.ip} is currently open on port {pingTask.Result.port}!");
            }

            Console.WriteLine("Peasant scan the ports successfully.");
        }

        static void ScanNetworkHost()
        {
            Console.Write("Gateway IP: ");
            string gw_ip = Console.ReadLine() ?? "192.168.1.1";

            Console.Write("Gateway Subnet: ");
            int gw_sm = System.Convert.ToInt16(Console.ReadLine());

            Console.WriteLine("Scanning the network mask provided...");

            List<Task<PingReply>> pingTasks = new List<Task<PingReply>>();
            string[] ipAddresses = SubnetMask.ListIp(gw_ip, gw_sm);
            foreach (string ipAddress in ipAddresses)
            {
                pingTasks.Add(PingAsync(ipAddress));
            }

            //Wait for all the tasks to complete
            Task.WaitAll(pingTasks.ToArray());
            
            //Now you can iterate over your list of pingTasks
            foreach (var pingTask in pingTasks)
            {
                if (pingTask.Result.Status != IPStatus.Success)
                    continue;

                //pingTask.Result is whatever type T was declared in PingAsync
                Console.WriteLine($"{pingTask.Result.Address.ToString()} is {(pingTask.Result.Status == IPStatus.Success ? "online" : "offline")} with a roundtrip time of {pingTask.Result.RoundtripTime}ms");
            }

            Console.WriteLine("Peasant mapped the network successfully.");
        }

        static Task<PingReply> PingAsync(string address)
        {
            var tcs = new TaskCompletionSource<PingReply>();
            Ping ping = new Ping();
            ping.PingCompleted += (obj, sender) =>
            {
                tcs.SetResult(sender.Reply);
            };
            ping.SendAsync(address, new object());
            return tcs.Task;
        }
    }
}
