namespace Tool_EnDecrypt
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            label1 = new Label();
            rbtnAES = new RadioButton();
            radioButton2 = new RadioButton();
            radioButton3 = new RadioButton();
            radioButton4 = new RadioButton();
            label2 = new Label();
            btnBrowseInput = new Button();
            txtInputFile = new TextBox();
            txtOutputFile = new TextBox();
            label3 = new Label();
            label4 = new Label();
            btnBrowseOutput = new Button();
            groupAES = new GroupBox();
            btnAESDecrypt = new Button();
            btnAESEncrypt = new Button();
            txtAESIV = new TextBox();
            label6 = new Label();
            label5 = new Label();
            txtAESKey = new TextBox();
            errorProvider1 = new ErrorProvider(components);
            groupBox1 = new GroupBox();
            button2 = new Button();
            button1 = new Button();
            label7 = new Label();
            groupAES.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = SystemColors.GradientInactiveCaption;
            label1.Location = new Point(563, 53);
            label1.Name = "label1";
            label1.Size = new Size(146, 20);
            label1.TabIndex = 0;
            label1.Text = "Welcome to my Tool\r\n";
            // 
            // rbtnAES
            // 
            rbtnAES.AutoSize = true;
            rbtnAES.Location = new Point(1053, 199);
            rbtnAES.Name = "rbtnAES";
            rbtnAES.Size = new Size(56, 24);
            rbtnAES.TabIndex = 1;
            rbtnAES.TabStop = true;
            rbtnAES.Text = "AES\r\n";
            rbtnAES.UseVisualStyleBackColor = true;
            rbtnAES.CheckedChanged += rbtnAES_CheckedChanged;
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Location = new Point(1052, 267);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(57, 24);
            radioButton2.TabIndex = 2;
            radioButton2.TabStop = true;
            radioButton2.Text = "DES\r\n";
            radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton3
            // 
            radioButton3.AutoSize = true;
            radioButton3.Location = new Point(1052, 329);
            radioButton3.Name = "radioButton3";
            radioButton3.Size = new Size(98, 24);
            radioButton3.TabIndex = 3;
            radioButton3.TabStop = true;
            radioButton3.Text = "Triple DES";
            radioButton3.UseVisualStyleBackColor = true;
            // 
            // radioButton4
            // 
            radioButton4.AutoSize = true;
            radioButton4.Location = new Point(1053, 411);
            radioButton4.Name = "radioButton4";
            radioButton4.Size = new Size(57, 24);
            radioButton4.TabIndex = 4;
            radioButton4.TabStop = true;
            radioButton4.Text = "RSA";
            radioButton4.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = SystemColors.GradientInactiveCaption;
            label2.Location = new Point(1052, 151);
            label2.Name = "label2";
            label2.Size = new Size(76, 20);
            label2.TabIndex = 5;
            label2.Text = "Algorithm";
            // 
            // btnBrowseInput
            // 
            btnBrowseInput.Location = new Point(333, 191);
            btnBrowseInput.Name = "btnBrowseInput";
            btnBrowseInput.Size = new Size(94, 29);
            btnBrowseInput.TabIndex = 6;
            btnBrowseInput.Text = "Browse";
            btnBrowseInput.UseVisualStyleBackColor = true;
            btnBrowseInput.Click += btnBrowseInput_Click;
            // 
            // txtInputFile
            // 
            txtInputFile.Location = new Point(70, 191);
            txtInputFile.Name = "txtInputFile";
            txtInputFile.Size = new Size(267, 27);
            txtInputFile.TabIndex = 8;
            // 
            // txtOutputFile
            // 
            txtOutputFile.Location = new Point(70, 320);
            txtOutputFile.Name = "txtOutputFile";
            txtOutputFile.Size = new Size(267, 27);
            txtOutputFile.TabIndex = 9;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = SystemColors.GradientInactiveCaption;
            label3.Location = new Point(70, 168);
            label3.Name = "label3";
            label3.Size = new Size(70, 20);
            label3.TabIndex = 10;
            label3.Text = "Input File";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = SystemColors.GradientInactiveCaption;
            label4.Location = new Point(70, 297);
            label4.Name = "label4";
            label4.Size = new Size(82, 20);
            label4.TabIndex = 11;
            label4.Text = "Output File";
            // 
            // btnBrowseOutput
            // 
            btnBrowseOutput.Location = new Point(333, 318);
            btnBrowseOutput.Name = "btnBrowseOutput";
            btnBrowseOutput.Size = new Size(94, 29);
            btnBrowseOutput.TabIndex = 12;
            btnBrowseOutput.Text = "Browse";
            btnBrowseOutput.UseVisualStyleBackColor = true;
            btnBrowseOutput.Click += btnBrowseOutput_Click;
            // 
            // groupAES
            // 
            groupAES.BackColor = SystemColors.AppWorkspace;
            groupAES.Controls.Add(btnAESDecrypt);
            groupAES.Controls.Add(btnAESEncrypt);
            groupAES.Controls.Add(txtAESIV);
            groupAES.Controls.Add(label6);
            groupAES.Controls.Add(label5);
            groupAES.Controls.Add(txtAESKey);
            groupAES.Location = new Point(475, 105);
            groupAES.Name = "groupAES";
            groupAES.Size = new Size(548, 381);
            groupAES.TabIndex = 13;
            groupAES.TabStop = false;
            groupAES.Text = "AES Tool";
            groupAES.Visible = false;
            // 
            // btnAESDecrypt
            // 
            btnAESDecrypt.Location = new Point(404, 321);
            btnAESDecrypt.Name = "btnAESDecrypt";
            btnAESDecrypt.Size = new Size(94, 29);
            btnAESDecrypt.TabIndex = 5;
            btnAESDecrypt.Text = "Decrypt";
            btnAESDecrypt.UseVisualStyleBackColor = true;
            btnAESDecrypt.Click += btnAESDecrypt_Click;
            // 
            // btnAESEncrypt
            // 
            btnAESEncrypt.Location = new Point(181, 321);
            btnAESEncrypt.Name = "btnAESEncrypt";
            btnAESEncrypt.Size = new Size(94, 29);
            btnAESEncrypt.TabIndex = 4;
            btnAESEncrypt.Text = "Encrypt";
            btnAESEncrypt.UseVisualStyleBackColor = true;
            btnAESEncrypt.Click += btnAESEncrypt_Click;
            // 
            // txtAESIV
            // 
            txtAESIV.Location = new Point(181, 175);
            txtAESIV.Name = "txtAESIV";
            txtAESIV.Size = new Size(295, 27);
            txtAESIV.TabIndex = 3;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(66, 182);
            label6.Name = "label6";
            label6.Size = new Size(92, 20);
            label6.TabIndex = 2;
            label6.Text = "Initial Vector";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(107, 101);
            label5.Name = "label5";
            label5.Size = new Size(33, 20);
            label5.TabIndex = 1;
            label5.Text = "Key";
            // 
            // txtAESKey
            // 
            txtAESKey.Location = new Point(181, 98);
            txtAESKey.Name = "txtAESKey";
            txtAESKey.Size = new Size(295, 27);
            txtAESKey.TabIndex = 0;
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // groupBox1
            // 
            groupBox1.BackColor = SystemColors.ButtonFace;
            groupBox1.Controls.Add(button2);
            groupBox1.Controls.Add(button1);
            groupBox1.Controls.Add(label7);
            groupBox1.Location = new Point(28, 94);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(548, 292);
            groupBox1.TabIndex = 14;
            groupBox1.TabStop = false;
            groupBox1.Text = "groupBox1";
            groupBox1.Visible = false;
            // 
            // button2
            // 
            button2.BackColor = Color.BurlyWood;
            button2.Location = new Point(305, 218);
            button2.Name = "button2";
            button2.Size = new Size(94, 29);
            button2.TabIndex = 2;
            button2.Text = "Decrypt";
            button2.UseVisualStyleBackColor = false;
            // 
            // button1
            // 
            button1.BackColor = Color.BurlyWood;
            button1.Location = new Point(92, 218);
            button1.Name = "button1";
            button1.Size = new Size(94, 29);
            button1.TabIndex = 1;
            button1.Text = "Encrypt\r\n";
            button1.UseVisualStyleBackColor = false;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(224, 39);
            label7.Name = "label7";
            label7.Size = new Size(67, 20);
            label7.TabIndex = 0;
            label7.Text = "DES tool";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1263, 632);
            Controls.Add(groupBox1);
            Controls.Add(groupAES);
            Controls.Add(btnBrowseOutput);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(txtOutputFile);
            Controls.Add(txtInputFile);
            Controls.Add(btnBrowseInput);
            Controls.Add(label2);
            Controls.Add(radioButton4);
            Controls.Add(radioButton3);
            Controls.Add(radioButton2);
            Controls.Add(rbtnAES);
            Controls.Add(label1);
            Name = "Form1";
            Text = "MainForm";
            groupAES.ResumeLayout(false);
            groupAES.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private Label label1;
        private RadioButton rbtnAES;
        private RadioButton radioButton2;
        private RadioButton radioButton3;
        private RadioButton radioButton4;
        private Label label2;
        private Button btnBrowseInput;
        private TextBox txtInputFile;
        private TextBox txtOutputFile;
        private Label label3;
        private Label label4;
        private Button btnBrowseOutput;
        private GroupBox groupAES;
        private TextBox txtAESIV;
        private Label label6;
        private Label label5;
        private TextBox txtAESKey;
        private Button btnAESDecrypt;
        private Button btnAESEncrypt;
        private ErrorProvider errorProvider1;
        private GroupBox groupBox1;
        private Label label7;
        private Button button2;
        private Button button1;
    }
}
