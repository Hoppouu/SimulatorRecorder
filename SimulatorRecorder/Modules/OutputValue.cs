using System.Data;

namespace SimulatorRecorder.Modules
{
    internal class OutputValue
    {
        private Dictionary<string, int> outputData;
        public OutputValue()
        {
            outputData = new Dictionary<string, int>();
            for (int i = 0; i < MyConstant.outputsName.Length; i++)
            {
                outputData.Add(MyConstant.outputsName[i], MyConstant.outputsInit[i]);
            }
        }
        public bool SetValue(string key, float value)
        {
            if (!outputData.ContainsKey(key))
            {
                return false;
            }

            int min = MyConstant.outputRange[key + "_MIN"];
            int max = MyConstant.outputRange[key + "_MAX"];

            if (value < min)
            {
                outputData[key] = min;
            }
            else if (value > max)
            {
                outputData[key] = max;
            }
            else
            {
                outputData[key] = (int)value;
            }

            return true;
        }

        public bool AddValue(string key, float value)
        {
            return SetValue(key, outputData[key] + value);
        }

        public bool ToggleValue(string key, float value)
        {
            if (!outputData.ContainsKey(key))
            {
                return false;
            }

            if (outputData[key] == 0)
            {
                return SetValue(key, value);
            }
            else
            {
                return SetValue(key, 0);
            }
        }

        public void SetTime(double time)
        {
            outputData["TIME"] = (int)(time * 10) * 100;
        }
        public int GetTime()
        {
            return outputData["TIME"];
        }
        public Dictionary<string, int> GetOutputDictionary()
        {
            return outputData;
        }

        public OutputValue GetClone()
        {
            OutputValue clone = new OutputValue();
            clone.outputData = new Dictionary<string, int>(this.outputData);
            return clone;
        }
    }
}
