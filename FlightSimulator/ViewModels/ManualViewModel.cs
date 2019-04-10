using FlightSimulator.Models;
using System;

namespace FlightSimulator.ViewModels.Windows
{
    class ManualViewModel
    {
        private ManualModel model = new ManualModel();


        // paths to simulator data, in order to know which set command to send
        private readonly string throttlePath = " /controls/engines/current-engine/throttle ";
        private readonly string rudderePath = " /controls/flight/rudder ";
        private readonly string aileronPath = " /controls/flight/aileron ";
        private readonly string elevatorPath = " /controls/flight/elevator ";

        public double Throttle
        {
            set => model.SendCommand("set" + throttlePath + Convert.ToString(value));
        }

        public double Rudder
        {
            set => model.SendCommand("set" + rudderePath + Convert.ToString(value));
        }

        public double Aileron
        {
            set => model.SendCommand("set" + aileronPath + Convert.ToString(value));
        }

        public double Elevator
        {
            set => model.SendCommand("set" + elevatorPath + Convert.ToString(value));
        }
    }
}
