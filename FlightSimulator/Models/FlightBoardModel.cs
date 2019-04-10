using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FlightSimulator.Communication;
using FlightSimulator.ViewModels;

namespace FlightSimulator.Models
{
    class FlightBoardModel : BaseNotify
    {
        private Info info; // Info Server

        public event PropertyChangedEventHandler PropertyChanged;

        public FlightBoardModel(Info info)
        {
            this.info = info;
        }

        private double lon;
        public double Lon
        {
            get { return lon; }

            set
            {
                lon = value;
                NotifyPropertyChanged("Lon"); // Notify viewModel
            }
        }

        private double lat;
        public double Lat
        {
            get { return lat; }

            set
            {
                lat = value;
                NotifyPropertyChanged("Lat"); // Notify viewModel
            }
        }

        // tell server to open and start reading
        public void Open(string ip, int port)
        {
            info.Open(ip, port);
            StartRead();

        }

        // read input in a new thread, notify view model about the new data to 
        void StartRead()
        {
            new Thread(delegate () {
                while (true)
                {
                    string[] args = info.Read();
                    Lon = Convert.ToDouble(args[0]);
                    Lat = Convert.ToDouble(args[1]);
                }
            }).Start();
        }


        public void NotifyPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
