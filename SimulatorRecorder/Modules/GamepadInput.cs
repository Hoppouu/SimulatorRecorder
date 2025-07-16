using Silk.NET.XInput;

//인덱스 위치
//0:DPadUp
//1:DPadDown
//2:DPadLeft
//3:DPadRight
//4:LeftThumb
//5:RightThumb
//6:LeftShoulder
//7:RightShoulder
//8:A
//9:B
//10:X
//11:Y
//12:LStickX
//13:LStickY
//14:RStickX
//15:RStickY
//16:LTrigger
//17:RTrigger
namespace SimulatorRecorder.Modules
{
    public class GamepadInput
    {
        public double time;
        public List<GamepadInputValue> values;
        public GamepadInput()
        {
            values = new List<GamepadInputValue>(18);
        }
        public void Set(double time, List<GamepadInputValue> values)
        {
            this.time = time;
            this.values.Clear();
            this.values.AddRange(values);
        }
    }
}
