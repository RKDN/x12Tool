namespace x12Tool {
    partial class ConfigMenu {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.ColorPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.okBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.FontPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.FlagPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // ColorPanel
            // 
            this.ColorPanel.AutoScroll = true;
            this.ColorPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ColorPanel.Location = new System.Drawing.Point(12, 31);
            this.ColorPanel.Name = "ColorPanel";
            this.ColorPanel.Size = new System.Drawing.Size(245, 283);
            this.ColorPanel.TabIndex = 0;
            // 
            // okBtn
            // 
            this.okBtn.Location = new System.Drawing.Point(141, 320);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(118, 23);
            this.okBtn.TabIndex = 1;
            this.okBtn.Text = "OK";
            this.okBtn.UseVisualStyleBackColor = true;
            this.okBtn.Click += new System.EventHandler(this.okBtn_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.Location = new System.Drawing.Point(265, 320);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(118, 23);
            this.cancelBtn.TabIndex = 2;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // FontPanel
            // 
            this.FontPanel.AutoScroll = true;
            this.FontPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FontPanel.Location = new System.Drawing.Point(263, 31);
            this.FontPanel.Name = "FontPanel";
            this.FontPanel.Size = new System.Drawing.Size(245, 142);
            this.FontPanel.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Colors:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(262, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Fonts:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(262, 176);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Flags:";
            // 
            // FlagPanel
            // 
            this.FlagPanel.AutoScroll = true;
            this.FlagPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FlagPanel.Location = new System.Drawing.Point(263, 192);
            this.FlagPanel.Name = "FlagPanel";
            this.FlagPanel.Size = new System.Drawing.Size(245, 122);
            this.FlagPanel.TabIndex = 6;
            // 
            // ConfigMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 351);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.FlagPanel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.FontPanel);
            this.Controls.Add(this.okBtn);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.ColorPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfigMenu";
            this.ShowIcon = false;
            this.Text = "ConfigMenu";
            this.Load += new System.EventHandler(this.ConfigMenu_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel ColorPanel;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button okBtn;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.FontDialog fontDialog1;
        private System.Windows.Forms.FlowLayoutPanel FontPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.FlowLayoutPanel FlagPanel;
    }
}