using SimulatorRecorder.Modules;
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
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
        private void button_start_Click(object sender, EventArgs e)
        {
            if(!TimerModule.IsRun())
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
            FileManager.WriteFile(controllerInputMoudle.GetBuffer());
            TimerModule.DoEndTimer();
            if (TimerModule.IsRun())
            {
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
    }
}
