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

        public void SetExecution(string action, string type)
        {
            DataCacher cache = new DataCacher();
            ScriptRunner runner = new ScriptRunner();
            ErrorHandeler error = new ErrorHandeler();

            switch (action)
            {
                case "touch":
                case "create":
                    runner.Run(() => { Make.File(type); });
                    break;
                case "rm":
                case "remove":
                case "delete":
                    if (File.Exists(type))
                    {
                        runner.Run(() => { File.Delete(type); }); 
                    }
                    if (Directory.Exists(type))
                    {
                        runner.Run(() => { Directory.Delete(type); });
                    }
                    break; 
                default:
                    error.DisplayError(ErrorHandeler.ErrorType.NotValidAction, this.Code);
                    break;
            }
        }
    }
}
