using System;
using System.Collections.Generic;

namespace ToolConvertVB6ToVBNet.Utils
{
    public static class CONST
    {
        public static string FILE_PATH = AppContext.BaseDirectory + @"\Setting.bin";
        public static string[] LIST_ITEM = new string[]
        {
                "imMask7Ctl.imMask","imNumber7Ctl.imNumber","imText6Ctl.imText", "FPSpreadADO.fpSpread",
                "imText7Ctl.imText", "imTime7Ctl.imTime","imCalendar7Ctl.imCalendar"
        };
        public static IDictionary<String, string> numberNames = new Dictionary<String, string>()
        {
            {"Imt","MitaniCorp.Base.Controls.CusTextBox" },
            {"Imn","MitaniCorp.Base.Controls.CusNumber" },
            {"Imd","MitaniCorp.Base.Controls.EraDate" },
            {"Imm","MitaniCorp.Base.Controls.CusMask" },
            {"Imh","MitaniCorp.Base.Controls.CusTime" }
        };

        #region Char 
        public static readonly char CHAR_EQUALS = '=';
        #endregion

        #region String File 
        public static readonly string VB_TEXTBOX = "VB.TextBox";

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

        public static readonly string STR_DIS_FORMAT = "DisplayFormat";
        public static readonly string STR_MAX_VALUE = "MaxValue";
        public static readonly string STR_MIN_VALUE = "MinValue";

        public static readonly string STR_END = "End";
        #endregion
    }
}