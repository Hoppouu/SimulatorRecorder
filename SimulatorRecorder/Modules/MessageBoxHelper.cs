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
}
