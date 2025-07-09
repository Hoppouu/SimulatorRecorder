using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace SimulatorRecorder.Modules
{
    static class ProgramModule
    {
        static private Process process = null!;

        static public void Launch(string[] args)
        {
            try
            {
                process = new Process();
                process.StartInfo.FileName = MyConstant.GetwindowsPlayerPath();

                process.StartInfo.Arguments = string.Join(" ", args);
                process.Start();
            }
            catch (Win32Exception)
            {
                string str = "잘못된 WindowsPlayer 경로입니다.\n좌상단의 옵션 메뉴에서 WindowsPlayer 경로를 설정해주세요";
                MessageBox.Show(str, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("예기치 않은 오류 발생:\n" + ex.Message, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        static public void Close()
        {
            if (process == null)
            {
                return;
            }

            bool hasExited = false;
            try
            {
                hasExited = process.HasExited;
            }
            catch (InvalidOperationException)
            {
                // 프로세스가 연결되어 있지 않음
                return;
            }

            if (!hasExited)
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
