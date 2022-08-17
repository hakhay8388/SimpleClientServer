using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.SocketLib;

namespace Server.nStrategyManager.nStrategies
{
    public class cBroadcasterStrategy : cBaseStrategy
    {
        public cBroadcasterStrategy(cStrategyManager _StrategyManager)
            :base(_StrategyManager)
        {
        }
        public override bool ReceivePacketData(cClient _Client, string _Data)
        {
            StrategyManager.Server.SendToAll(_Data);
            return true;
        }
    }
}
