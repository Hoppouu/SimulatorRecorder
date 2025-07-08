using SimulatorRecorder.Modules;
using System.Diagnostics;
using System.Windows.Forms;
namespace SimulatorRecorder
{
    public partial class MainForm : Form
    {
        private ControllerInputModule controllerInputMoudle;

        public MainForm()
        {
            InitializeComponent();
            TimerModule.Initialize(this.timer_main, 100);
            controllerInputMoudle = new ControllerInputModule();
            HotKeyModule.OnHotKeyPressed += HotKeyAction;
            HotKeyModule.RegisterHotKey(this.Handle);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            HotKeyModule.UnregisterHotKey(this.Handle);
            ProgramModule.Close();
            FileManager.SaveFolderPath();
            base.OnFormClosing(e);
        }

        protected override void WndProc(ref Message m)
        {
            HotKeyModule.ProcessHotKeyMessage(ref m);
            base.WndProc(ref m);
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            TimerModule.DoSemiStartTimer();
            controllerInputMoudle = new ControllerInputModule();
        }
        private void button_start_Click(object sender, EventArgs e)
        {
            if (FileManager.errorOccurred)
            {
                return;
            }
            if (!TimerModule.IsRun())
            {
                Console.WriteLine("Start");
            }
            else
            {
                Console.WriteLine("ReStart");
            }
            HotKeyModule.SendHotKeySignal();
            TimerModule.DoStartTimer();
            controllerInputMoudle = new ControllerInputModule();
        }

        private void button_end_Click(object sender, EventArgs e)
        {
            bool shouldWrite = false;
            if (FileManager.errorOccurred || TimerModule.IsRun())
            {
                TimerModule.DoEndTimer();
                HotKeyModule.SendHotKeySignal();
                shouldWrite = true;
            }

            if (shouldWrite)
            {
                if (FileManager.WriteFile(controllerInputMoudle.GetBuffer()))
                {
                    Console.WriteLine("End");
                    TimerModule.DoSemiStartTimer();
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

            controllerInputMoudle.GetButton();
            if (controllerInputMoudle.IsStartTimer())
            {
                TimerManage();
            }

            label_elapsed.Text = "진행 시간 : " + TimerModule.GetElapsedTime().ToString("F1");
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
            if (!TimerModule.IsRun())
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

        private void menu1_1_Click(object sender, EventArgs e)
        {
            FileManager.SetFilePath();
        }

        private void menu1_2_Click(object sender, EventArgs e)
        {
            MessageBoxHelper.TextBox(500, 100, "폴더 경로", FileManager.GetFolderPath(), true);
        }

        private void menu1_3_Click(object sender, EventArgs e)
        {
            string str = MyConstant.buttonManual;
            MessageBoxHelper.TextBox(500, 400, "버튼 설명", str);
        }

        private void button_SelectVideo_Click(object sender, EventArgs e)
        {
            string?[] path = FileManager.GetFilePath("동영상 파일 선택");
            if(path != null)
            {
                ProgramModule.Launch(path!);
            }
        }
    }
}
