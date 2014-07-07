using System;
using System.Windows.Threading;

namespace CountdownScreensaver
{
    /// <summary>
    /// Counts down seconds from given value to 0
    /// </summary>
    public class Counter 
    {
        private readonly DispatcherTimer _timer = new DispatcherTimer(DispatcherPriority.Normal);
        public int RemainingTime { get; private set; }
        public Counter(int remainingTime) 
        {
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += UpdateTimer;

            RemainingTime = remainingTime;
        }

        private void UpdateTimer(object sender, EventArgs eventArgs)
        {
            if (--RemainingTime > 0) return;
            RemainingTime = 0;
            _timer.Stop();
        }

        public event EventHandler Tick
        {
            add { _timer.Tick += value; }
            remove { _timer.Tick -= value; }
        }

        public void Start()
        {
            _timer.Start();
        }
    }
}
