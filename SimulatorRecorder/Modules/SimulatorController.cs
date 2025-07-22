using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System.Runtime.InteropServices;

namespace SimulatorRecorder.Modules
{

    public class SimulatorController
    {

        public void Call_VROA_MOBC_action(OutputValue? output)
        {
            if (!ProgramManager.IsRunMOBC || output == null)
            {
                return;
            }

            int Roll = output.OutputData[MyConstant.ROLL];
            int Pitch = output.OutputData[MyConstant.PITCH];
            int Yaw = output.OutputData[MyConstant.YAW];
            int Sway = output.OutputData[MyConstant.SWAY];
            int Surge = output.OutputData[MyConstant.SURGE];
            int Heave = output.OutputData[MyConstant.HEAVE];
            int Speed = output.OutputData[MyConstant.SPEED];
            int Blower1 = output.OutputData[MyConstant.BLOWER1];
#if DEBUG
            Console.Write(
                "Timer : " + ((int)(Math.Round(ProgramManager.GetElapsedTime(), 1) * 1000)).ToString() + "[TIME, " + output.OutputData["TIME"] + "]" +
                "[ROLL, " + Roll + "] " +
                "[PITCH, " + Pitch + "] " +
                "[YAW, " + Yaw + "] " +
                "[SWAY, " + Sway + "] " +
                "[SURGE, " + Surge + "] " +
                "[HEAVE, " + Heave + "] " +
                "[SPEED, " + Speed + "] " +
                "[BLOWER1, " + Blower1 + "]"
                );
            Console.WriteLine();
#endif
            VROA_MOBC_action(Roll, Pitch, Yaw, Sway, Surge, Heave, Speed, Blower1);
        }

        [DllImport(@".\DLL\AvSimDllMotionExternC")]
        private static extern void MotionControl__DOF_and_Blower(int nRoll, int nPitch, int nYaw, int nSway, int nSurge, int nHeave, int nSpeed, int nBlower);


        //장비제어 (모션부분)
        private unsafe void VROA_MOBC_action(int Roll, int Pitch, int Yaw, int Sway, int Surge, int Heave, int Speed, int Blower)
        {

            try
            {
                int Min(string key) => MyConstant.outputRange[key + "_MIN"];
                int Max(string key) => MyConstant.outputRange[key + "_MAX"];

                int nRoll = Clamp(Roll, Min(MyConstant.ROLL), Max(MyConstant.ROLL));
                int nPitch = Clamp(Pitch, Min(MyConstant.PITCH), Max(MyConstant.PITCH));
                int nYaw = Clamp(Yaw, Min(MyConstant.YAW), Max(MyConstant.YAW));
                int nSway = Clamp(Sway, Min(MyConstant.SWAY), Max(MyConstant.SWAY));
                int nSurge = Clamp(Surge, Min(MyConstant.SURGE), Max(MyConstant.SURGE));
                int nHeave = Clamp(Heave, Min(MyConstant.HEAVE), Max(MyConstant.HEAVE));
                int nSpeed = Clamp(Speed, Min(MyConstant.SPEED), Max(MyConstant.SPEED));
                int nBlower = Clamp(Blower, Min(MyConstant.BLOWER1), Max(MyConstant.BLOWER1));

                MotionControl__DOF_and_Blower(nRoll, nPitch, nYaw, nSway, nSurge, nHeave, nSpeed, nBlower); //action value
            }
            catch (Exception ee)
            {
                Console.WriteLine("ERR 110 " + ee);
            }
        }

        private int Clamp(int value, int min, int max)
        {
            return Math.Min(Math.Max(value, min), max);
        }
    }
}