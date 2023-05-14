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
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics; 
using QuickTools.QIO;
using QuickTools.QNet;
using QuickTools.QData;
using QuickTools.QCore;
using QuickTools.QColors;
using QuickTools.QConsole;
using QuickTools.QSecurity;
using QuickTools.QSecurity.FalseIO;

namespace QuickToolsScript
{
    public partial class CodeParser
    {
     

        /// <summary>
        /// This method set the exectution delegate Action that will handle the excution of the program
        /// </summary>
        /// <param name="action"></param>
        /// <param name="type"></param>
        public void SetExecution(string action, string type)
        {
              this.cache = new DataCacher();
              this.runner = new ScriptRunner();
              this.error = new ErrorHandeler();
              this.Target = Get.FixPath($"{ShellLoop.CurrentPath}{Get.Slash()}{type}");
              this.ClearTarget = Get.FixPath($"{ShellLoop.CurrentPath}");
              //type = Get.FixPath(type);
              // Get.Wait(this.Target);
            switch (action)
            {


                case "mkdir":
                    runner.Run(() => { Make.Directory(this.Target); });
                    break;
                case "touch":
                case "create":
                case "echo":
                    runner.Run(() => { Make.File(this.Target); });
                    break;
                case "rm":
                case "remove":
                case "delete":
                    runner.Run(() =>
                    {
                    if (File.Exists(this.Target))
                    {

                            //  Get.Yellow(this.Target);
                            System.GC.Collect();
                            System.GC.WaitForPendingFinalizers(); 
                            File.Delete(this.Target); 
                            return;
                        }

                        if (Directory.Exists(this.Target))
                        {
                            runner.Run(() => { Directory.Delete(this.Target); });

                            return;
                        }
                        else
                        {
                           Get.Print("File or Directory not found: ", type);
                        }
                    });
                    break;
                case "ls":
                case "list":
                case "list-files":
                    runner.Run(() => {
                        if (type[0] == '*')
                        {
                            //Get.Wait($"{this.Target.Substring(0,this.Target.LastIndexOf("*"))} {type.Substring(1)}");
                            Get.Ls(this.ClearTarget, type.Substring(1),true);
                            return;
                        }
                        else
                        {
                            Get.Ls(type);
                        }
                    });
                    break;
                case "ls-l":
                case "list-with-dates":
                    runner.Run(() =>
                    {
                        if(type != "-l")
                        {
                            Get.Ls(type, "");
                            return;
                        }
                        else
                        {
                            Get.Ls(ShellLoop.CurrentPath, "");
                        }
                   
                    });
                    break;
                case "cd":
                        runner.Run(() => {
                            //  Get.Cyan(ShellLoop.CurrentPath);
                            if (type[0] == '~')
                            {
                            }
                           if(type != ".." && type != Get.Slash() && type != $"{Get.Slash()}{Get.Slash()}"&&
                            type != $"{Get.Slash()}{Get.Slash()}{Get.Slash()}")
                            {
                                if (Directory.Exists(this.Target))
                                {
                                    ShellLoop.CurrentPath += $"{Get.Slash()}{type}";
                                    return;
                                }
                                else
                                {
                                    Get.Print("Directory Not Found:", type);
                                    return;
                                }
                              
                            }
                           if(type == "..")
                            {
                                ShellLoop.CurrentPath = ShellLoop.CurrentPath.Substring(0, ShellLoop.CurrentPath.LastIndexOf(Get.Slash()));
                            }

                        });
                    break;
                case "exe":
                    runner.Run(() => {
                        //Get.Wait(Get.IsWindow());
                        if (Get.IsWindow())
                        {
                            Process.Start(new ProcessStartInfo("cmd", $"/c start {type}"));
                        }
                        if (!Get.IsWindow())
                        {
                            Process.Start("open", type);
                        }
                    });
                    break;
                case "cat":
                    runner.Run(() => {
                        Get.WriteL(" ");
                       Get.Write(Reader.Read(this.Target));
                    });
                    break;
                case "size":
                case "du":
                    runner.Run(() => {
                      string file =  Get.FileSize(this.Target);
                        Get.Green();
                        Get.Write($"\n{Get.FileNameFromPath(this.Target)}\t");
                        Get.Yellow(); 
                        Get.Write($"{file}\n");
                    });
                    break;
                default:
                    error.DisplayError(ErrorHandeler.ErrorType.NotValidAction, this.Code);
                    break;
            }
        }
    }
}
/*
 
        public static void OpenBrowser(string url)
        {
            if (Get.IsWindow())
            {
                Process.Start(new ProcessStartInfo("cmd", $"/c start {url}"));
                return;
            }
            else
            {
                Process.Start("open", url);

            }
        }
 
 
 */
