namespace ToolConvertVB6ToVBNet
{
    partial class Main
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.imageListTree = new System.Windows.Forms.ImageList(this.components);
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.panelFooter = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panelTop = new System.Windows.Forms.Panel();
            this.btnReload = new System.Windows.Forms.Button();
            this.rbChange = new System.Windows.Forms.RadioButton();
            this.rbConvert = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDirectoryPath = new System.Windows.Forms.TextBox();
            this.btnDirectoryPath = new System.Windows.Forms.Button();
            this.panelTree = new System.Windows.Forms.Panel();
            this.treeViewDir = new System.Windows.Forms.TreeView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFolderPath = new System.Windows.Forms.TextBox();
            this.btnConvert = new System.Windows.Forms.Button();
            this.progressBarLoadDir = new System.Windows.Forms.ProgressBar();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.settingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.panelFooter.SuspendLayout();
            this.panelTop.SuspendLayout();
            this.panelTree.SuspendLayout();
            this.panel1.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageListTree
            // 
            this.imageListTree.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListTree.ImageStream")));
            this.imageListTree.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListTree.Images.SetKeyName(0, "Folder.ico");
            this.imageListTree.Images.SetKeyName(1, "File.ico");
            // 
            // panelFooter
            // 
            this.panelFooter.Controls.Add(this.label5);
            this.panelFooter.Controls.Add(this.label4);
            this.panelFooter.Controls.Add(this.label3);
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFooter.Location = new System.Drawing.Point(0, 540);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Size = new System.Drawing.Size(399, 21);
            this.panelFooter.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Right;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(212, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(118, 17);
            this.label5.TabIndex = 5;
            this.label5.Text = "© 2022 Copyright";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Right;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(330, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(11, 17);
            this.label4.TabIndex = 4;
            this.label4.Text = "|";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Right;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(341, 0);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 17);
            this.label3.TabIndex = 1;
            this.label3.Text = "Ver: 2.1";
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.btnReload);
            this.panelTop.Controls.Add(this.rbChange);
            this.panelTop.Controls.Add(this.rbConvert);
            this.panelTop.Controls.Add(this.label1);
            this.panelTop.Controls.Add(this.label6);
            this.panelTop.Controls.Add(this.txtDirectoryPath);
            this.panelTop.Controls.Add(this.btnDirectoryPath);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 25);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(399, 67);
            this.panelTop.TabIndex = 9;
            // 
            // btnReload
            // 
            this.btnReload.Image = ((System.Drawing.Image)(resources.GetObject("btnReload.Image")));
            this.btnReload.Location = new System.Drawing.Point(364, 33);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(28, 28);
            this.btnReload.TabIndex = 11;
            this.btnReload.UseVisualStyleBackColor = true;
            this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
            // 
            // rbChange
            // 
            this.rbChange.AutoSize = true;
            this.rbChange.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbChange.Location = new System.Drawing.Point(191, 8);
            this.rbChange.Name = "rbChange";
            this.rbChange.Size = new System.Drawing.Size(134, 22);
            this.rbChange.TabIndex = 10;
            this.rbChange.TabStop = true;
            this.rbChange.Text = "Change VB.NET";
            this.rbChange.UseVisualStyleBackColor = true;
            this.rbChange.CheckedChanged += new System.EventHandler(this.rbChange_CheckedChanged);
            // 
            // rbConvert
            // 
            this.rbConvert.AutoSize = true;
            this.rbConvert.Checked = true;
            this.rbConvert.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbConvert.Location = new System.Drawing.Point(55, 8);
            this.rbConvert.Name = "rbConvert";
            this.rbConvert.Size = new System.Drawing.Size(132, 22);
            this.rbConvert.TabIndex = 9;
            this.rbConvert.TabStop = true;
            this.rbConvert.Text = "VB6 => VB.NET";
            this.rbConvert.UseVisualStyleBackColor = true;
            this.rbConvert.CheckedChanged += new System.EventHandler(this.rbConvert_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select Directory";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(3, 8);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 18);
            this.label6.TabIndex = 8;
            this.label6.Text = "Mode";
            // 
            // txtDirectoryPath
            // 
            this.txtDirectoryPath.Enabled = false;
            this.txtDirectoryPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDirectoryPath.Location = new System.Drawing.Point(130, 34);
            this.txtDirectoryPath.Name = "txtDirectoryPath";
            this.txtDirectoryPath.Size = new System.Drawing.Size(195, 26);
            this.txtDirectoryPath.TabIndex = 1;
            // 
            // btnDirectoryPath
            // 
            this.btnDirectoryPath.Image = ((System.Drawing.Image)(resources.GetObject("btnDirectoryPath.Image")));
            this.btnDirectoryPath.Location = new System.Drawing.Point(331, 33);
            this.btnDirectoryPath.Name = "btnDirectoryPath";
            this.btnDirectoryPath.Size = new System.Drawing.Size(28, 28);
            this.btnDirectoryPath.TabIndex = 2;
            this.btnDirectoryPath.UseVisualStyleBackColor = true;
            this.btnDirectoryPath.Click += new System.EventHandler(this.btnDirectoryPath_Click);
            // 
            // panelTree
            // 
            this.panelTree.Controls.Add(this.treeViewDir);
            this.panelTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTree.Location = new System.Drawing.Point(0, 92);
            this.panelTree.Name = "panelTree";
            this.panelTree.Size = new System.Drawing.Size(399, 469);
            this.panelTree.TabIndex = 10;
            // 
            // treeViewDir
            // 
            this.treeViewDir.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeViewDir.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.treeViewDir.ImageIndex = 0;
            this.treeViewDir.ImageList = this.imageListTree;
            this.treeViewDir.Location = new System.Drawing.Point(6, 0);
            this.treeViewDir.Name = "treeViewDir";
            this.treeViewDir.SelectedImageIndex = 0;
            this.treeViewDir.Size = new System.Drawing.Size(387, 381);
            this.treeViewDir.TabIndex = 3;
            this.treeViewDir.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewDir_NodeMouseClick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtFolderPath);
            this.panel1.Controls.Add(this.btnConvert);
            this.panel1.Controls.Add(this.progressBarLoadDir);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 476);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(399, 64);
            this.panel1.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 18);
            this.label2.TabIndex = 5;
            this.label2.Text = "Select Folder";
            // 
            // txtFolderPath
            // 
            this.txtFolderPath.Enabled = false;
            this.txtFolderPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFolderPath.Location = new System.Drawing.Point(109, 3);
            this.txtFolderPath.Name = "txtFolderPath";
            this.txtFolderPath.ReadOnly = true;
            this.txtFolderPath.Size = new System.Drawing.Size(183, 26);
            this.txtFolderPath.TabIndex = 6;
            // 
            // btnConvert
            // 
            this.btnConvert.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConvert.Image = ((System.Drawing.Image)(resources.GetObject("btnConvert.Image")));
            this.btnConvert.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnConvert.Location = new System.Drawing.Point(300, 2);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new System.Drawing.Size(92, 28);
            this.btnConvert.TabIndex = 7;
            this.btnConvert.Text = "  Convert";
            this.btnConvert.UseVisualStyleBackColor = true;
            this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
            // 
            // progressBarLoadDir
            // 
            this.progressBarLoadDir.Location = new System.Drawing.Point(8, 34);
            this.progressBarLoadDir.Name = "progressBarLoadDir";
            this.progressBarLoadDir.Size = new System.Drawing.Size(384, 24);
            this.progressBarLoadDir.TabIndex = 4;
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(399, 25);
            this.menuStrip.TabIndex = 12;
            this.menuStrip.Text = "menuStrip";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingToolStripMenuItem});
            this.toolStripMenuItem1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(42, 21);
            this.toolStripMenuItem1.Text = "File";
            // 
            // settingToolStripMenuItem
            // 
            this.settingToolStripMenuItem.Name = "settingToolStripMenuItem";
            this.settingToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.settingToolStripMenuItem.Text = "Setting";
            this.settingToolStripMenuItem.Click += new System.EventHandler(this.settingToolStripMenuItem_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.DefaultExt = "txt";
            this.openFileDialog.Filter = "txt files (*.txt)|*.txt";
            this.openFileDialog.FilterIndex = 2;
            this.openFileDialog.ReadOnlyChecked = true;
            this.openFileDialog.RestoreDirectory = true;
            this.openFileDialog.ShowReadOnly = true;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(399, 561);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelFooter);
            this.Controls.Add(this.panelTree);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.MaximumSize = new System.Drawing.Size(415, 960);
            this.MinimumSize = new System.Drawing.Size(415, 450);
            this.Name = "Main";
            this.Text = "Convet VB6 To VB.Net";
            this.Load += new System.EventHandler(this.Main_Load);
            this.panelFooter.ResumeLayout(false);
            this.panelFooter.PerformLayout();
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelTree.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.ImageList imageListTree;
        private System.Windows.Forms.Panel panelFooter;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.RadioButton rbChange;
        private System.Windows.Forms.RadioButton rbConvert;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtDirectoryPath;
        private System.Windows.Forms.Button btnDirectoryPath;
        private System.Windows.Forms.Panel panelTree;
        private System.Windows.Forms.TreeView treeViewDir;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtFolderPath;
        private System.Windows.Forms.Button btnConvert;
        private System.Windows.Forms.ProgressBar progressBarLoadDir;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem settingToolStripMenuItem;
        private System.Windows.Forms.Button btnReload;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}

