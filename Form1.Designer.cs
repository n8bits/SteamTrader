namespace SteamTrader
{
    partial class Form1
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
            this.GoButton = new System.Windows.Forms.Button();
            this.CancelListingsButton = new System.Windows.Forms.Button();
            this.StopButton = new System.Windows.Forms.Button();
            this.MakeGemsButton = new System.Windows.Forms.Button();
            this.CaseSimulatorButton = new System.Windows.Forms.Button();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // GoButton
            // 
            this.GoButton.Location = new System.Drawing.Point(12, 12);
            this.GoButton.Name = "GoButton";
            this.GoButton.Size = new System.Drawing.Size(149, 62);
            this.GoButton.TabIndex = 0;
            this.GoButton.Text = "Go!";
            this.GoButton.UseVisualStyleBackColor = true;
            this.GoButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // CancelListingsButton
            // 
            this.CancelListingsButton.Location = new System.Drawing.Point(167, 12);
            this.CancelListingsButton.Name = "CancelListingsButton";
            this.CancelListingsButton.Size = new System.Drawing.Size(149, 62);
            this.CancelListingsButton.TabIndex = 1;
            this.CancelListingsButton.Text = "Cancel Listings";
            this.CancelListingsButton.UseVisualStyleBackColor = true;
            this.CancelListingsButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // StopButton
            // 
            this.StopButton.Enabled = false;
            this.StopButton.Location = new System.Drawing.Point(447, 12);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(149, 62);
            this.StopButton.TabIndex = 2;
            this.StopButton.Text = "Stop";
            this.StopButton.UseVisualStyleBackColor = true;
            this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // MakeGemsButton
            // 
            this.MakeGemsButton.Location = new System.Drawing.Point(12, 80);
            this.MakeGemsButton.Name = "MakeGemsButton";
            this.MakeGemsButton.Size = new System.Drawing.Size(149, 62);
            this.MakeGemsButton.TabIndex = 3;
            this.MakeGemsButton.Text = "Make Gems";
            this.MakeGemsButton.UseVisualStyleBackColor = true;
            this.MakeGemsButton.Click += new System.EventHandler(this.MakeGemsButton_Click);
            // 
            // CaseSimulatorButton
            // 
            this.CaseSimulatorButton.Location = new System.Drawing.Point(167, 80);
            this.CaseSimulatorButton.Name = "CaseSimulatorButton";
            this.CaseSimulatorButton.Size = new System.Drawing.Size(149, 62);
            this.CaseSimulatorButton.TabIndex = 4;
            this.CaseSimulatorButton.Text = "Open Fake Cases";
            this.CaseSimulatorButton.UseVisualStyleBackColor = true;
            this.CaseSimulatorButton.Click += new System.EventHandler(this.CaseSimulatorButton_Click);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(6, 19);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(69, 17);
            this.radioButton1.TabIndex = 5;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Undercut";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(6, 42);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(68, 17);
            this.radioButton2.TabIndex = 6;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Quicksell";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(6, 65);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(53, 17);
            this.radioButton3.TabIndex = 7;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "Mixed";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Controls.Add(this.radioButton3);
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Location = new System.Drawing.Point(450, 137);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(118, 105);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(643, 269);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.CaseSimulatorButton);
            this.Controls.Add(this.MakeGemsButton);
            this.Controls.Add(this.StopButton);
            this.Controls.Add(this.CancelListingsButton);
            this.Controls.Add(this.GoButton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button GoButton;
        private System.Windows.Forms.Button CancelListingsButton;
        private System.Windows.Forms.Button StopButton;
        private System.Windows.Forms.Button MakeGemsButton;
        private System.Windows.Forms.Button CaseSimulatorButton;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}

