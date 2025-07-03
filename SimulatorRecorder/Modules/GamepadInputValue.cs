using Silk.NET.XInput;
using System.Diagnostics.Tracing;

namespace SimulatorRecorder.Modules
{
    internal class GamepadInputValue
    {
        public string buttonName;
        public float buttonValue;

        public GamepadInputValue()
        {
            buttonName = "None";
            buttonValue = 0f;
        }
        public GamepadInputValue(string buttonName, float buttonValue)
        {
            this.buttonName = buttonName;
            this.buttonValue = buttonValue;
        }
    }
}
