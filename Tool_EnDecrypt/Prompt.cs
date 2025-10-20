using System;
using System.Windows.Forms;

namespace Tool_EnDecrypt
{
    public static class Prompt
    {
        public static string ShowDialog(string text, string caption)
        {
            Form prompt = new Form()
            {
                Width = 420,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterParent
            };
            Label textLabel = new Label() { Left = 10, Top = 10, Text = text, AutoSize = true };
            TextBox inputBox = new TextBox() { Left = 10, Top = 35, Width = 380 };
            inputBox.UseSystemPasswordChar = true;
            Button confirmation = new Button() { Text = "OK", Left = 240, Width = 75, Top = 65, DialogResult = DialogResult.OK };
            Button cancel = new Button() { Text = "Cancel", Left = 320, Width = 75, Top = 65, DialogResult = DialogResult.Cancel };
            prompt.Controls.Add(textLabel);
            prompt.Controls.Add(inputBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(cancel);
            prompt.AcceptButton = confirmation;
            var dr = prompt.ShowDialog();
            return dr == DialogResult.OK ? inputBox.Text : null;
        }
    }
}
