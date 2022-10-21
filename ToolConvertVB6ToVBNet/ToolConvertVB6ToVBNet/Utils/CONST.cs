using System;
using System.Collections.Generic;

namespace ToolConvertVB6ToVBNet.Utils
{
    public static class CONST
    {
        public static string FILE_PATH = AppContext.BaseDirectory + @"\Setting.bin";

        #region Char 
        public static readonly char CHAR_EQUALS = '=';
        public static readonly char CHAR_COMMA = ',';
        #endregion

        #region String File 

        public static readonly string VB_FRM = ".frm";
        public static readonly string VB_FRM_BK = ".frm-bk";
        public static readonly string VB_VBP = ".vbp";
        public static readonly string VB_VBP_BK = ".vbp-bk";
        public static readonly string OBJ_OCX = ".ocx";

        public static readonly string VB_NET_DESIGN = ".Designer.vb";
        #endregion

        #region String 
        public static readonly string STR_VER_BAR = "|";
        public static readonly string STR_EQUALS = "=";
        public static readonly string STR_QUOTATION_MARKS = "\"";

        public static readonly string ITEM_VB6 = "---Item VB6---";
        public static readonly string ITEM_VB6_BK = "---Item VBNetBk---";
        public static readonly string ITEM_VBNET = "---Item VBNet---";
        public static readonly string FUNC_VBNET = "---Func VBNET---";

        public static readonly string STR_BEGIN = "Begin ";
        public static readonly string STR_END = "End";
        #endregion
    }
}