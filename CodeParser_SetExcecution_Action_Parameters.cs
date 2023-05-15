
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
                        /*
                         * condition that allow to cancel or stop  the ScriptRunner
                         */
                        if(param.Length == 3)
                        {
                            if (param[2] == "-c" || param[2] == "-cancell")
                            {
                                runner.AllowToCancell = true; 
                            }
                        }
                        runner.Run(() => {
                            //Get.Wait($"{this.ClearTarget}");
                            Get.Wait($"{Path.GetFullPath(this.ClearTarget)}{Get.Slash()}{param[0]}");
                            FilesMaper maper = new FilesMaper($"{Path.GetFullPath(this.ClearTarget)}{Get.Slash()}{param[0]}");
                            maper.Map();
                            List<string> folders = maper.Directories;
                            List<string> files = maper.Files;

                            for (int current = files.Count - 1; current > 0; current--)
                            {
                                if(param.Length == 2)
                                {
                                    if (param[1] == "-d" || param[1] == "-debug")
                                    {
                                        Get.Red($"Deleting...: {files[current]}");

                                    }

                                }
                            }
                            GC.Collect();// to make sure that it release the path from the files 
                            for (int current = folders.Count - 1; current > 0; current--)
                            {
                                if (param.Length == 2)
                                {
                                    if (param[1] == "-d" || param[1] == "-debug")
                                    {
                                        Get.Red($"Deleting...: {folders[current]}");
                                    }
                                }
                            
                             }

                        });
                    }
                    break;
                case "find":
                case "search":
                       /*
                        * condition that allow to cancel or stop  the ScriptRunner
                        */
                        if (param.Length == 3)
                        {
                            if (param[2] == "-c" || param[2] == "-cancell")
                            {
                                runner.AllowToCancell = true;
                            }
                        }
                    // if is retroactive 
                    if (type == "-r" && param.Length >= 1)
                    {
                        runner.Run(() => {
                            //Get.Wait($"{this.ClearTarget}");
                            FilesMaper maper = new FilesMaper(this.ClearTarget);
                            if(param.Length == 2)
                            {
                                if (param[1] == "-d" || param[1] == "-debug")
                                {
                                    maper.AllowDebugger = true; 
                                }
                            }
                            maper.Map();
                            List<string> folders = maper.Directories;
                            List<string> files = maper.Files;
                            int fCurrent, goal;
                            goal = files.Count - 1;
                            // start at the end of the list the loop 
                            for (int current = files.Count - 1; current > 0; current--)
                            {
                                fCurrent = current;
                                if (param[0] == Get.FileNameFromPath(files[current]))
                                {
                                    ShellLoop.SelectedOject = files[current]; // auto select the object 
                                    Get.Green($"Founded: {files[current]}");
                                    this.SetExecution("selected"); // print the seleted object 
                                    return; 
                                }
//Get.Wait($"Target: {this.Target} ClearTarget: {this.ClearTarget} FilesCurrent: {files[current]} Param: {param[0]}");
                              //  Get.Yellow($" ClearTarget: {this.ClearTarget} FilesCurrent: {files[current]} Param: {param[0]}");

                               // new QProgressBar().Display(fCurrent,goal); 
                            }
                            Get.Red($"{param[0]} Was Not Founded  FilesChecked: {files.Count}");
                            return;
                        });
                    }
                    if (type == "-all" || type == "-a" && param.Length >= 1)
                    {
                        runner.Run(() => {
                            //Get.Wait($"{this.ClearTarget}");
                            FilesMaper maper = new FilesMaper(this.ClearTarget);
                            if (param.Length == 2)
                            {
                                if (param[1] == "-d" || param[1] == "-debug")
                                {
                                    maper.AllowDebugger = true;
                                }
                            }
                            maper.Map();
                            List<string> folders = maper.Directories;
                            List<string> files = maper.Files;
                            List<string> founded = new List<string>(); 
                            int fCurrent, goal;
                            goal = files.Count - 1;
                            for (int current = files.Count - 1; current > 0; current--)
                            {
                                fCurrent = current;
                                if (param[0] == Get.FileNameFromPath(files[current]))
                                {

                                    founded.Add(files[current]);
                                    //Get.Green($"Founded: {files[current]}"); 
                                }
                                if (param[0][0] == '*' && Get.FileExention(files[current]) == Get.FileExention(param[0]))
                                {
                                    founded.Add(files[current]);
                                }
                                //Get.Wait($"Target: {this.Target} ClearTarget: {this.ClearTarget} FilesCurrent: {files[current]} Param: {param[0]}");
                                //  Get.Yellow($" ClearTarget: {this.ClearTarget} FilesCurrent: {files[current]} Param: {param[0]}");

                                // new QProgressBar().Display(fCurrent,goal); 
                            }
                            if(founded.Count == 0)
                            {
                                Get.Red($"{param[0]} Was Not Founded  FilesChecked: {files.Count}");
                                return;
                            }
                            else
                            {
                                founded.ForEach((item) => { Get.Yellow(item); });
                                Get.Green($"File: {param[0]} Founded {founded.Count} Time's");
                            }
                            return;
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
