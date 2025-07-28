using System.Diagnostics.Contracts;
using Timer = System.Windows.Forms.Timer;

namespace SimulatorRecorder.Modules
{
    internal static class ProgramManager
    {
        private static DateTime startTime;
        private static TimeSpan elapsedTime;
        private static SimulatorController simulatorController = null!;
        private static Timer timer = null!;
        public static bool IsRunTimer { get; private set; }
        public static void Initialize(Timer timer, int interval, SimulatorController simulatorController)
        {
            IsRunTimer = false;
            if (ProgramManager.timer == null)
            {
                ProgramManager.timer = timer;
                ProgramManager.timer.Interval = interval;
            }
            if(ProgramManager.simulatorController == null)
            {
                ProgramManager.simulatorController = simulatorController;
            }
        }

        public static void Initialize(int interval)
        {
            if (ProgramManager.timer != null)
            {
                ProgramManager.timer.Interval = interval;
            }
        }
        public static void DoInitStart()
        {
            IsRunTimer = false;
            timer.Enabled = true;
        }
        public static void DoSemiStart()
        {
            IsRunTimer = false;
            timer.Enabled = true;
            simulatorController.resumeSimulation();
        }

        public static void DoSemiEnd()
        {
            timer.Enabled = false;
            simulatorController.StopSimulation();
        }

        public static void DoStart()
        {
            timer.Enabled = true;
            IsRunTimer = true;
            startTime = DateTime.Now;
            simulatorController.resumeSimulation();
        }

        public static void ResetTimer()
        {
            startTime = DateTime.Now;
        }

        public static void DoEnd()
        {
            timer.Enabled = false;
            IsRunTimer = false;
            simulatorController.StopSimulation();
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
