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


        #endregion

    }
}
