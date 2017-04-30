using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karanko.FUDI.Send
{
    class Program
    {
        static void Main(string[] args)
        {
            Netsend send = new Karanko.FUDI.Netsend(3001);
            send.Connect();
            while (send.Connected())
            {
                send.Message(Console.ReadLine());
            }
            Console.WriteLine();

        }
    }
}
