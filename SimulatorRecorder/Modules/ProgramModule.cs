using System.Diagnostics;

namespace SimulatorRecorder.Modules
{
    static class ProgramModule
    {
        static private Process? process;

        static public void Launch(string[] args)
        {
            process = new Process();
            process.StartInfo.FileName = MyConstant.unityDirectoryPath;
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
    }
}
