using Peasant.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
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
                Console.WriteLine("1. Check for active Host");
                Console.WriteLine("2. Exit Network Mapper");
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

        static bool PingHost(string ipAddress)
        {
            try
            {
                Ping ping = new Ping();
                PingReply reply = ping.Send(ipAddress);

                if (reply != null && reply.Status == IPStatus.Success)
                {
                    return true;
                }
            }
            catch (Exception)
            {
                // An exception occurred, indicating that the host is likely offline
            }

            return false;
        }

        static void ScanNetworkHost()
        {
            Console.WriteLine("Submit a support request");
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

            Console.WriteLine("Support mapped the network successfully.");
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
