using Silk.NET.XInput;
using System.Security.Cryptography.X509Certificates;
namespace SimulatorRecorder.Modules
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
        readonly static public string[] buttonsName;
        readonly static public XInputButtons[] buttonsKey;

        readonly static public string[] outputsName;
        readonly static public int[] outputsInit;

        readonly static public Dictionary<string, string> keyMapping;
        readonly static public Dictionary<string, float> keyOffest;
        readonly static public Dictionary<string, int> outputRange;
        readonly static public Dictionary<string, int> outputsInitDict;
        readonly static public Dictionary<string, bool> deadZoneList;

        readonly static public float deadZone = 0.2f;
        readonly static private float scaleOffset = (3276.7f) / 3;
        readonly static private float scaleFactor = 2.5f;

        readonly static public string buttonManual = @"
                LStickX     : ROLL

                LStickY     : PITCH

                RStickX     : YAW

                LTrigger    : HEAVE

                RTrigger    : HEAVE

                A           : BLOWER1

                B           : 버튼 값 리셋 버튼

                X           : 레코딩 시작/종료

            ";

        static MyConstant()
        {
            buttonsName = Enum.GetNames(typeof(XInputButtons));
            buttonsKey = (XInputButtons[])Enum.GetValues(typeof(XInputButtons));

            outputsName = new string[]
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

            outputsInit = new int[]
            {
                -1,     // TIME
                10000,  // ROLL
                10000,  // PITCH
                10000,  // YAW
                10000,  // SWAY
                10000,  // SURGE
                10000,  // HEAVE
                5,      // SPEED
                0       // BLOWER1
            };
            keyMapping = new Dictionary<string, string>
            {
                { "LStickX", "ROLL"},
                { "LStickY", "PITCH" },
                { "RStickX", "YAW" },
                { "LTrigger", "HEAVE" },
                { "RTrigger", "HEAVE" },
                { "A", "BLOWER1" },
                { "B", "" }
            };

            keyOffest = new Dictionary<string, float>
            {
                { "LStickX", scaleOffset * scaleFactor},
                { "LStickY", scaleOffset * scaleFactor },
                { "RStickX", scaleOffset * scaleFactor },
                { "LTrigger", -scaleOffset * scaleFactor},
                { "RTrigger", scaleOffset * scaleFactor},
                { "A", 60f },
                { "B", 1f }
            };

            outputRange = new Dictionary<string, int>
            {
                { "ROLL_MIN", 1000 },
                { "ROLL_MAX", 19000 },

                { "PITCH_MIN", 1000 },
                { "PITCH_MAX", 19000 },

                { "YAW_MIN", 1000 },
                { "YAW_MAX", 19000 },

                { "SWAY_MIN", 1000 },
                { "SWAY_MAX", 19000 },

                { "SURGE_MIN", 1000 },
                { "SURGE_MAX", 19000 },

                { "HEAVE_MIN", 1000 },
                { "HEAVE_MAX", 19000 },

                { "SPEED_MIN", 3 },
                { "SPEED_MAX", 200 },

                { "BLOWER1_MIN", 0 },
                { "BLOWER1_MAX", 100 }
            };

            outputsInitDict = new Dictionary<string, int>
            {
                { "ROLL", 10000 },
                { "PITCH", 10000 },
                { "YAW", 10000 },
                { "SWAY", 10000 },
                { "SURGE", 10000 },
                { "HEAVE", 10000 },
                { "SPEED", 5 },
                { "BLOWER1", 0 }
            };

            deadZoneList = new Dictionary<string, bool>
            {
                { "LStickX", true},
                { "LStickY", true },
                { "RStickX", true },
                { "LTrigger", false },
                { "RTrigger", false },
                { "A", false },
                { "B", false },
            };
        }
    }
}