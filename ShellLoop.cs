
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
        private ShellInput shell;

        public void Start()
        {
             this.shell = new ShellInput(System.Environment.UserName, System.Environment.MachineName);
             CodeParser parser;

            string input;
            input = null;
            

           
           // Get.Wait(ShellLoop.CurrentPath);
                 ShellLoop.CurrentPath = ShellLoop.CurrentPath != "" ? Directory.GetCurrentDirectory() : ShellLoop.CurrentPath;

            while (true)
            {
                this.shell.Notifications = DateTime.Now.ToString("H:m M:dd:yyyy");
                this.shell.CurrentPath = ShellLoop.CurrentPath;
                input = this.shell.StartInput();
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
