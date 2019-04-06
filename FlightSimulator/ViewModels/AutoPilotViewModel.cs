using FlightSimulator.Model;
using FlightSimulator.Models;
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace FlightSimulator.ViewModels
{
    class AutoPilotViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private AutoPilotModel model;

        public AutoPilotViewModel()
        {
            model = new AutoPilotModel();
           
        }

    }
}
