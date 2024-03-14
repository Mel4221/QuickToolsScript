using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuickTools.QCore;
using QuickTools.QIO;
using Settings;
using ErrorHandelers;
using MainLoop;
using States;
using System.Diagnostics;

namespace ClownShell
{
    namespace Init
    {
        public static class Program
        {
            /// <summary>
            /// The name.
            /// </summary>
            public const string Name = "ClownShell";
            public static int Main(string[] args)
            {
				ShellTrace.AddTrace("ClownShell Started");
        
				try
				{
                    ShellTrace.AddTrace($"Load Settings and Start Settings Watcher...");
                    //ShellTrace.AddTrace($"Auto Loading Settings Disabled!!!");
                    //ShellSettings.StartSettingsManager();

                    Get.Title(Program.Name);
                    ShellLoop shell;

                    if (args.Length == 0)
                    {
        						ShellTrace.AddTrace("Without Arguments");
        						shell = new ShellLoop();
                      shell.Start();
                      Shell.Exit();
                        return 0;
                    }
                    else
                    {
        						ShellTrace.AddTrace($"With Arguments Length: {args.Length}");
        						shell = new ShellLoop(args);
                      shell.Start();
                      Shell.Exit();
                        return 0;
                    }
                }
                catch (Exception ex)
                {
                    ShellTrace.AddTrace($"FATAL-ERROR Encountered {ex}");
                    ErrorHandeler error = new ErrorHandeler();
                    error.DisplayError(ErrorType.FATAL, "FATAL-ERROR");
                    Log.Event(ShellSettings.LogsFile, $"Shell Exited With a FATAL-ERROR More info in the logs file: \nStartTrace\n {ShellTrace.GetTrace()} \nEndTrace \nStartExeption \n{ex} \nEndExeption\n");
                    Get.White(ex);
                    Get.Alert($"There was a FATAL ERROR MORE INFO IN this path: \n{ShellSettings.LogsFile}");
                    Program.Main(new string[] { }); 
					        //Environment.Exit(1);
                    return 1;
                }
            }

        }
    }
}
