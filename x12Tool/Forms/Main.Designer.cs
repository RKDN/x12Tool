namespace x12Tool {
    partial class Main {
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.replaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.errorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.EditorErrorSplitter = new System.Windows.Forms.SplitContainer();
            this.errorList = new System.Windows.Forms.ListView();
            this.Segment = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.errorDescription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.InfoContainer = new System.Windows.Forms.SplitContainer();
            this.RepeatText = new System.Windows.Forms.TextBox();
            this.UsageText = new System.Windows.Forms.TextBox();
            this.NameInfoText = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.logoBox = new System.Windows.Forms.PictureBox();
            this.x12View = new x12Tool.DoubleBufferedTreeView();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EditorErrorSplitter)).BeginInit();
            this.EditorErrorSplitter.Panel1.SuspendLayout();
            this.EditorErrorSplitter.Panel2.SuspendLayout();
            this.EditorErrorSplitter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.InfoContainer)).BeginInit();
            this.InfoContainer.Panel1.SuspendLayout();
            this.InfoContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoBox)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(70)))));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.searchToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.configToolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1028, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // searchToolStripMenuItem
            // 
            this.searchToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.searchToolStripMenuItem1,
            this.replaceToolStripMenuItem});
            this.searchToolStripMenuItem.Enabled = false;
            this.searchToolStripMenuItem.Name = "searchToolStripMenuItem";
            this.searchToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.searchToolStripMenuItem.Text = "Edit";
            this.searchToolStripMenuItem.Click += new System.EventHandler(this.searchToolStripMenuItem_Click);
            // 
            // searchToolStripMenuItem1
            // 
            this.searchToolStripMenuItem1.Name = "searchToolStripMenuItem1";
            this.searchToolStripMenuItem1.Size = new System.Drawing.Size(115, 22);
            this.searchToolStripMenuItem1.Text = "Search";
            this.searchToolStripMenuItem1.Click += new System.EventHandler(this.searchToolStripMenuItem1_Click);
            // 
            // replaceToolStripMenuItem
            // 
            this.replaceToolStripMenuItem.Name = "replaceToolStripMenuItem";
            this.replaceToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
            this.replaceToolStripMenuItem.Text = "Replace";
            this.replaceToolStripMenuItem.Click += new System.EventHandler(this.replaceToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.errorsToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // errorsToolStripMenuItem
            // 
            this.errorsToolStripMenuItem.Name = "errorsToolStripMenuItem";
            this.errorsToolStripMenuItem.Size = new System.Drawing.Size(104, 22);
            this.errorsToolStripMenuItem.Text = "Errors";
            this.errorsToolStripMenuItem.Click += new System.EventHandler(this.errorsToolStripMenuItem_Click);
            // 
            // configToolStripMenuItem1
            // 
            this.configToolStripMenuItem1.Name = "configToolStripMenuItem1";
            this.configToolStripMenuItem1.Size = new System.Drawing.Size(55, 20);
            this.configToolStripMenuItem1.Text = "Config";
            this.configToolStripMenuItem1.Click += new System.EventHandler(this.configToolStripMenuItem1_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(70)))));
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 23);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.EditorErrorSplitter);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AutoScroll = true;
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(45)))));
            this.splitContainer1.Panel2.Controls.Add(this.InfoContainer);
            this.splitContainer1.Size = new System.Drawing.Size(1028, 541);
            this.splitContainer1.SplitterDistance = 762;
            this.splitContainer1.TabIndex = 7;
            // 
            // EditorErrorSplitter
            // 
            this.EditorErrorSplitter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EditorErrorSplitter.Location = new System.Drawing.Point(0, 0);
            this.EditorErrorSplitter.Name = "EditorErrorSplitter";
            this.EditorErrorSplitter.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // EditorErrorSplitter.Panel1
            // 
            this.EditorErrorSplitter.Panel1.Controls.Add(this.logoBox);
            this.EditorErrorSplitter.Panel1.Controls.Add(this.x12View);
            // 
            // EditorErrorSplitter.Panel2
            // 
            this.EditorErrorSplitter.Panel2.Controls.Add(this.errorList);
            this.EditorErrorSplitter.Panel2Collapsed = true;
            this.EditorErrorSplitter.Size = new System.Drawing.Size(762, 541);
            this.EditorErrorSplitter.SplitterDistance = 365;
            this.EditorErrorSplitter.TabIndex = 5;
            // 
            // errorList
            // 
            this.errorList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(45)))));
            this.errorList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Segment,
            this.errorDescription});
            this.errorList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.errorList.FullRowSelect = true;
            this.errorList.GridLines = true;
            this.errorList.Location = new System.Drawing.Point(0, 0);
            this.errorList.Name = "errorList";
            this.errorList.Size = new System.Drawing.Size(150, 46);
            this.errorList.TabIndex = 0;
            this.errorList.UseCompatibleStateImageBehavior = false;
            this.errorList.View = System.Windows.Forms.View.Details;
            this.errorList.SelectedIndexChanged += new System.EventHandler(this.errorList_SelectedIndexChanged);
            // 
            // Segment
            // 
            this.Segment.Text = "Segment";
            this.Segment.Width = 222;
            // 
            // errorDescription
            // 
            this.errorDescription.Text = "Error";
            this.errorDescription.Width = 698;
            // 
            // InfoContainer
            // 
            this.InfoContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.InfoContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.InfoContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.InfoContainer.IsSplitterFixed = true;
            this.InfoContainer.Location = new System.Drawing.Point(0, 0);
            this.InfoContainer.Name = "InfoContainer";
            this.InfoContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // InfoContainer.Panel1
            // 
            this.InfoContainer.Panel1.Controls.Add(this.RepeatText);
            this.InfoContainer.Panel1.Controls.Add(this.UsageText);
            this.InfoContainer.Panel1.Controls.Add(this.NameInfoText);
            // 
            // InfoContainer.Panel2
            // 
            this.InfoContainer.Panel2.AutoScroll = true;
            this.InfoContainer.Size = new System.Drawing.Size(262, 541);
            this.InfoContainer.SplitterDistance = 82;
            this.InfoContainer.TabIndex = 0;
            // 
            // RepeatText
            // 
            this.RepeatText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(45)))));
            this.RepeatText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RepeatText.Location = new System.Drawing.Point(3, 55);
            this.RepeatText.Name = "RepeatText";
            this.RepeatText.ReadOnly = true;
            this.RepeatText.Size = new System.Drawing.Size(254, 20);
            this.RepeatText.TabIndex = 2;
            // 
            // UsageText
            // 
            this.UsageText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(45)))));
            this.UsageText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UsageText.Location = new System.Drawing.Point(3, 29);
            this.UsageText.Name = "UsageText";
            this.UsageText.ReadOnly = true;
            this.UsageText.Size = new System.Drawing.Size(254, 20);
            this.UsageText.TabIndex = 1;
            // 
            // NameInfoText
            // 
            this.NameInfoText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(45)))));
            this.NameInfoText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NameInfoText.Location = new System.Drawing.Point(3, 3);
            this.NameInfoText.Name = "NameInfoText";
            this.NameInfoText.ReadOnly = true;
            this.NameInfoText.Size = new System.Drawing.Size(254, 20);
            this.NameInfoText.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // progressBar
            // 
            this.progressBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar.Location = new System.Drawing.Point(0, 562);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(1028, 2);
            this.progressBar.TabIndex = 8;
            // 
            // logoBox
            // 
            this.logoBox.BackColor = System.Drawing.Color.Transparent;
            this.logoBox.BackgroundImage = global::x12Tool.Properties.Resources.X12ToolLogo_Alt;
            this.logoBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.logoBox.Location = new System.Drawing.Point(12, 432);
            this.logoBox.Name = "logoBox";
            this.logoBox.Size = new System.Drawing.Size(119, 97);
            this.logoBox.TabIndex = 5;
            this.logoBox.TabStop = false;
            // 
            // x12View
            // 
            this.x12View.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(45)))));
            this.x12View.Dock = System.Windows.Forms.DockStyle.Fill;
            this.x12View.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
            this.x12View.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.x12View.FullRowSelect = true;
            this.x12View.HideSelection = false;
            this.x12View.Location = new System.Drawing.Point(0, 0);
            this.x12View.Margin = new System.Windows.Forms.Padding(3, 15, 3, 15);
            this.x12View.Name = "x12View";
            this.x12View.ShowPlusMinus = false;
            this.x12View.ShowRootLines = false;
            this.x12View.Size = new System.Drawing.Size(762, 541);
            this.x12View.TabIndex = 4;
            this.x12View.DrawNode += new System.Windows.Forms.DrawTreeNodeEventHandler(this.x12View_DrawNode);
            this.x12View.MouseClick += new System.Windows.Forms.MouseEventHandler(this.x12View_MouseClick);
            this.x12View.MouseUp += new System.Windows.Forms.MouseEventHandler(this.x12View_MouseUp);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(70)))));
            this.ClientSize = new System.Drawing.Size(1028, 564);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Main";
            this.Text = "X12Tool";
            this.Load += new System.EventHandler(this.Main_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.EditorErrorSplitter.Panel1.ResumeLayout(false);
            this.EditorErrorSplitter.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.EditorErrorSplitter)).EndInit();
            this.EditorErrorSplitter.ResumeLayout(false);
            this.InfoContainer.Panel1.ResumeLayout(false);
            this.InfoContainer.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.InfoContainer)).EndInit();
            this.InfoContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.logoBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private DoubleBufferedTreeView x12View;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripMenuItem searchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem errorsToolStripMenuItem;
        private System.Windows.Forms.SplitContainer EditorErrorSplitter;
        private System.Windows.Forms.ListView errorList;
        private System.Windows.Forms.ColumnHeader Segment;
        private System.Windows.Forms.ColumnHeader errorDescription;
        private System.Windows.Forms.SplitContainer InfoContainer;
        private System.Windows.Forms.TextBox RepeatText;
        private System.Windows.Forms.TextBox UsageText;
        private System.Windows.Forms.TextBox NameInfoText;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem searchToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem replaceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configToolStripMenuItem1;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.PictureBox logoBox;
    }
}

