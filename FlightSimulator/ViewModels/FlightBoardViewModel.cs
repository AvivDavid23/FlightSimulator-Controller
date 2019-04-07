using FlightSimulator.Models;
using FlightSimulator.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FlightSimulator.Views.Windows;
using System.ComponentModel;

namespace FlightSimulator.ViewModels
{
    public class FlightBoardViewModel : BaseNotify
    {
        public event PropertyChangedEventHandler PropertyChanged;

      

        public double Lon
        {
            get;
        }

        public double Lat
        {
            get;
        }

        #region Setting Command
        private ICommand settingsCommand; // Settings command for settings button
        public ICommand SettingsCommand { get { return settingsCommand ?? (settingsCommand = new CommandHandler(() => OnSttingsClick())); } }

        void OnSttingsClick()
        {
            new Settings().Show();
        }

        #endregion
        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
