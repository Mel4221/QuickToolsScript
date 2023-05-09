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
