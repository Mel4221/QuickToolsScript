
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
using QuickTools.QIO;
using QuickTools.QNet;
using QuickTools.QData;
using QuickTools.QCore;
using QuickTools.QColors;
using QuickTools.QConsole;
using QuickTools.QSecurity;
using QuickTools.QCore;
using QuickTools.QSecurity.FalseIO;
using System.Collections.Generic;
using System.Threading; 
namespace QuickToolsScript
{
    public class ShellLoop
    {
        public static string CurrentPath;
        public static string SelectedOject;
        public static string[] Disks; 
        private ShellInput shell;

        private MiniDB db;

  
 
        public bool ReferToDisk(string input)
        {
            bool refer = false;
            if(ShellLoop.Disks == null)
            {
                ShellLoop.Disks = Environment.GetLogicalDrives(); 
            }
            foreach (string disk in ShellLoop.Disks)
            {
                if(disk == input)
                {
                    return true; 
                }
            }


            return refer; 
        }
        public MiniDB GetHistory()
        {
            db = new MiniDB("QuickTools_Shell_History.xml", true);
            db.Load();
            return db; 
        }
        public void SaveHistory(string command)
        {
            db = new MiniDB("QuickTools_Shell_History.xml",true);
            db.AllowRepeatedKeys = true;
            db.Create();
            db.Load();
            db.AddKeyOnHot("command", command, DateTime.Now.ToLongDateString());
            db.HotRefresh();
        }


        //public static Thread BackGroundJob;
        ////                    Get.Title("QuickTools Shell");
        //public static int Status; 
        public void Start()
        {
            this.shell = new ShellInput(System.Environment.UserName, System.Environment.MachineName);
            CodeParser parser;

            string input;
            input = null;

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

                ShellLoop.CurrentPath = ShellLoop.CurrentPath != "" ? Environment.GetFolderPath(Environment.SpecialFolder.Desktop): ShellLoop.CurrentPath;

            while (true)
            {
                this.shell.Notifications = DateTime.Now.ToString("H:m M:dd:yyyy");
                this.shell.CurrentPath = ShellLoop.CurrentPath;
                input = this.shell.StartInput();
                SaveHistory(input); 
                if (input == "exit")
                {
                    Environment.Exit(0);
                    return;
                }
                if (input == "back" || input == "go-back" || input == "go back")
                {
                    break;
                    
                }
                else
                {
                    parser = new CodeParser(input);
                    parser.Start();
                }
            }
            new MainMenu().Start();
        }

    }
}
