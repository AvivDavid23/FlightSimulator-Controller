using System.IO;
using System.Net;
using System.Net.Sockets;

namespace FlightSimulator.Comunication
{
    class Info
    {
        private bool connected = false;
        private TcpListener listener;
        private TcpClient client;
        private BinaryReader reader;

        // open server with ip and port
        public void open(string ip, int port)
        {
            listener = new TcpListener(new IPEndPoint(IPAddress.Parse(ip), port));
            listener.Start();

        }

        // read simulator input and return it to the model
        public string[] read()
        {
            if (!connected)
            {
                connected = true;
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
