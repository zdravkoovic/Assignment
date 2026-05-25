using Assignment.Commands;
using Assignment.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Controls;
using System.Windows.Input;

namespace Assignment.ViewModels
{
    public class LoadersViewModel : ViewModelBase
    {
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

        public ICommand CancelCommand { get; }

        public LoadersViewModel()
        {
            Workers = new ObservableCollection<ThreadWorker> { new ThreadWorker(), new ThreadWorker(), new ThreadWorker() };

            foreach (var w in Workers)
            {
                RunWorker(w); 
            }

            CancelCommand = new RelayCommand(CancelWorker, CanCancel);
        }

        private void RunWorker(ThreadWorker worker)
        {
            var thread = new Thread(() =>
            {
                while (worker.Elapsed < worker.Duration)
                {
                    worker.Cts.Token.WaitHandle.WaitOne(1000);
                    if (worker.Cts.Token.IsCancellationRequested) break;
                    worker.Elapsed++;
                    OnPropertyChanged(nameof(TotalProgress));

                }
                OnPropertyChanged(nameof(TotalProgress));
            });

            thread.IsBackground = true;
            thread.Start();
        }

        private void CancelWorker(object obj)
        {
            var worker = obj as ThreadWorker;
            worker?.Cancel();
        }

        private bool CanCancel(object obj)
        {
            var worker = obj as ThreadWorker;
            if(worker == null)
                return false;
            return worker.IsActive && worker.Progress < 100.0;
        }
    }
}
