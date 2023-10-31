
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
using QuickTools.QData;
using QuickTools.QConsole;
using System.Reflection;
using ClownShell.Parser;
using QuickTools.QCore;
using System.IO; 
namespace ClownShell.Init
{
    public partial class ShellLoop
    {


        /// <summary>
        /// get or set the current path display in the Shell
        /// </summary>
        public static string CurrentPath;

        /// <summary>
        /// get or set the Selected object 
        /// </summary>
        public static string SelectedOject;

        /// will contains the relative path to acces to any file in the address 
        /// to avoid displaying this ~/Desktop/../../../../../ ext...
        /// </summary>
        public static string RelativePath;


        /// <summary>
        /// This is the file for the shell history by default is set to be ClownShell_History.xml
        /// </summary>
        public const string HistoryFile  =  "ClownShell_History.xml";

        /// <summary>
        /// by default save the history
        /// </summary>
        public bool AllowToSaveHistory { get; set; } = true;



        public ShellInput shell;
        private MiniDB db;
        private CodeParser parser = new CodeParser(); 


        public MiniDB GetHistory()
        {
            this.db = new MiniDB(HistoryFile, true);
            this.db.Load();
            return this.db;
        }
        private bool IsShellCommand(string command)
        {
            switch(command)
            {
                case "history":
                case "exit":
                    return true;
                default:
                    return false;
            }
        }
        public void SaveHistory(string command)
        {
            if (this.IsShellCommand(command)) return;// if is a shell command don't save it
            if(this.AllowToSaveHistory == false) return;// if is not allowed to show history return
            db = new MiniDB(HistoryFile,true);
            db.AllowRepeatedKeys = true;
            db.Create();
            db.Load();
            db.AddKeyOnHot("command", command, DateTime.Now.ToLongDateString());
            db.HotRefresh();
        }


        /// <summary>
        /// returns a grup o files located on the current path 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private string LoadCurrentItems(string path)
        {

            string  items;
            int max; 
            items = null;
            max = 0; 
            foreach(string dir in Directory.GetDirectories(path))
            { 
                if (max == 5) break;
                items += $"{Get.FolderFromPath(dir)}{Get.Slash()},";
                max++;
            }
            max = 0; 
            foreach (string file in Directory.GetFiles(path))
            {
                if (max == 5) break;
                items += Get.FileNameFromPath(file).Length > 5 ? Get.FileNameFromPath(file).Substring(0,5)+"...,":Get.FileNameFromPath(file)+",";
                max++;
            }
            return items;
        }
        //public static Thread BackGroundJob;
        ////                    Get.Title("QuickTools Shell");
        //public static int Status; 
        public void Start()
        {

            //BackGroundJob = new Thread(() => {
            //    while (true)
            //    {
            //        Status++;
            //        Get.Title($"QuickTools Shell  ActiveJob: {Status}");
            //        Thread.Sleep(1000);
            //    }
            //});
            //BackGroundJob.Start();
            //Get.Wait(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));// Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            // Get.Wait(ShellLoop.CurrentPath);
            //ShellLoop.CurrentPath = ShellLoop.CurrentPath != "" ? Get.Path : ShellLoop.CurrentPath;
            shell = new ShellInput(Environment.UserName, Environment.MachineName);
           

            string input;
            input = null;

            CurrentPath = CurrentPath != "" ? Environment.GetFolderPath(Environment.SpecialFolder.Desktop) : CurrentPath;
            while (true)
            {
                shell.Notifications = null;//LoadCurrentItems(CurrentPath==""?".":CurrentPath);//DateTime.Now.ToString("H:m:ss M:dd:yyyy");
                //this.shell.CurrentPath = Get.RelativePath(ShellLoop.CurrentPath);
                shell.CurrentPath = CurrentPath;
                
                // Get.Cyan("\n"+this.shell.CurrentPath);
                // ShellLoop.RelativePath = Get.RelativePath(ShellLoop.CurrentPath);
                //Get.Yellow(ShellLoop.RelativePath); 

                // Func<ConsoleKeyInfo> F = () => { return Console.ReadKey(); };
                input = shell.StartInput();

                SaveHistory(input);
                if (input == "back" || input == "go-back" || input == "go back")
                {
                    break;
                }
                else
                {
                    this.parser.Code = IConvert.TextToArray(input); 
                    //Print.List(this.parser.Code); 
                    //Get.Wait($"{this.parser.Code.Length}");
                    this.parser.Start();
                }
            }
            new MainMenu().Start();
        }

    }
}
