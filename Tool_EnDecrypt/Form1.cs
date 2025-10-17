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
                string ivText = txtAESIV.Text.Trim();

                if (string.IsNullOrEmpty(inputPath) || string.IsNullOrEmpty(outputPath))
                {
                    MessageBox.Show("⚠ Please select both input and output file paths!");
                    return;
                }

                if (string.IsNullOrEmpty(keyText) || string.IsNullOrEmpty(ivText))
                {
                    MessageBox.Show("⚠ Please enter both Key and IV!");
                    return;
                }

                byte[] key = Encoding.UTF8.GetBytes(keyText);
                byte[] iv = Encoding.UTF8.GetBytes(ivText);

                // Kiểm tra độ dài key và IV
                if (key.Length != 16 && key.Length != 24 && key.Length != 32)
                {
                    MessageBox.Show("❌ AES key must be 16, 24, or 32 bytes!");
                    return;
                }

                if (iv.Length != 16)
                {
                    MessageBox.Show("❌ AES IV must be 16 bytes!");
                    return;
                }

                byte[] plainBytes = File.ReadAllBytes(inputPath);

                using (Aes aes = Aes.Create())
                {
                    aes.Key = key;
                    aes.IV = iv;
                    aes.Padding = PaddingMode.PKCS7;

                    using (FileStream fs = new FileStream(outputPath, FileMode.Create))
                    using (CryptoStream cs = new CryptoStream(fs, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(plainBytes, 0, plainBytes.Length);
                    }
                }

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
                string ivText = txtAESIV.Text.Trim();

                if (string.IsNullOrEmpty(inputPath) || string.IsNullOrEmpty(outputPath))
                {
                    MessageBox.Show("⚠ Please select both input and output file paths!");
                    return;
                }

                if (string.IsNullOrEmpty(keyText) || string.IsNullOrEmpty(ivText))
                {
                    MessageBox.Show("⚠ Please enter both Key and IV!");
                    return;
                }

                byte[] key = Encoding.UTF8.GetBytes(keyText);
                byte[] iv = Encoding.UTF8.GetBytes(ivText);

                if (key.Length != 16 && key.Length != 24 && key.Length != 32)
                {
                    MessageBox.Show("❌ AES key must be 16, 24, or 32 bytes!");
                    return;
                }

                if (iv.Length != 16)
                {
                    MessageBox.Show("❌ AES IV must be 16 bytes!");
                    return;
                }

                byte[] cipherBytes = File.ReadAllBytes(inputPath);

                using (Aes aes = Aes.Create())
                {
                    aes.Key = key;
                    aes.IV = iv;
                    aes.Padding = PaddingMode.PKCS7;

                    using (FileStream fs = new FileStream(outputPath, FileMode.Create))
                    using (CryptoStream cs = new CryptoStream(fs, aes.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                    }
                }

                MessageBox.Show("✅ Decryption completed successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error during decryption: " + ex.Message);
            }
        }
    }
}
