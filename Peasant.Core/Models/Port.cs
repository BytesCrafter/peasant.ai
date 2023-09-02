using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peasant.Core.Models
{
    public class Port
    {
        public string ip { get; set; }
        public int port { get; set; }
        public bool isOpen { get; set; }

        public Port(string _ip, int _port, bool _isOpen)
        {
            ip = _ip;
            port = _port;
            isOpen = _isOpen;
        }
    }
}
