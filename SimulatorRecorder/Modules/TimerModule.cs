using Timer = System.Windows.Forms.Timer;

namespace SimulatorRecorder.Modules
{
    internal static class TimerModule
    {
        private static DateTime startTime;
        private static TimeSpan elapsedTime;
        private static Timer timer = null!;
        private static bool isRun;
        public static void Initialize(Timer timer, int interval)
        {
            if (TimerModule.timer == null)
            {
                TimerModule.timer = timer;
                TimerModule.timer.Interval = interval;
            }
        }
        private static void StartTimer()
        {
            isRun = true;
            startTime = DateTime.Now;
        }

        private static void EndTimer()
        {
            isRun = false;
        }

        public static void DoStartTimer()
        {
            timer.Enabled = true;
            StartTimer();
        }

        public static void DoEndTimer()
        {
            timer.Enabled = false;
            EndTimer();
        }

        public static double GetElapsedTime()
        {
            if (isRun)
            {
                elapsedTime = DateTime.Now - startTime;
                return elapsedTime.TotalSeconds;
            }
            else
            {
                return -1;
            }
        }

        public static bool IsRun()
        {
            return isRun;
        }
    }
}
