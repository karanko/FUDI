using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;


namespace Karanko.FUDI
{
    public class Netsend
    {
        private int _sourceport = Utils.FreeTcpPort();
        private int _port = -1;
        private string _host = "localhost";
        private UdpClient _udpsocket;
        public Netsend(int port)
        {
            // add type(UDP/TCP) in here
            _port = port;//add vaildation
        }
        public Netsend(string host, int port)
        {
            // add type(UDP/TCP) in here
            _host = host;
            _port = port;//add vaildation
        }
        public Netsend()
        {

        }
        public bool Disconnect()
        {
            if (Connected())
            {
                _udpsocket.Client.Disconnect(true);
                _udpsocket.Close();
                _udpsocket = null;//maybe
            }
            return Connected();
        }
        public bool Connect()
        {
            if (Connected())
            {
                Disconnect();
            }

            if (_port > 0)
            {
                _udpsocket = new UdpClient(_sourceport);
            }
            return Connected();

        }
        public void Connect(string host, int port)
        {
            // TODO: try to open port on all interfaces otherwise local only.
            _host = host;
            _port = port;
            Connect();
        }
        public bool Connected()
        {
            bool result = false;
            if (_udpsocket != null)
            {
                result = true;//_udpsocket.Client.Connected;              
            }
            ////improve logic here
            ////Console.WriteLine("Port: {0} : Connected: {1}",_port, result);
            return result;
        }
        public List<int> Message(List<int> msgs)
        {
            List<int> results = new List<int>();
            foreach (int msg in msgs)
            {
                results.Add(Message(msg));

            }
            return results;
        }
        public List<int> Message(List<string> msgs)
        {
            List<int> results = new List<int>();
            foreach (string msg in msgs)
            {
                results.Add(Message(msg));

            }
            return results;
        }
        public int Message(object msg)
        {
            return Message(msg.ToString());
        }
        public int Message(string msg)
        {
            msg += ";";
            msg += Environment.NewLine;
            byte[] data = Encoding.ASCII.GetBytes(msg);
            return _udpsocket.Send(data, data.Length, new IPEndPoint(Utils.GetIpFromHostname(_host), _port));
        }

    }
}
