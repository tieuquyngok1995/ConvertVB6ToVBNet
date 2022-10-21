using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolConvertVB6ToVBNet.Model
{
    [Serializable]
    internal class SettingModel
    {
        public int mode { get; set; }

        public String directoryPath { get; set; }

        public Dictionary<string, string> dicItemVB6 { get; set; }

        public Dictionary<string, List<string>> dicItemVB6Bk { get; set; }

        public Dictionary<string, string> dicItemVBNet { get; set; }

        public Dictionary<string, string> dicFunVBNet { get; set; }

        public SettingModel() { }
    }
}
