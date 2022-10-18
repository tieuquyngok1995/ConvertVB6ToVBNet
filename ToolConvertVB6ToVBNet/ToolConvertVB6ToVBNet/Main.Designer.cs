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
            this.label1 = new System.Windows.Forms.Label();
            this.panLeft = new System.Windows.Forms.Panel();
            this.progressBarLoadDir = new System.Windows.Forms.ProgressBar();
            this.treeViewDir = new System.Windows.Forms.TreeView();
            this.btnDirectoryPath = new System.Windows.Forms.Button();
            this.txtDirectoryPath = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.btnConvert = new System.Windows.Forms.Button();
            this.txtFolderPath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.imageListTree = new System.Windows.Forms.ImageList(this.components);
            this.panLeft.SuspendLayout();
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
            this.panLeft.Controls.Add(this.btnConvert);
            this.panLeft.Controls.Add(this.progressBarLoadDir);
            this.panLeft.Controls.Add(this.txtFolderPath);
            this.panLeft.Controls.Add(this.treeViewDir);
            this.panLeft.Controls.Add(this.label2);
            this.panLeft.Controls.Add(this.btnDirectoryPath);
            this.panLeft.Controls.Add(this.txtDirectoryPath);
            this.panLeft.Controls.Add(this.label1);
            this.panLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panLeft.Location = new System.Drawing.Point(0, 0);
            this.panLeft.Name = "panLeft";
            this.panLeft.Size = new System.Drawing.Size(335, 480);
            this.panLeft.TabIndex = 1;
            // 
            // progressBarLoadDir
            // 
            this.progressBarLoadDir.Location = new System.Drawing.Point(6, 449);
            this.progressBarLoadDir.Name = "progressBarLoadDir";
            this.progressBarLoadDir.Size = new System.Drawing.Size(320, 23);
            this.progressBarLoadDir.TabIndex = 4;
            // 
            // treeViewDir
            // 
            this.treeViewDir.ImageIndex = 0;
            this.treeViewDir.ImageList = this.imageListTree;
            this.treeViewDir.Location = new System.Drawing.Point(6, 32);
            this.treeViewDir.Name = "treeViewDir";
            this.treeViewDir.SelectedImageIndex = 0;
            this.treeViewDir.Size = new System.Drawing.Size(320, 380);
            this.treeViewDir.TabIndex = 3;
            this.treeViewDir.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewDir_NodeMouseClick);
            // 
            // btnDirectoryPath
            // 
            this.btnDirectoryPath.Location = new System.Drawing.Point(303, 5);
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
            this.txtDirectoryPath.Size = new System.Drawing.Size(186, 21);
            this.txtDirectoryPath.TabIndex = 1;
            // 
            // btnConvert
            // 
            this.btnConvert.Image = ((System.Drawing.Image)(resources.GetObject("btnConvert.Image")));
            this.btnConvert.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnConvert.Location = new System.Drawing.Point(254, 422);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new System.Drawing.Size(72, 22);
            this.btnConvert.TabIndex = 7;
            this.btnConvert.Text = "    Convert";
            this.btnConvert.UseVisualStyleBackColor = true;
            this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
            // 
            // txtFolderPath
            // 
            this.txtFolderPath.Font = new System.Drawing.Font("MS UI Gothic", 10F);
            this.txtFolderPath.Location = new System.Drawing.Point(97, 423);
            this.txtFolderPath.Name = "txtFolderPath";
            this.txtFolderPath.ReadOnly = true;
            this.txtFolderPath.Size = new System.Drawing.Size(151, 21);
            this.txtFolderPath.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 10F);
            this.label2.Location = new System.Drawing.Point(6, 425);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 14);
            this.label2.TabIndex = 5;
            this.label2.Text = "Select Folder";
            // 
            // imageListTree
            // 
            this.imageListTree.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListTree.ImageStream")));
            this.imageListTree.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListTree.Images.SetKeyName(0, "Folder.ico");
            this.imageListTree.Images.SetKeyName(1, "File.ico");
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(336, 480);
            this.Controls.Add(this.panLeft);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main";
            this.Text = "Convet VB6 To VB.Net";
            this.panLeft.ResumeLayout(false);
            this.panLeft.PerformLayout();
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
        private System.Windows.Forms.TextBox txtFolderPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnConvert;
        private System.Windows.Forms.ImageList imageListTree;
    }
}

