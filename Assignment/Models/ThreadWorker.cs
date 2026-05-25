using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Input;

namespace Assignment.Models
{
    public class ThreadWorker : INotifyPropertyChanged
    {
        private static readonly Random _random = new Random();
        private double _elapsed;
        private bool _isActive;

        public CancellationTokenSource Cts { get; } = new CancellationTokenSource();

        public int Duration { get; set; }
        public double Elapsed
        {
            get => _elapsed;
            set
            {
                _elapsed = value;
                OnPropertyChanged(nameof(Elapsed));
                OnPropertyChanged(nameof(Progress));
                Application.Current?.Dispatcher.Invoke(CommandManager.InvalidateRequerySuggested);
            }
        }

        public bool IsActive
        {
            get => _isActive;
            set
            {
                _isActive = value;
                OnPropertyChanged(nameof(IsActive));
            }
        }

        public double Progress =>
            Duration == 0 ? 0 : Math.Min(100.0 * Elapsed / Duration, 100);

        public ThreadWorker()
        {
            Duration = _random.Next(10, 51);
            IsActive = true;
        }

        public void Cancel()
        {
            IsActive = false;
            Cts.Cancel();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
