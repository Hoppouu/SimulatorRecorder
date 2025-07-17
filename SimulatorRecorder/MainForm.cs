using SimulatorRecorder.Modules;
using System.Diagnostics;
using System.Windows.Forms;
namespace SimulatorRecorder
{
    public partial class MainForm : Form
    {
        private ControllerInputModule controllerInputModule;
        private PlaybackModule playbackModule;
        bool stateButtonRecord;
        bool stateButtonPlay;
        public MainForm()
        {
            InitializeComponent();
            Init();
            FileManager.Init();
            ProgramManager.Initialize(this.timer_main, 100);
            controllerInputModule = new ControllerInputModule();
            playbackModule = new PlaybackModule();
            HotKeyModule.OnHotKeyPressed += HotKeyAction;
            HotKeyModule.RegisterHotKey(this.Handle);
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

        private void Init()
        {

            button_record_start.Visible = true;
            button_record_end.Visible = true;
            button_play.Visible = true;
            button_record.Visible = true;
            button_play_selectCSV.Visible = false;
            button_play_start.Visible = false;



            button_record_start.Enabled = false;
            button_record_end.Enabled = false;
            button_selectVideo.Enabled = false;
            button_play_stop.Visible = false;
            button_record_stop.Visible = false;

            stateButtonPlay = false;
            stateButtonRecord = false;
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            ProgramManager.DoSemiStart();
            controllerInputModule = new ControllerInputModule();
        }
        private void button_record_start_Click(object sender, EventArgs e)
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
            controllerInputModule = new ControllerInputModule();
        }

        private void button_record_end_Click(object sender, EventArgs e)
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
                if (FileManager.WriteFile(controllerInputModule.GetBuffer()))
                {
                    Console.WriteLine("End");
                    ProgramManager.DoSemiStart();
                }
            }
        }

        private void TimerEvent(object sender, EventArgs e)
        {
            if (!controllerInputModule.IsConnected())
            {
                didFindController.Visible = true;
                return;
            }
            else
            {
                didFindController.Visible = false;
            }

            if (stateButtonRecord)
            {
                controllerInputModule.Run();
                if (controllerInputModule.IsPressDownStartKey())
                {
                    TimerManage();
                }
                TimerEventText();
            }
            else if (stateButtonPlay)
            {
                if (playbackModule.Playback())
                {
                    button_play_stop.PerformClick();
                }

            }
        }
        private void TimerEventText()
        {
            label_elapsed.Text = "진행 시간 : " + ProgramManager.GetElapsedTime().ToString("F1");
            ROLL.Text = $"{MyConstant.ROLL}    : "      + controllerInputModule.GetOutput(MyConstant.ROLL);
            PITCH.Text = $"{MyConstant.PITCH}   : "     + controllerInputModule.GetOutput(MyConstant.PITCH);
            YAW.Text = $"{MyConstant.YAW}     : "       + controllerInputModule.GetOutput(MyConstant.YAW);
            SWAY.Text = $"{MyConstant.SWAY}    : "      + controllerInputModule.GetOutput(MyConstant.SWAY);
            SURGE.Text = $"{MyConstant.SURGE}   : "     + controllerInputModule.GetOutput(MyConstant.SURGE);
            HEAVE.Text = $"{MyConstant.HEAVE}   : "     + controllerInputModule.GetOutput(MyConstant.HEAVE);
            SPEED.Text = $"{MyConstant.SPEED}   : "     + controllerInputModule.GetOutput(MyConstant.SPEED);
            BLOWER1.Text = $"{MyConstant.BLOWER1} : "   + controllerInputModule.GetOutput(MyConstant.BLOWER1);
        }

        private void TimerManage()
        {
            if (!ProgramManager.IsRunTimer)
            {
                button_record_start.PerformClick();
            }
            else
            {
                button_record_end.PerformClick();
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

        private void button_selectVideo_Click(object sender, EventArgs e)
        {
            string?[] path = FileManager.SetFilePath("동영상 파일 선택");
            if (path != null)
            {
                Modules.ProcessManager.Launch(path!);
            }
        }

        private void menu1_Click(object sender, EventArgs e)
        {

        }

        private void button_record_Click(object sender, EventArgs e)
        {
            if (stateButtonRecord || stateButtonPlay)
            {
                return;
            }
            Init();

            stateButtonPlay = false;
            stateButtonRecord = true;

            button_selectVideo.Enabled = true;

            button_record.Visible = false;
            button_record_start.Enabled = true;
            button_record_end.Enabled = true;
            button_record_stop.Visible = true;
            ProgramManager.Initialize(100);
            ProgramManager.DoSemiStart();

        }

        private void button_play_Click(object sender, EventArgs e)
        {
            if (stateButtonPlay || ProgramManager.IsRunTimer)
            {
                return;
            }
            TimerEventText();
            ProgramManager.DoEnd();
            button_record_stop.PerformClick();

            stateButtonRecord = false;

            button_selectVideo.Enabled = true;

            button_record_start.Visible = false;
            button_record_end.Visible = false;

            button_play_selectCSV.Visible = true;
            button_play_start.Visible = true;
            button_play_start.Enabled = false;
            button_play_stop.Visible = true;

            ProgramManager.Initialize(50);

        }
        private void button_record_stop_Click(object sender, EventArgs e)
        {
            Init();
            stateButtonRecord = false;
            ProgramManager.DoEnd();
        }

        private void button_play_stop_Click(object sender, EventArgs e)
        {
            button_play_stop.Visible = false;
            button_play_start.Visible = true;
            button_play_start.Enabled = true;
            playbackModule.PlayReset();
            stateButtonPlay = false;
            ProgramManager.DoEnd();
            HotKeyModule.SendHotKeySignal();
        }

        private void button_play_selecetCSV_Click(object sender, EventArgs e)
        {
            List<OutputValue> list = FileManager.ReadCSV("CSV파일을 선택해주세요.");


            if(list.Count != 0)
            {
                playbackModule = new PlaybackModule();
                playbackModule.InitPlayBack(list);
            }
            if(playbackModule.IsPlayReady)
            {
                button_play_start.Enabled = true;
            }

        }


        private void button_play_start_Click(object sender, EventArgs e)
        {
            button_play_start.Visible = false;
            button_play_stop.Visible = true;
            stateButtonPlay = true;
            HotKeyModule.SendHotKeySignal();
            ProgramManager.DoStart();
        }
    }
}
