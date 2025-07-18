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
            //play button
            button_play_selectCSV.Enabled = false;
            button_play_start.Enabled = false;
            button_play_stop.Enabled = false;

            button_play_selectCSV.Visible = false;
            button_play_start.Visible = false;
            button_play_stop.Visible = false;
            //

            //record button
            button_record_start.Enabled = false;
            button_record_end.Enabled = false;

            button_record_start.Visible = false;
            button_record_end.Visible = false;
            //

            stateButtonPlay = false;
            stateButtonRecord = false;

            label_elapsedRate.Visible = false;
            label_elapsedRate.Text = "진행율 : 0%";
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
            if (controllerInputModule != null)
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
                    TimerEventText(controllerInputModule.GetOutput());
                }
            }
            else if(playbackModule != null)
            {
                if (stateButtonPlay)
                {
                    TimerEventText(playbackModule.CurOutput);
                    if (playbackModule.Playback())
                    {
                        button_play_stop.PerformClick();
                    }

                }
            }
        }
        private void TimerEventText(OutputValue output)
        {
            if (output == null)
            {
                return;
            }
            if (stateButtonPlay)
            {
                label_elapsedRate.Text = "진행율 : " + playbackModule.GetElapsedRate().ToString("F0") + "%";
            }

            label_elapsed.Text = "진행 시간 : " + ProgramManager.GetElapsedTime().ToString("F1");
            ROLL.Text = $"{MyConstant.ROLL}    : " + output.OutputData[MyConstant.ROLL];
            PITCH.Text = $"{MyConstant.PITCH}   : " + output.OutputData[MyConstant.PITCH];
            YAW.Text = $"{MyConstant.YAW}     : " + output.OutputData[MyConstant.YAW];
            SWAY.Text = $"{MyConstant.SWAY}    : " + output.OutputData[MyConstant.SWAY];
            SURGE.Text = $"{MyConstant.SURGE}   : " + output.OutputData[MyConstant.SURGE];
            HEAVE.Text = $"{MyConstant.HEAVE}   : " + output.OutputData[MyConstant.HEAVE];
            SPEED.Text = $"{MyConstant.SPEED}   : " + output.OutputData[MyConstant.SPEED];
            BLOWER1.Text = $"{MyConstant.BLOWER1} : " + output.OutputData[MyConstant.BLOWER1];
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
            if (ProgramManager.IsRunTimer)
            {
                return;
            }
            string?[] path = FileManager.SetFilePath("동영상 파일 선택");
            if (path != null)
            {
                Modules.ProcessManager.Launch(path!);
            }
        }

        private void button_record_Click(object sender, EventArgs e)
        {
            if (ProgramManager.IsRunTimer || stateButtonRecord)
            {
                return;
            }
            Init();
            button_selectVideo.Enabled = true;
            button_record_start.Enabled = true;
            button_record_end.Enabled = true;

            button_selectVideo.Visible = true;
            button_record_start.Visible = true;
            button_record_end.Visible = true;

            stateButtonRecord = true;

            ProgramManager.Initialize(100);
            ProgramManager.DoSemiStart();

            if (playbackModule != null)
            {
                playbackModule.EndModule();
                playbackModule = null!;
            }
            controllerInputModule = new ControllerInputModule();
        }

        private void button_play_Click(object sender, EventArgs e)
        {
            if (ProgramManager.IsRunTimer || stateButtonPlay)
            {
                return;
            }
            ProgramManager.DoEnd();
            TimerEventText(new OutputValue());

            Init();
            button_play_selectCSV.Enabled = true;
            button_selectVideo.Enabled = true;

            button_play_selectCSV.Visible = true;
            button_play_start.Visible = true;
            button_selectVideo.Visible = true;
            label_elapsedRate.Visible = true;

            stateButtonPlay = true;

            ProgramManager.Initialize(50);
            if(controllerInputModule != null)
            {
                controllerInputModule.EndModule();
                controllerInputModule = null!;
            }
            playbackModule = new PlaybackModule();

        }

        private void button_play_stop_Click(object sender, EventArgs e)
        {
            button_play_start.Enabled = true;
            button_play_stop.Enabled = false;

            button_play_start.Visible = true;
            button_play_stop.Visible = false;
            playbackModule.PlayReset();
            stateButtonPlay = false;
            ProgramManager.DoEnd();
            HotKeyModule.SendHotKeySignal();
        }

        private void button_play_selecetCSV_Click(object sender, EventArgs e)
        {
            if(ProgramManager.IsRunTimer)
            {
                return;
            }
            List<OutputValue> list = FileManager.ReadCSV("CSV파일을 선택해주세요.");


            if (list.Count != 0)
            {
                playbackModule.InitPlayBack(list);
            }
            if (playbackModule.IsPlayReady)
            {
                button_play_start.Enabled = true;
            }

        }


        private void button_play_start_Click(object sender, EventArgs e)
        {
            button_play_start.Enabled = false;
            button_play_stop.Enabled = true;

            button_play_start.Visible = false;
            button_play_stop.Visible = true;
            stateButtonPlay = true;
            HotKeyModule.SendHotKeySignal();
            ProgramManager.DoStart();
        }
    }
}
