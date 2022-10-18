using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToolConvertVB6ToVBNet
{
    public partial class Main : Form
    {
        private string strFullPathSelect;

        private string[] lstFind;

        readonly string VB_TEXTBOX = "VB.TextBox";

        public Main()
        {
            InitializeComponent();

            lstFind = new string[] {
                "imMask7Ctl.imMask","imNumber7Ctl.imNumber","imText6Ctl.imText", "FPSpreadADO.fpSpread",
                "imText7Ctl.imText", "imTime7Ctl.imTime","imCalendar7Ctl.imCalendar" };
        }

        private void btnDirectoryPath_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.SelectedPath = txtDirectoryPath.Text;
            DialogResult drResult = folderBrowserDialog.ShowDialog();
            if (drResult == DialogResult.OK)
            {
                txtDirectoryPath.Text = folderBrowserDialog.SelectedPath;

                // Setting Inital Value of Progress Bar
                progressBarLoadDir.Value = 0;
                // Clear All Nodes if Already Exists
                treeViewDir.Nodes.Clear();
                LoadDirectory(txtDirectoryPath.Text);
            }
        }

        public void LoadDirectory(string Dir)
        {
            DirectoryInfo di = new DirectoryInfo(Dir);
            //Setting ProgressBar Maximum Value
            progressBarLoadDir.Maximum = Directory.GetFiles(Dir, "*.*", SearchOption.AllDirectories).Length + Directory.GetDirectories(Dir, "**", SearchOption.AllDirectories).Length;
            TreeNode tds = treeViewDir.Nodes.Add(di.Name);
            tds.Tag = di.FullName;
            tds.ImageIndex = 0;
            tds.SelectedImageIndex = 0;
            LoadFiles(Dir, tds);
            LoadSubDirectories(Dir, tds);
        }

        private void LoadFiles(string dir, TreeNode td)
        {
            string[] Files = Directory.GetFiles(dir, "*.*");
            // Loop through them to see files
            foreach (string file in Files)
            {
                FileInfo fi = new FileInfo(file);
                TreeNode tds = td.Nodes.Add(fi.Name);
                tds.Tag = fi.FullName;
                tds.ImageIndex = 1;
                tds.SelectedImageIndex = 1;
                UpdateProgress();
            }
        }

        private void LoadSubDirectories(string dir, TreeNode td)
        {
            // Get all subdirectories
            string[] subdirectoryEntries = Directory.GetDirectories(dir);
            // Loop through them to see if they have any other subdirectories
            foreach (string subdirectory in subdirectoryEntries)
            {
                DirectoryInfo di = new DirectoryInfo(subdirectory);
                TreeNode tds = td.Nodes.Add(di.Name);
                tds.Tag = di.FullName;
                tds.ImageIndex = 0;
                tds.SelectedImageIndex = 0;
                LoadFiles(subdirectory, tds);
                LoadSubDirectories(subdirectory, tds);
                UpdateProgress();
            }
        }
          
        private void UpdateProgress()
        {
            if (progressBarLoadDir.Value < progressBarLoadDir.Maximum)
            {
                progressBarLoadDir.Value++;
                int percent = (int)(((double)progressBarLoadDir.Value / (double)progressBarLoadDir.Maximum) * 100);
                progressBarLoadDir.CreateGraphics().DrawString(percent.ToString() + "%", new Font("Arial", (float)8.25, FontStyle.Regular), Brushes.Black, new PointF(progressBarLoadDir.Width / 2 - 10, progressBarLoadDir.Height / 2 - 7));
                Application.DoEvents();
            }
        }

        private void treeViewDir_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (!e.Node.Text.Contains("."))
            {
                txtFolderPath.Text = e.Node.Text;
                strFullPathSelect = e.Node.Tag.ToString();
            }
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(strFullPathSelect))
                {
                    // Setting Inital Value of Progress Bar
                    progressBarLoadDir.Value = 0;

                    //Setting ProgressBar Maximum Value
                    progressBarLoadDir.Maximum = Directory.GetFiles(strFullPathSelect, "*.*", SearchOption.AllDirectories).Length;

                    string[] Files = Directory.GetFiles(strFullPathSelect, "*.*");
                    foreach (string file in Files)
                    {
                        File.SetAttributes(file, FileAttributes.Normal);

                        string fileBk = String.Empty;
                        if (file.LastIndexOf(".frm") != -1 && file.LastIndexOf(".frm-bk") == -1)
                        {
                            fileBk = file.Replace(".frm", ".frm-bk");
                            if (File.Exists(fileBk))
                            {
                                File.SetAttributes(fileBk, FileAttributes.Normal);
                                File.Delete(fileBk);
                            }
                            File.Copy(file, fileBk);

                            readFileAndChange(file, 0);
                        }
                        if (file.LastIndexOf(".vbp") != -1 && file.LastIndexOf(".vbp-bk") == -1)
                        {
                            fileBk = file.Replace(".vbp", ".vbp-bk");
                            if (File.Exists(fileBk))
                            {
                                File.SetAttributes(fileBk, FileAttributes.Normal);
                                File.Delete(fileBk);
                            }
                            File.Copy(file, fileBk);

                            readFileAndChange(file, 1);
                        }
                        UpdateProgress();
                    }

                    progressBarLoadDir.Value = progressBarLoadDir.Maximum;
                }
            }
            catch (IOException io)
            {
                MessageBox.Show("Conver is IOException: " + io.Message, "Error", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Conver is Exception: " + ex.Message, "Error", MessageBoxButtons.OK);
            }
        }

        private void readFileAndChange(string path, int mode)
        {
            StreamReader sr = new StreamReader(path, Encoding.GetEncoding(932));
            String[] rows = Regex.Split(sr.ReadToEnd(), "\r\n");
            sr.Close();

            StreamWriter sw = new StreamWriter(
                new FileStream(path, FileMode.Open, FileAccess.ReadWrite), Encoding.GetEncoding(932));
            for (int i = 0; i < rows.Length; i++)
            {
                foreach (string txtFind in lstFind)
                {
                    if (mode == 0 && rows[i].Contains(txtFind))
                    {
                        rows[i] = rows[i].Replace(txtFind, VB_TEXTBOX);
                    }
                }

                if (mode == 1 && rows[i].LastIndexOf(".ocx") != -1)
                {
                    rows[i] = "\'" + rows[i];
                }

                sw.WriteLine(rows[i]);
            }
            sw.Close();
        }

        private static FileAttributes RemoveAttribute(FileAttributes attributes, FileAttributes attributesToRemove)
        {
            return attributes & ~attributesToRemove;
        }
    }
}
