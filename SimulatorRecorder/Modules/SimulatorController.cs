using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System.Runtime.InteropServices;

namespace SimulatorRecorder.Modules
{

    public class SimulatorController
    {
        private uint getRoll = 0;
        private uint getPitch = 0;
        private uint getYaw = 0;
        private uint getSway = 0;
        private uint getSurge = 0;
        private uint getHeave = 0;
        private uint getSpeed = 30;
        private uint getBlower = 0;

        private int connectResult = 0;

        [DllImport("AvSimDllMotionExternC")]
        private static extern int MotionControl__Initial(); //통신초기화(시작)

        //DOF(자유도)
        //Roll(left/right rotate), Yaw(좌우 움직임), pitch (Front/Rear), heave(up/down), surge(forward/backward)

        [DllImport("AvSimDllMotionExternC")]
        private static extern int MotionControl__Destroy(); //통신해제

        //이게 처음에 만들어진 dll에 있는 함수171218 | 180121
        [DllImport("AvSimDllMotionExternC")]
        private unsafe static extern void MotionControl__DOF_and_Blower_and_Circle_and_DO_and_DI_and_Axis(IntPtr pnRoll, IntPtr pnPitch, IntPtr pnYaw, IntPtr pnSway, IntPtr pnSurge, IntPtr pnHeave, IntPtr pnSpeed, IntPtr pnBlower, IntPtr pnCircle, IntPtr pnCircleSpeed, IntPtr pnDO, IntPtr pnDI, uint[] arrSrcPos, uint[] arrDstPos, uint[] arrEcdPos, bool bResp = true);

        //이게 나중에 만들어진 dll에 있는 함수191113
        [DllImport("AvSimDllMotionExternC")]
        private unsafe static extern void MotionControlV2__DOF_and_Blower_and_Circling_and_DO_and_DI_and_Axis(IntPtr pnRoll, IntPtr pnPitch, IntPtr pnYaw, IntPtr pnSway, IntPtr pnSurge, IntPtr pnHeave, IntPtr pnSpeed, IntPtr pnBlower, IntPtr pnCircle, IntPtr pnCircleSpeed, IntPtr pnDO, IntPtr pnDI, uint[] arrSrcPos, uint[] arrDstPos, uint[] arrEcdPos, bool bResp = true);

        [DllImport("AvSimDllMotionExternC")]
        private unsafe static extern void MotionControl__DOF_and_Blower_and_DO_and_DI_Data_Obtain(IntPtr pnRoll, IntPtr pnPitch, IntPtr pnYaw, IntPtr pnSway, IntPtr pnSurge, IntPtr pnHeave, IntPtr pnSpeed, IntPtr pnBlower, IntPtr pnDO, IntPtr pnDI);


        public SimulatorController()
        {
            //dll_mode = Setting.DLL;

            //EventDispatcher.Register(EventDispatcherKey.SIMUL_CONNECT, Connect);
            //EventDispatcher.Register(EventDispatcherKey.SIMUL_DISCONNECT, DisConnect);
            //EventDispatcher.Register(EventDispatcherKey.SIMUL_MOUNT, MountDismountMotio);
            //EventDispatcher.Register(EventDispatcherKey.SIMUL_CENTER, CenterMotion);
            //EventDispatcher.Register<String>(EventDispatcherKey.SIMUL_CUSTOM, CustomMotion);

            //EventDispatcher.Register(EventDispatcherKey.SIMUL_BELTOFF, SetOffSeatBelt);
            //EventDispatcher.Register(EventDispatcherKey.SIMUL_BELTON, SetOnSeatBelt);
        }


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

        [DllImport("AvSimDllMotionExternC")]
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

        public void Connect()
        {
            //            connectResult = MotionControl__Initial();

            //            //180121 에서는 0이 성공 1이 실패...
            //            if (dll_mode == Constants.DLL_MODE_180121)
            //            {
            //                connectResult = connectResult == 0 ? 1 : 0;
            //            }
            //            LogManager.Log("(VR1) MotionControl__Initial [Init Success!!  Return: " + connectResult.ToString() + "]");
            //            getRoll = (uint)MyConstant.outputsInitDict[MyConstant.ROLL];
            //            getPitch = (uint)MyConstant.outputsInitDict[MyConstant.PITCH];
            //            getYaw = (uint)MyConstant.outputsInitDict[MyConstant.YAW];
            //            getSway = (uint)MyConstant.outputsInitDict[MyConstant.SWAY];
            //            getSurge = (uint)MyConstant.outputsInitDict[MyConstant.SURGE];
            //            getHeave = (uint)MyConstant.outputsInitDict[MyConstant.HEAVE];
            //            getSpeed = (uint)MyConstant.outputsInitDict[MyConstant.SPEED];
            //            getBlower = (uint)MyConstant.outputsInitDict[MyConstant.BLOWER1];

            //            VROA_MOBC_action(
            //                    (int)getRoll,
            //                    (int)getPitch,
            //                    (int)getYaw,
            //                    (int)getSway,
            //                    (int)getSurge,
            //                    (int)getHeave,
            //                    (int)getSpeed,
            //                    (int)getBlower
            //                );
            //            //VROA_MOBC_action(10000, 10000, 15000, 20000, 10000, 10000, 30, 0);
        }

        public void DisConnect()
        {
            //    getRoll = (uint)MyConstant.outputsInitDict[MyConstant.ROLL];
            //    getPitch = (uint)MyConstant.outputsInitDict[MyConstant.PITCH];
            //    getYaw = (uint)MyConstant.outputsInitDict[MyConstant.YAW];
            //    getSway = (uint)MyConstant.outputsInitDict[MyConstant.SWAY];
            //    getSurge = (uint)MyConstant.outputsInitDict[MyConstant.SURGE];
            //    getHeave = (uint)MyConstant.outputsInitDict[MyConstant.HEAVE];
            //    getSpeed = (uint)MyConstant.outputsInitDict[MyConstant.SPEED];
            //    getBlower = (uint)MyConstant.outputsInitDict[MyConstant.BLOWER1];

            //    VROA_MOBC_action(
            //            (int)getRoll,
            //            (int)getPitch,
            //            (int)getYaw,
            //            (int)getSway,
            //            (int)getSurge,
            //            (int)getHeave,
            //            (int)getSpeed,
            //            (int)getBlower
            //        );

            //    int destroyResult = MotionControl__Destroy();

            //    //180121 에서는 0이 성공 1이 실패...
            //    if (dll_mode == Constants.DLL_MODE_180121)
            //    {
            //        destroyResult = destroyResult == 0 ? 1 : 0;
            //    }

            //    connectResult = 0;
            //    LogManager.Log("MotionControl__Destroy [Destory Success!!  Return:: " + destroyResult.ToString() + "]");
        }
    }

}