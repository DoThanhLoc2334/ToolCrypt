using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tool_EnDecrypt
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            buttonBrowseInput.Click += buttonBrowseInput_Click;
            buttonBrowseOutput.Click += buttonBrowseOutput_Click;
        }

        private void buttonBrowseInput_Click(object sender, EventArgs e)
        {
            // Hộp thoại chọn file input
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Chọn file đầu vào";
            openFileDialog.Filter = "Tất cả các file (*.*)|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                textBoxInput.Text = openFileDialog.FileName;
            }
        }

        private void buttonBrowseOutput_Click(object sender, EventArgs e)
        {
            // Hộp thoại chọn vị trí lưu file output
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Chọn nơi lưu file đầu ra";
            saveFileDialog.Filter = "Tất cả các file (*.*)|*.*";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                textBoxOutput.Text = saveFileDialog.FileName;
            }
        }
    }
}
