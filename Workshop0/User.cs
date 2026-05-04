using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Timers;

namespace Workshop0
{
    public class User : INotifyPropertyChanged
    {
        private System.Timers.Timer _timer = new(1000);

        public User() { 
            _timer.Elapsed += (s, e) => FireLoggedDurationChanged();
            _timer.Start();
        }

        private void FireLoggedDurationChanged() => 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LoggedDuration)));

        public event PropertyChangedEventHandler? PropertyChanged; // Evènement à déclencher dés qu'une propriété bindée est modifiée

        public string Name {
            get => field; 
            set
            {
                if (field != value)
                {
                    field = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
                }
            }
        } = Environment.UserName;
        public string? Login {
            get => field; 
            set {
                if (field != value)
                {
                    field = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Login)));
                }
            }
        } = null;
        public DateTime LoggedOn { 
            get => field;
            set
            {
                if (field != value)
                {
                    field = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LoggedOn)));
                    FireLoggedDurationChanged();
                }
            }
        } = DateTime.Now;
        public TimeSpan LoggedDuration => DateTime.Now - LoggedOn;

        public override string ToString() => 
            $"Name: {Name}, Login: {Login}, Logged On: {LoggedOn}, Logged Duration: {LoggedDuration}";
    }
}
