namespace x12Tool {
    partial class ReplaceDialog {
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
            this.segmentTypesDropDown = new System.Windows.Forms.ComboBox();
            this.elementNumberDropDown = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.regexFilterTxt = new System.Windows.Forms.TextBox();
            this.resultsList = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.findBtn = new System.Windows.Forms.Button();
            this.ReplaceBtn = new System.Windows.Forms.Button();
            this.secondaryIdDropdown = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.replaceTxt = new System.Windows.Forms.TextBox();
            this.errorTxt = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.fakerFields = new System.Windows.Forms.ListView();
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lengthVal = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.generatedRadio = new System.Windows.Forms.RadioButton();
            this.replaceRadio = new System.Windows.Forms.RadioButton();
            this.StatusTxt = new System.Windows.Forms.Label();
            this.pBar = new System.Windows.Forms.ProgressBar();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lengthVal)).BeginInit();
            this.SuspendLayout();
            // 
            // segmentTypesDropDown
            // 
            this.segmentTypesDropDown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(45)))));
            this.segmentTypesDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.segmentTypesDropDown.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.segmentTypesDropDown.ForeColor = System.Drawing.Color.White;
            this.segmentTypesDropDown.FormattingEnabled = true;
            this.segmentTypesDropDown.Location = new System.Drawing.Point(100, 261);
            this.segmentTypesDropDown.Name = "segmentTypesDropDown";
            this.segmentTypesDropDown.Size = new System.Drawing.Size(121, 21);
            this.segmentTypesDropDown.TabIndex = 0;
            this.segmentTypesDropDown.SelectedIndexChanged += new System.EventHandler(this.segmentTypesDropDown_SelectedIndexChanged);
            // 
            // elementNumberDropDown
            // 
            this.elementNumberDropDown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(45)))));
            this.elementNumberDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.elementNumberDropDown.Enabled = false;
            this.elementNumberDropDown.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.elementNumberDropDown.ForeColor = System.Drawing.Color.White;
            this.elementNumberDropDown.FormattingEnabled = true;
            this.elementNumberDropDown.Location = new System.Drawing.Point(100, 315);
            this.elementNumberDropDown.Name = "elementNumberDropDown";
            this.elementNumberDropDown.Size = new System.Drawing.Size(121, 21);
            this.elementNumberDropDown.TabIndex = 1;
            this.elementNumberDropDown.SelectedIndexChanged += new System.EventHandler(this.elementNumberDropDown_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 264);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Segment Type";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 318);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Element Number";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(240, 323);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Regex Filter";
            // 
            // regexFilterTxt
            // 
            this.regexFilterTxt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(45)))));
            this.regexFilterTxt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.regexFilterTxt.ForeColor = System.Drawing.Color.White;
            this.regexFilterTxt.Location = new System.Drawing.Point(309, 320);
            this.regexFilterTxt.Name = "regexFilterTxt";
            this.regexFilterTxt.Size = new System.Drawing.Size(289, 20);
            this.regexFilterTxt.TabIndex = 6;
            this.regexFilterTxt.TextChanged += new System.EventHandler(this.regexFilterTxt_TextChanged);
            // 
            // resultsList
            // 
            this.resultsList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(45)))));
            this.resultsList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.resultsList.ForeColor = System.Drawing.Color.White;
            this.resultsList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.resultsList.Location = new System.Drawing.Point(12, 12);
            this.resultsList.Name = "resultsList";
            this.resultsList.Size = new System.Drawing.Size(586, 243);
            this.resultsList.TabIndex = 7;
            this.resultsList.UseCompatibleStateImageBehavior = false;
            this.resultsList.View = System.Windows.Forms.View.Details;
            this.resultsList.VirtualMode = true;
            this.resultsList.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.resultsList_RetrieveVirtualItem);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Line Number";
            this.columnHeader1.Width = 494;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "LoopId";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Segment Text";
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Segment Type";
            // 
            // findBtn
            // 
            this.findBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.findBtn.ForeColor = System.Drawing.Color.White;
            this.findBtn.Location = new System.Drawing.Point(413, 527);
            this.findBtn.Name = "findBtn";
            this.findBtn.Size = new System.Drawing.Size(83, 23);
            this.findBtn.TabIndex = 8;
            this.findBtn.Text = "Find";
            this.findBtn.UseVisualStyleBackColor = true;
            this.findBtn.Click += new System.EventHandler(this.findBtn_Click);
            // 
            // ReplaceBtn
            // 
            this.ReplaceBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ReplaceBtn.ForeColor = System.Drawing.Color.White;
            this.ReplaceBtn.Location = new System.Drawing.Point(502, 527);
            this.ReplaceBtn.Name = "ReplaceBtn";
            this.ReplaceBtn.Size = new System.Drawing.Size(96, 23);
            this.ReplaceBtn.TabIndex = 9;
            this.ReplaceBtn.Text = "Replace";
            this.ReplaceBtn.UseVisualStyleBackColor = true;
            this.ReplaceBtn.Click += new System.EventHandler(this.ReplaceBtn_Click);
            // 
            // secondaryIdDropdown
            // 
            this.secondaryIdDropdown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(45)))));
            this.secondaryIdDropdown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.secondaryIdDropdown.Enabled = false;
            this.secondaryIdDropdown.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.secondaryIdDropdown.ForeColor = System.Drawing.Color.White;
            this.secondaryIdDropdown.FormattingEnabled = true;
            this.secondaryIdDropdown.Location = new System.Drawing.Point(100, 288);
            this.secondaryIdDropdown.Name = "secondaryIdDropdown";
            this.secondaryIdDropdown.Size = new System.Drawing.Size(121, 21);
            this.secondaryIdDropdown.TabIndex = 10;
            this.secondaryIdDropdown.SelectedIndexChanged += new System.EventHandler(this.secondaryIdDropdown_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 291);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Secondary Id";
            // 
            // replaceTxt
            // 
            this.replaceTxt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(45)))));
            this.replaceTxt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.replaceTxt.ForeColor = System.Drawing.Color.White;
            this.replaceTxt.Location = new System.Drawing.Point(58, 19);
            this.replaceTxt.Name = "replaceTxt";
            this.replaceTxt.Size = new System.Drawing.Size(522, 20);
            this.replaceTxt.TabIndex = 13;
            // 
            // errorTxt
            // 
            this.errorTxt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(45)))));
            this.errorTxt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.errorTxt.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.errorTxt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(94)))), ((int)(((byte)(115)))));
            this.errorTxt.Location = new System.Drawing.Point(309, 264);
            this.errorTxt.Multiline = true;
            this.errorTxt.Name = "errorTxt";
            this.errorTxt.ReadOnly = true;
            this.errorTxt.Size = new System.Drawing.Size(289, 50);
            this.errorTxt.TabIndex = 14;
            this.errorTxt.TabStop = false;
            this.errorTxt.Text = "Regex Validation Output";
            this.errorTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.fakerFields);
            this.groupBox1.Controls.Add(this.lengthVal);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.generatedRadio);
            this.groupBox1.Controls.Add(this.replaceRadio);
            this.groupBox1.Controls.Add(this.replaceTxt);
            this.groupBox1.Location = new System.Drawing.Point(12, 342);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(586, 179);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Replace";
            // 
            // fakerFields
            // 
            this.fakerFields.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(45)))));
            this.fakerFields.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5});
            this.fakerFields.ForeColor = System.Drawing.Color.White;
            this.fakerFields.FullRowSelect = true;
            this.fakerFields.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.fakerFields.HideSelection = false;
            this.fakerFields.Location = new System.Drawing.Point(113, 45);
            this.fakerFields.Name = "fakerFields";
            this.fakerFields.Size = new System.Drawing.Size(467, 128);
            this.fakerFields.TabIndex = 20;
            this.fakerFields.UseCompatibleStateImageBehavior = false;
            this.fakerFields.View = System.Windows.Forms.View.Details;
            this.fakerFields.Enter += new System.EventHandler(this.fakerFields_Enter);
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Data Type";
            this.columnHeader5.Width = 463;
            // 
            // lengthVal
            // 
            this.lengthVal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(45)))));
            this.lengthVal.ForeColor = System.Drawing.Color.White;
            this.lengthVal.Location = new System.Drawing.Point(58, 71);
            this.lengthVal.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.lengthVal.Name = "lengthVal";
            this.lengthVal.Size = new System.Drawing.Size(49, 20);
            this.lengthVal.TabIndex = 19;
            this.lengthVal.ValueChanged += new System.EventHandler(this.lengthVal_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 71);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "Length:";
            // 
            // generatedRadio
            // 
            this.generatedRadio.AutoSize = true;
            this.generatedRadio.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.generatedRadio.Location = new System.Drawing.Point(6, 45);
            this.generatedRadio.Name = "generatedRadio";
            this.generatedRadio.Size = new System.Drawing.Size(100, 17);
            this.generatedRadio.TabIndex = 15;
            this.generatedRadio.Text = "Generated Data";
            this.generatedRadio.UseVisualStyleBackColor = true;
            // 
            // replaceRadio
            // 
            this.replaceRadio.AutoSize = true;
            this.replaceRadio.Checked = true;
            this.replaceRadio.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.replaceRadio.Location = new System.Drawing.Point(6, 22);
            this.replaceRadio.Name = "replaceRadio";
            this.replaceRadio.Size = new System.Drawing.Size(45, 17);
            this.replaceRadio.TabIndex = 14;
            this.replaceRadio.TabStop = true;
            this.replaceRadio.Text = "Text";
            this.replaceRadio.UseVisualStyleBackColor = true;
            // 
            // StatusTxt
            // 
            this.StatusTxt.AutoSize = true;
            this.StatusTxt.Location = new System.Drawing.Point(9, 532);
            this.StatusTxt.Name = "StatusTxt";
            this.StatusTxt.Size = new System.Drawing.Size(0, 13);
            this.StatusTxt.TabIndex = 16;
            // 
            // pBar
            // 
            this.pBar.Location = new System.Drawing.Point(200, 527);
            this.pBar.Name = "pBar";
            this.pBar.Size = new System.Drawing.Size(207, 23);
            this.pBar.TabIndex = 17;
            this.pBar.Visible = false;
            // 
            // ReplaceDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(70)))));
            this.ClientSize = new System.Drawing.Size(610, 558);
            this.Controls.Add(this.pBar);
            this.Controls.Add(this.StatusTxt);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.errorTxt);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.secondaryIdDropdown);
            this.Controls.Add(this.ReplaceBtn);
            this.Controls.Add(this.findBtn);
            this.Controls.Add(this.resultsList);
            this.Controls.Add(this.regexFilterTxt);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.elementNumberDropDown);
            this.Controls.Add(this.segmentTypesDropDown);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ReplaceDialog";
            this.ShowIcon = false;
            this.Text = "Replace Dialog";
            this.Load += new System.EventHandler(this.ReplaceDialog_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lengthVal)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox segmentTypesDropDown;
        private System.Windows.Forms.ComboBox elementNumberDropDown;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox regexFilterTxt;
        private System.Windows.Forms.ListView resultsList;
        private System.Windows.Forms.Button findBtn;
        private System.Windows.Forms.Button ReplaceBtn;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ComboBox secondaryIdDropdown;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox replaceTxt;
        private System.Windows.Forms.TextBox errorTxt;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton replaceRadio;
        private System.Windows.Forms.RadioButton generatedRadio;
        private System.Windows.Forms.NumericUpDown lengthVal;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListView fakerFields;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.Label StatusTxt;
        private System.Windows.Forms.ProgressBar pBar;
    }
}