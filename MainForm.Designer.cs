namespace Tool_EnDecrypt
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            textBoxInput = new TextBox();
            textBoxOutput = new TextBox();
            radioButton1 = new RadioButton();
            radioButton2 = new RadioButton();
            buttonBrowseInput = new Button();
            buttonBrowseOutput = new Button();
            label3 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(126, 113);
            label1.Name = "label1";
            label1.Size = new Size(70, 20);
            label1.TabIndex = 0;
            label1.Text = "Input File";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(126, 196);
            label2.Name = "label2";
            label2.Size = new Size(82, 20);
            label2.TabIndex = 1;
            label2.Text = "Output File";
            // 
            // textBoxInput
            // 
            textBoxInput.Location = new Point(126, 136);
            textBoxInput.Name = "textBoxInput";
            textBoxInput.Size = new Size(286, 27);
            textBoxInput.TabIndex = 2;
            // 
            // textBoxOutput
            // 
            textBoxOutput.Location = new Point(126, 219);
            textBoxOutput.Name = "textBoxOutput";
            textBoxOutput.Size = new Size(286, 27);
            textBoxOutput.TabIndex = 3;
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.Location = new Point(852, 175);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(56, 24);
            radioButton1.TabIndex = 4;
            radioButton1.TabStop = true;
            radioButton1.Text = "AES";
            radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Location = new Point(852, 249);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(57, 24);
            radioButton2.TabIndex = 5;
            radioButton2.TabStop = true;
            radioButton2.Text = "DES";
            radioButton2.UseVisualStyleBackColor = true;
            // 
            // buttonBrowseInput
            // 
            buttonBrowseInput.Location = new Point(408, 136);
            buttonBrowseInput.Name = "buttonBrowseInput";
            buttonBrowseInput.Size = new Size(94, 29);
            buttonBrowseInput.TabIndex = 6;
            buttonBrowseInput.Text = "Browse";
            buttonBrowseInput.UseVisualStyleBackColor = true;
            buttonBrowseInput.Click += buttonBrowseInput_Click;
            // 
            // buttonBrowseOutput
            // 
            buttonBrowseOutput.Location = new Point(408, 217);
            buttonBrowseOutput.Name = "buttonBrowseOutput";
            buttonBrowseOutput.Size = new Size(94, 29);
            buttonBrowseOutput.TabIndex = 7;
            buttonBrowseOutput.Text = "Browse";
            buttonBrowseOutput.UseVisualStyleBackColor = true;
            buttonBrowseOutput.Click += buttonBrowseOutput_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(866, 136);
            label3.Name = "label3";
            label3.Size = new Size(76, 20);
            label3.TabIndex = 8;
            label3.Text = "Algorithm";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1357, 497);
            Controls.Add(label3);
            Controls.Add(buttonBrowseOutput);
            Controls.Add(buttonBrowseInput);
            Controls.Add(radioButton2);
            Controls.Add(radioButton1);
            Controls.Add(textBoxOutput);
            Controls.Add(textBoxInput);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "MainForm";
            Text = "MainForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox textBoxInput;
        private TextBox textBoxOutput;
        private RadioButton radioButton1;
        private RadioButton radioButton2;
        private Button buttonBrowseInput;
        private Button buttonBrowseOutput;
        private Label label3;
    }
}