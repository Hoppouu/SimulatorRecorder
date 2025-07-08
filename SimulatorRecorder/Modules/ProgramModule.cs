using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace SimulatorRecorder.Modules
{
    static class ProgramModule
    {
        static private Process process = null!;
        static private string WindowsPlayerPath = MyConstant.baseWindowsPlayerPath;

        static public void Launch(string[] args)
        {
            process = new Process();
            process.StartInfo.FileName = WindowsPlayerPath;
            process.StartInfo.Arguments = string.Join(" ", args);
            process.Start();
        }

        static public void Close()
        {
            if (process != null && !process.HasExited)
            {
                process.CloseMainWindow();
                if (!process.WaitForExit(3000))
                {
                    process.Kill();
                    process.WaitForExit();
                }
            }
        }
        static public void SetFilePath(string path)
        {
            WindowsPlayerPath = path;
        }
    }
}
