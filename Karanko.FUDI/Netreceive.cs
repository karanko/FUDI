using System;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;

namespace Karanko.FUDI
{
    public class Netreceive
    {
        //// https://yal.cc/cs-dotnet-asynchronous-udp-example

        private int _port = -1;
        private string _host = "localhost";
        private UdpClient _udpsocket;
        public Netreceive(int port)
        {
            // add type(UDP/TCP) in here
            _port = port;//add vaildation
        }

        public Netreceive(string host, int port)
        {
            // add type(UDP/TCP) in here
            _host = host;
            _port = port;//add vaildation
        }
        public Netreceive()
        {

        }
        public bool Disconnect()
        {
            if(Connected())
            {             
                _udpsocket.Client.Disconnect(true);
                _udpsocket.Close();
                _udpsocket = null;//maybe
            }

            return Connected();
        }
        public event EventHandler OnReceive;

        public bool Connect()
        {
            if(Connected())
            {
                Disconnect();
            }

            if (_port > 0)
            {
                _udpsocket = new UdpClient(_port);
                _udpsocket.BeginReceive(new AsyncCallback(OnUdpData), _udpsocket);
            }
            return Connected();

        }
        public void Connect(string host, int port)
        {
            //TODO: try to open port on all interfaces otherwise local only.
            _host = host;
            _port = port;
            Connect();
        }
        public bool Connected()
        {
            bool result = false; 
            if(_udpsocket != null)
            {
                result = true;//_udpsocket.Client.Connected;              
            }
            //improve logic here
            //Console.WriteLine("Port: {0} : Connected: {1}",_port, result);
            return result;
        }

        private void OnUdpData(IAsyncResult result)
        {
            if(!Connected())
            {
                Console.WriteLine("Not Connected");
                return;
            }
            UdpClient socket = result.AsyncState as UdpClient;
            IPEndPoint source = new IPEndPoint(0, 0);
            byte[] message = socket.EndReceive(result, ref source);
            string stringResult = System.Text.Encoding.UTF8.GetString(message);
            stringResult = Regex.Replace(stringResult, @"\t|\n|\r|;", ""); //cleanup for FUDI

            EventHandler handler = OnReceive;
            if (null != handler) handler(stringResult, EventArgs.Empty);

            socket.BeginReceive(new AsyncCallback(OnUdpData), socket);
        }

    }
}
