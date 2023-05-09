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
        public  void Print(object content,object type )
        {
            Get.Red();
            Get.Write($"\n{content} ");
            Get.Yellow();
            Get.Write($"'{type}'\n");
        }

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
            this.Target = $"{ShellLoop.CurrentPath}{Get.Slash()}{type}"; 
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
                            this.Print("File or Directory not found: ", type);
                        }
                    });
                    break;
                case "ls":
                case "list":
                case "list-files":
                    runner.Run(() => { Get.Ls(type); });
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
                                    this.Print("Directory Not Found:", type);
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
                       Get.WriteL(Reader.Read(this.Target));
                    });
                    break;  
                default:
                    error.DisplayError(ErrorHandeler.ErrorType.NotValidAction, this.Code);
                    break;
            }
        }
    }
}
