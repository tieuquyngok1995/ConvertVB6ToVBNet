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

        public SettingModel() { }
    }
}
