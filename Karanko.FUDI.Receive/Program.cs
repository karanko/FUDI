using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karanko.FUDI.Receive
{
    class Program
    {
        static void Main(string[] args)
        {
            Netreceive x = new Netreceive(9090);
            x.OnReceive += new EventHandler(DoWork);
            x.Connect();
           
            Console.ReadLine();
            x.Disconnect();
        }
        private static void DoWork(object s, EventArgs e)
        {
            string message = (string)s;
            Console.WriteLine(message);
        }
    }
}
