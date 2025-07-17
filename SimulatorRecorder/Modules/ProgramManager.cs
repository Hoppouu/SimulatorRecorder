using System.Diagnostics.Contracts;
using Timer = System.Windows.Forms.Timer;

namespace SimulatorRecorder.Modules
{
    internal static class ProgramManager
    {
        private static DateTime startTime;
        private static TimeSpan elapsedTime;
        private static Timer timer = null!;
        public static bool IsRunTimer { get; private set; }
        public static bool IsRunMOBC { get; private set; }
        public static void Initialize(Timer timer, int interval)
        {
            IsRunTimer = false;
            IsRunMOBC = false;
            if (ProgramManager.timer == null)
            {
                ProgramManager.timer = timer;
                ProgramManager.timer.Interval = interval;
            }
        }

        public static void Initialize(int interval)
        {
            if (ProgramManager.timer != null)
            {
                ProgramManager.timer.Interval = interval;
            }
        }
        public static void DoSemiStart()
        {
            IsRunTimer = false;
            IsRunMOBC = true;
            timer.Enabled = true;
        }

        public static void DoSemiEnd()
        {
            timer.Enabled = false;
            IsRunMOBC = false;
        }

        public static void DoStart()
        {
            timer.Enabled = true;
            IsRunTimer = true;
            IsRunMOBC = true;
            startTime = DateTime.Now;
        }

        public static void ResetTimer()
        {
            startTime = DateTime.Now;
        }

        public static void DoEnd()
        {
            timer.Enabled = false;
            IsRunTimer = false;
            IsRunMOBC = false;
        }

        public static double GetElapsedTime()
        {
            if (IsRunTimer)
            {
                elapsedTime = DateTime.Now - startTime;
                return elapsedTime.TotalSeconds;
            }
            else
            {
                return 0;
            }
        }

    }
}
