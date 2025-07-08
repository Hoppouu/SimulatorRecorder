using System.Collections.Specialized;

namespace SimulatorRecorder.Modules
{
    internal class OutputModule
    {
        private OutputValue outputValue;

        public OutputModule()
        {
            outputValue = new OutputValue();

        }

        private int MappingValue(float buttonValue, string buttonMapping)
        {
            int min = MyConstant.outputRange[buttonMapping + "_MIN"];
            int max = MyConstant.outputRange[buttonMapping + "_MAX"];
            return (int)(((buttonValue + 1) / 2) * (max - min) + min); ;
        }
        private bool IsDeadZone(GamepadInputValue input)
        {
            bool isExistList = MyConstant.deadZoneList[input.buttonName];

            bool isDeadZone = Math.Abs(input.buttonValue) < MyConstant.deadZone;
            return isExistList && isDeadZone;
        }
        private void InitOutput(string buttonName)
        {
            string buttonMapping = MyConstant.keyMapping[buttonName];
            int mappingInitValue = MyConstant.outputsInitDict[buttonMapping];
            outputValue.SetValue(buttonMapping, mappingInitValue);
        }
        public void SetOutput(double time, List<GamepadInputValue> values)
        {
            outputValue.SetTime(time);
            for (int i = 0; i < values.Count; i++)
            {
                if (!MyConstant.keyMapping.ContainsKey(values[i].buttonName))
                {
                    continue;
                }
                if(!MyConstant.keyOffest.ContainsKey(values[i].buttonName))
                {
                    continue;
                }
                if (IsDeadZone(values[i]))
                {
                    InitOutput(values[i].buttonName);
                    continue;
                }

                if (values[i].buttonName == "B")
                {
                    if (values[i].buttonPressDown)
                    {
                        outputValue.SetInitOutputData();
                    }
                    continue;
                }

                string buttonMapping = MyConstant.keyMapping[values[i].buttonName];
                float buttonOffset = MyConstant.keyOffest[values[i].buttonName];
                if (values[i].buttonName == "A")
                {
                    if(values[i].buttonPressDown)
                    {
                        outputValue.ToggleValue(buttonMapping, buttonOffset);
                    }
                }
                else if (values[i].buttonName == "LTrigger" || values[i].buttonName == "RTrigger")
                {
                    outputValue.AddValue(buttonMapping, values[i].buttonValue * buttonOffset);
                }
                else
                {
                    outputValue.SetValue(buttonMapping, MappingValue(values[i].buttonValue, buttonMapping));
                }
            }
        }
        public int GetOutput (string key)
        {
            return outputValue.GetOutputDictionary()[key];
        }
        public double GetOutputTime()
        {
            return outputValue.GetTime();
        }
        public OutputValue GetOutputValue()
        {
            return outputValue.GetClone();
        }
    }
}
