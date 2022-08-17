using Core.SocketLib;
using Core.SocketLib.nPacket.nReceiver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Client
{
    public class cClientManager : IPacketReceiver
    {
        IPacketReceiver m_ExternalPacketReceiver { get; set; }
        public cClientManager(IPacketReceiver _ExternalPacketReceiver = null)
        {
            m_ExternalPacketReceiver = _ExternalPacketReceiver;
        }

        public cClient Client { get; set; }

        public void ReceivePacketData(cClient _Client, string _Data)
        {
            if (m_ExternalPacketReceiver != null)
            {
                m_ExternalPacketReceiver.ReceivePacketData(_Client, _Data);
            }
            ClearCurrentConsoleLine();
            Console.WriteLine(_Data);
            Console.Write("Message : ");
        }

        public void ClearCurrentConsoleLine()
        {
            int __CurrentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, __CurrentLineCursor);
        }

        public void TryToConnect(string _IP, int _Port)
        {
            Socket __ConnectingSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            int __Counter = 0;
            while (!__ConnectingSocket.Connected)
            {
                Thread.Sleep(1000);

                try
                {
                    __ConnectingSocket.Connect(new IPEndPoint(IPAddress.Parse(_IP), _Port));
                }
                catch 
                {
                    __Counter++;
                    if (__Counter > 10)
                    {
                        throw new Exception("Server not found!");
                    }
                }
            }
            SetupClient(__ConnectingSocket);
        }
        private void SetupClient(Socket _Socket)
        {
            Client = new cClient(_Socket, Guid.NewGuid().ToString(), this);
        }
    }
}
