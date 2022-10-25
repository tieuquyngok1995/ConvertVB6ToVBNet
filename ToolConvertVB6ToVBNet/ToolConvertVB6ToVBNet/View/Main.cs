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
        private int mode;

        private string strFullPathSelect;

        private TreeNode treeNode;
        private SettingModel objSetting;

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
                    MessageBox.Show("Read save file setting is IOException: " + io.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Read save file setting is Exception: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (strFullPathSelect.Equals(txtDirectoryPath.Text))
            {
                // Load Directory
                LoadDirectory();
            }
            else if (treeNode != null && Directory.Exists(strFullPathSelect))
            {
                string dir = strFullPathSelect;

                // Setting Inital Value of Progress Bar
                progressBarLoadDir.Value = 0;

                treeNode.Nodes.Clear();

                //Setting ProgressBar Maximum Value
                progressBarLoadDir.Maximum = Directory.GetFiles(dir, "*.*", SearchOption.AllDirectories).Length +
                    Directory.GetDirectories(dir, "**", SearchOption.AllDirectories).Length;


                LoadFiles(dir, treeNode);

                LoadSubDirectories(dir, treeNode);
            }
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
                treeNode = e.Node;
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

            MessageBox.Show("Convert is done!!!\r\nPlease check the content convert", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
                progressBarLoadDir.CreateGraphics().DrawString(percent.ToString() + "%", new Font("Microsoft Sans Serif", (float)10, FontStyle.Regular),
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
            Dictionary<string, string> dicItemVBNet = new Dictionary<string, string>();
            Dictionary<string, List<string>> dicItemVBNetBk = new Dictionary<string, List<string>>();
            Dictionary<string, List<string>> dicItemVBNetRe = new Dictionary<string, List<string>>();
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
                else if (row.Equals(CONST.ITEM_VBNET)) { mode = 1; continue; }
                else if (row.Equals(CONST.ITEM_VBNET_BK)) { mode = 2; continue; }
                else if (row.Equals(CONST.ITEM_VBNET_RE)) { mode = 3; continue; }
                else if (row.Equals(CONST.FUNC_VBNET)) { mode = 4; continue; }

                string[] arrRow = row.Split(CONST.CHAR_EQUALS);
                if (mode == 0 && arrRow.Length == 2)
                {
                    dicItemVB6.Add(arrRow[0], arrRow[1]);
                }
                else if (mode == 1 && arrRow.Length == 2)
                {
                    dicItemVBNet.Add(arrRow[0], arrRow[1]);
                }
                else if (mode == 2 && arrRow.Length == 2)
                {
                    dicItemVBNetBk.Add(arrRow[0], arrRow[1].Split(CONST.CHAR_COMMA).ToList());
                }
                else if (mode == 3 && arrRow.Length == 2)
                {
                    dicItemVBNetRe.Add(arrRow[0], arrRow[1].Split(CONST.CHAR_COMMA).ToList());
                }
                else if (mode == 4 && arrRow.Length == 2)
                {
                    dicFunVBNet.Add(arrRow[0], arrRow[1]);
                }
            }

            objSetting.dicItemVB6 = dicItemVB6;
            objSetting.dicItemVBNet = dicItemVBNet;
            objSetting.dicItemVBNetBk = dicItemVBNetBk;
            objSetting.dicItemVBNetRemove = dicItemVBNetRe;
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
                MessageBox.Show("Convert VB6 to VB.NET is IOException: " + io.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Convert VB6 to VB.NET is Exception: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    StringBuilder sbLog = new StringBuilder();

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
                        else if (file.LastIndexOf(CONST.VB_NET_VB) != -1)
                        {
                            //readAndEditFileVBNet(file);
                        }

                        UpdateProgress();
                    }

                    progressBarLoadDir.Value = progressBarLoadDir.Maximum;
                }
            }
            catch (IOException io)
            {
                MessageBox.Show("Edit VB.NET is IOException: " + io.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Edit VB.NET is Exception: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                int index = 0;

                List<string> lstItem = new List<string>(objSetting.dicItemVB6.Keys);
                List<string> lstKeyItemBk = new List<string>(objSetting.dicItemVBNetBk.Keys);
                List<string> lstItemBk = new List<string>();

                for (int i = 0; i < rows.Length; i++)
                {
                    string row = rows[i];

                    if (row.Trim().Contains(CONST.STR_VB6_BEGIN))
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

                    if (isBk && !row.Trim().Equals(CONST.STR_VB6_END) && !string.IsNullOrEmpty(row))
                    {
                        string[] arrItem = row.Split(CONST.CHAR_EQUALS);

                        if (arrItem.Length > 1 && arrItem[0].Trim().Equals(CONST.STR_INDEX))
                        {
                            index = int.Parse(arrItem[1].Trim());
                        }

                        string item = arrItem[0].Trim();
                        lstItemBk = objSetting.dicItemVBNetBk[nameItemBk];

                        foreach (string itemBk in lstItemBk)
                        {
                            if (item.Equals(itemBk))
                            {
                                tag += row.Replace(CONST.STR_QUOTATION_MARKS, CONST.STR_QUOTATION_MARKS_CHANGE) + CONST.STR_VER_BAR;
                                break;
                            }
                        }
                    }
                    else if (isBk && row.Trim().Equals(CONST.STR_VB6_END))
                    {
                        row += CUtils.createItemBK(nameItem, index, tag.Remove(tag.Length - 1, 1));

                        nameItem = string.Empty;
                        tag = string.Empty;
                        isBk = false;
                        index = 0;
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
                MessageBox.Show("Read and Edit file VB6 is Exception: " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            File.WriteAllText(path, String.Empty);

            // Write file
            StreamWriter sw = new StreamWriter(
                new FileStream(path, FileMode.Open, FileAccess.ReadWrite), Encoding.GetEncoding(932));
            try
            {
                string lastName = string.Empty;

                List<string> lstItem = new List<string>(objSetting.dicItemVBNet.Keys);
                List<string> lstItemRe = new List<string>(objSetting.dicItemVBNetRemove.Keys);
                for (int i = 0; i < rows.Length; i++)
                {
                    string row = rows[i];

                    if (string.IsNullOrEmpty(row)) continue;

                    if ((row.Contains(CONST.STR_VBNET_W_EVENT) || row.Contains(CONST.STR_VBNET_ME))
                        && row.Contains(CONST.STR_VBNET_SYS_TEXTBOX))
                    {
                        if (row.Contains(CONST.STR_BK)) row = string.Empty;

                        foreach (string item in lstItem)
                        {
                            if (row.Contains(item))
                            {
                                row = row.Replace(CONST.STR_VBNET_SYS_TEXTBOX, objSetting.dicItemVBNet[item]);
                                break;
                            }
                        }
                        sw.WriteLine(row);
                        continue;
                    }

                    if(row.Contains(CONST.STR_VBNET_W_EVENT) && row.Contains(CONST.STR_VBNET_SYS_TEXTBOXARR))
                    {
                        sw.WriteLine(String.Empty);
                        continue;
                    }

                    // Remove item in dic item remove
                    if (row.Contains(CONST.STR_VBNET_ME))
                    {
                        foreach (string itemRe in lstItemRe)
                        {
                            if (row.Contains(itemRe) && row.Contains(CONST.STR_DOT + objSetting.dicItemVBNetRemove[itemRe]))
                            {
                                row = string.Empty;
                                break;
                            }
                        }
                    }

                    // Remove and add property bk
                    if (row.Contains(CONST.STR_VBNET_ME))
                    {
                        string[] arrRow = row.Trim().Split(CONST.CHAR_DOT);
                        if (arrRow.Length >= 1 && !arrRow[1].Equals(lastName))
                        {
                            string nameCheck = lastName;
                            if (nameCheck.LastIndexOf(CONST.STR_UNDERSCORE) > 0) nameCheck = nameCheck.Insert(nameCheck.LastIndexOf(CONST.STR_UNDERSCORE), CONST.STR_BK);

                            if (arrRow[1].Equals(nameCheck) && !row.Contains(CONST.STR_TAG))
                            {
                                row = string.Empty;
                            }
                            else if (arrRow[1].Equals(nameCheck) && row.Contains(CONST.STR_TAG))
                            {
                                row = editAndAddItemBKVBNetDesign(lastName, row);
                            }
                            else
                            {
                                lastName = arrRow[1];
                            }
                        }

                        if (row.Contains(CONST.STR_BK) 
                            && (row.Contains(CONST.STR_ADD) || row.Contains(CONST.STR_NEW) || 
                                row.Contains(CONST.STR_VBNET_SYS_COMPONENT) || row.Contains(CONST.STR_SET_INDEX)))
                        {
                            row = string.Empty;
                        }
                    }

                    sw.WriteLine(row);
                }
                sw.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Read and Edit file VBNet Desgin is Exception: " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                sw.Close();
            }
        }

        /// <summary>
        /// Read and edit file VB.NET
        /// </summary>
        /// <param name="path"></param>
        private void readAndEditFileVBNet(string path)
        {
            string log = string.Empty;

            // Read file
            StreamReader sr = new StreamReader(path, Encoding.GetEncoding(932));
            String[] rows = Regex.Split(sr.ReadToEnd(), "\r\n");
            sr.Close();

            File.WriteAllText(path, String.Empty);

            // Write file
            StreamWriter sw = new StreamWriter(
                new FileStream(path, FileMode.Open, FileAccess.ReadWrite), Encoding.GetEncoding(932));

            try
            {
                for (int i = 0; i < rows.Length; i++)
                {
                    string row = rows[i];
                    if (row.Contains(CONST.STR_VBNET_UPGRADE_WARNING)) continue;

                    sw.WriteLine(row);
                }
                sw.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Read and Edit file VBNet is Exception: " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                sw.Close();
            }
        }

        private void writeLog(string path)
        {
            path = path.Replace(CONST.VB_NET_VB, CONST.FILE_LOG);
        }

        /// <summary>
        /// Edit And Add Item Backup to file VBNetDesign
        /// </summary>
        /// <param name="name"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        private string editAndAddItemBKVBNetDesign(string name, string row)
        {
            string result = string.Empty;

            row = row.Remove(0, row.IndexOf(CONST.CHAR_QUOTATION_MARKS)).Replace(CONST.STR_QUOTATION_MARKS, string.Empty);
            string[] arrRow = row.Trim().Split(CONST.CHAR_VER_BAR);

            foreach (string item in arrRow)
            {
                result += CUtils.addPropertyVBNetDesign(name, item.Trim());
            }

            return result.Remove(result.Length - 2, 2);
        }
        #endregion

    }
}
