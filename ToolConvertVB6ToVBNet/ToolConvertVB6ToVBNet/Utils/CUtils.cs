using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolConvertVB6ToVBNet.Utils
{
    public static class CUtils
    {
        #region Create Template
        /// <summary>
        /// Create item textbox backup value in tag item
        /// </summary>
        /// <param name="name"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        public static string createItemBK(string name, int index, string tag)
        {
            string strIndex = string.Empty;

            StringBuilder sb = new StringBuilder();
            sb.Append("\r\nBegin VB.TextBox {0}Backup\r\n");
            sb.Append("Height = 480\r\n");
            sb.Append("{1}\r\n");
            sb.Append("Left = 0\r\n");
            sb.Append("Tag = \"{2}\"\r\n");
            sb.Append("Top = 0\r\n");
            sb.Append("Width = 1200\r\n");
            sb.Append("End");

            if (index != -1)
            {
                strIndex = "Index=" + index.ToString();
            }

            return string.Format(sb.ToString(), name, strIndex, tag);
        }

        /// <summary>
        /// Add property backup in to item
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string addPropertyVBNetDesign(string name, string value)
        {
            if (value.Contains(CONST.STR_QUOTATION_MARKS_CHANGE)) value = value.Replace(CONST.STR_QUOTATION_MARKS_CHANGE, CONST.STR_QUOTATION_MARKS);

            StringBuilder sb = new StringBuilder();
            sb.Append("Me.{0}.{1}\r\n");

            return string.Format(sb.ToString(), name, value);
        }

        public static string createNoteLog(string path, int count, string funChange)
        {
            string filename = Path.GetFileName(path);
            StringBuilder sb = new StringBuilder();

            sb.Append("Report log convert file: {0}\r\n");
            sb.Append("<==================================================================================================>\r\n");
            sb.Append("Function count: {1}\r\n");
            sb.Append("<==================================================================================================>\r\n");
            sb.Append("{2}");
            sb.Append("<==================================================================================================>");

            return string.Format(sb.ToString(), filename, count, funChange);
        }

        public static string createChangeLog(string func, int line)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("Function change: {0}, line {1}\r\n");

            return string.Format(sb.ToString(), func, line);
        }

        public static string createChangeFuncLog(int line, string fun, string funC)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("    + Line {0}, [{1}] convert to [{2}]\r\n");

            return string.Format(sb.ToString(), line, fun, funC);
        }
        #endregion

    }
}
