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
using QuickTools.QColors;
using ClownShell.Parser;
using ClownShell.Settings;
using ClownShell.Parser.Scripts.RuleChecks;
using QuickTools.QIO;
using System.Reflection;
using System;
namespace ClownShell.Init
{
    internal class Program
    {

        public const string Name = "ClownShell";

        static int Main(string[] args)
        {
            //Log.Event("working", "it works");
            //Get.Wait(ShellSettings.LogsFile);

            //Get.Wait($"{Get.DataPath(ClownShell.Settings.ShellSettings.ShellVariablesDB+Get.Slash()+"user")}");
            //Get.Wait(Get.DataPath("shell"));
            /*
            green "Starting...";
            touch file.txt; 
            green "File_Created_Sucessfully"; 
             */

            //Get.Wait(FixPath("../box/secure/QuickTools.xml"));
            //Get.Wait(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));// Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            //while(true)Get.Box(Get.Input().Text);
            //return 0; 
            //Get.Wait(ClownShell.Helpers.Helper.IsExecutable("./file.txt")); 
            Get.Title(Program.Name);
            //CodeParser parser = new CodeParser();
            //CodeParser.CodeResult result;
            //MainMenu menu;
            //Print.List(args); 
            //Get.Wait(Get.Slash());
            ShellLoop shellLoop;
            ScriptChecker checker;
            string[] commands = args;
            try
            {
                if (commands.Length == 0)
                {
                    Log.Event(ShellSettings.LogsFile, $"Shell Started With no Arguments");
                    shellLoop = new ShellLoop();
                    shellLoop.Start();
                    Log.Event(ShellSettings.LogsFile, $"Shell Exited With no Events");
                    Environment.Exit(0);
                    return 0;
                }
                else
                {
                    Log.Event(ShellSettings.LogsFile, $"Shell Started With Arguments Length: {args.Length}");
                    checker = new ScriptChecker();
                    checker.Check(args);
                    Log.Event(ShellSettings.LogsFile, $"Shell Exited With no Events");
                    Environment.Exit(0);
                    return 0;
                }
            }
            catch(Exception ex)
            {
                new ClownShell.ErrorHandler.ErrorHandeler().DisplayError(ClownShell.ErrorHandler.ErrorType.FATAL, "FATAL-ERROR");
                Log.Event(ShellSettings.LogsFile, $"Shell Exited With a FATAL-ERROR More info in the logs file:  \n{ex}");
                Get.Alert($"There was a FATAL ERROR MORE INFO IN this path: \n{ShellSettings.LogsFile}.log");
                Environment.Exit(1);
                return 1; 
            }
         
            //if (parser.CheckFile(commands[0]).IsValid)
            //{
            //    result = parser.CheckFile(commands[0]);
            //    commands = IConvert.TextToArray(result.Code);
            //}
            //Color.Yellow(commands[0]);

            //parser = new CodeParser();
            //parser.Code = commands;
            //for debugging porpuses
            //checker = new ScriptChecker(); 
            //checker.Check(args);
            //Get.Wait();

        }
    }
}
