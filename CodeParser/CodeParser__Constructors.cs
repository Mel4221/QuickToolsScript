using QuickTools.QCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClownShell
{
    public partial class CodeParser
    {

        public CodeParser()
        {

        }

        public CodeParser(string frofileOrCode)
        {
            this.Code = IConvert.TextToArray(frofileOrCode);
        }
        public CodeParser(string[] fromArgs)
        {
            this.Code = fromArgs;

        }

    }
}
