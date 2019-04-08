using FlightSimulator.Models;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Media;

namespace FlightSimulator.ViewModels
{
    class AutoPilotViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Brush background = Brushes.White; // Background color

        private string commands; // Commands to be sent
        public string Commands
        {
            get { return commands; }

            set
            {
                commands = value;
                if (!string.IsNullOrEmpty(Commands) && Background == Brushes.White) Background = Brushes.LightPink; // if background is white and no text
                else if (string.IsNullOrEmpty(Commands)) Background = Brushes.White; // if text is not empty
            }
        }
        public Brush Background
        {
            get
            {
                return background;
            }
            set
            {
                background = value;
                NotifyPropertyChanged("Background");
            }

        }

        // TODO: Ok command //
        #region okCommand
        private ICommand okCommand; // Ok command for clear button
        public ICommand OkCommand
        {
            get
            {
                return okCommand ?? (okCommand = new CommandHandler(() =>
                {
                    Commands = ""; // remove text
                    NotifyPropertyChanged(Commands); // Notify view!
                    Background = Brushes.White; // make background white again
                }));
            }
        }

        #endregion


        #region clearCommand
        private ICommand clearCommand; // Clear command for clear button
        public ICommand ClearCommand
        {
            get
            {
                return clearCommand ?? (clearCommand = new CommandHandler(() =>
                {
                    Commands = "";
                    Background = Brushes.White;
                    NotifyPropertyChanged(Commands); // Notify view!
                }));
            }
        }

        #endregion

        public void NotifyPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

    }
}
