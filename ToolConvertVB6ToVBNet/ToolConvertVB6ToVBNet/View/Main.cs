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
        private int numFuncCount;

        private string fullPathSelect;

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
        /// Info tool
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("*********** Tool Convert VB6 to VB.NET " + lblVer.Text + " ***********\r\n" +
                "  * Tool chỉ hỗ trợ giảm bớt thao tác khi convert, vẫn có sai sót vui lòng kiểm tra lại sau khi sử dụng.\r\n" +
                "  * Mọi vấn đề thắc mắc cũng như góp ý phản hồi bug phát sinh vui lòng liên hệ team 3.2.\r\n" +
                "*********** Xin cảm ơn! ***********", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        /// Text box directory path click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtDirectoryPath_Click(object sender, EventArgs e)
        {
            txtDirectoryPath.SelectAll();
        }

        /// <summary>
        /// Text box directory paste value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtDirectoryPath_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.V)
            {
                if (!string.IsNullOrEmpty(txtDirectoryPath.Text) && Directory.Exists(txtDirectoryPath.Text))
                {
                    // Load Directory
                    LoadDirectory();
                }
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
            if (fullPathSelect.Equals(txtDirectoryPath.Text))
            {
                // Load Directory
                LoadDirectory();
            }
            else if (treeNode != null && Directory.Exists(fullPathSelect))
            {
                string dir = fullPathSelect;

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
            fullPathSelect = e.Node.Tag.ToString();

            if (Directory.Exists(fullPathSelect) || File.Exists(fullPathSelect))
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

                string[] arrRow = row.Split(CONST.CHAR_VER_BAR);
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
                if (!string.IsNullOrEmpty(fullPathSelect) && File.Exists(fullPathSelect))
                {
                    if (fullPathSelect.LastIndexOf(CONST.VB_FRM) != -1)
                    {
                        // Set  value progress bar
                        setProgressBar(false);

                        readAndEditFileVB6(fullPathSelect);

                        UpdateProgress();

                        MessageBox.Show("Convert is done!!!\r\n" +
                            "The total number of functions in the " + Path.GetFileName(fullPathSelect) + " file is " + numFuncCount, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        numFuncCount = 0;
                    } else
                    {
                        MessageBox.Show("File format incorrectly", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else if (!string.IsNullOrEmpty(fullPathSelect) && Directory.Exists(fullPathSelect))
                {
                    // Set  value progress bar
                    setProgressBar(true);

                    string[] Files = Directory.GetFiles(fullPathSelect, "*.*");
                    foreach (string file in Files)
                    {
                        File.SetAttributes(file, FileAttributes.Normal);

                        string fileBk = String.Empty;

                        readAndEditFileVB6(file);

                        UpdateProgress();
                    }

                    progressBarLoadDir.Value = progressBarLoadDir.Maximum;

                    MessageBox.Show("Convert is done!!!\r\nPlease check the content convert", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                if (!string.IsNullOrEmpty(fullPathSelect) && File.Exists(fullPathSelect))
                {
                    // Set  value progress bar
                    setProgressBar(false);

                    File.SetAttributes(fullPathSelect, FileAttributes.Normal);

                    if (fullPathSelect.LastIndexOf(CONST.VB_NET_DESIGN) != -1)
                    {
                        readAndEditFileVBNetDesign(fullPathSelect);
                    }
                    else if (fullPathSelect.LastIndexOf(CONST.VB_NET_VB) != -1)
                    {
                        readAndEditFileVBNet(fullPathSelect);
                    } else
                    {
                        UpdateProgress();

                        MessageBox.Show("File format incorrectly", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    UpdateProgress();

                    MessageBox.Show("Convert is done!!!\r\nPlease check the content convert", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (!string.IsNullOrEmpty(fullPathSelect) && Directory.Exists(fullPathSelect))
                {
                    // Set  value progress bar
                    setProgressBar(true);

                    string[] Files = Directory.GetFiles(fullPathSelect, "*.*");
                    foreach (string file in Files)
                    {
                        File.SetAttributes(file, FileAttributes.Normal);

                        if (file.LastIndexOf(CONST.VB_NET_DESIGN) != -1)
                        {
                            readAndEditFileVBNetDesign(file);
                        }
                        else if (file.LastIndexOf(CONST.VB_NET_VB) != -1)
                        {
                            readAndEditFileVBNet(file);
                        }

                        UpdateProgress();
                    }

                    progressBarLoadDir.Value = progressBarLoadDir.Maximum;

                    MessageBox.Show("Convert is done!!!\r\nPlease check the content convert", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        /// <param name="modeFile">mode = 0 change file, mode = 1 comment obj .ocx</param>
        private void readAndEditFileVB6(string path)
        {
            string nameItem = string.Empty, nameItemBk = string.Empty, tag = string.Empty;
            bool isBk = false, isObjOCX = false;
            int index = -1;

            List<string> lstItem = new List<string>(objSetting.dicItemVB6.Keys);
            List<string> lstKeyItemBk = new List<string>(objSetting.dicItemVBNetBk.Keys);

            if (path.LastIndexOf(CONST.VB_FRM) != -1 && path.LastIndexOf(CONST.VB_FRM_BK) == -1)
            {
                string fileBk = path.Replace(CONST.VB_FRM, CONST.VB_FRM_BK);
                if (File.Exists(fileBk))
                {
                    File.SetAttributes(fileBk, FileAttributes.Normal);
                    File.Delete(fileBk);
                }
                File.Copy(path, fileBk);
            }
            else if (path.LastIndexOf(CONST.VB_VBP) != -1 && path.LastIndexOf(CONST.VB_VBP) == -1)
            {
                string fileBk = path.Replace(CONST.VB_VBP, CONST.VB_VBP_BK);
                if (File.Exists(fileBk))
                {
                    File.SetAttributes(fileBk, FileAttributes.Normal);
                    File.Delete(fileBk);
                }
                File.Copy(path, fileBk);
                isObjOCX = true;
            }
            else
            {
                return;
            }

            // Read file
            StreamReader sr = new StreamReader(path, Encoding.GetEncoding(932));
            String[] rows = Regex.Split(sr.ReadToEnd(), "\r\n");
            sr.Close();

            // Clear file
            File.WriteAllText(path, String.Empty);

            // Write file
            StreamWriter sw = new StreamWriter(
                new FileStream(path, FileMode.Open, FileAccess.ReadWrite), Encoding.GetEncoding(932));

            try
            {
                for (int i = 0; i < rows.Length; i++)
                {
                    string row = rows[i];

                    if (row.Trim().Contains(CONST.STR_VB6_BEGIN))
                    {
                        // Change item to TextBox
                        foreach (string item in lstItem)
                        {
                            if (row.Contains(item))
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
                        List<string> lstItemBk = objSetting.dicItemVBNetBk[nameItemBk];

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
                        index = -1;
                    }

                    // Comment obj OCX
                    if (isObjOCX && (row.LastIndexOf(CONST.OBJ_OCX) != -1 || row.LastIndexOf(CONST.OBJ_OIP11) != -1))
                    {
                        row = "\'" + row;
                    }

                    // Count Func
                    if (string.IsNullOrEmpty(row)) { }
                    else if (row.Contains(CONST.STR_VBNET_PUBLIC_SUB) || row.Contains(CONST.STR_VBNET_PRIVATE_SUB) ||
                             row.Contains(CONST.STR_VBNET_PUBLIC_FUNC) || row.Contains(CONST.STR_VBNET_PRIVATE_FUNC))
                    {
                        numFuncCount++;
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
            string lastName = string.Empty, fileBk = String.Empty;

            List<string> lstItem = new List<string>(objSetting.dicItemVBNet.Keys);
            List<string> lstItemRe = new List<string>(objSetting.dicItemVBNetRemove.Keys);

            // Edit and baclup file
            if (path.LastIndexOf(CONST.VB_NET_DESIGN) != -1 && path.LastIndexOf(CONST.VB_NET_DESIGN_BK) == -1)
            {
                fileBk = path.Replace(CONST.VB_NET_DESIGN, CONST.VB_NET_DESIGN_BK);
                if (File.Exists(fileBk))
                {
                    File.SetAttributes(fileBk, FileAttributes.Normal);
                    File.Delete(fileBk);
                }
                File.Copy(path, fileBk);
            }
            else
            {
                return;
            }

            // Read file
            StreamReader sr = new StreamReader(path, Encoding.GetEncoding(932));
            String[] rows = Regex.Split(sr.ReadToEnd(), "\r\n");
            sr.Close();

            // Clear file
            File.WriteAllText(path, String.Empty);

            // Write file
            StreamWriter sw = new StreamWriter(
                new FileStream(path, FileMode.Open, FileAccess.ReadWrite), Encoding.GetEncoding(932));
            try
            {
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

                    if (row.Contains(CONST.STR_VBNET_W_EVENT) && row.Contains(CONST.STR_VBNET_SYS_TEXTBOXARR))
                    {
                        sw.WriteLine(String.Empty);
                        continue;
                    }

                    // Remove item in dic item remove
                    if (row.Contains(CONST.STR_VBNET_ME))
                    {
                        foreach (string keyItemRe in lstItemRe)
                        {
                            if (row.Contains(keyItemRe))
                            {
                                foreach (string itemRe in objSetting.dicItemVBNetRemove[keyItemRe])
                                {
                                    if (row.Contains(CONST.STR_DOT + itemRe))
                                    {
                                        row = string.Empty;
                                        break;
                                    }
                                }
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
                            if (nameCheck.LastIndexOf(CONST.STR_UNDERSCORE) > 0)
                            {
                                nameCheck = nameCheck.Insert(nameCheck.LastIndexOf(CONST.STR_UNDERSCORE), CONST.STR_BK);
                            }
                            else
                            {
                                nameCheck = nameCheck + CONST.STR_BK;
                            }

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

                    // Check and add item imeMode
                    if (row.Contains(CONST.STR_VBNET_ME) && row.Contains(CONST.STR_IME_MODE))
                    {
                        if (row.Contains(CONST.STR_IME_MODE_HIRAGANE))
                        {
                            row += CONST.STR_NEW_LINE + CUtils.addPropertyVBNetDesign(lastName, CONST.STR_CHK_ASCII_KANJI);
                        }
                        else if (row.Contains(CONST.STR_IME_MODE_KATAKANA) || row.Contains(CONST.STR_IME_MODE_KATAKANA_HF))
                        {
                            row += CONST.STR_NEW_LINE + CUtils.addPropertyVBNetDesign(lastName, CONST.STR_CHK_ASCII_KANA);
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
            string logFunChange = string.Empty, logChange = string.Empty, nameFuc = string.Empty;
            string pathFile = string.Empty, fileBk = String.Empty;
            int countFunc = 0, lineFunc = 0;

            List<string> lstFunc = new List<string>(objSetting.dicFunVBNet.Keys);

            if (path.LastIndexOf(CONST.VB_NET_VB) != -1 && path.LastIndexOf(CONST.VB_NET_VB_BK) == -1 &&
               !path.Contains(CONST.FILE_ASSEMBLY))
            {
                fileBk = path.Replace(CONST.VB_NET_VB, CONST.VB_NET_VB_BK);
                if (File.Exists(fileBk))
                {
                    File.SetAttributes(fileBk, FileAttributes.Normal);
                    File.Delete(fileBk);
                }
                File.Copy(path, fileBk);
            }
            else
            {
                return;
            }

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
                for (int line = 0; line < rows.Length; line++)
                {
                    string row = rows[line];
                    if (row.Contains(CONST.STR_VBNET_UPGRADE_WARNING) || row.Contains(CONST.STR_VBNET_STRICT_OFF) ||
                        row.Contains(CONST.STR_VBNET_EXPLICIT_ON)) continue;

                    if (string.IsNullOrEmpty(row)) { }
                    else if (row.Contains(CONST.STR_VBNET_PUBLIC_SUB))
                    {
                        nameFuc = row.Substring(0, row.IndexOf(CONST.STR_ROUND_BRAC)).Replace(CONST.STR_VBNET_PUBLIC_SUB, string.Empty);
                        countFunc++; lineFunc = line;
                    }
                    else if (row.Contains(CONST.STR_VBNET_PRIVATE_SUB))
                    {
                        nameFuc = row.Substring(0, row.IndexOf(CONST.STR_ROUND_BRAC)).Replace(CONST.STR_VBNET_PRIVATE_SUB, string.Empty);
                        countFunc++; lineFunc = line;
                    }
                    else if (row.Contains(CONST.STR_VBNET_PUBLIC_FUNC))
                    {
                        nameFuc = row.Substring(0, row.IndexOf(CONST.STR_ROUND_BRAC)).Replace(CONST.STR_VBNET_PUBLIC_FUNC, string.Empty);
                        countFunc++; lineFunc = line;
                    }
                    else if (row.Contains(CONST.STR_VBNET_PRIVATE_FUNC))
                    {
                        nameFuc = row.Substring(0, row.IndexOf(CONST.STR_ROUND_BRAC)).Replace(CONST.STR_VBNET_PRIVATE_FUNC, string.Empty);
                        countFunc++; lineFunc = line;
                    }
                    else if (row.Trim().Equals(CONST.STR_VBNET_END_FUNC) || row.Trim().Equals(CONST.STR_VBNET_END_SUB))
                    {
                        logFunChange += logChange;
                        logChange = string.Empty;
                        nameFuc = string.Empty;
                    }
                    else
                    {
                        foreach (string item in lstFunc)
                        {
                            if (row.Contains(item))
                            {
                                string itemChange = objSetting.dicFunVBNet[item];

                                if (string.IsNullOrEmpty(logChange))
                                {
                                    logChange = CUtils.createChangeLog(nameFuc.Trim(), lineFunc);
                                }
                                logChange += CUtils.createChangeFuncLog(line, item, itemChange);
                                row.Replace(item, itemChange);
                                break;
                            }
                        }
                    }

                    sw.WriteLine(row);
                }
                sw.Close();

                pathFile = path;
                path = path.Replace(CONST.VB_NET_VB, CONST.FILE_LOG);

                if (File.Exists(path)) File.Delete(path);

                // Write file log
                sw = new StreamWriter(File.Open(pathFile, FileMode.Create), Encoding.GetEncoding(932));

                string logNote = CUtils.createNoteLog(path, countFunc, logFunChange);
                rows = logNote.Split(CONST.STRING_SEPARATORS, StringSplitOptions.None);
                foreach (string row in rows)
                {
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

        private void setProgressBar(bool isFolder = true)
        {
            if (isFolder)
            {
                // Setting Inital Value of Progress Bar
                progressBarLoadDir.Value = 0;

                //Setting ProgressBar Maximum Value
                progressBarLoadDir.Maximum = Directory.GetFiles(fullPathSelect, "*.*", SearchOption.AllDirectories).Length;
            }
            else
            {
                // Setting Inital Value of Progress Bar
                progressBarLoadDir.Value = 0;

                //Setting ProgressBar Maximum Value
                progressBarLoadDir.Maximum = 1;
            }
        }

        #endregion
    }
}
