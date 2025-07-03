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
        }
        private void button_start_Click(object sender, EventArgs e)
        {
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
            if (TimerModule.IsRun())
            {
                FileManager.WriteFile(controllerInputMoudle.GetBuffer());
                TimerModule.DoEndTimer();
                Console.WriteLine("End");
            }
        }

        private void TimerEvent(object sender, EventArgs e)
        {
            label_elapsed.Text = "진행 시간 : " + TimerModule.GetElapsedTime().ToString("F1");
            controllerInputMoudle.GetButton();
            GamepadInput temp = controllerInputMoudle.GetCurState();
            label_LstickX.Text = "L stick X : " + temp.values[12].buttonValue.ToString("F2");
            label_LstickY.Text = "L stick Y : " + temp.values[13].buttonValue.ToString("F2");
            label_RstickX.Text = "R stick X : " + temp.values[14].buttonValue.ToString("F2");
            label_RstickY.Text = "R stick Y : " + temp.values[15].buttonValue.ToString("F2");
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
