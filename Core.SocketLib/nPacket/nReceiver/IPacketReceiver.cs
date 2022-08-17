using System;
using System.Collections.Generic;
using System.Text;

namespace Core.SocketLib.nPacket.nReceiver
{
    public interface IPacketReceiver
    {
        void ReceivePacketData(cClient _Client, string _Data);
    }
}
