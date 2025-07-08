using Silk.NET.XInput;
namespace SimulatorRecorder.Modules
{
    internal class ControllerInputModule
    {
        private OutputModule outputModule;
        private List<OutputValue> buffer;
        private List<GamepadInput> bufferGameInput;
        private XInput xinput;
        private State state;
        private int curIndex;
        public ControllerInputModule()
        {
            outputModule = new OutputModule();
            buffer = new List<OutputValue>();
            bufferGameInput = new List<GamepadInput>();
            xinput = XInput.GetApi();
            state = new State();
            curIndex = -1;
        }

        public bool IsConnected()
        {
            uint controllerIndex = 0;
            for (uint i = 0; i < 4; i++)
            {
                controllerIndex = i;
                uint result = xinput.GetState(controllerIndex, ref state);

                if (result == 0)
                {
                    //Console.WriteLine($"컨트롤러 {controllerIndex} 연결됨");
                    return true;
                }
            }

            return false;
        }

        private State? GetCurState()
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

        private float IsPressDown(int i, GamepadInputValue input)
        {
            if(GetPreState() == null)
            {
                return 0f;
            }

            if (GetPreState().values[i].buttonValue <= 0.1f && input.buttonValue >= 0.9f)
            {
                if (!input.buttonPressDown)
                {
                    input.buttonPressDown = true;
                    return 1.0f;
                }
                else
                {
                    return 1.0f;
                }
            }

            if (input.buttonValue <= 0.1f)
            {
                input.buttonPressDown = false;
            }
            return 0f;
        }

        public void GetButton()
        {
            if (GetCurState() == null)
            {
                return;
            }
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
                    isPressed ? 1.0f : 0.0f
                );

                if (IsPressDown(i, input) != 0)
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

            outputModule.SetOutput(curState.time, curState.values);
            ++curIndex;
            buffer.Add(outputModule.GetOutputValue());
            bufferGameInput.Add(curState);
        }
        public List<OutputValue> GetBuffer()
        {
            return buffer;
        }
        private GamepadInput GetPreState()
        {
            if(curIndex < 0)
            {
                return null!;
            }

            return bufferGameInput[curIndex];
        }

        public string GetOutput(string key)
        {
            return outputModule.GetOutput(key).ToString();
        }

        public double GetOutputTime()
        {
            return outputModule.GetOutputTime();
        }

        public bool IsStartTimer()
        {
            return GetPreState().values[10].buttonPressDown;
        }
    }
}
