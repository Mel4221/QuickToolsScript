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
                case "ls":
                        runner.Run(() => { Get.Ls(type); });
                    break;
                case "ls-l":
                        runner.Run(() => { Get.Ls(type,""); });
                    break;
                case "cd":
                        runner.Run(() => {
                          //  Get.Cyan(ShellLoop.CurrentPath);
                           if(type != "..")
                            {
                                if (Directory.Exists($"{ShellLoop.CurrentPath}{Get.Slash()}{type}"))
                                {
                                    ShellLoop.CurrentPath += $"{Get.Slash()}{type}";
                                    return;
                                }
                                else
                                {
                                    Get.Red();
                                    Get.Write($"\n Directory Not Found: ");
                                    Get.Yellow();
                                    Get.Write($"'{type}'\n");
                                    return;
                                }
                              
                            }
                           if(type == "..")
                            {
                                ShellLoop.CurrentPath = ShellLoop.CurrentPath.Substring(0, ShellLoop.CurrentPath.LastIndexOf(Get.Slash()));
                            }

                        });
                    break;
                case "cat":
                    runner.Run(() => {
                       Get.WriteL(Reader.Read($"{ShellLoop.CurrentPath}{Get.Slash()}{type}"));
                    });
                    break;  
                default:
                    error.DisplayError(ErrorHandeler.ErrorType.NotValidAction, this.Code);
                    break;
            }
        }
    }
}
