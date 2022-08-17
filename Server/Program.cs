using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class Program
    {
        static void Main(string[] args)
        {
            cServer __Server = new cServer(1234);
            __Server.StartListening();
            Console.ReadLine();
        }
    }
}
