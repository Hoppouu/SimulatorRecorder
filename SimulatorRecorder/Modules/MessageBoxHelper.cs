using SimulatorRecorder.Modules;

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
}
