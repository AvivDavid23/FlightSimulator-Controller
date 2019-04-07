namespace FlightSimulator.Models.Interface
{
    interface ITelnetServer
    {
        void accept(string ip, int port);

        void read(string command);

        void close();
    }
}
