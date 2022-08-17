using Core.SocketLib.nPacket.nReceiver;
using Core.SocketLib.nPacket.nSender;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Core.SocketLib
{
    public class cClient
    {
        public Socket OwnerSocket { get; private set; }
        public cPacketReceiver PacketReceiver { get; private set; }
        public cPacketSender PacketSender { get; private set; }
        public string Id { get; private set; }
        public bool IsConnected { get { return OwnerSocket.Connected; } }

        public cClient(Socket _OwnerSocket, string _Id, IPacketReceiver _PacketReceiver)
        {
            Id = _Id;
            OwnerSocket = _OwnerSocket;
            PacketSender = new cPacketSender(this);
            PacketReceiver = new cPacketReceiver(this, _PacketReceiver);
            PacketReceiver.StartReceiving();
        }

        public void Disconnect()
        {
            OwnerSocket.Disconnect(true);
        }
    }
}
