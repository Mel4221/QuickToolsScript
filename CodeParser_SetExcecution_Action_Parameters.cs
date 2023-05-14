
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
// THE SOFTWARE.using System;
using System; 
using System.IO;
using System.Diagnostics;
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
using System.Threading;
using QuickTools.QSecurity.FalseIO;

namespace QuickToolsScript
{
    public partial class CodeParser
    {
        private string[] GetParameters(string[] parameters)
        {
            string[] param;
            int current, goal;
            goal = parameters.Length;
            string cmds = ""; 
            for (current = 2; current < goal; current++)
            {
                cmds +=  Get.FixPath(parameters[current])+" ";
            }
            param = IConvert.TextToArray(cmds);
            return param;
        }

        public void SetExecution(string action, string type, string[] parameters)
        {
            string fix = type[0] == '>' ? type.Substring(1) : Get.FixPath($"{ShellLoop.CurrentPath}{Get.Slash()}{type}");
            this.cache = new DataCacher();
            this.runner = new ScriptRunner();
            this.error = new ErrorHandeler();
            this.Target = Get.FixPath(fix);
            this.ClearTarget = Get.FixPath($"{ShellLoop.CurrentPath}");

            string[] param =   this.GetParameters(parameters);
            Get.Yellow("For Testing: ");
            Print.List(param);
            switch (action)
            {
                case "mv":
                    break;
                case "create":
                    if (type == "zero-file" && param.Length >= 2)
                    {
                        if (Get.IsNumber(param[0]))
                        {
                            runner.Run(() => {
                                Console.Title = Console.Title + "Working With A BackGround Job";
                                int size = int.Parse(param[0]);
                                Binary.CreateZeroFile(param[1], size, false);
                                Console.Title = " Task Completed";
                                Thread.Sleep(5000);
                                Console.Title = Program.Name;
                            }, true);
                            return;
                        }
                        else
                        {
                            Get.Yellow($"The Right Way is: ");
                            Get.Green($"create zero-file gbSize fileName");
                            Get.Green("create zero-file 1 zeroFile.iso");

                        }
                    }
                    break;
                case "rm":
                    if (type == "-r" && param.Length >= 1)
                    {
                        runner.Run(() => {
                            //Get.Wait($"{this.ClearTarget}");
                            FilesMaper maper = new FilesMaper(this.ClearTarget);
                            maper.Map();
                            List<string> folders = maper.Directories;
                            List<string> files = maper.Files;
                            for (int current = files.Count - 1; current > 0; current--)
                            {
                                Get.Yellow($"Deleting...: {files[current]}");
                                File.Delete(files[current]); 
                            }
                            GC.Collect();// to make sure that it release the path from the files 
                            for (int current = folders.Count - 1; current > 0; current--)
                            {
                                Get.Red($"Deleting...: {folders[current]}");
                                Directory.Delete(folders[current]);
                            }

                        });
                    }
                    break;
                case "cp":
                    //Get.Wait($"T: {this.Target} CT: {this.ClearTarget}");
                    runner.Run(() => {
                        //Get.Wait($"Target: {this.Target}");
                        //  Get.Wait($"{this.Target}  {this.ClearTarget}");

                      //  Get.Wait($"Target: {this.Target} Param: {Helper.CheckForPath(param[0])}");

                        if (File.Exists(this.Target))
                        {
                            Get.Wait($"{this.Target} {Helper.CheckForPath(param[0])}");
                            File.Copy(this.Target, Helper.CheckForPath(param[0]));
                            Get.Ok();
                            return;
                        }
                        else
                        {
                            Get.Print("It looks like i did not find the file ", this.Target); 
                        }
                    });
                    break;
                case "ls":
                case "list":
                case "list-files":
                    runner.Run(() => {
                        if (param.Length != 1)
                        {
                            error.DisplayError(ErrorHandeler.ErrorType.NotValidParameter, this.Code);

                            return;
                        }
                     
                        if (type == "-l")
                        {
                            Get.Ls(param[0], "");
                            return;
                        }
                        
                    });
                    break;
                case "wget":
                    break;
                case "secure":
                    break;
                default:
                    error.DisplayError(ErrorHandeler.ErrorType.NotValidAction, this.Code);
                    break;
            }
        }

    }
}
