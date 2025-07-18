using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;

namespace SimulatorRecorder.Modules
{
    public class OutputValue
    {
        private Dictionary<string, int> outputData;

        private ReadOnlyDictionary<string, int> readOnlyOutputData;

        public ReadOnlyDictionary<string, int> OutputData { get => readOnlyOutputData; }

        public OutputValue()
        {
            outputData = new Dictionary<string, int>();
            for (int i = 0; i < MyConstant.outputsName.Length; i++)
            {
                outputData.Add(MyConstant.outputsName[i], MyConstant.outputsInit[i]);
            }
            readOnlyOutputData = new ReadOnlyDictionary<string, int>(outputData);
        }

        public OutputValue(OutputValue other) : this()
        {
            for (int i = 0; i < MyConstant.outputsName.Length; i++)
            {
                outputData[MyConstant.outputsName[i]] = other.outputData[MyConstant.outputsName[i]];
            }
        }

        public void Set(string key, int value)
        {
            outputData[key] = value;
        }
        
        public void ToAttractionJson()
        {

        }
    }
}
