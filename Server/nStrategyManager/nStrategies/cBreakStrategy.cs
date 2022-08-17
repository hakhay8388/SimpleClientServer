using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.SocketLib;

namespace Server.nStrategyManager.nStrategies
{
    public class cBreakStrategy : cBaseStrategy
    {
        int m_ExceptionCount { get; set; }
        DateTime m_LastMessageTime { get; set; }
        public cBreakStrategy(cStrategyManager _StrategyManager)
            :base(_StrategyManager)
        {
            m_LastMessageTime = DateTime.Now.AddSeconds(-10);
            m_ExceptionCount = 0;
        }
        public override bool ReceivePacketData(cClient _Client, string _Data)
        {
            if ((DateTime.Now - m_LastMessageTime).TotalMilliseconds < 1000)
            {
                m_ExceptionCount++;
                if (m_ExceptionCount > 1)
                {
                    _Client.PacketSender.Send("You sended many message in a second and Disconnected!");
                    _Client.Disconnect();

                    StrategyManager.Server.RemoveClient(_Client);
                    return false;
                }
                else
                {
                    _Client.PacketSender.Send("You sended many message in a second!");
                }
            }
            m_LastMessageTime = DateTime.Now;
            return true;
        }
    }
}
