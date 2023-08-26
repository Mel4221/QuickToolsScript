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
using QuickTools.QIO;
using QuickTools.QNet;
using QuickTools.QCore;
using QuickTools.QData;
using QuickTools.QColors;
using QuickTools.QConsole;
using QuickTools.QSecurity;
using QuickTools.QSecurity.FalseIO;


namespace ClownShell.Init
{
    internal class Program
    {

        public const string Name = "ClownShell";



        static int Main(string[] args)
        {


            //Get.Wait(FixPath("../box/secure/QuickTools.xml"));
            //Get.Wait(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));// Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            //while(true)Get.Box(Get.Input().Text);
            //return 0; 
            Get.Title(Name);
            CodeParser parser = new CodeParser();
            CodeParser.CodeResult result;
            MainMenu menu;
            ShellLoop shellLoop;
            string[] commands = args;

            if (commands.Length == 0)
            {
                commands = new string[] { "shell" };
            }

            if (parser.CheckFile(commands[0]).IsValid)
            {
                result = parser.CheckFile(commands[0]);
                commands = IConvert.TextToArray(result.Code);
            }
            Color.Yellow(commands[0]);
            if (commands.Length > 0)
            {

                if (commands[0] == "shell")
                {
                    shellLoop = new ShellLoop();
                    shellLoop.Start();
                    return 0;
                }

                parser = new CodeParser();
                parser.Code = commands;
                //for debugging porpuses
                parser.Start();
                Get.Wait();
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
