
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

using System.Diagnostics;
using System.Text;
using QuickTools.QIO;
using QuickTools.QData;
using QuickTools.QCore;
using ClownShell.Init;
using ClownShell.Helpers;
using ClownShell.ScripRunner;
using ClownShell.ErrorHandler; 
namespace ClownShell
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
            //string fix = type[0] == '>' ? type.Substring(1) : Get.FixPath($"{ShellLoop.CurrentPath}{Get.Slash()}{type}");
            this.cache = new DataCacher();
            this.runner = new ScriptRunner(this);// passing this class for it to have acess to the code being handle 
            this.error = new ErrorHandeler();
            //this.Target = Get.FixPath(fix);
            //this.SubTarget = Get.FixPath($"{ShellLoop.CurrentPath}");
            this.RedirectedText = new StringBuilder();
            string[] param =   this.GetParameters(parameters);
            this.Parameters = param;
            this.Action = action;
            this.Type = type;
            this.Target = $"{ShellLoop.CurrentPath}{Get.Slash()}{type}";
            this.SubTarget = $"{ShellLoop.CurrentPath}{Get.Slash()}{param[0]}";

            Get.Yellow("For Testing: ");
            Print.List(param);
            switch (action)
            {
                case "mv":
                    runner.Run(() =>
                    {

                        string _param = param[0];

                        this.Target = $"{ShellLoop.CurrentPath}{Get.Slash()}{type}";
                        this.SubTarget = $"{ShellLoop.CurrentPath}{Get.Slash()}{_param}";
                        Get.Blue($"Target: {this.SubTarget} SubTarget: {this.SubTarget}");

                        CodeParser parser = Helper.ResolvePath(this);

                        if (parser.PathResolved)
                        {
                            this.Target = parser.Target;
                            this.SubTarget = parser.SubTarget;
                        }

                        Binary.CopyBinaryFile(this.Target, this.SubTarget);
                        GC.Collect();
                        File.Delete(this.Target); 
                    });
                    break;
                case "create":
                    //create zero-file 1 file.txt
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
                        if (param.Length == 3)
                        {
                            if (param[2] == "-c" || param[2] == "-cancell")
                            {
                                runner.AllowToCancell = true;
                            }
                        }
                        runner.Run(() => {
                            //Get.Wait($"{this.SubTarget}");
                            Get.Red($"{Path.GetFullPath(this.SubTarget)}{Get.Slash()}{param[0]}");
                            Get.Blue($"{Get.Path}");
                            string str = $"{Get.RelativePath($"{this.Target}")}{this.SubTarget.Substring(this.SubTarget.IndexOf(Get.Slash()) + 1)}{Get.Slash()}{param[0]}";

                            Get.Yellow(str);

                            //Writer.Write("file.txt",str);
                            //Get.Wait();
                            FilesMaper maper = new FilesMaper(str);

                            maper.Map();

                            List<string> folders = maper.Directories;
                            List<string> files = maper.Files;

                            for (int current = files.Count - 1; current > 0; current--)
                            {
                                if (param.Length == 2)
                                {
                                    if (param[1] == "-d" || param[1] == "-debug")
                                    {

                                        Get.Red($"Deleting...: {files[current]}");

                                    }
                                    if (param[1] == ">")
                                    {
                                        this.RedirectedText.Append(files[current] + "\n");
                                    }

                                }
                            }
                            //Get.Ok();
                            GC.Collect();// to make sure that it release the path from the files 
                            for (int current = folders.Count - 1; current > 0; current--)
                            {
                                if (param.Length == 2)
                                {
                                    if (param[1] == "-d" || param[1] == "-debug")
                                    {
                                        Get.Red($"Deleting...: {folders[current]}");
                                    }
                                    if (param[1] == ">")
                                    {
                                        this.RedirectedText.Append(folders[current] + "\n");
                                    }
                                }

                            }
                            Get.Ok();
                            if (param.Length == 2)
                            {
                                if (param[1] == ">")
                                {
                                    Writer.Write($"{this.SubTarget}{Get.Slash()}{param[2]}", this.RedirectedText.ToString());
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
                            //Get.Wait($"{this.SubTarget}");
                            FilesMaper maper = new FilesMaper(this.SubTarget);
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
                                //Get.Wait($"Target: {this.Target} ClearTarget: {this.SubTarget} FilesCurrent: {files[current]} Param: {param[0]}");
                                //  Get.Yellow($" ClearTarget: {this.SubTarget} FilesCurrent: {files[current]} Param: {param[0]}");

                                // new QProgressBar().Display(fCurrent,goal); 
                            }
                            Get.Red($"{param[0]} Was Not Founded  FilesChecked: {files.Count}");
                            return;
                        });
                    }
                    if (type == "-all" || type == "-a" && param.Length >= 1)
                    {
                        runner.Run(() => {
                            //Get.Wait($"{this.SubTarget}");
                            FilesMaper maper = new FilesMaper(this.SubTarget);
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
                                }//if the file extention is equal to the file founded add it to the list 
                                if (param[0][0] == '*' && Get.FileExention(files[current]) == Get.FileExention(param[0]))
                                {
                                    founded.Add(files[current]);
                                }
                                //Get.Wait($"Target: {this.Target} ClearTarget: {this.SubTarget} FilesCurrent: {files[current]} Param: {param[0]}");
                                //  Get.Yellow($" ClearTarget: {this.SubTarget} FilesCurrent: {files[current]} Param: {param[0]}");

                                // new QProgressBar().Display(fCurrent,goal); 
                            }
                            if (founded.Count == 0)
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
                    runner.Run(() => {
                        // is trying to select multiple 
                        if (type.Contains("*"))
                        {

                            Get.Yellow($"Selecting Multiples");
                            CodeParser helper = Helper.ResolvePath(this);
                            this.SubTarget = helper.SubTarget;
                            this.Target = helper.Target;
                            Get.Green($"Target: {this.Target} SubTarget: {this.SubTarget}");
                            string path = this.Target.Substring(0,this.Target.IndexOf("*"));
                            bool allowDebugger = false; 
                            Get.Yellow($"Source: {path}");
                            FilesMaper maper = new FilesMaper(path);
                            //allow Debgugger condition
                            if(param.Length > 1)
                            {
                                if (param[1] == "-d" || param[1] == "-debug")
                                {
                                    maper.AllowDebugger = true;
                                    allowDebugger = true; 
                                }
                            }
                             List<string> files = maper.MapOnlyFiles(this.Target);
                            if(files.Count == 0)
                            {
                                Get.Red($"Not File Founded that match the given Extention: .{Get.FileExention(this.Target)}");
                                return;
                            }
                             for(int f = 0; f < files.Count; f++)
                            {
                                if (allowDebugger)
                                {
                                    Get.Green($"{this.SubTarget}{Get.FileNameFromPath(files[f])}");
                                }
                                
                                Get.Red($"{this.SubTarget}{Get.FileNameFromPath(files[f])}");
                                Binary.CopyBinaryFile(files[f], $"{this.SubTarget}{Get.FileNameFromPath(files[f])}");
                            }


                            return;
                        }
                        
                        string _param = param[0];
                        this.Target = $"{ShellLoop.CurrentPath}{Get.Slash()}{type}";
                        this.SubTarget = $"{ShellLoop.CurrentPath}{Get.Slash()}{_param}";

                        Get.Blue($"Target: {this.SubTarget} SubTarget: {this.SubTarget}");

                        CodeParser parser = Helper.ResolvePath(this);

                        if (parser.PathResolved)
                        {
                            this.Target = parser.Target;
                            this.SubTarget = parser.SubTarget; 
                        }
                        
                        Binary.CopyBinaryFile(this.Target, this.SubTarget);

                     //   Get.Red($"Target: {obj.Target} SubTarget: {obj.SubTarget}");
                        return;

                        ////this.SubTarget = param[0]; 
                        //// to create a local variable 
                        //if (Helper.ReferToDisk(type))
                        //{
                        //    this.Target = type;
                        //    Get.Cyan($"Refer To Disk the Type: {type} = Target");
                        //}

                        //if (Helper.ReferToDisk(_param))
                        //{
                        //    this.SubTarget = _param;
                        //    Get.Cyan($"Refer To Disk The Param[0]: {_param} = ClearTarget");

                        //}
                        ////type.Substring(type.IndexOf(Get.Slash()) + 1).ToLower()
                        //if (Helper.HasSpecialFolder(_param) != null)
                        // {
                        //     this.SubTarget = Helper.HasSpecialFolder(_param);
                        //     Get.Cyan($"Has Special Folder param[0]: {_param} = ClearTarget");
                        // }

                        // if (Helper.HasSpecialFolder(type) != null)
                        // {
                        //    //this.Target = $"{Helper.HasSpecialFolder(type)}{Get.Slash()}{this.SubTarget}";
                        //    this.Target = $"{Helper.HasSpecialFolder(type)}";

                        //    Get.Cyan($"Has Special Folder Type: {type} = Target");
                        // }
                        // if (_param== ".")
                        // {
                        //     string slash = ShellLoop.CurrentPath[ShellLoop.CurrentPath.Length - 1].ToString() == Get.Slash() ? null : Get.Slash();

                        //     this.SubTarget = $"{this.SubTarget}{slash}{Get.FileNameFromPath(this.Target)}";
                        //     Get.Cyan($"Refer to the local directory param[0]: {_param} = ClearTarget");

                        //     //Get.Wait(this.SubTarget);
                        // }
                      
                     //    Get.Wait(Helper.HasSpecialFolder("desktop"));

                         //Get.Yellow($"Target: {this.Target}  ClearTarget: {this.SubTarget}");


                        //reads the data // this version works perfect 
                        //File.SetAttributes(this.Target, FileAttributes.Normal);
                        //File.Copy(this.Target, this.SubTarget);

                        // byte[] bytes = Binary.Reader(this.Target);
                        //Get.Yellow($"File: {this.Target} Length: {bytes.Length} Hash: {Get.HashCode(bytes)}");

                        //Binary.CopyBinaryFile(this.Target, this.SubTarget);

                        //write's it at the given clearTarget
                        //Binary.Writer(this.SubTarget, bytes);

                        //File.SetAttributes(this.SubTarget, FileAttributes.Normal);

                        // Writer.Write()
                        // Get.Yellow($"Status: {status}");
                        /*
                            Get.Green($"Target: {this.Target} Clear: {this.SubTarget} Param: {param[0]}");
                           Get.Green($"With Helper: {Helper.CheckForPath(param[0])}");
                           Get.Green($"Type: {Get.FixPath(type.ToUpper())} ");
                           Get.Yellow($"Refer To Disk: {ShellLoop.ReferToDisk(type.ToUpper())} Type: {type.ToUpper()}");
                           */

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
                            
                            return;
                        }
                        
                    });
                       
                    break;
                case "exe":
                    runner.Run(() => {
                        //Get.Wait(Get.IsWindow());
                        if (Get.IsWindow())
                        {
                            Process cmd = new Process();
                           // Get.Yellow(this.Target);
                            cmd.StartInfo.FileName = type;// $"{ShellLoop.CurrentPath}{Get.Slash()}{type}";//"cmd.exe";
                                                                      //cmd.StartInfo.Arguments;
                                                                      //cmd.StartInfo.RedirectStandardInput = true;
                            cmd.StartInfo.RedirectStandardOutput = false;  // true;
                            cmd.StartInfo.CreateNoWindow = false;
                            cmd.StartInfo.UseShellExecute = false;
                            cmd.StartInfo.Arguments = IConvert.ArrayToText(param);// Helper.ResolvePath(this).Target; //"ping www.google.com"; //Helper.ResolvePath(this).Target;

                            cmd.Start();
                            cmd.WaitForExit();
                        }
                        if (!Get.IsWindow())
                        {
                            Process.Start("open", type);
                        }
                    });
                    break;
                case "wget":
                    break;
                case "secure":
                    switch (type)
                    {
                        case "encrypt":
                        case "-e":
                        case "-E":
                            Print.List(param);

                            Get.Red("Not Done Yet"); 
                           


                            //secure.Encrypt()
                            break;
                        case "decrypt":
                        case "-d":
                        case "-D":
                            Get.Red("Not Done Yet");
                            break;
                    }                    
                    break;
                default:
                    error.DisplayError(ErrorHandeler.ErrorType.NotValidAction, this.Code);
                    break;
            }
        }

    }
}
