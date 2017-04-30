using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Karanko.FUDI
{
    internal class Utils
    {

        private static Dictionary<string, IPAddress> _ipaddresscache = new Dictionary<string, IPAddress>();
        internal static int FreeTcpPort()
        {
            TcpListener l = new TcpListener(IPAddress.Loopback, 0);
            l.Start();
            int port = ((IPEndPoint)l.LocalEndpoint).Port;
            l.Stop();
            return port;
        }

        internal static IPAddress GetIpFromHostname(string host)
        {
            host = host.ToLower();

            if (_ipaddresscache.ContainsKey(host))
            {
                return _ipaddresscache[host];
            }

            IPAddress address;
            if (IPAddress.TryParse(host, out address))
            {
                _ipaddresscache.Add(host, address);
            }

            IPHostEntry hostEntry = Dns.GetHostEntry(host);


            foreach (IPAddress add in hostEntry.AddressList)
            {
                if (add.AddressFamily == AddressFamily.InterNetwork)
                {
                    _ipaddresscache.Add(host, add);
                    return _ipaddresscache[host];
                }
            }
            //fail back to nay address type
            if (hostEntry.AddressList.Length > 0)
            {
                _ipaddresscache.Add(host, hostEntry.AddressList[0]);
                return _ipaddresscache[host];
            }

            return IPAddress.None;
        }
    }
}
