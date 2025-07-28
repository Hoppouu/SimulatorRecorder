using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Reflection.Emit;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Xml;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Text.RegularExpressions;


namespace SimulatorRecorder.Modules
{
    public class SimulatorController
    {
        int connectResult = 0;

        //json read 변경 2021.05.24 (완도VR)
        //JObject json_motion;        

        private readonly SimulatorDataProvider dataProvider;
        public SimulatorController(SimulatorDataProvider provider)
        {
            this.dataProvider = provider;
        }

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

        [DllImport("AvSimDllMotionExternC")]
        private static extern void MotionControl__DOF_and_Blower(int nRoll, int nPitch, int nYaw, int nSway, int nSurge, int nHeave, int nSpeed, int nBlower);

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

        private void VROA_MOBC_action(OutputValue? output)
        {
            if (output == null)
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

        public void setTRANSFORM()
        {
            try
            {
                XmlTextWriter writer1 = new XmlTextWriter("TRANSFORM.xml", Encoding.UTF8);
                writer1.Formatting = System.Xml.Formatting.Indented;
                writer1.WriteStartDocument();
                writer1.WriteStartElement("TRANSFORM");
                writer1.WriteStartElement("POSITION");

                writer1.WriteStartElement("X");
                writer1.WriteString("0");
                writer1.WriteEndElement();
                writer1.WriteStartElement("Y");
                writer1.WriteString("0");
                writer1.WriteEndElement();
                writer1.WriteStartElement("Z");
                writer1.WriteString("0");
                writer1.WriteEndElement();
                writer1.WriteEndElement();

                writer1.WriteStartElement("ROTATION");
                writer1.WriteStartElement("X");
                writer1.WriteString("0");
                writer1.WriteEndElement();
                writer1.WriteStartElement("Y");
                writer1.WriteString("0");
                writer1.WriteEndElement();
                writer1.WriteStartElement("Z");
                writer1.WriteString("0");
                writer1.WriteEndElement();
                writer1.WriteEndElement();

                writer1.WriteEndElement();
                writer1.Flush();
                writer1.Close();

                Console.Write("SET");
            }
            catch (Exception ee)
            {
                Console.Write(ee.Message);
            }
        }

        public void Connect()
        {            
            connectResult = MotionControl__Initial();

            Console.Write("(VR1) MotionControl__Initial [Init Success!!  Return: " + connectResult.ToString() + "]\n");

            VROA_MOBC_action(new OutputValue());
            //VROA_MOBC_action(10000, 10000, 15000, 20000, 10000, 10000, 30, 0);            
        }

        public void DisConnect()
        {
            VROA_MOBC_action(new OutputValue());

            int destroyResult = MotionControl__Destroy();

            connectResult = 0;
            Console.Write("MotionControl__Destroy [Destory Success!!  Return:: " + destroyResult.ToString() + "]\n");

        }

        private void ExecuteSimulation()
        {
            try
            {
                VROA_MOBC_action(dataProvider.outputValue);
            }
            catch (Exception err)
            {
                Console.Write("100 : " + err);
            }
        }

        bool isRunning = false;
        bool isSimulating = false;
        public void StartSimulation()
        {
            Console.Write("[SimulatorController] 시뮬레이터 시작!\n");
            if (isSimulating) return; // 중복 실행 방지

            isRunning = true;
            isSimulating = false;
            Console.Write("[SimulatorController] 시뮬레이터 시작!!!\n");

            Task.Run(async () =>
            {
                Console.Write(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>VR1 start\n");
                int destroyResult = MotionControl__Destroy();
                Console.Write("MotionControl__Destroy [Destory Success!!  Return:: " + destroyResult.ToString() + "]\n");

                setTRANSFORM();
                
                

                connectResult = MotionControl__Initial();
                Console.Write("(VR1) MotionControl__Initial [Init Success!!  Return: " + connectResult.ToString() + "]\n");
                dataProvider.SetInitMotion();
                ExecuteSimulation();

                await Task.Delay(3000); //여기 3초 딜레이 있음. 3초 이후에 모션 제어 시작

                //dataProvider.Start();

                while(isRunning)
                {
                    while (isSimulating)
                    {
                        ExecuteSimulation(); // ✅ 모션 적용 (VROA_MOBC_action 호출)
                        await Task.Delay(100); // ✅ 0.1초 단위 실행
                    }
                }

                setTRANSFORM();
                dataProvider.SetInitMotion();
                ExecuteSimulation();

                await Task.Delay(3000); //여기 3초 딜레이 있음. 3초 이후에 모션 제어 시작
            });
        }

        public void resumeSimulation()
        {
            isSimulating = true;
            Console.Write("[SimulatorController] 시뮬레이터 재개!\n");
        }
        public void StopSimulation()
        {
            isSimulating = false;
            Console.Write("[SimulatorController] 시뮬레이터 중지!\n");
        }

        public void EndSimulation()
        {
            isSimulating = false;
            isRunning = false;
            Console.Write("[SimulatorController] 시뮬레이터 최종 종료!\n");
        }

        public int GetSimulationStatus()
        {
            return connectResult;
        }

        public static int Clamp(int value, int min, int max)
        {
            return Math.Min(Math.Max(value, min), max);
        }
    }
}
