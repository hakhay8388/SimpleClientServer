using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Core.SocketLib.nPacket.nSender
{
    public class cPacketSender
    {
        private cClient Client;

        public cPacketSender(cClient _Client)
        {
            Client = _Client;
        }

        public void Send(string _Data)
        {
            if (Client.IsConnected)
            {
                try
                {
                    var __FullPacket = new List<byte>();
                    __FullPacket.AddRange(BitConverter.GetBytes(_Data.Length));
                    __FullPacket.AddRange(Encoding.Default.GetBytes(_Data));

                    Client.OwnerSocket.Send(__FullPacket.ToArray());
                }
                catch (Exception _Ex)
                {

                    throw new Exception();
                }
            }
            else
            {
                Console.WriteLine("Disconnected!");
            }
        }        
    }
}
