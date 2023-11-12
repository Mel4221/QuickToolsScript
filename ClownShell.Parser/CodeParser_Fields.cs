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
        public string[] Code { get; set; }
        public VirtualStack VStack { get; set; } = new VirtualStack(); 
    }
}
