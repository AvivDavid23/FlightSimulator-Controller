using System.IO;
using System.Net;
using System.Net.Sockets;

namespace FlightSimulator.Communication
{
    class Info
    {
        public bool Connected { get; set; } = false;
        public bool Stop { get; set; } = false;
        private TcpListener listener;
        private TcpClient client;
        private BinaryReader reader;



        // open server with ip and port
        public void Open(string ip, int port)
        {
            if (listener != null) Close();
            listener = new TcpListener(new IPEndPoint(IPAddress.Parse(ip), port));
            listener.Start();
        }

        public void Close() { client.Close(); listener.Stop(); Connected = false; }

        // read simulator input and return it to the model
        public string[] Read()
        {
            if (!Connected)
            {
                Connected = true;
                client = listener.AcceptTcpClient();
                reader = new BinaryReader(client.GetStream());
            }
            string input = ""; // input will be stored here
            char s;
            while ((s = reader.ReadChar()) != '\n') input += s; // read untill \n
            string[] param = input.Split(','); // split by comma
            string[] ret = { param[0], param[1] }; // lon nad lat only
            return ret;

        }
    }
}
