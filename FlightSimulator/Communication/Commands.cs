using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace FlightSimulator.Communication
{
    class Commands
    {
        private TcpClient client; // client

        private BinaryWriter writer; // writer

        public bool Connected { get; set; } = false; // is the clinet connected?

        #region Singleton
        private static Commands m_Instance = null;
        public static Commands Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new Commands();
                }
                return m_Instance;
            }
        }
        #endregion

        public void Reset() { m_Instance = null; }
        // connect to server
        public void Connect(string ip, int port)
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ip), port);
            client = new TcpClient();
            while (!client.Connected) // keep trynig to connect
            {
                try { client.Connect(ep); }
                catch (Exception) { }
            }
            Connected = true;
            writer = new BinaryWriter(client.GetStream());

        }

        // send commands to the simulator
        public void SendCommands(string input)
        {
            if (string.IsNullOrEmpty(input)) return; // in case where user pressed ok and text is empty
            string[] commands = input.Split('\n');
            foreach (string command in commands)
            {
                string tmp = command + "\r\n";
                writer.Write(System.Text.Encoding.ASCII.GetBytes(tmp));
                System.Threading.Thread.Sleep(2000); // 2 seconds delay
            }
        }
    }
}
