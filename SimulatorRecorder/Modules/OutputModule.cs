using System.Collections.ObjectModel;
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

        public void SetOutputValue(double time, List<GamepadInputValue> values)
        {
            outputValue.Set(MyConstant.TIME, (int)(time * 10) * 100);

            for (int i = 0; i < values.Count; i++)
            {
                if (!MyConstant.keyMapping.ContainsKey(values[i].buttonName))
                {
                    continue;
                }
                if (!MyConstant.keyOffest.ContainsKey(values[i].buttonName))
                {
                    continue;
                }
                if (IsDeadZone(values[i]))
                {
                    InitValue(values[i].buttonName);
                    continue;
                }

                if (values[i].buttonName == MyConstant.B)
                {
                    if (values[i].buttonPressDown)
                    {
                        InitValues();
                    }
                    continue;
                }

                string buttonMapping = MyConstant.keyMapping[values[i].buttonName];
                float buttonOffset = MyConstant.keyOffest[values[i].buttonName];
                if (values[i].buttonName == MyConstant.A)
                {
                    if (values[i].buttonPressDown)
                    {
                        ToggleValue(buttonMapping, buttonOffset);
                    }
                }
                else if (values[i].buttonName == MyConstant.LTrigger || values[i].buttonName == MyConstant.RTrigger)
                {
                    AddValue(buttonMapping, values[i].buttonValue * buttonOffset);
                }
                else
                {
                    SetValue(buttonMapping, MappingValue(values[i].buttonValue, buttonMapping));
                }
            }
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

            bool isDeadZone = Math.Abs(input.buttonValue) < MyConstant.GetDeadZone();
            return isExistList && isDeadZone;
        }

        private void InitValue(string buttonName)
        {
            string buttonMapping = MyConstant.keyMapping[buttonName];
            int mappingInitValue = MyConstant.outputsInitDict[buttonMapping];
            outputValue.Set(buttonMapping, mappingInitValue);
        }

        public int GetValue(string key)
        {
            return outputValue.OutputData[key];
        }

        private void InitValues()
        {
            for (int i = 1; i < MyConstant.outputsName.Length; i++)
            {
                outputValue.Set(MyConstant.outputsName[i], MyConstant.outputsInit[i]);
            }
        }

        private bool SetValue(string key, float value)
        {
            if (!outputValue.OutputData.ContainsKey(key))
            {
                return false;
            }

            int min = MyConstant.outputRange[key + "_MIN"];
            int max = MyConstant.outputRange[key + "_MAX"];

            if (value < min)
            {
                outputValue.Set(key, min);
            }
            else if (value > max)
            {
                outputValue.Set(key, max);
            }
            else
            {
                outputValue.Set(key, (int)value);
            }

            return true;
        }

        private bool AddValue(string key, float value)
        {
            return SetValue(key, outputValue.OutputData[key] + value);
        }

        private bool ToggleValue(string key, float value)
        {
            if (!outputValue.OutputData.ContainsKey(key))
            {
                return false;
            }

            if (outputValue.OutputData[key] == 0)
            {
                return SetValue(key, value);
            }
            else
            {
                return SetValue(key, 0);
            }
        }

        public OutputValue GetOutputValue()
        {
            return new OutputValue(this.outputValue);
        }
    }
}
