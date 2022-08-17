using Core.SocketLib;
using Core.SocketLib.nPacket.nReceiver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.nStrategyManager
{
    public abstract class cBaseStrategy 
    {
        protected cStrategyManager StrategyManager { get; set; }
        public cBaseStrategy(cStrategyManager _StrategyManager)
        {
            StrategyManager = _StrategyManager;
        }
        public abstract bool ReceivePacketData(cClient _Client, string _Data);
    }
}
