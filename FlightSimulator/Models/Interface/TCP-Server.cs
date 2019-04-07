namespace FlightSimulator.Models.Interface
{
    interface TCP_Server
    {
        void accept(string ip, int port);

        void read(string command);

        void close();
    }
}
