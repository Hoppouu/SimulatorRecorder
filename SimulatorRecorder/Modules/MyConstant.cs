
namespace Silk.NET.XInput
{
    [Flags]
    public enum XInputButtons : ushort
    {
        DPadUp = XInput.GamepadDpadUp,
        DPadDown = XInput.GamepadDpadDown,
        DPadLeft = XInput.GamepadDpadLeft,
        DPadRight = XInput.GamepadDpadRight,
        LeftThumb = XInput.GamepadLeftThumb,
        RightThumb = XInput.GamepadRightThumb,
        LeftShoulder = XInput.GamepadLeftShoulder,
        RightShoulder = XInput.GamepadRightShoulder,
        A = XInput.GamepadA,
        B = XInput.GamepadB,
        X = XInput.GamepadX,
        Y = XInput.GamepadY
    }

    public static class MyConstant
    {
        readonly static public XInputButtons[] buttonsKey;
        readonly static public string[] buttonsName;
        readonly static public string[] formNames;
        static MyConstant()
        {
            buttonsKey = (XInputButtons[])Enum.GetValues(typeof(XInputButtons));
            buttonsName = Enum.GetNames(typeof(XInputButtons));
            formNames = new string[]
            {
                "TIME",
                "ROLL",
                "PITCH",
                "YAW",
                "SWAY",
                "SURGE",
                "HEAVE",
                "SPEED",
                "BLOWER1"
            };
        }
    }
}