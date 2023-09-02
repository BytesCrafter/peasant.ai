using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Peasant.Core.Helpers
{
    public class SubnetMask
    {
        public static string[] ListIp(string gatewayIpAddress = "192.168.1.1", int subnetMask = 255)
        {
            List<string> ipList = new List<string>();
            string[] subnet = gatewayIpAddress.Split('.');

            for (int i = 1; i < subnetMask; i++)
            {
                ipList.Add(subnet[0] + "." + subnet[1] + "." + subnet[2] + "." + i);
            }

            return ipList.ToArray();
        }
    }
}
