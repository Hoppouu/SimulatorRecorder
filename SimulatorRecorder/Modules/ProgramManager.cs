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
        public static void DoSemiStart()
        {
            IsRunTimer = false;
            timer.Enabled = true;
        }

        public static void DoSemiEnd()
        {
            timer.Enabled = false;
        }

        public static void DoStart()
        {
            timer.Enabled = true;
            IsRunTimer = true;
            startTime = DateTime.Now;
        }

        public static void DoEnd()
        {
            timer.Enabled = false;
            IsRunTimer = false;
        }

        public static void DoStartMOBC()
        {
            IsRunMOBC = true;
        }
        public static void EndStartMOBC()
        {
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
