using Core.SocketLib;
using Core.SocketLib.nPacket.nReceiver;
using Server.nStrategyManager.nStrategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.nStrategyManager
{
    public class cStrategyManager : IPacketReceiver
    {
        public cServer Server { get; set; }
        public List<cBaseStrategy> StrategyList { get; set; }
        public cStrategyManager(cServer _Server)
        {
            StrategyList = new List<cBaseStrategy>();
            Server = _Server;
            Init();
        }

        public void Init()
        {
            StrategyList.Add(new cBreakStrategy(this));
            StrategyList.Add(new cBroadcasterStrategy(this));
        }

        public void ReceivePacketData(cClient _Client, string _Data)
        {
            foreach(cBaseStrategy __Strategy in StrategyList)
            {
                if (!__Strategy.ReceivePacketData(_Client, _Data))
                {
                    break;
                }
            }
        }
    }
}
