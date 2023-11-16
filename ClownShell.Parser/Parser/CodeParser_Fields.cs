using QuickTools.QData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser
{
    public partial class CodeParser
    {
        public string[] Code { get; set; } = new string[0];
        public string Action { get; set; } = "NULL";
        public string Type { get; set; } = "NULL";
        public string[] Parameters { get; set; } = new string[0];
    }
}
