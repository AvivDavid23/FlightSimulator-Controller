using FlightSimulator.Model;
using FlightSimulator.Models;
using System.ComponentModel;
using System.Windows.Input;

namespace FlightSimulator.ViewModels
{
    class AutoPilotViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private AutoPilotModel model; // Model

        private string commands; // Commands for simulator

        public string Commands { get { return commands; } set { commands = value; } }

        public AutoPilotViewModel()
        {
            model = new AutoPilotModel();

        }

        // TODO: Ok command //
        #region okCommand
        private ICommand okCommand; // Ok command for clear button
        public ICommand OkCommand { get { return okCommand ?? (okCommand = new CommandHandler(() => OnOkClick())); } }

        void OnOkClick()
        {
        
        }
        #endregion

        #region clearCommand
        private ICommand clearCommand; // Clear command for clear button
        public ICommand ClearCommand { get { return clearCommand ?? (clearCommand = new CommandHandler(() => OnClearClick())); } }

        void OnClearClick()
        {
            Commands = "";
            NotifyPropertyChanged(Commands); // Notify view!
        }
        #endregion

        public void NotifyPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
