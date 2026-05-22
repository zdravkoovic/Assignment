using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Assignment.Models
{
    public class ThreadWorker
    {
        private static readonly Random _random = new Random();

        public int Duration { get; set; }
        public int Elapsed { get; set; }

        public bool IsActive { get; set; }

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
        }
    }
}
