using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
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
        /// Menu setting event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void settingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult drResult = openFileDialog.ShowDialog();
            if (drResult == DialogResult.OK)
            {
                try
                {
                    string path = openFileDialog.FileName;
                    if (File.Exists(path))
                    {
                        File.SetAttributes(path, FileAttributes.Normal);

                        readAndSaveSetting(path);
                    }
                }
                catch (IOException io)
                {
                    MessageBox.Show("Read save file setting is IOException: " + io.Message, "Error", MessageBoxButtons.OK);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Read save file setting is Exception: " + ex.Message, "Error", MessageBoxButtons.OK);
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
                readAndSaveSetting(folderBrowserDialog.SelectedPath);
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
            }
            else
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
            if (mode == 0)
            {
                convertVB6ToVBNet();
            }
            else
            {
                editVBNet();
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
        /// Read file setting and save
        /// </summary>
        /// <param name="path"></param>
        private void readAndSaveSetting(string path)
        {
            int mode = -1;
            Dictionary<string, string> dicItemVB6 = new Dictionary<string, string>();
            Dictionary<string, List<string>> dicItemVB6Bk = new Dictionary<string, List<string>>();
            Dictionary<string, string> dicItemVBNet = new Dictionary<string, string>();
            Dictionary<string, string> dicFunVBNet = new Dictionary<string, string>();

            // Read file
            StreamReader sr = new StreamReader(path, Encoding.GetEncoding(932));
            String[] rows = Regex.Split(sr.ReadToEnd(), "\r\n");
            sr.Close();

            for (int i = 0; i < rows.Length; i++)
            {
                string row = rows[i].Trim();

                if (string.IsNullOrEmpty(row)) continue;

                if (row.Equals(CONST.ITEM_VB6)) { mode = 0; continue; }
                else if (row.Equals(CONST.ITEM_VB6_BK)) { mode = 1; continue; }
                else if (row.Equals(CONST.ITEM_VBNET)) { mode = 2; continue; }
                else if (row.Equals(CONST.FUNC_VBNET)) { mode = 3; continue; }

                string[] arrRow = row.Split(CONST.CHAR_EQUALS);
                if (mode == 0 && arrRow.Length == 2)
                {
                    dicItemVB6.Add(arrRow[0], arrRow[1]);
                }
                else if (mode == 1 && arrRow.Length == 2)
                {
                    dicItemVB6Bk.Add(arrRow[0], arrRow[1].Split(CONST.CHAR_COMMA).ToList());
                }
                else if (mode == 2 && arrRow.Length == 2)
                {
                    dicItemVBNet.Add(arrRow[0], arrRow[1]);
                }
                else if (mode == 3 && arrRow.Length == 2)
                {
                    dicFunVBNet.Add(arrRow[0], arrRow[1]);
                }
            }

            objSetting.dicItemVB6 = dicItemVB6;
            objSetting.dicItemVB6Bk = dicItemVB6Bk;
            objSetting.dicItemVBNet = dicItemVBNet;
            objSetting.dicFunVBNet = dicFunVBNet;

            BinarySerialization.WriteToBinaryFile<SettingModel>(objSetting);
        }

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
            catch (IOException io)
            {
                MessageBox.Show("Convert VB6 to VB.NET is IOException: " + io.Message, "Error", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Convert VB6 to VB.NET is Exception: " + ex.Message, "Error", MessageBoxButtons.OK);
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
            catch (IOException io)
            {
                MessageBox.Show("Edit VB.NET is IOException: " + io.Message, "Error", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Edit VB.NET is Exception: " + ex.Message, "Error", MessageBoxButtons.OK);
            }
        }

        /// <summary>
        /// Read and edit file VB6 From
        /// </summary>
        /// <param name="path">path file</param>
        /// <param name="mode">mode = 0 change file, mode = 1 comment obj .ocx</param>
        private void readAndEditFileVB6(string path, int mode)
        {
            // Read file
            StreamReader sr = new StreamReader(path, Encoding.GetEncoding(932));
            String[] rows = Regex.Split(sr.ReadToEnd(), "\r\n");
            sr.Close();

            // Write file
            StreamWriter sw = new StreamWriter(
                new FileStream(path, FileMode.Open, FileAccess.ReadWrite), Encoding.GetEncoding(932));

            try
            {
                string nameItem = string.Empty, nameItemBk = string.Empty, tag = string.Empty;
                bool isBk = false;

                List<string> lstItem = new List<string>(objSetting.dicItemVB6.Keys);
                List<string> lstKeyItemBk = new List<string>(objSetting.dicItemVB6Bk.Keys);
                List<string> lstItemBk = new List<string>();


                for (int i = 0; i < rows.Length; i++)
                {
                    string row = rows[i];

                    if (row.Trim().Contains(CONST.STR_BEGIN))
                    {
                        // Change item to TextBox
                        foreach (string item in lstItem)
                        {
                            if (mode == 0 && row.Contains(item))
                            {
                                foreach (string itemBk in lstKeyItemBk)
                                {
                                    if (item.Equals(itemBk))
                                    {
                                        nameItemBk = itemBk;
                                        nameItem = row.Remove(0, row.IndexOf(itemBk) + itemBk.Length).Trim();
                                        isBk = true;
                                        break;
                                    }
                                }

                                row = row.Replace(item, objSetting.dicItemVB6[item]);
                                break;
                            }
                        }
                        sw.WriteLine(row);
                        continue;
                    }

                    if (isBk && !row.Trim().Equals(CONST.STR_END) && !string.IsNullOrEmpty(row))
                    {
                        string item = row.Split(CONST.CHAR_EQUALS)[0].Trim();

                        lstItemBk = objSetting.dicItemVB6Bk[nameItemBk];
                        foreach (string itemBk in lstItemBk)
                        {
                            if (item.Equals(itemBk))
                            {
                                tag += row.Replace(CONST.STR_QUOTATION_MARKS, string.Empty) + CONST.STR_VER_BAR;
                                break;
                            }
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
                    if (mode == 1 && row.LastIndexOf(CONST.OBJ_OCX) != -1)
                    {
                        row = "\'" + row;
                    }

                    sw.WriteLine(row);
                }
                sw.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Read and Edit file VB6 is Exception: " + e.Message, "Error", MessageBoxButtons.OK);
                sw.Close();
            }
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
            try
            {
                for (int i = 0; i < rows.Length; i++)
                {
                    string row = rows[i];

                    sw.WriteLine(row);
                }
                sw.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Read and Edit file VBNet is Exception: " + e.Message, "Error", MessageBoxButtons.OK);

                sw.Close();
            }
        }
        #endregion

    }
}
