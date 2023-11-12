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
namespace ClownShell
{
    namespace Init
    {
        public static class Program
        {

            public const string Name = "ClownShell";
            public static int Main(string[] args)
            {

                try
                {

                    Get.Title(Program.Name);
                    ShellLoop shell;

                    if (args.Length == 0)
                    {
                        shell = new ShellLoop();
                        shell.Start();
                        return 0;
                    }
                    else
                    {
                        shell = new ShellLoop(args);
                        shell.Start();
                        return 0;
                    }
                }
                catch (Exception ex)
                {
                    ErrorHandeler error = new ErrorHandeler();
                    error.DisplayError(ErrorType.FATAL, "FATAL-ERROR");
                    Log.Event(ShellSettings.LogsFile, $"Shell Exited With a FATAL-ERROR More info in the logs file:  \n{ex}");
                    Get.White(ex);
                    Get.Alert($"There was a FATAL ERROR MORE INFO IN this path: \n{ShellSettings.LogsFile}.log");
                    Environment.Exit(1);
                    return 1;
                }
            }

        }
    }
}
