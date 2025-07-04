using Silk.NET.XInput;
using System.Diagnostics.Tracing;

namespace SimulatorRecorder.Modules
{
    internal class GamepadInputValue
    {
        public string buttonName;
        public float buttonValue;
        public bool buttonPressDown;

        public GamepadInputValue()
        {
            buttonName = "None";
            buttonValue = 0f;
            buttonPressDown = false;
        }
        public GamepadInputValue(string buttonName, float buttonValue, bool buttonPressDown = false)
        {
            this.buttonName = buttonName;
            this.buttonValue = buttonValue;
            this.buttonPressDown = buttonPressDown;
        }
    }
}
