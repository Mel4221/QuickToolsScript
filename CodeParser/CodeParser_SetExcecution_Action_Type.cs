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
namespace ClownShell
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

              //string fix = type[0] == '>' ? type.Substring(1) : Get.FixPath($"{ShellLoop.CurrentPath}{Get.Slash()}{type}");
              //Get.Wait(fix);
              this.cache = new DataCacher();
              this.runner = new ScriptRunner();
              this.error = new ErrorHandeler();

            // get the path with the given type and if it does not have an slash add it acordintly 
            string tar = type[0] == '~' || type[0] == '.' || Helper.ReferToDisk(type)? type :  $"{ShellLoop.CurrentPath}{Get.Slash()}{type}";
            if (tar.Contains('@'))
            {
                tar = tar.Substring(tar.IndexOf('@')+1);
            }
            //Get.Wait(Get.FixPath("*.txt"));
              this.Target = Get.FixPath(tar);

              this.SubTarget = Get.FixPath(type);
              Get.Yellow($"Target: {this.Target} SubTarget: {this.SubTarget}");
              Get.Cyan($"Helper: {Helper.ResolvePath(this).Target}");
              //this.Target = Get.FixPath(fix);
              //this.SubTarget = Get.FixPath($"{ShellLoop.CurrentPath}");
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

                        if (type == "disk")
                        {
                            Get.PrintDisks();
                            return;
                        }if(type  == "-l")
                        {
                            //ls -l
                        }


                        //Get.Yellow($"{this.Target}     ClearTarget: {this.SubTarget} Type: {type}");
                        //Get.Blue(Path.GetDirectoryName(this.Target));
                        // Get.Yellow(ShellLoop.RelativePath);
                        // Get.Wait(type);
                        this.Target = ShellLoop.CurrentPath;
                        //this.SubTarget = type; 
                        //CodeParser helper = Helper.ResolvePath(this);
                        //this.Target = helper.Target;
                        //this.SubTarget = helper.SubTarget;
                        //Get.Cyan($"Target: {this.Target} SubTarget: {this.SubTarget}");
     
                        Get.Ls(this.Target,null);
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
                            if(type.Length <3)
                            {
                                type = type + "000";
                            }
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
                            if (Helper.ReferToDisk(type.ToUpper()))
                            {
                                ShellLoop.CurrentPath = type; 
                                return;
                            }
                            if (type[0] == '~')
                            {
                                string p = type.Substring(type.IndexOf(Get.Slash()) + 1).ToLower();
                                switch (p)
                                {
                                    case "desktop":
                                        ShellLoop.CurrentPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                                        break;
                                    case "documents":
                                        ShellLoop.CurrentPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                                        break;
                                    case "downloads":
                                        string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                                        string str = $"{path.Substring(0, path.LastIndexOf(Get.Slash()))}{Get.Slash()}Downloads";
                                        ShellLoop.CurrentPath = str;  
                                        break;
                                    case "mycomputer":
                                        ShellLoop.CurrentPath = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
                                        break;

                                }
                                //Get.Wait($"{}");
                                // Environment.SpecialFolder.MyComputer
                                return;
                            }
                            if (type != ".." && type != Get.Slash() && type != $"{Get.Slash()}{Get.Slash()}"&&
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
                case "cat":
                    runner.Run(() => {
                    //Get.Wait(this.Target);
                    Get.WriteL(" ");
                        string str = Helper.ResolvePath(this).Target; 
                    Get.Yellow(str);
                       Get.Write(Reader.Read(str));
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
                            Options.Label = this.SubTarget;
                            Options.SelectorL = "> ";
                            Options.SelectorR = "";
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
