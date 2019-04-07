using FlightSimulator.Models;
using FlightSimulator.Views.Windows;
using System.ComponentModel;
using System.Windows.Input;

namespace FlightSimulator.ViewModels
{
    public class FlightBoardViewModel : BaseNotify
    {
        Settings settingsChild = new Settings();

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

        // Allow to create only one settings window:
        void OnSttingsClick()
        {

            if (!settingsChild.IsLoaded)
            {
                settingsChild = new Settings();
                settingsChild.Show();
            }
            else settingsChild.Show();



        }

        #endregion
        public void NotifyPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
