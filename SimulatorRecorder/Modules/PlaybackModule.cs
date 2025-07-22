using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace SimulatorRecorder.Modules
{
    public class PlaybackModule
    {
        private List<OutputValue> record;
        private SimulatorController simulatorController;
        private int curIdx;

        private readonly OutputValue InitOutput = new OutputValue();

        public OutputValue CurOutput { get; private set; } = new OutputValue();
        public bool IsPlayReady { get; private set; } = false;
        public bool IsEnd { get; private set; }
        public PlaybackModule(SimulatorController simulatorController)
        {
            record = new List<OutputValue>();
            this.simulatorController = simulatorController;
            IsEnd = false;
            curIdx = 1;
        }

        public void InitPlayBack(List<OutputValue> record)
        {
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

        public OutputValue GetCurOutput()
        {
            return CurOutput;
        }

        public double GetElapsedRate()
        {
            return 1.0 * (curIdx - 1) / (record.Count - 1) * 100;
        }
        private OutputValue? Next()
        {
            int curTime = (int)(Math.Round(ProgramManager.GetElapsedTime(), 1) * 1000);
            if (curIdx >= record.Count)
            {
                IsEnd = true;
                CurOutput = InitOutput;
                return InitOutput;
            }
            if (curTime < record[curIdx].OutputData[MyConstant.TIME])
            {
                return null;
            }
            CurOutput = record[curIdx++];
            return CurOutput;
        }   

        private void Reset()
        {
            curIdx = 1;
            IsEnd = false;
        }
    }
}
