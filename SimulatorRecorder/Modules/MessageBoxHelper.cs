using SimulatorRecorder.Modules;
using System.Diagnostics;

public static class MessageBoxHelper
{
    public static DialogResult ShowTopMost(string message, string title,
        MessageBoxButtons buttons = MessageBoxButtons.OK,
        MessageBoxIcon icon = MessageBoxIcon.None)
    {
        using (Form topmostForm = new Form())
        {
            topmostForm.TopMost = true;
            topmostForm.StartPosition = FormStartPosition.Manual;
            topmostForm.Size = new System.Drawing.Size(1, 1);
            topmostForm.Location = new System.Drawing.Point(-1000, -1000);
            topmostForm.ShowInTaskbar = false;

            DialogResult result = DialogResult.None;

            topmostForm.Shown += (s, e) =>
            {
                topmostForm.Activate();
                topmostForm.BringToFront();
                topmostForm.Focus();

                result = MessageBox.Show(topmostForm, message, title, buttons, icon);
                topmostForm.Close();
            };

            topmostForm.ShowDialog();

            return result;
        }
    }

    public static void TextBox(int width, int heihgt, string title, string text, bool drag = false)
    {
        Form copyForm = new Form
        {
            Text = title,
            Size = new System.Drawing.Size(width, heihgt),
            StartPosition = FormStartPosition.CenterScreen,
            FormBorderStyle = FormBorderStyle.FixedDialog,
            MaximizeBox = false,
            MinimizeBox = false
        };

        TextBox textBox = new TextBox
        {
            Multiline = true,
            ReadOnly = true,
            Text = text,
            Dock = DockStyle.Fill,
            ScrollBars = ScrollBars.None,
            Font = new System.Drawing.Font("Consolas", 12)
        };

        copyForm.Controls.Add(textBox);
        if(!drag)
        {
            copyForm.Shown += (s, e) => copyForm.ActiveControl = null;
        }
        copyForm.ShowDialog();
    }

    public static int InputBox(string title, string message, int startValue = 0)
    {
        string value = startValue.ToString();
        if (InputDialog.Show(title, message, ref value) == DialogResult.OK)
        {
            if (int.TryParse(value, out int result))
            {
                return (result);
            }
        }

        return startValue;
    }
}
public class InputDialog : Form
{
    private Label label;
    private TextBox textBox;
    private Button buttonOk;
    private Button buttonCancel;

    public string InputText => textBox.Text;

    public InputDialog(string title, string prompt, string defaultValue = "")
    {
        // 폼 속성
        this.Text = title;
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.StartPosition = FormStartPosition.CenterParent;
        this.ClientSize = new Size(300, 120);
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.ShowInTaskbar = false;
        this.AcceptButton = buttonOk;
        this.CancelButton = buttonCancel;

        // 라벨
        label = new Label();
        label.Text = prompt;
        label.AutoSize = true;
        label.Location = new Point(10, 10);
        this.Controls.Add(label);

        // 텍스트박스
        textBox = new TextBox();
        textBox.Size = new Size(260, 20);
        textBox.Location = new Point(10, 35);
        textBox.Text = defaultValue;
        this.Controls.Add(textBox);

        // 확인 버튼
        buttonOk = new Button();
        buttonOk.Text = "확인";
        buttonOk.DialogResult = DialogResult.OK;
        buttonOk.Location = new Point(110, 70);
        this.Controls.Add(buttonOk);

        // 취소 버튼
        buttonCancel = new Button();
        buttonCancel.Text = "취소";
        buttonCancel.DialogResult = DialogResult.Cancel;
        buttonCancel.Location = new Point(190, 70);
        this.Controls.Add(buttonCancel);

        this.AcceptButton = buttonOk;
        this.CancelButton = buttonCancel;

        textBox.TextChanged += OnTextChanged;
    }

    private void OnTextChanged(object? sender, EventArgs e)
    {
        if (int.TryParse(textBox.Text, out int number))
        {
            if(0 <= number && number <= 100)
            {
                buttonOk.Enabled = true;
                return;
            }
        }
        
        buttonOk.Enabled = false;
    }

    public static DialogResult Show(string title, string prompt, ref string input)
    {
        using (InputDialog dialog = new InputDialog(title, prompt, input))
        {
            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                input = dialog.InputText;
            }

            return result;
        }
    }
}
