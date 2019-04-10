using FlightSimulator.Communication;
using System.Threading;

namespace FlightSimulator.Models
{
    class AutoPilotModel
    {
        // send commands to the simulator
        public void SendCommands(string input)
        {
            new Thread(delegate ()
            {
                Commands.Instance.SendCommands(input);
            }).Start();

        }

    }
}

