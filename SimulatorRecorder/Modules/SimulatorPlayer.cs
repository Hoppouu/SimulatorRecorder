using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimulatorRecorder.Modules
{
    internal class SimulatorPlayer
    {
        string attactionJsonPath = "";
        bool isPlaying = false;

        List<OutputValue> data = new List<OutputValue>();
        SimulatorDataProvider provider;

        long startTime = 0;
        int vidTime = 0;

        //millisecond
        int delay = 0;

        public SimulatorPlayer(string attactionJsonPath, int delay, SimulatorDataProvider provider)
        {            
            this.provider = provider;
            this.attactionJsonPath = attactionJsonPath;
            this.delay = delay;
            data = CSVReader.Read(this.attactionJsonPath);
        }

        public void Start()
        {
            if(isPlaying) return;

            isPlaying = true;
            startTime = DateTime.UtcNow.Ticks + (delay * 10000);
        }

        public void Stop()
        {
            isPlaying = false;
        }

        public bool UpdateMotionData()
        {
            long elapsedTime = (DateTime.UtcNow.Ticks - startTime) / TimeSpan.TicksPerMillisecond; // ✅ 경과 시간 (ms)
            vidTime = (int)(elapsedTime / 100); // csv 파일의 시간 단위가 0.01초이므로 100으로 나누어줌
                        
            vidTime = Math.Max(0, vidTime);

            //LogManager.Log("simul t : " + vidTime.ToString());

            if (vidTime >= data.Count)
            {
                Console.Write("[SimulatorPlayer] 영상 재생 종료");
                Stop();
                isPlaying = false;
                return false;
            }

            OutputValue temp = data[vidTime];
            provider.SetMotion(temp);

            return true;
        }

    }
}
