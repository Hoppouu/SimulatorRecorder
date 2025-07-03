using Silk.NET.XInput;
using System.Reflection.Metadata;
namespace SimulatorRecorder.Modules
{
    internal class ControllerInputModule
    {
        private List<GamepadInput> buffer;
        private XInput xinput;
        private State state;
        private int curIndex;
        private uint controllerIndex = 0;
        public ControllerInputModule()
        {
            buffer = new List<GamepadInput>();
            xinput = XInput.GetApi();
            state = new State();
            curIndex = -1;
        }

        private bool IsConnected()
        {
            controllerIndex = xinput.GetState(controllerIndex, ref state);
            return controllerIndex == 0;
        }

        private State? GetState()
        {
            if (IsConnected())
            {
                return state;
            }
            else
            {
                return null;
            }
        }

        private float NormStick(int value)
        {
            if (value < 0)
            {
                return value / 32768f;
            }
            else
            {
                return value / 32767f;
            }
        }

        private float NormTrigger(int value)
        {
            return value / 255f;
        }

        public void GetButton()
        {
            if (GetState() == null)
            {
                return;
            }
            ++curIndex;
            List<GamepadInputValue> temp = new List<GamepadInputValue>();
            GamepadInput curState = new GamepadInput();
            ushort wButtons = state.Gamepad.WButtons;
            double time = TimerModule.GetElapsedTime();
            for (int i = 0; i < MyConstant.buttonsKey.Length; i++)
            {
                XInputButtons button = MyConstant.buttonsKey[i];
                bool isPressed = (wButtons & (ushort)button) != 0;

                GamepadInputValue input = new GamepadInputValue(
                    MyConstant.buttonsName[i],
                    isPressed ? 1f : 0f
                );
                if (isPressed)
                {
                    Console.WriteLine($"{button} 버튼 눌림");
                }
                temp.Add(input);
            }

            void AddAnalog(string name, float value)
            {
                temp.Add(new GamepadInputValue(name, value));
            }
            AddAnalog("LStickX", NormStick(state.Gamepad.SThumbLX));
            AddAnalog("LStickY", NormStick(state.Gamepad.SThumbLY));
            AddAnalog("RStickX", NormStick(state.Gamepad.SThumbRX));
            AddAnalog("RStickY", NormStick(state.Gamepad.SThumbRY));
            AddAnalog("LTrigger", NormTrigger(state.Gamepad.BLeftTrigger));
            AddAnalog("RTrigger", NormTrigger(state.Gamepad.BRightTrigger));
            curState.Set(time, temp);
            if (curState.values[16].buttonValue > 0)
            {
                Console.WriteLine($"LTrigger 버튼 눌림 {curState.values[16].buttonValue}");
            }
            if (curState.values[17].buttonValue > 0)
            {
                Console.WriteLine($"RTrigger 버튼 눌림 {curState.values[17].buttonValue}");
            }
            buffer.Add(curState);
        }
        public List<GamepadInput> GetBuffer()
        {
            return buffer;
        }
        public GamepadInput GetCurState()
        {
            return buffer[curIndex];
        }

    }
}
