using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Core.SocketLib.nPacket.nReceiver
{
    public class cPacketReceiver
    {
        IPacketReceiver m_PacketReciever { get; set; }

        cClient m_Client { get; set; }

        byte[] m_Buffer { get; set; }

        public cPacketReceiver(cClient _Client, IPacketReceiver _PacketReciever)
        {
            m_Client = _Client;
            m_PacketReciever = _PacketReciever;
        }

        public void StartReceiving()
        {
            try
            {
                m_Buffer = new byte[4];
                m_Client.OwnerSocket.BeginReceive(m_Buffer, 0, m_Buffer.Length, SocketFlags.None, ReceiveCallback, null);
            }
            catch { }
        }

        private void ReceiveCallback(IAsyncResult _AsyncResult)
        {
            try
            {
                if (m_Client.OwnerSocket.EndReceive(_AsyncResult) > 1)
                {
                    m_Buffer = new byte[BitConverter.ToInt32(m_Buffer, 0)];
                    m_Client.OwnerSocket.Receive(m_Buffer, m_Buffer.Length, SocketFlags.None);
                    string __Data = Encoding.Default.GetString(m_Buffer);
                    if (m_PacketReciever != null)
                    {
                        m_PacketReciever.ReceivePacketData(m_Client, __Data);
                    }                    
                    StartReceiving();
                }
                else
                {
                    m_Client.Disconnect();
                }
            }
            catch
            {
                if (!m_Client.IsConnected)
                {
                    m_Client.Disconnect();
                }
                else
                {
                    StartReceiving();
                }
            }
        }       
    }
}
