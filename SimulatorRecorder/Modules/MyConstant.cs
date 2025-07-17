using Silk.NET.XInput;
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
        //Outputs
        public const string TIME = "TIME";
        public const string ROLL = "ROLL";
        public const string PITCH = "PITCH";
        public const string YAW = "YAW";
        public const string SWAY = "SWAY";
        public const string SURGE = "SURGE";
        public const string HEAVE = "HEAVE";
        public const string SPEED = "SPEED";
        public const string BLOWER1 = "BLOWER1";


        //Buttons
        public const string LStickX = "LStickX";
        public const string LStickY = "LStickY";
        public const string RStickX = "RStickX";
        public const string RStickY = "RStickY";
        public const string LTrigger = "LTrigger";
        public const string RTrigger = "RTrigger";
        public const string LButton = "LButton";
        public const string RButton = "RButton";
        public const string A = "A";
        public const string B = "B";
        public const string X = "X";
        public const string Y = "Y";


        readonly static public string[] buttonsName;
        readonly static public XInputButtons[] buttonsKey;

        readonly static public string[] outputsName;
        readonly static public int[] outputsInit;

        readonly static public Dictionary<string, string> keyMapping;
        readonly static public Dictionary<string, float> keyOffest;
        readonly static public Dictionary<string, int> outputRange;
        readonly static public Dictionary<string, int> outputsInitDict;
        readonly static public Dictionary<string, bool> deadZoneList;

        readonly static private float scaleOffset = (3276.7f) / 3;
        readonly static private float scaleFactor = 2.5f;

        readonly static public string settingFilePath = Path.Combine(Directory.GetCurrentDirectory(), "setting.env");
        readonly static public string basePath = AppDomain.CurrentDomain.BaseDirectory;
        readonly static public string baseWindowsPlayerPath = ".\\WindowsPlayer\\WindowsPlayer.exe";

        static private string windowsPlayerPath = Path.Combine(basePath, "WindowsPlayer\\WindowsPlayer.exe");
        static private float deadZone = 0.2f;

        readonly static public string manual = @$"
                {LStickX}     : {ROLL}

                {LStickY}     : {PITCH}

                {RStickX}     : {YAW}

                {LTrigger}    : {HEAVE}

                {RTrigger}    : {HEAVE}

                    {A}       : {BLOWER1}

                    {B}       : 버튼 값 리셋 버튼

                    {X}       : 레코딩 시작/종료

            ";

        static MyConstant()
        {
            buttonsName = Enum.GetNames(typeof(XInputButtons));
            buttonsKey = (XInputButtons[])Enum.GetValues(typeof(XInputButtons));

            outputsName = new string[]
            {
                TIME,
                ROLL,
                PITCH,
                YAW,
                SWAY,
                SURGE,
                HEAVE,
                SPEED,
                BLOWER1
            };

            outputsInit = new int[]
            {
                0,     // TIME
                10000,  // ROLL
                10000,  // PITCH
                10000,  // YAW
                10000,  // SWAY
                10000,  // SURGE
                10000,  // HEAVE
                30,      // SPEED
                0       // BLOWER1
            };
            keyMapping = new Dictionary<string, string>
            {
                { LStickX, ROLL},
                { LStickY, PITCH },
                { RStickX, YAW },
                { LTrigger, HEAVE },
                { RTrigger, HEAVE },
                { A, BLOWER1 },
                { B, "" }
            };

            keyOffest = new Dictionary<string, float>
            {
                { LStickX, scaleOffset * scaleFactor},
                { LStickY, scaleOffset * scaleFactor },
                { RStickX, scaleOffset * scaleFactor },
                { LTrigger, -scaleOffset * scaleFactor},
                { RTrigger, scaleOffset * scaleFactor},
                { A, 60f },
                { B, 1f }
            };

            outputRange = new Dictionary<string, int>
            {
                { ROLL + "_MIN", 1000 },
                { ROLL + "_MAX", 19000 },

                { PITCH + "_MIN", 1000 },
                { PITCH + "_MAX", 19000 },

                { YAW + "_MIN", 1000 },
                { YAW + "_MAX", 19000 },

                { SWAY + "_MIN", 1000 },
                { SWAY + "_MAX", 19000 },

                { SURGE + "_MIN", 1000 },
                { SURGE + "_MAX", 19000 },

                { HEAVE + "_MIN", 1000 },
                { HEAVE + "_MAX", 19000 },

                { SPEED + "_MIN", 3 },
                { SPEED + "_MAX", 200 },

                { BLOWER1 + "_MIN", 0 },
                { BLOWER1 + "_MAX", 100 }
            };

            outputsInitDict = new Dictionary<string, int>
            {
                { ROLL, 10000 },
                { PITCH, 10000 },
                { YAW, 10000 },
                { SWAY, 10000 },
                { SURGE, 10000 },
                { HEAVE, 10000 },
                { SPEED, 5 },
                { BLOWER1, 0 }
            };

            deadZoneList = new Dictionary<string, bool>
            {
                { LStickX, true},
                { LStickY, true },
                { RStickX, true },
                { LTrigger, false },
                { RTrigger, false },
                { A, false },
                { B, false },
            };
        }

        public static void SetDeadZone(float x)
        {
            deadZone = x;
        }

        public static float GetDeadZone()
        {
            return deadZone;
        }

        public static void SetwindowsPlayerPath(string x)
        {
            windowsPlayerPath = x;
        }
        public static string GetwindowsPlayerPath()
        {
            return windowsPlayerPath;
        }
    }
}