using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuickTools.QIO;
using QuickTools.QNet;
using QuickTools.QData;
using QuickTools.QCore;
using QuickTools.QColors;
using QuickTools.QConsole;
using QuickTools.QSecurity;
using QuickTools.QCore;
using QuickTools.QSecurity.FalseIO;

namespace QuickToolsScript
{
    public partial class CodeParser
    {

        public void SetExecution(string action, string type, string[] parameters)
        {
            DataCacher cache = new DataCacher();
            ScriptRunner runner = new ScriptRunner();
            ErrorHandeler error = new ErrorHandeler();
            switch (action)
            {
                case "secure":
                    break;
                default:
                    error.DisplayError(ErrorHandeler.ErrorType.NotValidAction, this.Code);
                    break;
            }
        }

    }
}
