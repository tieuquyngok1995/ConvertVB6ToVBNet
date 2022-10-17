namespace ToolConvertVB6ToVBNet
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
            this.label1 = new System.Windows.Forms.Label();
            this.panLeft = new System.Windows.Forms.Panel();
            this.progressBarLoadDir = new System.Windows.Forms.ProgressBar();
            this.treeViewDir = new System.Windows.Forms.TreeView();
            this.btnDirectoryPath = new System.Windows.Forms.Button();
            this.txtDirectoryPath = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnConvert = new System.Windows.Forms.Button();
            this.txtFolderPath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panLeft.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 10F);
            this.label1.Location = new System.Drawing.Point(6, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select Directory";
            // 
            // panLeft
            // 
            this.panLeft.Controls.Add(this.progressBarLoadDir);
            this.panLeft.Controls.Add(this.treeViewDir);
            this.panLeft.Controls.Add(this.btnDirectoryPath);
            this.panLeft.Controls.Add(this.txtDirectoryPath);
            this.panLeft.Controls.Add(this.label1);
            this.panLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panLeft.Location = new System.Drawing.Point(0, 0);
            this.panLeft.Name = "panLeft";
            this.panLeft.Size = new System.Drawing.Size(321, 450);
            this.panLeft.TabIndex = 1;
            // 
            // progressBarLoadDir
            // 
            this.progressBarLoadDir.Location = new System.Drawing.Point(6, 420);
            this.progressBarLoadDir.Name = "progressBarLoadDir";
            this.progressBarLoadDir.Size = new System.Drawing.Size(306, 23);
            this.progressBarLoadDir.TabIndex = 4;
            // 
            // treeViewDir
            // 
            this.treeViewDir.Location = new System.Drawing.Point(6, 32);
            this.treeViewDir.Name = "treeViewDir";
            this.treeViewDir.Size = new System.Drawing.Size(306, 380);
            this.treeViewDir.TabIndex = 3;
            this.treeViewDir.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewDir_NodeMouseDoubleClick);
            // 
            // btnDirectoryPath
            // 
            this.btnDirectoryPath.Location = new System.Drawing.Point(289, 5);
            this.btnDirectoryPath.Name = "btnDirectoryPath";
            this.btnDirectoryPath.Size = new System.Drawing.Size(23, 23);
            this.btnDirectoryPath.TabIndex = 2;
            this.btnDirectoryPath.Text = "...";
            this.btnDirectoryPath.UseVisualStyleBackColor = true;
            this.btnDirectoryPath.Click += new System.EventHandler(this.btnDirectoryPath_Click);
            // 
            // txtDirectoryPath
            // 
            this.txtDirectoryPath.Font = new System.Drawing.Font("MS UI Gothic", 10F);
            this.txtDirectoryPath.Location = new System.Drawing.Point(111, 6);
            this.txtDirectoryPath.Name = "txtDirectoryPath";
            this.txtDirectoryPath.Size = new System.Drawing.Size(171, 21);
            this.txtDirectoryPath.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnConvert);
            this.panel1.Controls.Add(this.txtFolderPath);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(321, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(251, 450);
            this.panel1.TabIndex = 2;
            // 
            // btnConvert
            // 
            this.btnConvert.Location = new System.Drawing.Point(140, 34);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new System.Drawing.Size(108, 22);
            this.btnConvert.TabIndex = 7;
            this.btnConvert.Text = "Convert";
            this.btnConvert.UseVisualStyleBackColor = true;
            this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
            // 
            // txtFolderPath
            // 
            this.txtFolderPath.Font = new System.Drawing.Font("MS UI Gothic", 10F);
            this.txtFolderPath.Location = new System.Drawing.Point(97, 7);
            this.txtFolderPath.Name = "txtFolderPath";
            this.txtFolderPath.ReadOnly = true;
            this.txtFolderPath.Size = new System.Drawing.Size(151, 21);
            this.txtFolderPath.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 10F);
            this.label2.Location = new System.Drawing.Point(6, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 14);
            this.label2.TabIndex = 5;
            this.label2.Text = "Select Folder";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panLeft);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panLeft.ResumeLayout(false);
            this.panLeft.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panLeft;
        private System.Windows.Forms.Button btnDirectoryPath;
        private System.Windows.Forms.TextBox txtDirectoryPath;
        private System.Windows.Forms.TreeView treeViewDir;
        private System.Windows.Forms.ProgressBar progressBarLoadDir;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtFolderPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnConvert;
    }
}

