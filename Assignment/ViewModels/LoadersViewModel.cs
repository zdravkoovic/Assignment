using Assignment.Commands;
using Assignment.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Assignment.ViewModels
{
    public class LoadersViewModel : ViewModelBase
    {
        private readonly Timer _timer;
        public ObservableCollection<ThreadWorker> Workers { get; }

        public double TotalProgress
        {
            get
            {
                var activeWorkers = Workers.Where(w => w.IsActive).ToList();

                if (!activeWorkers.Any())
                    return 0.0;

                return activeWorkers.Average(w => w.Progress);
            }
        }

        public LoadersViewModel()
        {
            Workers = new ObservableCollection<ThreadWorker>
            {
                new ThreadWorker(),
                new ThreadWorker(),
                new ThreadWorker()
            };

            _timer = new Timer(1000);
            _timer.Elapsed += OnTimerElapsed;
            _timer.AutoReset = true;
            _timer.Start();
        }

        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            foreach(var worker in Workers.Where(w => w.IsActive))
            {
                if(worker.Elapsed < worker.Duration)
                {
                    worker.Elapsed++;
                }
            }

            OnPropertyChanged(nameof(TotalProgress));
        }
    }
}
