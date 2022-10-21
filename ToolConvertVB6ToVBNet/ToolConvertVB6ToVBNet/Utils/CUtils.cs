using System;
using System.Collections.Generic;
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
        public static string createItemBK(string name, string tag)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("\r\nBegin VB.TextBox {0}_bk\r\n");
            sb.Append("Height = 480\r\n");
            sb.Append("Left = 0\r\n");
            sb.Append("Tag = \"{1}\"\r\n");
            sb.Append("Top = 0\r\n");
            sb.Append("Width = 1200\r\n");
            sb.Append("End");

            return string.Format(sb.ToString(), name, tag);
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
        #endregion

    }
}
