using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace SimulatorRecorder.Modules
{
    public class PlaybackModule
    {
        private List<OutputValue> record;
        private SimulatorController simulatorController;

        private readonly OutputValue InitOutput = new OutputValue();
        private int idx;

        public bool IsPlayReady { get; private set; } = false;
        public bool IsEnd { get; private set; }
        public PlaybackModule()
        {
            record = new List<OutputValue>();
            simulatorController = new SimulatorController();
            IsEnd = false;
            idx = 1;
        }

        public void InitPlayBack(List<OutputValue> record)
        {
            Console.WriteLine(record.Count);
            if (record.Count == 0)
            {
                IsPlayReady = false;
                return;
            }

            SetRecord(record);
            IsPlayReady = true;
        }
        public bool Playback()
        {
            if (IsEnd)
            {
                //IsPlayReady = false;
                PlayReset();
            }
            simulatorController.Call_VROA_MOBC_action(Next());

            return IsEnd;
        }

        public void PlayReset()
        {
            Reset();
        }

        public void SetRecord(List<OutputValue> list)
        {
            record = list;
        }


        public OutputValue? Next()
        {
            int curTime = (int)(Math.Round(ProgramManager.GetElapsedTime(), 1) * 1000);
            if (idx >= record.Count)
            {
                IsEnd = true;
                return InitOutput;
            }
            if (curTime < record[idx].OutputData[MyConstant.TIME])
            {
                return null;
            }

            return record[idx++];
        }

        public void Reset()
        {
            idx = 1;
            IsEnd = false;
        }
    }
}
