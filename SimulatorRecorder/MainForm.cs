using SimulatorRecorder.Modules;
using System.Diagnostics;
using System.Windows.Forms;
namespace SimulatorRecorder
{
    public partial class MainForm : Form
    {
        private ControllerInputModule controllerInputMoudle;
        bool stateButtonRecording;
        bool stateButtonPlay;
        public MainForm()
        {
            InitializeComponent();
            Init();
            FileManager.Init();
            ProgramManager.Initialize(this.timer_main, 100);
            controllerInputMoudle = new ControllerInputModule();
            HotKeyModule.OnHotKeyPressed += HotKeyAction;
            HotKeyModule.RegisterHotKey(this.Handle);
        }
        private void Init()
        {
            stateButtonRecording = false;
            stateButtonPlay = false;
            button_selectVideo.Enabled = false;
            button_start.Enabled = false;
            button_end.Enabled = false;
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            HotKeyModule.UnregisterHotKey(this.Handle);
            Modules.ProcessManager.Close();
            FileManager.SaveEnvFile();
            base.OnFormClosing(e);
        }

        protected override void WndProc(ref Message m)
        {
            HotKeyModule.ProcessHotKeyMessage(ref m);
            base.WndProc(ref m);
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            ProgramManager.DoSemiStart();
            controllerInputMoudle = new ControllerInputModule();
        }
        private void button_start_Click(object sender, EventArgs e)
        {
            if (FileManager.errorOccurred)
            {
                return;
            }
            if (!ProgramManager.IsRunTimer)
            {
                Console.WriteLine("Start");
            }
            else
            {
                Console.WriteLine("ReStart");
            }
            HotKeyModule.SendHotKeySignal();
            ProgramManager.DoStart();
            controllerInputMoudle = new ControllerInputModule();
        }

        private void button_end_Click(object sender, EventArgs e)
        {
            bool shouldWrite = false;
            if (FileManager.errorOccurred || ProgramManager.IsRunTimer)
            {
                ProgramManager.DoEnd();
                HotKeyModule.SendHotKeySignal();
                shouldWrite = true;
            }

            if (shouldWrite)
            {
                if (FileManager.WriteFile(controllerInputMoudle.GetBuffer()))
                {
                    Console.WriteLine("End");
                    ProgramManager.DoSemiStart();
                }
            }
        }

        private void TimerEvent(object sender, EventArgs e)
        {
            if (!controllerInputMoudle.IsConnected())
            {
                didFindController.Visible = true;
                return;
            }
            else
            {
                didFindController.Visible = false;
            }

            controllerInputMoudle.Run();
            if (controllerInputMoudle.IsPressDownStartKey())
            {
                TimerManage();
            }
            TimerEventText();
        }
        private void TimerEventText()
        {
            label_elapsed.Text = "진행 시간 : " + ProgramManager.GetElapsedTime().ToString("F1");
            ROLL.Text = "ROLL    : " + controllerInputMoudle.GetOutput("ROLL");
            PITCH.Text = "PITCH   : " + controllerInputMoudle.GetOutput("PITCH");
            YAW.Text = "YAW     : " + controllerInputMoudle.GetOutput("YAW");
            SWAY.Text = "SWAY    : " + controllerInputMoudle.GetOutput("SWAY");
            SURGE.Text = "SURGE   : " + controllerInputMoudle.GetOutput("SURGE");
            HEAVE.Text = "HEAVE   : " + controllerInputMoudle.GetOutput("HEAVE");
            SPEED.Text = "SPEED   : " + controllerInputMoudle.GetOutput("SPEED");
            BLOWER1.Text = "BLOWER1 : " + controllerInputMoudle.GetOutput("BLOWER1");
        }

        private void TimerManage()
        {
            if (!ProgramManager.IsRunTimer)
            {
                button_start.PerformClick();
            }
            else
            {
                button_end.PerformClick();
            }
        }

        private void HotKeyAction()
        {
            TimerManage();
        }

        private void menu1_setSavePath_Click(object sender, EventArgs e)
        {
            FileManager.SetSaveFilePath();
        }

        private void menu1_getSavePath_Click(object sender, EventArgs e)
        {
            MessageBoxHelper.TextBox(500, 100, "폴더 경로", FileManager.GetFolderPath(), true);
        }
        private void menu1_setUnityPath_Click(object sender, EventArgs e)
        {
            FileManager.SetUnityFilePath("WindowsPlayer를 선택해주세요");
        }
        private void menu1_deadzon_Click(object sender, EventArgs e)
        {
            FileManager.SetDeadZone();
        }
        private void menu1_manual_Click(object sender, EventArgs e)
        {
            string str = MyConstant.manual;
            MessageBoxHelper.TextBox(500, 400, "사용 설명", str);
        }

        private void button_SelectVideo_Click(object sender, EventArgs e)
        {
            string?[] path = FileManager.SetFilePath("동영상 파일 선택");
            if (path != null)
            {
                Modules.ProcessManager.Launch(path!);
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {

        }

        private void menu1_Click(object sender, EventArgs e)
        {

        }

        private void button_startRecord_Click(object sender, EventArgs e)
        {
            if (stateButtonRecording)
            {
                return;
            }
            button_selectVideo.Enabled = true;
            button_start.Enabled = true;
            button_end.Enabled = true;
            ProgramManager.DoSemiStart();
            ProgramManager.DoStartMOBC();
            button_stopRecording.Visible = true;
            button_play.Visible = false;
        }

        private void button_play_Click(object sender, EventArgs e)
        {
            if (stateButtonPlay || ProgramManager.IsRunTimer)
            {
                return;
            }

            button_selectVideo.Enabled = false;
            button_start.Enabled = false;
            button_end.Enabled = false;
            controllerInputMoudle = new ControllerInputModule();
            TimerEventText();
            ProgramManager.DoEnd();
            FileManager.readCSV("CSV파일을 선택해주세요.");
        }

        private void button_stopRecording_Click(object sender, EventArgs e)
        {
            ProgramManager.DoEnd();
            button_stopRecording.Visible = false;
            button_play.Visible = true;
        }
    }
}
