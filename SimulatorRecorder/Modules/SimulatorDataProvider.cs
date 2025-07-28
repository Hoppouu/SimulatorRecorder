using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using SimulatorRecorder.Modules;

namespace SimulatorRecorder.Modules
{
    public class SimulatorDataProvider
    {
        //private ESimulateDataMode mode;

        public OutputValue outputValue;

        public SimulatorDataProvider()
        {
            outputValue = new OutputValue();
        }

        public void SetMotion(int roll, int pitch, int yaw, int sway, int surge, int heave, int speed, int blower)
        {
            outputValue.Set(MyConstant.ROLL, roll);
            outputValue.Set(MyConstant.PITCH, pitch);
            outputValue.Set(MyConstant.YAW, yaw);
            outputValue.Set(MyConstant.SWAY, sway);
            outputValue.Set(MyConstant.SURGE, surge);
            outputValue.Set(MyConstant.HEAVE, heave);
            outputValue.Set(MyConstant.SPEED, speed);
            outputValue.Set(MyConstant.BLOWER1, blower);
        }

        public void SetMotion(OutputValue? output)
        {
            if(output == null)
            {
                return;
            }
            outputValue = output;
        }

        public void SetInitMotion()
        {
            outputValue = new OutputValue();
        }
    }
}
