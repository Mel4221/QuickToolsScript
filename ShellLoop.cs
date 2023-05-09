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
