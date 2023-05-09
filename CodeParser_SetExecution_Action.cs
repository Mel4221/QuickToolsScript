using System;
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
        /// <summary>
        /// runs the action without any other 
        /// </summary>
        /// <param name="action"></param>
        public void SetExecution(string action)
        {
            DataCacher cache = new DataCacher();
            ScriptRunner runner = new ScriptRunner();
            ErrorHandeler error = new ErrorHandeler();
            switch (action)
            {
                case "console-clear":
                case "clear":
                    runner.Run(() => { Get.Clear(); });
                    break;
                case "set-color-pink":
                case "pink":
                    runner.Run(() => { Get.Pink(); });
                    break;
                case "set-color-red":
                case "red":
                    runner.Run(() => { Get.Red(); });
                    break;
                case "set-color-blue":
                case "blue":
                    runner.Run(() => { Get.Blue(); });
                    break;
                case "set-color-yellow":
                case "yellow":
                    runner.Run(() => { Get.Yellow(); });
                    break;
                case "set-color-green":
                case "green":
                    runner.Run(() => { Get.Green(); });
                    break;
                case "set-color-gray":
                case "gray":
                    runner.Run(() => { Get.Gray(); });
                    break;
                case "set-color-cyan":
                case "cyan":
                    runner.Run(() => { Get.Cyan(); });
                    break;
                case "set-color-black":
                case "black":
                    runner.Run(() => { Get.Black(); });
                    break;
                case "clear-cache":
                case "cache-reset":
                    runner.Run(() => { cache.ClearCache(); });
                    break;
                case "clear-logs":
                    runner.Run(() => { Log.ClearLogs(); });
                    break;
                case "ls":
                     runner.Run(() => { Get.Ls(ShellLoop.CurrentPath); });
                   // Get.Ls(ShellLoop.CurrentPath);
                    break;
                case "ls-l":
                    runner.Run(() => { Get.Ls(ShellLoop.CurrentPath,""); });
                    break;
                case "get-input":
                case "input":
                    runner.Run(() => {
                        cache.Cach("EntryInput", Get.Input("Type Something: ").Text);
                    });
                    break;
                default:
                    error.DisplayError(ErrorHandeler.ErrorType.NotValidAction, this.Code);
                    break;
            }
        }

    }
}
