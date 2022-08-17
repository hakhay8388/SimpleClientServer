using Core.SocketLib;
using Server.nStrategyManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class cServer
    {
        public List<cClient> Clients = new List<cClient>();
        public Socket MainListenerSocket { private set; get; }
        public int Port { private set; get; }

        public cServer(int _Port)
        {
            Port = _Port;
            MainListenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void StartListening()
        {
            try
            {
                Console.WriteLine($"Listening started port:{Port} protocol type: {ProtocolType.Tcp}");
                MainListenerSocket.Bind(new IPEndPoint(IPAddress.Any, Port));
                MainListenerSocket.Listen(10);
                MainListenerSocket.BeginAccept(AcceptCallback, MainListenerSocket);
            }
            catch (Exception _Ex)
            {
                throw new Exception("Listening Error : " + _Ex);
            }
        }

        private cClient GetClientByID(string _Guid)
        {
            cClient __Client = Clients.Find(__Item => __Item.Id == _Guid);
            return __Client;
        }

        private string CreateID()
        {
            string __Guid = Guid.NewGuid().ToString();
            while (GetClientByID(__Guid) != null)
            {
                __Guid = Guid.NewGuid().ToString();
            }
            return __Guid;
        }

        private void AcceptCallback(IAsyncResult _AsyncResult)
        {
            try
            {
                Console.WriteLine($"Accept CallBack port:{Port} protocol type: {ProtocolType.Tcp}");
                Socket __AcceptedSocket = MainListenerSocket.EndAccept(_AsyncResult);

                Clients.Add(new cClient(__AcceptedSocket, CreateID(), new cStrategyManager(this)));

                MainListenerSocket.BeginAccept(AcceptCallback, MainListenerSocket);
            }
            catch (Exception _Ex)
            {
                throw new Exception("Base Accept Error : " + _Ex);
            }
        }

        public void RemoveClient(string _Id)
        {
            Clients.Remove(GetClientByID(_Id));
        }

        public void RemoveClient(cClient _Client)
        {
            Clients.Remove(_Client);
        }

        public void SendToAll(string _Data)
        {
            foreach(cClient __Client in Clients)
            {
                if (__Client.IsConnected)
                {
                    __Client.PacketSender.Send(_Data);
                }
            }
        }
    }
}
