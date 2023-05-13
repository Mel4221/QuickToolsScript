//
// ${Melquiceded Balbi Villanueva}
//
// Author:
//       ${Melquiceded} <${melquiceded.balbi@gmail.com}>
//
// Copyright (c) ${2089} MIT
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
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
using System.IO;

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
        

           // Get.Wait(Get.DataPath());
            this.cache = new DataCacher();
            this.runner = new ScriptRunner();
            this.error = new ErrorHandeler();
            this.Target = Get.FixPath($"{ShellLoop.CurrentPath}");
            switch (action)
            {
                case "reset-path":
                    ShellLoop.CurrentPath = Directory.GetCurrentDirectory();
                    break; 
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
                     runner.Run(() => { Get.Ls(this.Target); });
                   // Get.Ls(ShellLoop.CurrentPath);
                    break;
                case "ls-l":
                    runner.Run(() => { Get.Ls(this.Target,""); });
                    break;
                case "get-input":
                case "input":
                    runner.Run(() => {
                        cache.Cach("EntryInput", Get.Input("Type Something: ").Text);
                    });
                    break;
                case "select":
                case "-S":
                    runner.Run(() => {
                        string[] files = new FilesMaper().GetFiles(this.Target);
                        string[] folders = Directory.GetDirectories(this.Target);
                        string[] both = new string[files.Length+folders.Length];
                        if (files.Length > 0)
                        {
                            for (int current = 0; current < files.Length; current++)
                            {
                                both[current] = Get.FileNameFromPath(files[current]);
                            }
                        }
                        int bothLength = both.Length - 1;  
                        if(folders.Length > 0)
                        {
                            for (int current = 0; current < folders.Length; current++)
                            {
                                string path = folders[current];
                                both[bothLength] = path.Substring(path.LastIndexOf(Get.Slash()) + 1);
                                bothLength--;
                            }
                        }
            
                        Options option = new Options(both);
                        Options.Label = this.Target;
                        Options.SelectorL = "> "; 
                        Options.SelectorR = ""; 
                        int selection = option.Pick();
                        ShellLoop.SelectedOject = $"{this.Target}{Get.Slash()}{both[selection]}";
                        //Get.Yellow(this.Target);
                        //Get.Wait(ShellLoop.SelectedOject);
                    });
                    break;
                case "select?":
                case "selected":
                case "-S?":
                    runner.Run(() => {
                        Get.Blue();
                        Get.WriteL(" ");
                        Get.Write($"Object Selected: ");
                        Get.Yellow();
                        Get.Write($"{ShellLoop.SelectedOject}");
                    });
                    break;
                case "clear-selected":
                case "clear-S":
                     runner.Run(() => {
                         ShellLoop.SelectedOject = null;
                    });
                    break;
                case "history":
                    runner.Run(() => {
                      
                        MiniDB db =  new ShellLoop().GetHistory();
                        Get.WriteL(" ");
                        db.DataBase.ForEach((item) => {
                            Get.Green();
                            Get.Write($"No: {item.Id} ");
                            Get.Yellow();
                            Get.Write($"Command: {item.Value} ");
                            Get.Blue();
                            Get.Write($"Date: {item.Relation} ");
                            Get.WriteL(" ");
                        });
                    });
                    break; 
                default:
                    error.DisplayError(ErrorHandeler.ErrorType.NotValidAction, this.Code);
                    break;
            }
        }

    }
}
