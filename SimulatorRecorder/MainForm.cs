using SimulatorRecorder.Modules;
using System.Windows.Forms;
namespace SimulatorRecorder
{
    public partial class MainForm : Form
    {
        private ControllerInputModule controllerInputMoudle;
        private HotKeyModule hotKeyModule;

        public MainForm()
        {
            InitializeComponent();
            TimerModule.Initialize(this.timer_main, 100);
            controllerInputMoudle = new ControllerInputModule();
            hotKeyModule = new HotKeyModule();
            hotKeyModule.OnHotKeyPressed += HotKeyAction;
            hotKeyModule.RegisterHotKey(this.Handle);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            FileManager.SaveFolderPath();
            hotKeyModule.UnregisterHotKey(this.Handle);
            base.OnFormClosing(e);
        }

        protected override void WndProc(ref Message m)
        {
            hotKeyModule.ProcessHotKeyMessage(ref m);
            base.WndProc(ref m);
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            TimerModule.DoSemiStartTimer();
            controllerInputMoudle = new ControllerInputModule();
        }
        private void button_start_Click(object sender, EventArgs e)
        {
            if(FileManager.errorOccurred)
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
            TimerModule.DoStartTimer();
            controllerInputMoudle = new ControllerInputModule();
        }

        private void button_end_Click(object sender, EventArgs e)
        {
            bool shouldWrite = false;
            if (FileManager.errorOccurred)
            {
                shouldWrite = true;
            }
            else if (TimerModule.IsRun())
            {
                TimerModule.DoEndTimer();
                shouldWrite = true;
            }

            if(shouldWrite)
            {
                if (FileManager.WriteFile(controllerInputMoudle.GetBuffer()))
                {
                    Console.WriteLine("End");
                }
            }
        }

        private void TimerEvent(object sender, EventArgs e)
        {
            controllerInputMoudle.GetButton();
            label_elapsed.Text  = "진행 시간 : " + TimerModule.GetElapsedTime().ToString("F1");
            ROLL.Text           = "ROLL    : " + controllerInputMoudle.GetOutput("ROLL");
            PITCH.Text          = "PITCH   : " + controllerInputMoudle.GetOutput("PITCH");
            YAW.Text            = "YAW     : " + controllerInputMoudle.GetOutput("YAW");
            SWAY.Text           = "SWAY    : " + controllerInputMoudle.GetOutput("SWAY");
            SURGE.Text          = "SURGE   : " + controllerInputMoudle.GetOutput("SURGE");
            HEAVE.Text          = "HEAVE   : " + controllerInputMoudle.GetOutput("HEAVE");
            SPEED.Text          = "SPEED   : " + controllerInputMoudle.GetOutput("SPEED");
            BLOWER1.Text        = "BLOWER1 : " + controllerInputMoudle.GetOutput("BLOWER1");
        }

        private void HotKeyAction()
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

        private void menu1_1_Click(object sender, EventArgs e)
        {
            FileManager.SetFilePath();
        }

        private void menu1_2_Click(object sender, EventArgs e)
        {
            Form copyForm = new Form
            {
                Text = "폴더 경로",
                Size = new System.Drawing.Size(500, 100),
                StartPosition = FormStartPosition.CenterScreen,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false
            };

            TextBox textBox = new TextBox
            {
                Multiline = true,
                ReadOnly = true,
                Text = FileManager.GetFolderPath(),
                Dock = DockStyle.Fill,
                ScrollBars = ScrollBars.None,
                Font = new System.Drawing.Font("Consolas", 12)
            };

            copyForm.Controls.Add(textBox);
            copyForm.ShowDialog();
        }
    }
}
