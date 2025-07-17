using Silk.NET.XInput;
namespace SimulatorRecorder.Modules
{
    public abstract class ControllerInputModuleHeader
    {
        //XBox컨트롤러가 연결되어 있는지 확인.
        public abstract bool IsConnected();
        
        //버튼을 눌렀는지 확인하고 누른 버튼에 맞는 변수 값을 조정.
        public abstract void Run();

        //기록된 Output리스트를 반환
        public abstract List<OutputValue> GetBuffer();

        //key(ROLL, PITCH...)에 해당되는 output value를 가져옴.
        public abstract string GetOutput(string key);

        //버튼을 누른 시각을 가져옴.
        public abstract double GetOutputTime();

        //X버튼을 눌렀는지 확인. X버튼을 눌러서도 녹화 시작을 하기 위함.
        public abstract bool IsPressDownStartKey();


        //현재 버튼 상태를 가져옴.
        protected abstract State? GetCurState();

        //스틱의 값(-32768 ~ 32767)을 (-1 ~ 1)사이의 값으로 정규화한다. 
        protected abstract float NormStick(int value);

        //트리거의 값(0 ~ 255)을 (0 ~ 1)사의 값으로 정규화한다.
        protected abstract float NormTrigger(int value);

        //버튼이 최초 한번 눌린 상태인지 확인. 꾹 눌렀을 때 계속 함수가 실행하는 걸 방지하기 위함.
        protected abstract float SetPressDown(int i, GamepadInputValue input);
        
        //버튼의 전 상태를 가져옴.
        protected abstract GamepadInput GetPreState();

        //SimulatorController의 VROA_MOBC_action(시뮬레이터 제어) 함수를 호출하는 함수.
        protected abstract void CallVROA_MOBC_action(OutputValue output);
    }
}