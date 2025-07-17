using Silk.NET.XInput;
namespace SimulatorRecorder.Modules
{
    public partial class ControllerInputModule
    {
        private OutputModule outputModule;
        private SimulatorController simulatorController;
        private List<OutputValue> buffer;
        private List<GamepadInput> bufferGameInput;
        PlaybackModule playbackModule;
        private XInput xinput;
        private State state;
        private int curIndex;

        public bool IsPlayReady { get; private set; } = false;
        public ControllerInputModule()
        {
            outputModule = new OutputModule();
            simulatorController = new SimulatorController();
            playbackModule = new PlaybackModule();
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

        private float SetPressDown(int i, GamepadInputValue input)
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

        public void Run()
        {
            if (GetCurState() == null)
            {
                return;
            }
            List<GamepadInputValue> temp = new List<GamepadInputValue>();
            GamepadInput curState = new GamepadInput();
            ushort wButtons = state.Gamepad.WButtons;
            double time = ProgramManager.GetElapsedTime();
            for (int i = 0; i < MyConstant.buttonsKey.Length; i++)
            {
                XInputButtons button = MyConstant.buttonsKey[i];
                bool isPressed = (wButtons & (ushort)button) != 0;

                GamepadInputValue input = new GamepadInputValue(
                    MyConstant.buttonsName[i],
                    isPressed ? 1.0f : 0.0f
                );
                SetPressDown(i, input);
                //if (SetPressDown(i, input) != 0)
                //{
                //    Console.WriteLine($"{button} 버튼 눌림");
                //}
                temp.Add(input);
            }

            void AddAnalog(string name, float value)
            {
                temp.Add(new GamepadInputValue(name, value));
            }
            AddAnalog(MyConstant.LStickX, NormStick(state.Gamepad.SThumbLX));
            AddAnalog(MyConstant.LStickY, NormStick(state.Gamepad.SThumbLY));
            AddAnalog(MyConstant.RStickX, NormStick(state.Gamepad.SThumbRX));
            AddAnalog(MyConstant.RStickY, NormStick(state.Gamepad.SThumbRY));
            AddAnalog(MyConstant.LTrigger, NormTrigger(state.Gamepad.BLeftTrigger));
            AddAnalog(MyConstant.RTrigger, NormTrigger(state.Gamepad.BRightTrigger));
            curState.Set(time, temp);
            //if (curState.values[16].buttonValue > 0)
            //{
            //    Console.WriteLine($"{MyConstant.LTrigger} 버튼 눌림 {curState.values[16].buttonValue}");
            //}
            //if (curState.values[17].buttonValue > 0)
            //{
            //    Console.WriteLine($"{MyConstant.RTrigger} 버튼 눌림 {curState.values[17].buttonValue}");
            //}

            outputModule.SetOutputValue(curState.time, curState.values);
            simulatorController.Call_VROA_MOBC_action(outputModule.GetOutputValue());

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
            return outputModule.GetValue(key).ToString();
        }

        public bool IsPressDownStartKey()
        {
            return GetPreState().values[10].buttonPressDown;
        }

        public void InitPlayBack(List<OutputValue> record)
        {
            Console.WriteLine(record.Count);
            if(record.Count == 0)
            {
                IsPlayReady = false;
                return;
            }

            playbackModule.SetRecord(record);
            IsPlayReady = true;
        }
        public bool Playback()
        {
            if(playbackModule.IsEnd)
            {
                //IsPlayReady = false;
                PlayReset();
            }
            simulatorController.Call_VROA_MOBC_action(playbackModule.Next());

            return playbackModule.IsEnd;
        }

        public void PlayReset()
        {
            playbackModule.Reset();
        }
    }
}
