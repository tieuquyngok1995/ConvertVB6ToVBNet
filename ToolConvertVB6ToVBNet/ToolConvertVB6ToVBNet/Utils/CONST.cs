using System;
using System.Collections.Generic;

namespace ToolConvertVB6ToVBNet.Utils
{
    public static class CONST
    {
        public static string FILE_PATH = AppContext.BaseDirectory + @"\Setting.bin";
        public static string[] STRING_SEPARATORS = new string[] { "\r\n" };

        #region Char 
        public static readonly char CHAR_QUOTATION_MARKS = '"';
        public static readonly char CHAR_DOT = '.';
        public static readonly char CHAR_EQUALS = '=';
        public static readonly char CHAR_COMMA = ',';
        public static readonly char CHAR_VER_BAR = '|';
        #endregion

        #region String File 

        public static readonly string VB_FRM = ".frm";
        public static readonly string VB_FRM_BK = ".frm-bk";
        public static readonly string VB_VBP = ".vbp";
        public static readonly string VB_VBP_BK = ".vbp-bk";
        public static readonly string OBJ_OCX = ".ocx";
        public static readonly string OBJ_OIP11 = "oip11.tlb";

        public static readonly string VB_NET_DESIGN = ".Designer.vb";
        public static readonly string VB_NET_DESIGN_BK = ".Designer.vb-bk";
        public static readonly string VB_NET_VB = ".vb";
        public static readonly string VB_NET_VB_BK = ".vb-bk";
        public static readonly string FILE_LOG = ".log";

        public static readonly string FILE_ASSEMBLY = "AssemblyInfo.vb";

        #endregion

        #region String 
        public static readonly string STR_VER_BAR = "|";
        public static readonly string STR_ROUND_BRAC = "(";
        public static readonly string STR_EQUALS = "=";
        public static readonly string STR_DOT = ".";
        public static readonly string STR_UNDERSCORE = "_";
        public static readonly string STR_QUOTATION_MARKS = "\"";
        public static readonly string STR_QUOTATION_MARKS_CHANGE = "[-]";
        public static readonly string STR_NEW_LINE = "\r\n";

        public static readonly string STR_BK = "Backup";
        public static readonly string STR_INDEX = "Index";
        public static readonly string STR_SET_INDEX = ".SetIndex";
        public static readonly string STR_TAG = ".Tag";
        public static readonly string STR_ADD = ".Add";
        public static readonly string STR_IME_MODE = ".ImeMode";
        public static readonly string STR_IME_MODE_HIRAGANE = "System.Windows.Forms.ImeMode.Hiragana";
        public static readonly string STR_IME_MODE_KATAKANA = "System.Windows.Forms.ImeMode.Katakana";
        public static readonly string STR_IME_MODE_KATAKANA_HF = "System.Windows.Forms.ImeMode.KatakanaHalf";
        public static readonly string STR_CHK_ASCII_KANJI = "ChkAscii = \"ASCIITYPE_KANJI\"";
        public static readonly string STR_CHK_ASCII_KANA = "ChkAscii = \"ASCIITYPE_KANA\"";
        public static readonly string STR_NEW = "New";

        public static readonly string ITEM_VB6 = "---Item VB6---";
        public static readonly string ITEM_VBNET = "---Item VBNet---";
        public static readonly string ITEM_VBNET_BK = "---Item VBNet Bk---";
        public static readonly string ITEM_VBNET_RE = "---Item VBNet Remove---";
        public static readonly string FUNC_VBNET = "---Func VBNet---";

        public static readonly string STR_VB6_BEGIN = "Begin ";
        public static readonly string STR_VB6_END = "End";
        public static readonly string STR_VBNET_W_EVENT = "Public WithEvents ";
        public static readonly string STR_VBNET_ME = "Me.";
        public static readonly string STR_VBNET_SYS_TEXTBOX = "System.Windows.Forms.TextBox";
        public static readonly string STR_VBNET_SYS_COMPONENT = "System.ComponentModel.ISupportInitialize";
        public static readonly string STR_VBNET_UPGRADE_WARNING = "UPGRADE_WARNING: Couldn't resolve default property of object";
        public static readonly string STR_VBNET_STRICT_OFF = "Option Strict Off";
        public static readonly string STR_VBNET_EXPLICIT_ON = "Option Explicit On";
        public static readonly string STR_VBNET_PUBLIC_SUB = "Public Sub";
        public static readonly string STR_VBNET_PRIVATE_SUB = "Private Sub";
        public static readonly string STR_VBNET_PUBLIC_FUNC = "Public Function";
        public static readonly string STR_VBNET_PRIVATE_FUNC = "Private Function";
        public static readonly string STR_VBNET_END_FUNC = "End Function";
        public static readonly string STR_VBNET_END_SUB = "End Sub";
        #endregion
    }
}