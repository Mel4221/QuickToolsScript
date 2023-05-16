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

              string fix = type[0] == '>' ? type.Substring(1) : Get.FixPath($"{ShellLoop.CurrentPath}{Get.Slash()}{type}");
              //Get.Wait(fix);
              this.cache = new DataCacher();
              this.runner = new ScriptRunner();
              this.error = new ErrorHandeler();
              this.Target = Get.FixPath(fix);
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
                        //Get.Yellow($"{this.Target}     ClearTarget: {this.ClearTarget} Type: {type}");
                        //Get.Blue(Path.GetDirectoryName(this.Target));
                        // Get.Yellow(ShellLoop.RelativePath);
                        // Get.Wait(type);
                        if (type[0] == '~')
                        {
                             Get.Ls(Helper.HasSpecialFolder(type));
                            return; 
                        }
                        if (type == "disk")
                        {
                            Print.List(Environment.GetLogicalDrives());
                            return;
                        }
                        if(type == "-l")
                        {
                            Get.Ls(this.ClearTarget, "");
                            return; 
                        }
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
                case "cd":
                        runner.Run(() => {

                            //Get.Yellow($"{this.Target}     ClearTarget: {this.ClearTarget}");
                            // Print.List(Environment.GetLogicalDrives());
                            //   Get.Wait();
                            //  Get.Cyan(ShellLoop.CurrentPath);
                            //Get.Wait(type.ToUpper());
                            //Get.Wait(new ShellLoop().ReferToDisk(type.ToUpper()));
                            if (ShellLoop.ReferToDisk(type.ToUpper()))
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
                        //Get.Wait(this.Target);
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
                case "select":
                case "-S":
                    runner.Run(() => { 
                    //Get.Wait(type);
                        if (type[0] == '*')
                        {
                            string[] files = new FilesMaper().GetFiles(this.ClearTarget);
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
                            Options.Label = this.ClearTarget;
                            Options.SelectorL = "> ";
                            Options.SelectorR = "";
                            int selection = option.Pick();
                            ShellLoop.SelectedOject = $"{this.ClearTarget}{Get.Slash()}{Get.FileNameFromPath(files[selection])}";
                            return;
                        }  
                      
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
