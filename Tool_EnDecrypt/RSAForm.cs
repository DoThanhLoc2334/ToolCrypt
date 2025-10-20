using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Tool_EnDecrypt
{
    public partial class RSAForm : Form
    {
        private SimpleRSA rsa;

        TextBox txtKeySize, txtE, txtN, txtD;
        Button btnGen, btnSavePub, btnSavePriv, btnLoadPub, btnLoadPriv;
        TextBox txtPlain, txtCipherB64, txtInFile, txtOutFile;
        Button btnBrowseIn, btnBrowseOut, btnEncText, btnDecText, btnEncFile, btnDecFile;

        public RSAForm()
        {
            InitializeComponent();
            BuildUI();
        }

        private void BuildUI()
        {
            this.Text = "RSA (Academic)";
            this.Width = 700; this.Height = 600;

            var lblKeySize = new Label() { Text = "KeySize (bits)", Left = 20, Top = 20 };
            txtKeySize = new TextBox() { Left = 140, Top = 18, Width = 80, Text = "1024" };

            var lblE = new Label() { Text = "Public e", Left = 240, Top = 20 };
            txtE = new TextBox() { Left = 310, Top = 18, Width = 120, Text = "65537" };

            btnGen = new Button() { Left = 450, Top = 16, Width = 120, Text = "Generate" };
            btnGen.Click += (s, e) => {
                int bits = int.Parse(txtKeySize.Text.Trim());
                var eVal = System.Numerics.BigInteger.Parse(txtE.Text.Trim());
                rsa = SimpleRSA.Generate(bits, eVal);
                txtN.Text = rsa.N.ToString();
                txtD.Text = rsa.D.ToString();
                MessageBox.Show("Key generated.");
            };

            var lblN = new Label() { Text = "Modulus n", Left = 20, Top = 60 };
            txtN = new TextBox() { Left = 20, Top = 80, Width = 650, Height = 60, Multiline = true, ScrollBars = ScrollBars.Vertical };

            var lblD = new Label() { Text = "Private d", Left = 20, Top = 150 };
            txtD = new TextBox() { Left = 20, Top = 170, Width = 650, Height = 60, Multiline = true, ScrollBars = ScrollBars.Vertical };

            btnSavePub = new Button() { Left = 20, Top = 240, Width = 120, Text = "Save Public" };
            btnSavePriv = new Button() { Left = 150, Top = 240, Width = 120, Text = "Save Private" };
            btnLoadPub = new Button() { Left = 280, Top = 240, Width = 120, Text = "Load Public" };
            btnLoadPriv = new Button() { Left = 410, Top = 240, Width = 120, Text = "Load Private" };

            btnSavePub.Click += (s, e) => SaveText("public.txt", $"n={txtN.Text}\nE={txtE.Text}");
            btnSavePriv.Click += (s, e) => SaveText("private.txt", $"n={txtN.Text}\nD={txtD.Text}");
            btnLoadPub.Click += (s, e) => LoadPub();
            btnLoadPriv.Click += (s, e) => LoadPriv();

            var lblPlain = new Label() { Text = "Plain Text", Left = 20, Top = 280 };
            txtPlain = new TextBox() { Left = 20, Top = 300, Width = 300 };
            btnEncText = new Button() { Left = 330, Top = 298, Width = 90, Text = "Encrypt" };
            btnDecText = new Button() { Left = 430, Top = 298, Width = 90, Text = "Decrypt" };
            var lblCipher = new Label() { Text = "Cipher (Base64)", Left = 20, Top = 330 };
            txtCipherB64 = new TextBox() { Left = 20, Top = 350, Width = 650 };

            btnEncText.Click += (s, e) => {
                CheckPub();
                var data = Encoding.UTF8.GetBytes(txtPlain.Text);
                // Chia block: mỗi block < ModulusBytes; để đơn giản: blockSize = ModulusBytes-1
                int k = rsa.ModulusBytes;
                var blocks = CryptoUtils.Chunk(data, k - 1);
                using var ms = new MemoryStream();
                foreach (var b in blocks)
                {
                    var c = rsa.EncryptBlock(b);
                    ms.Write(c, 0, c.Length);
                }
                txtCipherB64.Text = Convert.ToBase64String(ms.ToArray());
            };

            btnDecText.Click += (s, e) => {
                CheckPriv();
                var all = Convert.FromBase64String(txtCipherB64.Text);
                int k = rsa.ModulusBytes;
                if (all.Length % k != 0) { MessageBox.Show("Cipher length invalid."); return; }
                using var ms = new MemoryStream();
                for (int ofs = 0; ofs < all.Length; ofs += k)
                {
                    var block = new byte[k];
                    Buffer.BlockCopy(all, ofs, block, 0, k);
                    var p = rsa.DecryptBlock(block);
                    // strip leading zeros
                    int i = 0; while (i < p.Length && p[i] == 0) i++;
                    var trimmed = new byte[p.Length - i];
                    Buffer.BlockCopy(p, i, trimmed, 0, trimmed.Length);
                    ms.Write(trimmed, 0, trimmed.Length);
                }
                txtPlain.Text = Encoding.UTF8.GetString(ms.ToArray());
            };

            // File encrypt/decrypt
            var lblIn = new Label() { Left = 20, Top = 390, Text = "Input File" };
            txtInFile = new TextBox() { Left = 20, Top = 410, Width = 450 };
            btnBrowseIn = new Button() { Left = 480, Top = 408, Width = 90, Text = "Browse" };
            btnBrowseIn.Click += (s, e) => { using var d = new OpenFileDialog(); if (d.ShowDialog() == DialogResult.OK) txtInFile.Text = d.FileName; };

            var lblOut = new Label() { Left = 20, Top = 440, Text = "Output File" };
            txtOutFile = new TextBox() { Left = 20, Top = 460, Width = 450 };
            btnBrowseOut = new Button() { Left = 480, Top = 458, Width = 90, Text = "Browse" };
            btnBrowseOut.Click += (s, e) => { using var d = new SaveFileDialog(); if (d.ShowDialog() == DialogResult.OK) txtOutFile.Text = d.FileName; };

            btnEncFile = new Button() { Left = 20, Top = 500, Width = 120, Text = "Encrypt File" };
            btnDecFile = new Button() { Left = 150, Top = 500, Width = 120, Text = "Decrypt File" };

            btnEncFile.Click += (s, e) => {
                try
                {
                    CheckPub();
                    var input = File.ReadAllBytes(txtInFile.Text);
                    int k = rsa.ModulusBytes;
                    var blocks = CryptoUtils.Chunk(input, k - 1);
                    using var ms = new MemoryStream();
                    foreach (var b in blocks)
                    {
                        var c = rsa.EncryptBlock(b);
                        ms.Write(c, 0, c.Length);
                    }
                    File.WriteAllBytes(txtOutFile.Text, ms.ToArray());
                    MessageBox.Show("RSA file encryption done.");
                }
                catch (Exception ex) { MessageBox.Show("Enc error: " + ex.Message); }
            };

            btnDecFile.Click += (s, e) => {
                try
                {
                    CheckPriv();
                    var all = File.ReadAllBytes(txtInFile.Text);
                    int k = rsa.ModulusBytes;
                    if (all.Length % k != 0) throw new Exception("Cipher length invalid.");
                    using var ms = new MemoryStream();
                    for (int ofs = 0; ofs < all.Length; ofs += k)
                    {
                        var block = new byte[k];
                        Buffer.BlockCopy(all, ofs, block, 0, k);
                        var p = rsa.DecryptBlock(block);
                        int i = 0; while (i < p.Length && p[i] == 0) i++;
                        var trimmed = new byte[p.Length - i];
                        Buffer.BlockCopy(p, i, trimmed, 0, trimmed.Length);
                        ms.Write(trimmed, 0, trimmed.Length);
                    }
                    File.WriteAllBytes(txtOutFile.Text, ms.ToArray());
                    MessageBox.Show("RSA file decryption done.");
                }
                catch (Exception ex) { MessageBox.Show("Dec error: " + ex.Message); }
            };

            this.Controls.AddRange(new Control[]{
                lblKeySize, txtKeySize, lblE, txtE, btnGen,
                lblN, txtN, lblD, txtD, btnSavePub, btnSavePriv, btnLoadPub, btnLoadPriv,
                lblPlain, txtPlain, btnEncText, btnDecText, lblCipher, txtCipherB64,
                lblIn, txtInFile, btnBrowseIn, lblOut, txtOutFile, btnBrowseOut, btnEncFile, btnDecFile
            });
        }

        private void SaveText(string defaultName, string content)
        {
            using var s = new SaveFileDialog() { FileName = defaultName, Filter = "Text|*.txt" };
            if (s.ShowDialog() == DialogResult.OK) File.WriteAllText(s.FileName, content);
        }

        private void LoadPub()
        {
            using var o = new OpenFileDialog() { Filter = "Text|*.txt" };
            if (o.ShowDialog() != DialogResult.OK) return;
            var t = File.ReadAllText(o.FileName);
            // Định dạng đơn giản: n=..., E=...
            foreach (var line in t.Split('\n'))
            {
                var ln = line.Trim();
                if (ln.StartsWith("n=")) txtN.Text = ln.Substring(2).Trim();
                if (ln.StartsWith("E=")) txtE.Text = ln.Substring(2).Trim();
            }
            // tạo RSA (public-only)
            var n = System.Numerics.BigInteger.Parse(txtN.Text.Trim());
            var e = System.Numerics.BigInteger.Parse(txtE.Text.Trim());
            // Fix for the CS1503 error: Argument 4: cannot convert from 'long' to 'int'.
            // The issue is that `n.GetBitLength()` returns a `long`, but the constructor of `SimpleRSA` expects an `int` for the fourth argument.
            // The fix is to explicitly cast the result of `n.GetBitLength()` to `int`.

            int keyBits = checked((int)n.GetBitLength());
            rsa = new SimpleRSA(n, e, 0, keyBits);

            MessageBox.Show("Public key loaded.");
        }

        private void LoadPriv()
        {
            using var o = new OpenFileDialog() { Filter = "Text|*.txt" };
            if (o.ShowDialog() != DialogResult.OK) return;
            var t = File.ReadAllText(o.FileName);
            foreach (var line in t.Split('\n'))
            {
                var ln = line.Trim();
                if (ln.StartsWith("n=")) txtN.Text = ln.Substring(2).Trim();
                if (ln.StartsWith("D=")) txtD.Text = ln.Substring(2).Trim();
            }
            var n = System.Numerics.BigInteger.Parse(txtN.Text.Trim());
            var d = System.Numerics.BigInteger.Parse(txtD.Text.Trim());
            // mặc định e=65537 để hiển thị, có thể không cần
            txtE.Text = string.IsNullOrWhiteSpace(txtE.Text) ? "65537" : txtE.Text;
            int keyBits = checked((int)n.GetBitLength());
            rsa = new SimpleRSA(n, System.Numerics.BigInteger.Parse(txtE.Text), d, keyBits);

            MessageBox.Show("Private key loaded.");
        }

        private void CheckPub()
        {
            if (rsa == null || rsa.N == 0 || rsa.E == 0) throw new Exception("Public key not ready.");
        }
        private void CheckPriv()
        {
            if (rsa == null || rsa.N == 0 || rsa.D == 0) throw new Exception("Private key not ready.");
        }
    }

    internal static class BigIntBits
    {
        // tiện ích lấy số bit xấp xỉ của N (cho hiển thị)
        public static int GetBitLength(this System.Numerics.BigInteger n)
        {
            var bytes = n.ToByteArray(isUnsigned: true, isBigEndian: true);
            if (bytes.Length == 0) return 0;
            int bits = (bytes.Length - 1) * 8;
            byte msb = bytes[0];
            for (int i = 7; i >= 0; i--) { if ((msb & (1 << i)) != 0) { bits += (i + 1); break; } }
            return bits;
        }
    }
}
