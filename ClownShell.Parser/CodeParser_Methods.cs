using ClownShell.Init;
using ClownShell.ScripRunner;
using ClownShell.ErrorHandler;
using ClownShell.Helpers;
using QuickTools.QCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace ClownShell.Parser
{
    public partial class CodeParser
    {
        public string ToQoutesString(string input)
        {
            return $"'{input}'".Replace("'", '"'.ToString());
        }

        /// <summary>
        /// Returns the pah properly bounded to the actual path
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public string GetPathWithType(string type)
        {
            return ShellLoop.CurrentPath[ShellLoop.CurrentPath.Length-1]==Get.Slash()[0] ? $"{ShellLoop.CurrentPath}{type}" : $"{ShellLoop.CurrentPath}{Get.Slash()}{type}";
        }

        public bool HasExecutable(string path)
        {
             
             for(int ch = path.Length-1; ch > 0; ch--)
            {
                if (path[ch] == '/' || path[ch] == '\\') 
                {
                    if (path[ch -1] == '.')
                    {
                        return true; 
                    }
                }
            }
            return false; 
        }
        private void RunProcess(string code)
        {
            //ScriptRunner runner = new ScriptRunner();
            //Process cmd = new Process();
            //runner.Run(() => {

            //    cmd.StartInfo.FileName = $"{code}";//"cmd.exe";
            //                                       //cmd.StartInfo.Arguments;
            //                                       //cmd.StartInfo.RedirectStandardInput = true;
            //    cmd.StartInfo.RedirectStandardOutput = false;  // true;
            //    cmd.StartInfo.CreateNoWindow = false;
            //    cmd.StartInfo.UseShellExecute = false;
            //    //cmd.StartInfo.Arguments = "ping www.google.com"; //Helper.ResolvePath(this).Target;

            //    cmd.Start();
            //    cmd.WaitForExit();
            //    /* execute "dir" */

            //    //cmd.StandardInput.WriteLine(this.SubTarget);
            //    //cmd.StandardInput.Flush();
            //    //cmd.StandardInput.Close();
            //    //Console.WriteLine(cmd.StandardOutput.ReadToEnd());
                return;
            //});

        }

        private bool IsRootPath(string path)
        {
            try
            {
                return Path.IsPathRooted(path); 
            }
            catch
            {
                return false; 
            }
        }
        public void RunExecutable(string file)
        {

            ErrorHandeler error = new ErrorHandeler(); 

            if (IsRootPath(file))
            {
                this.RunProcess(file);
                return;
            }
            if (File.Exists(this.GetPathWithType(file)))
            {
                this.RunProcess(this.GetPathWithType(file));// }";//"cmd.exe";
                return;
            }
            if (Helper.HasSpecialFolder(file) != null)
            {
                this.RunProcess(Helper.HasSpecialFolder(file)); 
                return;
            }
            else
            {
                error.DisplayError(ErrorType.NotImplemented, $"The code is recongnized but sadly there are only 2 wasy to run a program and is either by providing the entired path or  by being in the same directory on the shell and running the program");
            }
        }

        /// <summary>
        /// this is not an string formatter this is more like an array
        /// unifier this joings the arrays of strings into one if it finds
        /// that they have an open and closing qoutes
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private string[] FormatStrings(string[] code)
        {
            List<string> text = new List<string>();
            bool isOpen, isNext;
            isOpen = false;
            isNext = false;
            string temp = "";
            /*
                this is not an string formatter this is more like an array 
                unifier this joings the arrays of strings into one if it finds 
                that they have an open and closing qoutes 
             */
            foreach (string cmd in code)
            {

                if (!isOpen && !cmd.Contains('"'.ToString()))
                {
                    text.Add(cmd);
                }
                if (isOpen && cmd.Contains('"'.ToString()))
                {
                    temp+=cmd+" ";
                    text.Add(temp);
                    temp ="";
                    isOpen = false;
                    isNext = true;
                }
                if (cmd.Contains('"'.ToString()) && isNext == false)
                {
                    isOpen = true;
                }
                if (isOpen)
                {
                    temp+=cmd+" ";
                }

                isNext = false;
            }


            //Print.List(text);
            //Get.Wait(); 
            return text.ToArray();

            //return new int[] {-1,-1};
        }
        public void ReplaceSpecialCharacters()
        {

        }
        public void FixStringFormat()
        {
            this.Code = this.FormatStrings(this.Code);

        }

    }
}
