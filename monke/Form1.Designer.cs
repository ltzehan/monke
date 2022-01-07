namespace monke
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.keyboardComboBox = new System.Windows.Forms.ComboBox();
            this.updateKeyboard = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // keyboardComboBox
            // 
            this.keyboardComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.keyboardComboBox.FormattingEnabled = true;
            this.keyboardComboBox.Location = new System.Drawing.Point(12, 50);
            this.keyboardComboBox.Name = "keyboardComboBox";
            this.keyboardComboBox.Size = new System.Drawing.Size(225, 28);
            this.keyboardComboBox.TabIndex = 0;
            this.keyboardComboBox.SelectedIndexChanged += new System.EventHandler(this.keyboardChange);
            // 
            // updateKeyboard
            // 
            this.updateKeyboard.Location = new System.Drawing.Point(12, 101);
            this.updateKeyboard.Name = "updateKeyboard";
            this.updateKeyboard.Size = new System.Drawing.Size(94, 29);
            this.updateKeyboard.TabIndex = 2;
            this.updateKeyboard.Text = "button1";
            this.updateKeyboard.UseVisualStyleBackColor = true;
            this.updateKeyboard.Click += new System.EventHandler(this.submitKeyboard);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Keyboard";
            //
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.updateKeyboard);
            this.Controls.Add(this.keyboardComboBox);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.onFormClosing);
            this.Load += new System.EventHandler(this.onFormLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox keyboardComboBox;
        private System.Windows.Forms.Button updateKeyboard;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    }
}
