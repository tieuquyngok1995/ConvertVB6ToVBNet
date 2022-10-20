using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using ToolConvertVB6ToVBNet.Common;
using ToolConvertVB6ToVBNet.Model;
using ToolConvertVB6ToVBNet.Utils;

namespace ToolConvertVB6ToVBNet
{
    public partial class Main : Form
    {
        private SettingModel objSetting;
        private int mode;

        private string strFullPathSelect;

        public Main()
        {
            InitializeComponent();
        }

        #region Event Form
        /// <summary>
        /// Main Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Main_Load(object sender, EventArgs e)
        {
            objSetting = BinarySerialization.ReadFromBinaryFile<SettingModel>();

            if (objSetting.mode == 1) rbChange.Checked = true;

            if (!string.IsNullOrEmpty(objSetting.directoryPath))
            {
                txtDirectoryPath.Text = objSetting.directoryPath;

                if (Directory.Exists(objSetting.directoryPath))
                {
                    // Load Directory
                    LoadDirectory();
                }
            }
        }

        /// <summary>
        /// Radio Convert Check
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbConvert_CheckedChanged(object sender, EventArgs e)
        {
            if (rbConvert.Checked)
            {
                mode = 0;

                objSetting.mode = mode;
                BinarySerialization.WriteToBinaryFile<SettingModel>(objSetting);
            }
        }

        /// <summary>
        /// Radio Change Check
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbChange_CheckedChanged(object sender, EventArgs e)
        {
            if (rbChange.Checked)
            {
                mode = 1;

                objSetting.mode = mode;
                BinarySerialization.WriteToBinaryFile<SettingModel>(objSetting);
            }
        }

        /// <summary>
        /// Button Select Folder Click Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDirectoryPath_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.SelectedPath = txtDirectoryPath.Text;
            DialogResult drResult = folderBrowserDialog.ShowDialog();
            if (drResult == DialogResult.OK)
            {
                txtDirectoryPath.Text = folderBrowserDialog.SelectedPath;

                objSetting.directoryPath = txtDirectoryPath.Text;
                BinarySerialization.WriteToBinaryFile<SettingModel>(objSetting);

                // Load Directory
                LoadDirectory();
            }
        }

        /// <summary>
        /// Reload tree
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReload_Click(object sender, EventArgs e)
        {
            // Load Directory
            LoadDirectory();
        }

        /// <summary>
        /// Tree Folder Mouse Select 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeViewDir_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            strFullPathSelect = e.Node.Tag.ToString();

            if (Directory.Exists(strFullPathSelect))
            {
                txtFolderPath.Text = e.Node.Text;
            } else
            {
                txtFolderPath.Text = string.Empty;
            }
        }

        /// <summary>
        /// Button Convrt Click Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConvert_Click(object sender, EventArgs e)
        {
            try
            {
                if (mode == 0)
                {
                    convertVB6ToVBNet();
                } else
                {
                    editVBNet();
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
        #endregion

        #region Function Load Directory and File to Tree
        /// <summary>
        /// Load Directory
        /// </summary>
        public void LoadDirectory()
        {
            string dir = txtDirectoryPath.Text;

            // Setting Inital Value of Progress Bar
            progressBarLoadDir.Value = 0;
            // Clear All Nodes if Already Exists
            treeViewDir.Nodes.Clear();

            //Setting ProgressBar Maximum Value
            progressBarLoadDir.Maximum = Directory.GetFiles(dir, "*.*", SearchOption.AllDirectories).Length +
                Directory.GetDirectories(dir, "**", SearchOption.AllDirectories).Length;

            DirectoryInfo di = new DirectoryInfo(dir);

            TreeNode tds = treeViewDir.Nodes.Add(di.Name);
            tds.Tag = di.FullName;
            tds.ImageIndex = 0;
            tds.SelectedImageIndex = 0;

            LoadFiles(dir, tds);

            LoadSubDirectories(dir, tds);
        }

        /// <summary>
        /// Load File
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="td"></param>
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

        /// <summary>
        /// Load Sub Directories
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="td"></param>
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

        /// <summary>
        /// Update Progress bar
        /// </summary>
        private void UpdateProgress()
        {
            if (progressBarLoadDir.Value < progressBarLoadDir.Maximum)
            {
                progressBarLoadDir.Value++;
                int percent = (int)(((double)progressBarLoadDir.Value / (double)progressBarLoadDir.Maximum) * 100);
                progressBarLoadDir.CreateGraphics().DrawString(percent.ToString() + "%", new Font("MS UI Gothic", (float)10, FontStyle.Regular),
                    Brushes.Black, new PointF(progressBarLoadDir.Width / 2 - 10, progressBarLoadDir.Height / 2 - 7));
                Application.DoEvents();
            }
        }
        #endregion

        #region Function edit change file 
        /// <summary>
        /// Convert VB6 to VB.Net
        /// </summary>
        private void convertVB6ToVBNet()
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
                        if (file.LastIndexOf(CONST.VB_FRM) != -1 && file.LastIndexOf(CONST.VB_FRM_BK) == -1)
                        {
                            fileBk = file.Replace(CONST.VB_FRM, CONST.VB_FRM_BK);
                            if (File.Exists(fileBk))
                            {
                                File.SetAttributes(fileBk, FileAttributes.Normal);
                                File.Delete(fileBk);
                            }
                            File.Copy(file, fileBk);

                            readAndEditFileVB6(file, 0);
                        }
                        if (file.LastIndexOf(CONST.VB_VBP) != -1 && file.LastIndexOf(CONST.VB_VBP) == -1)
                        {
                            fileBk = file.Replace(CONST.VB_VBP, CONST.VB_VBP_BK);
                            if (File.Exists(fileBk))
                            {
                                File.SetAttributes(fileBk, FileAttributes.Normal);
                                File.Delete(fileBk);
                            }
                            File.Copy(file, fileBk);

                            readAndEditFileVB6(file, 1);
                        }
                        UpdateProgress();
                    }

                    progressBarLoadDir.Value = progressBarLoadDir.Maximum;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Edit File Designer VB.Net
        /// </summary>
        private void editVBNet()
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

                        if (file.LastIndexOf(CONST.VB_NET_DESIGN) != -1)
                        {
                            readAndEditFileVBNetDesign(file);
                        }

                        UpdateProgress();
                    }

                    progressBarLoadDir.Value = progressBarLoadDir.Maximum;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Read and edit file VB6 From
        /// </summary>
        /// <param name="path"></param>
        /// <param name="mode"></param>
        private void readAndEditFileVB6(string path, int mode)
        {
            string nameItem = string.Empty;
            string tag = string.Empty;
            bool isBk = false;

            // Read file
            StreamReader sr = new StreamReader(path, Encoding.GetEncoding(932));
            String[] rows = Regex.Split(sr.ReadToEnd(), "\r\n");
            sr.Close();

            // Write file
            StreamWriter sw = new StreamWriter(
                new FileStream(path, FileMode.Open, FileAccess.ReadWrite), Encoding.GetEncoding(932));

            for (int i = 0; i < rows.Length; i++)
            {
                string row = rows[i];

                // Change item to TextBox
                foreach (string txtFind in CONST.LIST_ITEM)
                {
                    if (mode == 0 && row.Contains(txtFind))
                    {
                        if (txtFind.Equals(CONST.LIST_ITEM[1]))
                        {
                            nameItem = rows[i].Remove(0, row.IndexOf(CONST.LIST_ITEM[1]) + CONST.LIST_ITEM[1].Length).Trim();
                            isBk = true;
                        }

                        row = row.Replace(txtFind, CONST.VB_TEXTBOX);
                    }
                }

                if (isBk && !row.Trim().Equals(CONST.STR_END) && !string.IsNullOrEmpty(row))
                {
                    string item = rows[i].Trim().Split(CONST.CHAR_EQUALS)[0].Trim();

                    if (item.Equals(CONST.STR_DIS_FORMAT) || item.Equals(CONST.STR_MAX_VALUE) || item.Equals(CONST.STR_MIN_VALUE))
                    {
                        tag += row.Trim().Replace(CONST.STR_QUOTATION_MARKS, string.Empty) + CONST.STR_VER_BAR;
                    }
                }
                else if (isBk && row.Trim().Equals(CONST.STR_END))
                {
                    row += CUtils.createItemBK(nameItem, tag.Remove(tag.Length - 1, 1));

                    nameItem = string.Empty;
                    tag = string.Empty;
                    isBk = false;
                }

                // comment obj OCX
                if (mode == 1 && rows[i].LastIndexOf(CONST.OBJ_OCX) != -1)
                {
                    row = "\'" + row;
                }

                sw.WriteLine(row);
            }
            sw.Close();
        }

        /// <summary>
        /// Read and edit file VB.NET Design
        /// </summary>
        /// <param name="path"></param>
        private void readAndEditFileVBNetDesign(string path)
        {
            // Read file
            StreamReader sr = new StreamReader(path, Encoding.GetEncoding(932));
            String[] rows = Regex.Split(sr.ReadToEnd(), "\r\n");
            sr.Close();

            // Write file
            StreamWriter sw = new StreamWriter(
                new FileStream(path, FileMode.Open, FileAccess.ReadWrite), Encoding.GetEncoding(932));

            for (int i = 0; i < rows.Length; i++)
            {
                string row = rows[i];

                sw.WriteLine(row);
            }
            sw.Close();
        }
        #endregion
    }
}
