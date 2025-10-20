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
            Build3DESUI();

            // Bật/tắt group theo RadioButton
            rbtnAES.CheckedChanged += (s, e) => { groupAES.Visible = rbtnAES.Checked; group3DES.Visible = false; };
            radioButton3.CheckedChanged += (s, e) => { group3DES.Visible = radioButton3.Checked; groupAES.Visible = false; };

            // Mở Form RSA khi chọn radioButton4
            radioButton4.CheckedChanged += (s, e) => {
                if (radioButton4.Checked)
                {
                    using (var f = new RSAForm()) { f.ShowDialog(this); }
                    radioButton4.Checked = false; // trở lại main form
                }
            };


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
        private void Build3DESUI()
        {
            group3DES = new GroupBox
            {
                Text = "Triple DES (EDE-CBC)",
                Location = new Point(475, 105),
                Size = new Size(548, 381),
                Visible = false
            };
            var lblKey = new Label { Location = new Point(86, 100), Text = "Key (16/24 bytes)" };
            txt3DESKey = new TextBox { Location = new Point(181, 98), Size = new Size(295, 27) };
            var lblIV = new Label { Location = new Point(106, 175), Text = "IV (8 bytes)" };
            txt3DESIV = new TextBox { Location = new Point(181, 173), Size = new Size(295, 27) };
            btn3DESEncrypt = new Button { Location = new Point(181, 321), Size = new Size(94, 29), Text = "Encrypt" };
            btn3DESDecrypt = new Button { Location = new Point(404, 321), Size = new Size(94, 29), Text = "Decrypt" };

            btn3DESEncrypt.Click += btn3DESEncrypt_Click;
            btn3DESDecrypt.Click += btn3DESDecrypt_Click;

            group3DES.Controls.AddRange(new Control[] { lblKey, txt3DESKey, lblIV, txt3DESIV, btn3DESEncrypt, btn3DESDecrypt });
            this.Controls.Add(group3DES);
        }
        private void btn3DESEncrypt_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtInputFile.Text) || string.IsNullOrWhiteSpace(txtOutputFile.Text))
                { MessageBox.Show("Please select input/output file."); return; }

                var key = Encoding.UTF8.GetBytes(txt3DESKey.Text.Trim());
                var iv = Encoding.UTF8.GetBytes(txt3DESIV.Text.Trim());

                if (!(key.Length == 16 || key.Length == 24)) { MessageBox.Show("3DES key must be 16 or 24 bytes."); return; }
                if (iv.Length != 8) { MessageBox.Show("3DES IV must be 8 bytes."); return; }

                TripleDESEngine.EncryptFile(txtInputFile.Text, txtOutputFile.Text, key, iv);
                MessageBox.Show("✅ 3DES encryption done.");
            }
            catch (Exception ex) { MessageBox.Show("3DES Encrypt error: " + ex.Message); }
        }

        private void btn3DESDecrypt_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtInputFile.Text) || string.IsNullOrWhiteSpace(txtOutputFile.Text))
                { MessageBox.Show("Please select input/output file."); return; }

                var key = Encoding.UTF8.GetBytes(txt3DESKey.Text.Trim());
                var iv = Encoding.UTF8.GetBytes(txt3DESIV.Text.Trim());

                if (!(key.Length == 16 || key.Length == 24)) { MessageBox.Show("3DES key must be 16 or 24 bytes."); return; }
                if (iv.Length != 8) { MessageBox.Show("3DES IV must be 8 bytes."); return; }

                TripleDESEngine.DecryptFile(txtInputFile.Text, txtOutputFile.Text, key, iv);
                MessageBox.Show("✅ 3DES decryption done.");
            }
            catch (Exception ex) { MessageBox.Show("3DES Decrypt error: " + ex.Message); }
        }


    }
}
