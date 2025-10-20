using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace Tool_EnDecrypt
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // Ẩn GroupBox AES ban đầu
            groupAES.Visible = false;

            // Gán sự kiện cho RadioButton
            rbtnAES.CheckedChanged += rbtnAES_CheckedChanged;

            // Gán sự kiện cho 2 nút Encrypt / Decrypt
            btnAESEncrypt.Click += btnAESEncrypt_Click;
            btnAESDecrypt.Click += btnAESDecrypt_Click;

        }

        private void rbtnAES_CheckedChanged(object sender, EventArgs e)
        {
            groupAES.Visible = rbtnAES.Checked;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            groupAES.Visible = false;
        }

        private void btnBrowseInput_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select Input File";
            openFileDialog.Filter = "All Files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtInputFile.Text = openFileDialog.FileName;
            }
        }

        private void btnBrowseOutput_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Select Output File";
            saveFileDialog.Filter = "All Files (*.*)|*.*";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtOutputFile.Text = saveFileDialog.FileName;
            }
        }

        // ==================== AES ENCRYPT ====================
        private void btnAESEncrypt_Click(object sender, EventArgs e)
        {
            try
            {
                string inputPath = txtInputFile.Text;
                string outputPath = txtOutputFile.Text;
                string keyText = txtAESKey.Text.Trim();

                string plainText = File.ReadAllText(inputPath);
                string encrypted = SimpleAES.Encrypt(plainText, keyText);
                File.WriteAllText(outputPath, encrypted);

                MessageBox.Show("✅ Encryption completed successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error during encryption: " + ex.Message);
            }
        }


        // ==================== AES DECRYPT ====================
        private void btnAESDecrypt_Click(object sender, EventArgs e)
        {
            try
            {
                string inputPath = txtInputFile.Text;
                string outputPath = txtOutputFile.Text;
                string keyText = txtAESKey.Text.Trim();

                string cipherText = File.ReadAllText(inputPath);
                string decrypted = SimpleAES.Decrypt(cipherText, keyText);
                File.WriteAllText(outputPath, decrypted);

                MessageBox.Show("✅ Decryption completed successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error during decryption: " + ex.Message);
            }
        }


    }
}
