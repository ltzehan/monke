namespace monke
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.label1 = new System.Windows.Forms.Label();
            this.keyboardComboBox = new System.Windows.Forms.ComboBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.subtitlesCheckbox = new System.Windows.Forms.CheckBox();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(16, 149);
            this.label1.Margin = new System.Windows.Forms.Padding(16, 8, 16, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 25);
            this.label1.TabIndex = 3;
            this.label1.Text = "Key switches";
            // 
            // keyboardComboBox
            // 
            this.keyboardComboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(195)))), ((int)(((byte)(217)))));
            this.keyboardComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.keyboardComboBox.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.keyboardComboBox.FormattingEnabled = true;
            this.keyboardComboBox.Location = new System.Drawing.Point(16, 182);
            this.keyboardComboBox.Margin = new System.Windows.Forms.Padding(16, 0, 16, 16);
            this.keyboardComboBox.Name = "keyboardComboBox";
            this.keyboardComboBox.Size = new System.Drawing.Size(225, 33);
            this.keyboardComboBox.TabIndex = 0;
            this.keyboardComboBox.SelectedIndexChanged += new System.EventHandler(this.OnKeyboardChange);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this.panel1);
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Controls.Add(this.keyboardComboBox);
            this.flowLayoutPanel1.Controls.Add(this.label2);
            this.flowLayoutPanel1.Controls.Add(this.subtitlesCheckbox);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(272, 321);
            this.flowLayoutPanel1.TabIndex = 4;
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 16);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(272, 125);
            this.panel1.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(16, 239);
            this.label2.Margin = new System.Windows.Forms.Padding(16, 8, 16, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(195, 25);
            this.label2.TabIndex = 7;
            this.label2.Text = "Accessibility Settings";
            // 
            // subtitlesCheckbox
            // 
            this.subtitlesCheckbox.AutoSize = true;
            this.subtitlesCheckbox.Location = new System.Drawing.Point(16, 276);
            this.subtitlesCheckbox.Margin = new System.Windows.Forms.Padding(16, 4, 16, 4);
            this.subtitlesCheckbox.Name = "subtitlesCheckbox";
            this.subtitlesCheckbox.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.subtitlesCheckbox.Size = new System.Drawing.Size(93, 24);
            this.subtitlesCheckbox.TabIndex = 8;
            this.subtitlesCheckbox.Text = "Subtitles";
            this.subtitlesCheckbox.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(195)))), ((int)(((byte)(217)))));
            this.ClientSize = new System.Drawing.Size(272, 319);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormClosing);
            this.Load += new System.EventHandler(this.OnFormLoad);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox keyboardComboBox;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox subtitlesCheckbox;
    }
}
