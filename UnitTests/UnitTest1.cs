using System;
using System.Collections.Generic;
using System.Threading;
using Client;
using Core.SocketLib;
using Core.SocketLib.nPacket.nReceiver;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Server;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1: IPacketReceiver
    {
        static cServer m_Server { get; set; }
        static cClientManager m_ClientManager1 { get; set; }
        static cClientManager m_ClientManager2 { get; set; }
        static List<Tuple<cClient, string>> RecievedMessage { get; set; }

        static string FirstTestMessage = "TEST_ABCD";
        static string SecondTestMessage = "OTHER_TEST_ABCD";
        static string ServerIP = "127.0.0.1";
        static int ServerPort = 1234;

        [TestMethod]
        public void TestUnit1_CreateServer()
        {
            m_Server = new cServer(ServerPort);
            m_Server.StartListening();
        }

        [TestMethod]
        public void TestUnit2_CreateClient1()
        {
            m_ClientManager1 = new cClientManager(this);
            m_ClientManager1.TryToConnect(ServerIP, ServerPort);
        }

        [TestMethod]
        public void TestUnit3_CreateClient2()
        {
            m_ClientManager2 = new cClientManager(this);
            m_ClientManager2.TryToConnect(ServerIP, ServerPort);
        }

        public void Test_SendMessage(cClientManager _ClientManager, string _Messsage)
        {
            RecievedMessage = new List<Tuple<cClient, string>>();

            _ClientManager.Client.PacketSender.Send(_Messsage);
            int __Counter = 0;
            while (RecievedMessage.Count < 2 && __Counter < 10)
            {
                Thread.Sleep(1000);
                __Counter++;
            }
            if (RecievedMessage.Count != 2)
            {
                Assert.Fail();
            }
            else
            {
                for (int i = 0; i < RecievedMessage.Count; i++)
                {
                    if ((RecievedMessage[i].Item1.Id != m_ClientManager1.Client.Id || RecievedMessage[i].Item2 != _Messsage)
                        && (RecievedMessage[i].Item1.Id != m_ClientManager2.Client.Id || RecievedMessage[i].Item2 != _Messsage))
                    {
                        Assert.Fail();
                        break;
                    }
                }
            }
        }

        [TestMethod]
        public void TestUnit4_FirstClient_SendMessage()
        {
            Test_SendMessage(m_ClientManager1, FirstTestMessage);
        }

        [TestMethod]
        public void TestUnit5_SecondClient_SendMessage()
        {
            Test_SendMessage(m_ClientManager2, SecondTestMessage);
        }

        public void ReceivePacketData(cClient _Client, string _Data)
        {
            RecievedMessage.Add(new Tuple<cClient, string>(_Client, _Data));
        }
    }
}
