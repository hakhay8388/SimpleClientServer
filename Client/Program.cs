using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            cClientManager __ClientManager = new cClientManager();
            __ClientManager.TryToConnect("127.0.0.1", 1234);

            Console.Write("Enter your name : ");
            string __Name = Console.ReadLine();

            Console.Write("Message : ");
            string __Command = Console.ReadLine();
            while (__Command != "exit")
            {
                __ClientManager.Client.PacketSender.Send(__Name + " : " + __Command);

                Console.Write("Message : ");
                __Command = Console.ReadLine();
            }

            Console.Write("Program Ended!");
        }
    }
}
