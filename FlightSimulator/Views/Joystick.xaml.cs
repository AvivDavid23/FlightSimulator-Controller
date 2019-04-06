using FlightSimulator.Model.EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FlightSimulator.Views
{
    /// <summary>
    /// Interaction logic for Joystick.xaml
    /// </summary>
    public partial class Joystick : UserControl
    {
        /// <summary>Current Aileron</summary>
        public static readonly DependencyProperty AileronProperty =
            DependencyProperty.Register("Aileron", typeof(double), typeof(Joystick),null);

        /// <summary>Current Elevator</summary>
        public static readonly DependencyProperty ElevatorProperty =
            DependencyProperty.Register("Elevator", typeof(double), typeof(Joystick), null);

        /// <summary>How often should be raised StickMove event in degrees</summary>
        public static readonly DependencyProperty AileronStepProperty =
            DependencyProperty.Register("AileronStep", typeof(double), typeof(Joystick), new PropertyMetadata(1.0));

        /// <summary>How often should be raised StickMove event in Elevator units</summary>
        public static readonly DependencyProperty ElevatorStepProperty =
            DependencyProperty.Register("ElevatorStep", typeof(double), typeof(Joystick), new PropertyMetadata(1.0));

        /* Unstable - needs work */
        ///// <summary>Indicates whether the joystick knob resets its place after being released</summary>
        //public static readonly DependencyProperty ResetKnobAfterReleaseProperty =
        //    DependencyProperty.Register(nameof(ResetKnobAfterRelease), typeof(bool), typeof(VirtualJoystick), new PropertyMetadata(true));

        /// <summary>Current Aileron in degrees from 0 to 360</summary>
        public double Aileron
        {
            get { return Convert.ToDouble(GetValue(AileronProperty)); }
            set { SetValue(AileronProperty, value); }
        }

        /// <summary>current Elevator (or "power"), from 0 to 100</summary>
        public double Elevator
        {
            get { return Convert.ToDouble(GetValue(ElevatorProperty)); }
            set { SetValue(ElevatorProperty, value); }
        }

        /// <summary>How often should be raised StickMove event in degrees</summary>
        public double AileronStep
        {
            get { return Convert.ToDouble(GetValue(AileronStepProperty)); }
            set
            {
                if (value < 1) value = 1; else if (value > 90) value = 90;
                SetValue(AileronStepProperty, Math.Round(value));
            }
        }

        /// <summary>How often should be raised StickMove event in Elevator units</summary>
        public double ElevatorStep
        {
            get { return Convert.ToDouble(GetValue(ElevatorStepProperty)); }
            set
            {
                if (value < 1) value = 1; else if (value > 50) value = 50;
                SetValue(ElevatorStepProperty, value);
            }
        }

        /// <summary>Indicates whether the joystick knob resets its place after being released</summary>
        //public bool ResetKnobAfterRelease
        //{
        //    get { return Convert.ToBoolean(GetValue(ResetKnobAfterReleaseProperty)); }
        //    set { SetValue(ResetKnobAfterReleaseProperty, value); }
        //}

        /// <summary>Delegate holding data for joystick state change</summary>
        /// <param name="sender">The object that fired the event</param>
        /// <param name="args">Holds new values for Aileron and Elevator</param>
        public delegate void OnScreenJoystickEventHandler(Joystick sender, VirtualJoystickEventArgs args);

        /// <summary>Delegate for joystick events that hold no data</summary>
        /// <param name="sender">The object that fired the event</param>
        public delegate void EmptyJoystickEventHandler(Joystick sender);

        /// <summary>This event fires whenever the joystick moves</summary>
        public event OnScreenJoystickEventHandler Moved;

        /// <summary>This event fires once the joystick is released and its position is reset</summary>
        public event EmptyJoystickEventHandler Released;

        /// <summary>This event fires once the joystick is captured</summary>
        public event EmptyJoystickEventHandler Captured;

        private Point _startPos;
        private double _prevAileron, _prevElevator;
        private double canvasWidth, canvasHeight;
        private readonly Storyboard centerKnob;

        public Joystick()
        {
            InitializeComponent();

            Knob.MouseLeftButtonDown += Knob_MouseLeftButtonDown;
            Knob.MouseLeftButtonUp += Knob_MouseLeftButtonUp;
            Knob.MouseMove += Knob_MouseMove;

            centerKnob = Knob.Resources["CenterKnob"] as Storyboard;
        }

        private void Knob_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _startPos = e.GetPosition(Base);
            _prevAileron = _prevElevator = 0;
            canvasWidth = Base.ActualWidth - KnobBase.ActualWidth;
            canvasHeight = Base.ActualHeight - KnobBase.ActualHeight;
            Captured?.Invoke(this);
            Knob.CaptureMouse();

            centerKnob.Stop();
        }

        private void Knob_MouseMove(object sender, MouseEventArgs e)
        {
            ///!!!!!!!!!!!!!!!!!
            /// YOU MUST CHANGE THE FUNCTION!!!!
            ///!!!!!!!!!!!!!!
            if (!Knob.IsMouseCaptured) return;

            Point newPos = e.GetPosition(Base);

            Point deltaPos = new Point(newPos.X - _startPos.X, newPos.Y - _startPos.Y);

            double distance = Math.Round(Math.Sqrt(deltaPos.X * deltaPos.X + deltaPos.Y * deltaPos.Y));
            if (distance >= canvasWidth / 2 || distance >= canvasHeight / 2)
                return;
            Aileron = -deltaPos.Y;
            Elevator = deltaPos.X;

            knobPosition.X = deltaPos.X;
            knobPosition.Y = deltaPos.Y;

            if (Moved == null ||
                (!(Math.Abs(_prevAileron - Aileron) > AileronStep) && !(Math.Abs(_prevElevator - Elevator) > ElevatorStep)))
                return;

            Moved?.Invoke(this, new VirtualJoystickEventArgs { Aileron = Aileron, Elevator = Elevator });
            _prevAileron = Aileron;
            _prevElevator = Elevator;

        }

        private void Knob_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Knob.ReleaseMouseCapture();
            centerKnob.Begin();
        }

        private void centerKnob_Completed(object sender, EventArgs e)
        {
            Aileron = Elevator = _prevAileron = _prevElevator = 0;
            Released?.Invoke(this);
        }

    }
}
