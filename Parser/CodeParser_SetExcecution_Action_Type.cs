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

using System.Diagnostics;
using QuickTools.QIO;
using QuickTools.QData;
using QuickTools.QCore;
using QuickTools.QConsole;
using ClownShell.Init;
using ClownShell.Helpers;
using ClownShell.ScripRunner;
using ClownShell.ErrorHandler;
using ClownShell.BackGroundFunctions;
using System;
using System.IO;
using System.Threading;
using System.Collections.Generic; 

namespace ClownShell.Parser
{
    public partial class CodeParser
    {
        private void Call(string action , string type , string[] param)
        {
            this.SetExecution(action , type , param );  
        }
        private void Call(string action , string type)
        {
            this.SetExecution(action, type); 
        }
        private void Call(string action)
        {
            this.SetExecution(action); 
        }

        /// <summary>
        /// This method set the exectution delegate Action that will handle the excution of the program
        /// </summary>
        /// <param name="action"></param>
        /// <param name="type"></param>
        public void SetExecution(string action, string type)
        {

            //string fix = type[0] == '>' ? type.Substring(1) : Get.FixPath($"{ShellLoop.CurrentPath}{Get.Slash()}{type}");
            //Get.Wait(fix);
            this.cache  =  new DataCacher();
            this.runner =  new ScriptRunner();
            this.runner.RunningCode = this.Code;
            this.error  =  new ErrorHandeler();

            // get the path with the given type and if it does not have an slash add it acordintly 
            //string tar = type[0] == '~' || type[0] == '.' || Helper.ReferToDisk(type)? type :  $"{ShellLoop.CurrentPath}{Get.Slash()}{type}";

            //if (tar.Contains('@'))
            //{
            //    tar = tar.Substring(tar.IndexOf('@')+1);
            //}
            //Get.Wait(Get.FixPath("*.txt"));
            //this.Target = Get.FixPath(tar);

            // this = Helper.ResolvePath(this).Target; 
            // this.Target = temp == null || temp == ""? type:temp; 
            this.SubTarget = Get.FixPath(type);
            //this.Target = $"{this.Target}{Get.Slash()}{this.SubTarget}"; 
            //Get.Yellow($"Target: {this.Target} SubTarget: {this.SubTarget}");
            //Get.Cyan($"Helper: {Helper.ResolvePath(this).Target}");

            //this.Target = Get.FixPath(fix;
            //this.SubTarget = Get.FixPath($"{ShellLoop.CurrentPath}");
            //type = Get.FixPath(type);
            // Get.Wait(this.Target);
            switch (action)
            {


                case "wait":
                    this.runner.RunningCodeInfo = $"waiting for {type}ms";
                    this.runner.RunningBackGroundCodeName = "Process Wait...";
                    runner.Run(() => {
                        this.Call("sleep",type);
                        this.Call("title", "|");
                        this.Call("sleep", type);
                        this.Call("title", "-");
                        this.Call("sleep", type);
                        this.Call("title", "/");
                        this.Call("sleep", type);
                        this.Call("title", "-");
                        this.Call("sleep", type);
                        this.Call("title", "\\");
                        this.Call("sleep", type);
                        this.Call("title", "|");
                        this.Call("sleep", type);
                        this.Call("reset-title");
                        return; 
                    }, true); 
                    break;
                case "resume":
                    runner.Run(() => {
                        BackGroundJob.Resume(int.Parse(type));
                    });
                    break;
                case "pause":
                case "stop":
                    runner.Run(() => {
                        BackGroundJob.Pause(int.Parse(type));
                    });
                    break;
                case "kill":
                    runner.Run(() => {
                        BackGroundJob.Kill(int.Parse(type));    
                    }); 
                    break;
                case "set-title":
                case "title":
                    runner.Run(() => {
                        Get.Title(type); 
                    });
                    break;
                case "mkdir":
                    runner.Run(() => { 
                        
                        Make.Directory(this.Target); 
                    });
                    break;
                case "touch":
                case "create":
                case "echo":
                    runner.Run(() => { Make.File(type); });
                    break;
                case "set-color-pink":
                case "pink":
                    runner.Run(() => { Get.Pink(type); });
                    break;
                case "set-color-red":
                case "red":
                    runner.Run(() => { Get.Red(type); });
                    break;
                case "set-color-blue":
                case "blue":
                    runner.Run(() => { Get.Blue(type); });
                    break;
                case "set-color-yellow":
                case "yellow":
                    runner.Run(() => { Get.Yellow(type); });
                    break;
                case "set-color-green":
                case "green":
                    runner.Run(() => { Get.Green(type); });
                    break;
                case "set-color-gray":
                case "gray":
                    runner.Run(() => { Get.Gray(type); });
                    break;
                case "set-color-cyan":
                case "cyan":
                    runner.Run(() => { Get.Cyan(type); });
                    break;
                case "set-color-black":
                case "black":
                    runner.Run(() => { Get.Black(type); });
                    break;
                case "rm":
                case "remove":
                case "delete":
                    runner.Run(() =>
                    {
                    if (File.Exists(this.Target))
                    {

                            //  Get.Yellow(this.Target);
                             GC.Collect();
                             GC.WaitForPendingFinalizers(); 
                            File.Delete(this.Target); 
                            return;
                    }

                    if (Directory.Exists(this.Target))
                    {
                            GC.Collect();
                            GC.WaitForPendingFinalizers();
                            Directory.Delete(this.Target);
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

                      
                        if (this.Target == "disk")
                        {
                            Get.PrintDisks();
                            return;
                        }if(this.Target == "-l")
                        {
                            Get.Ls(this.Target, null);
                            return;
                        }if(this.Target == ".")
                        {
                            Get.Ls(ShellLoop.CurrentPath); 
                            return;
                        }
                        else
                        {
                            Get.Ls(type); 
                            return;
                        }


                        //Get.Yellow($"{this.Target}     ClearTarget: {this.SubTarget} Type: {type}");
                        //Get.Blue(Path.GetDirectoryName(this.Target));
                        // Get.Yellow(ShellLoop.RelativePath);
                        // Get.Wait(type);
                        //this.SubTarget = type; 
                        //CodeParser helper = Helper.ResolvePath(this);
                        //this.Target = helper.Target;
                        //this.SubTarget = helper.SubTarget;
                        //Get.Cyan($"Target: {this.Target} SubTarget: {this.SubTarget}");
     
                        //if(type == "-l")
                        //{
                        //    Get.Ls(helper.Target, "");
                        //    return; 
                        //}
                        //if (type.Contains('*'))
                        //{
                        //    //Get.Wait($"{this.Target.Substring(0,this.Target.LastIndexOf("*"))} {type.Substring(1)}");
                        //    // Get.Wait(type);  //Get.FileExention(type)
                        //    //Get.Wait(this.Target.Substring(this.Target.LastIndexOf(Get.Slash())));
                        //    //this get all the files that has this given type 
                        //    Get.Ls(this.Target,this.SubTarget,true);
                        //    return;
                        //}
                        //else
                        //{
                        //    if (Directory.Exists(this.Target))
                        //    {
                        //        Get.Ls(this.Target);
                        //        return;
                        //    }
             
                        //}
                    });
                    break;
                case "sleep":
                    runner.Run(() => {
                        int number;
                        if (Get.IsNumber(type))
                        {
                           
                            number = int.Parse(type);
                            Thread.Sleep(number); 
                            return;
                        }
                        else
                        {
                            new ErrorHandeler().DisplayError(ErrorHandeler.ErrorType.NotValidParameter, "The Value Must Be a number");
                        }
                    }); 
                    break;
                case "cd":
                        runner.Run(() => {

                            //Get.Yellow($"{this.Target}     ClearTarget: {this.SubTarget}");
                            // Print.List(Environment.GetLogicalDrives());
                            //   Get.Wait();
                            //  Get.Cyan(ShellLoop.CurrentPath);
                            //Get.Wait(type.ToUpper());
                            //Get.Wait(new ShellLoop().ReferToDisk(type.ToUpper()));
                            string path = null; 
                            
                            if (Helper.ReferToDisk(type.ToUpper()))
                            {
                                ShellLoop.CurrentPath = type; 
                                return;
                            }
                            if(Helper.HasSpecialFolder(type) != null)
                            {
                                ShellLoop.CurrentPath = Helper.HasSpecialFolder(type);
                                return;
                            }

                            if (type.Contains(".") && type.Length <= 3)
                            {
                                path = $"{ShellLoop.CurrentPath}{Get.Slash()}../";
                                ShellLoop.CurrentPath = Helper.RemoveDirs(path,Helper.CountDirs(path));
                                return;
                            }
                            else
                            {
                                ShellLoop.CurrentPath += $"{Get.Slash()}{type}";
                            }

                        });
                    break;
                case "cat":
                    runner.Run(() => {

                        string file = null; 
                        if (Helper.HasSpecialFolder(type) != null)
                        {
                            file = Helper.HasSpecialFolder(type); 
                        }if(file == null)
                        {
                            file = Path.Combine(ShellLoop.CurrentPath, type);
                        }
                        if (!File.Exists(file))
                        {
                            Get.Red($"The file {file} was not found!!!");
                            return;
                        }
                        else
                        {
                            try
                            {
                                Get.Write(Reader.Read(file));
                            }catch(Exception ex)
                            {
                                Get.Red($"There was an error while reading the file more info: \n{ex.Message}");
                            }
                        }
              
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
                case "select":
                case "-S":
                    runner.Run(() => { 
                    //Get.Wait(type);
                        if (type[0] == '*')
                        {
                            string[] files = new FilesMaper().GetFiles(this.SubTarget);
                            List<string> withExt = new List<string>();
                            foreach(string file in files)
                            {
                                if (Get.FileExention(file) == Get.FileExention(type))
                                {
                                    withExt.Add(file);
                                }
                            }
                            if(withExt != null || withExt.Count > 0)
                            {
                                files = IConvert.ToType<string>.ToArray(withExt); 
                            }
                            //Print.List(withExt);
                            //Get.Wait();
                            Options option = new Options(files);
                            option.Label = this.SubTarget;
                            option.SelectorL = "> ";
                            option.SelectorR = "";
                            int selection = option.Pick();
                            ShellLoop.SelectedOject = $"{this.SubTarget}{Get.Slash()}{Get.FileNameFromPath(files[selection])}";
                            return;
                        }
                   // C: \Users\William\Desktop\~\Desktop\Q.dll
                    });
                    break;
                case "get-hash":
                case "hash":
                    runner.Run(() => {
                        if (File.Exists(this.Target))
                        {
                            byte[] bytes = Binary.Reader(this.Target);
                            Get.Print($"File: {Get.FileNameFromPath(this.Target)}",$"Hash: {Get.HashCode(bytes)}");
                            return;
                        }
                        else
                        {
                            Get.Red($"File Not Found: {this.Target}");
                        }
                    });

                        break;
                case "cmd":
                    runner.Run(() => {
                        Process cmd = new Process();

                        cmd.StartInfo.FileName = "cmd";//"cmd.exe";
                                                       //cmd.StartInfo.Arguments;
                                                       //cmd.StartInfo.RedirectStandardInput = true;
                        cmd.StartInfo.RedirectStandardOutput = false;  // true;
                        cmd.StartInfo.CreateNoWindow = false;
                        cmd.StartInfo.UseShellExecute = false;
                        //cmd.StartInfo.Arguments = "ping www.google.com"; //Helper.ResolvePath(this).Target;

                        cmd.Start();
                        cmd.WaitForExit();
                        /* execute "dir" */

                        //cmd.StandardInput.WriteLine(this.SubTarget);
                        //cmd.StandardInput.Flush();
                        //cmd.StandardInput.Close();
                        //Console.WriteLine(cmd.StandardOutput.ReadToEnd());
                    });
                    break;
                case "vim":
                    runner.Run(() => {
                        Process cmd = new Process();
                        
                        cmd.StartInfo.FileName = $"{Get.Path}editors/vim/vim.exe";//"cmd.exe";
                        //cmd.StartInfo.Arguments;
                        //cmd.StartInfo.RedirectStandardInput = true;
                        cmd.StartInfo.RedirectStandardOutput = false;  // true;
                        cmd.StartInfo.CreateNoWindow = false;
                        cmd.StartInfo.UseShellExecute = false;
                        cmd.StartInfo.Arguments = Helper.ResolvePath(this).Target; //"ping www.google.com"; //Helper.ResolvePath(this).Target;

                        cmd.Start();
                        cmd.WaitForExit();
                    });
                    break;
                case "nano":
                    runner.Run(() => {
                        Process cmd = new Process();

                        cmd.StartInfo.FileName = $"{Get.Path}editors/nano/nano.exe";//"cmd.exe";
                                                                                 //cmd.StartInfo.Arguments;
                                                                                 //cmd.StartInfo.RedirectStandardInput = true;
                        cmd.StartInfo.RedirectStandardOutput = false;  // true;
                        cmd.StartInfo.CreateNoWindow = false;
                        cmd.StartInfo.UseShellExecute = false;
                        cmd.StartInfo.Arguments = this.Target; //Helper.ResolvePath(this).Target;

                        cmd.Start();
                        cmd.WaitForExit();
                    });
                    break;
                case "xxd":
                    runner.Run(() => {
                        Process cmd = new Process();

                        cmd.StartInfo.FileName = $"{Get.Path}editors/vim/xxd.exe";//"cmd.exe";
                                                                                  //cmd.StartInfo.Arguments;
                                                                                  //cmd.StartInfo.RedirectStandardInput = true;
                        cmd.StartInfo.RedirectStandardOutput = false;  // true;
                        cmd.StartInfo.CreateNoWindow = false;
                        cmd.StartInfo.UseShellExecute = false;
                        cmd.StartInfo.Arguments = Helper.ResolvePath(this).Target; //"ping www.google.com"; //Helper.ResolvePath(this).Target;

                        cmd.Start();
                        cmd.WaitForExit();
                    });
                    break;
                case "edit":
                case "notepad":
                    runner.Run(() => {

                        Process cmd = new Process();
                        if (Get.IsWindow())
                        {
                            cmd.StartInfo.FileName = "notepad";
                            cmd.StartInfo.Arguments = Helper.ResolvePath(this).Target;

                        }
                        if (!Get.IsWindow())
                        {
                            cmd.StartInfo.FileName = "open";
                            cmd.StartInfo.Arguments = Helper.ResolvePath(this).Target;
                        }

                        //cmd.StartInfo.Arguments;
                        cmd.StartInfo.RedirectStandardInput = true;
                        cmd.StartInfo.RedirectStandardOutput = true;
                        cmd.StartInfo.CreateNoWindow = true;
                        cmd.StartInfo.UseShellExecute = false;

                        
                        //Get.Yellow($"Target: {this.Target} SubTarget: {this.SubTarget}");
                        //CodeParser parser = 
                        //Get.Wait($"Target: {parser.Target} SubTarget: {parser.SubTarget}");
                        cmd.Start();

                        /* execute "dir" */
                        //cmd.StandardInput.WriteLine();
                        cmd.StandardInput.Flush();
                        cmd.StandardInput.Close();
                        Console.WriteLine(cmd.StandardOutput.ReadToEnd());
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
