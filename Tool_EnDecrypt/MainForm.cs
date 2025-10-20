using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
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
            //buttonBrowseInput.Click += buttonBrowseInput_Click;
            //buttonBrowseOutput.Click += buttonBrowseOutput_Click;
            buttonEncrypt.Click += buttonEncrypt_Click;
            buttonDecrypt.Click += buttonDecrypt_Click;
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
        private void buttonEncrypt_Click(object sender, EventArgs e)
        {
            try
            {
                string inputPath = textBoxInput.Text;
                string outputPath = textBoxOutput.Text;
                if (string.IsNullOrWhiteSpace(inputPath) || !File.Exists(inputPath))
                {
                    MessageBox.Show("Chưa chọn file input hợp lệ.");
                    return;
                }
                if (string.IsNullOrWhiteSpace(outputPath))
                {
                    MessageBox.Show("Chưa chọn file output.");
                    return;
                }

                string password = Prompt.ShowDialog("Nhập key:", "");
                if (password == null) return;

                byte[] fileData = File.ReadAllBytes(inputPath);

                if (radioButton1.Checked) // === AES ===
                {
                    // derive 16-byte key
                    byte[] key;
                    using (SHA256 sha = SHA256.Create())
                    {
                        var hash = sha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                        key = new byte[16];
                        Array.Copy(hash, 0, key, 0, 16);
                    }

                    // pad PKCS7 (block = 16)
                    int blockSize = 16;
                    int padLen = blockSize - (fileData.Length % blockSize);
                    if (padLen == 0) padLen = 16;
                    byte[] padded = new byte[fileData.Length + padLen];
                    Array.Copy(fileData, padded, fileData.Length);
                    for (int i = fileData.Length; i < padded.Length; i++) padded[i] = (byte)padLen;

                    var aes = new AESAlgorithms(key);

                    byte[] iv = new byte[16];
                    using (var rng = RandomNumberGenerator.Create())
                        rng.GetBytes(iv);

                    byte[] cipher = new byte[padded.Length + 16];
                    Array.Copy(iv, 0, cipher, 0, 16);
                    byte[] prev = (byte[])iv.Clone();
                    byte[] block = new byte[16];

                    for (int offset = 0; offset < padded.Length; offset += 16)
                    {
                        for (int i = 0; i < 16; i++)
                            block[i] = (byte)(padded[offset + i] ^ prev[i]);
                        aes.EncryptBlock(block);
                        Array.Copy(block, 0, cipher, 16 + offset, 16);
                        Array.Copy(block, prev, 16);
                    }

                    File.WriteAllBytes(outputPath, cipher);
                    MessageBox.Show("Encrypt AES xong. IV lưu ở 16 byte đầu file output.");
                }
                else if (radioButton2.Checked) // === DES ===
                {
                    // derive 8-byte key
                    byte[] key;
                    using (SHA1 sha = SHA1.Create())
                    {
                        var hash = sha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                        key = new byte[8];
                        Array.Copy(hash, 0, key, 0, 8);
                    }

                    // pad PKCS7 (block = 8)
                    int blockSize = 8;
                    int padLen = blockSize - (fileData.Length % blockSize);
                    if (padLen == 0) padLen = 8;
                    byte[] padded = new byte[fileData.Length + padLen];
                    Array.Copy(fileData, padded, fileData.Length);
                    for (int i = fileData.Length; i < padded.Length; i++) padded[i] = (byte)padLen;

                    byte[] iv = new byte[8];
                    using (var rng = RandomNumberGenerator.Create())
                        rng.GetBytes(iv);

                    byte[] cipher = new byte[padded.Length + 8];
                    Array.Copy(iv, 0, cipher, 0, 8);
                    byte[] prev = (byte[])iv.Clone();
                    byte[] block = new byte[8];

                    for (int offset = 0; offset < padded.Length; offset += 8)
                    {
                        for (int i = 0; i < 8; i++)
                            block[i] = (byte)(padded[offset + i] ^ prev[i]);

                        string plainBlock = Encoding.ASCII.GetString(block);
                        string cipherBlockStr = des.Encrypt(plainBlock, Encoding.ASCII.GetString(key));
                        byte[] cipherBlock = Encoding.ASCII.GetBytes(cipherBlockStr);

                        Array.Copy(cipherBlock, 0, cipher, 8 + offset, 8);
                        Array.Copy(cipherBlock, prev, 8);
                    }

                    File.WriteAllBytes(outputPath, cipher);
                    MessageBox.Show("Encrypt DES xong. IV lưu ở 8 byte đầu file output.");
                }
                else
                {
                    MessageBox.Show("Chưa chọn thuật toán mã hoá.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi encrypt: " + ex.Message);
            }
        }

        private void buttonDecrypt_Click(object sender, EventArgs e)
        {
            try
            {
                string inputPath = textBoxInput.Text;
                string outputPath = textBoxOutput.Text;
                if (string.IsNullOrWhiteSpace(inputPath) || !File.Exists(inputPath))
                {
                    MessageBox.Show("Chưa chọn file input hợp lệ.");
                    return;
                }
                if (string.IsNullOrWhiteSpace(outputPath))
                {
                    MessageBox.Show("Chưa chọn file output.");
                    return;
                }

                string password = Prompt.ShowDialog("Nhập mật khẩu để giải mã:", "Password");
                if (password == null) return;

                byte[] cipherData = File.ReadAllBytes(inputPath);

                if (radioButton1.Checked) // === AES ===
                {
                    byte[] key;
                    using (SHA256 sha = SHA256.Create())
                    {
                        var hash = sha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                        key = new byte[16];
                        Array.Copy(hash, 0, key, 0, 16);
                    }

                    if (cipherData.Length < 16) throw new Exception("File quá ngắn (thiếu IV).");

                    byte[] iv = new byte[16];
                    Array.Copy(cipherData, 0, iv, 0, 16);
                    byte[] cipher = new byte[cipherData.Length - 16];
                    Array.Copy(cipherData, 16, cipher, 0, cipher.Length);

                    var aes = new AESAlgorithms(key);
                    byte[] plainPadded = new byte[cipher.Length];
                    byte[] prev = (byte[])iv.Clone();
                    byte[] block = new byte[16];

                    for (int offset = 0; offset < cipher.Length; offset += 16)
                    {
                        Array.Copy(cipher, offset, block, 0, 16);
                        aes.DecryptBlock(block);
                        for (int i = 0; i < 16; i++)
                            plainPadded[offset + i] = (byte)(block[i] ^ prev[i]);
                        Array.Copy(cipher, offset, prev, 0, 16);
                    }

                    int padLen = plainPadded[^1];
                    if (padLen < 1 || padLen > 16) throw new Exception("Sai padding hoặc mật khẩu.");
                    File.WriteAllBytes(outputPath, plainPadded[..^padLen]);
                    MessageBox.Show("Decrypt AES xong.");
                }
                else if (radioButton2.Checked) // === DES ===
                {
                    byte[] key;
                    using (SHA1 sha = SHA1.Create())
                    {
                        var hash = sha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                        key = new byte[8];
                        Array.Copy(hash, 0, key, 0, 8);
                    }

                    if (cipherData.Length < 8) throw new Exception("File quá ngắn (thiếu IV).");

                    byte[] iv = new byte[8];
                    Array.Copy(cipherData, 0, iv, 0, 8);
                    byte[] cipher = new byte[cipherData.Length - 8];
                    Array.Copy(cipherData, 8, cipher, 0, cipher.Length);

                    byte[] plainPadded = new byte[cipher.Length];
                    byte[] prev = (byte[])iv.Clone();
                    byte[] block = new byte[8];

                    for (int offset = 0; offset < cipher.Length; offset += 8)
                    {
                        Array.Copy(cipher, offset, block, 0, 8);

                        string cipherBlockStr = Encoding.ASCII.GetString(block);
                        string plainBlockStr = des.Decrypt(cipherBlockStr, Encoding.ASCII.GetString(key));
                        byte[] plainBlock = Encoding.ASCII.GetBytes(plainBlockStr);

                        for (int i = 0; i < 8; i++)
                            plainPadded[offset + i] = (byte)(plainBlock[i] ^ prev[i]);

                        Array.Copy(block, prev, 8);
                    }

                    int padLen = plainPadded[^1];
                    if (padLen < 1 || padLen > 8) throw new Exception("Sai padding hoặc mật khẩu.");
                    File.WriteAllBytes(outputPath, plainPadded[..^padLen]);
                    MessageBox.Show("Decrypt DES xong.");
                }
                else
                {
                    MessageBox.Show("Chưa chọn thuật toán.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi decrypt: " + ex.Message);
            }
        }

    }
}
