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
using QuickTools.QCore;
using System;
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
    internal class Program
    {

        public const string Name = "QuickTools Shell";

        
        static int Main(string[] args)
        {

            string str = "app.exe";


            //Get.Wait(FixPath("../box/secure/QuickTools.xml"));
            //Get.Wait(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));// Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            //while(true)Get.Box(Get.Input().Text);
             //return 0; 
            Get.Title(Program.Name);
            CodeParser parser;
            MainMenu menu;
            ShellLoop shellLoop;
            args = new string[] { "shell"}; 
            
            if(args.Length > 0)
            {
                if (args[0] == "shell")
                {
                    shellLoop = new ShellLoop();
                    shellLoop.Start();
                    return 0; 
                }

                parser = new CodeParser(args);
                parser.Start(); 
                return 0;
            }
            else
            {


                menu = new MainMenu(); 
                menu.Start();
                return 0;
            }
           
        }
    }
}
