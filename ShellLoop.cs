using System;
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
        public List<Thread> ShellBackGroundThreads; 
        public void Start()
        {
            CodeParser parser;
            ShellInput shell = new ShellInput(System.Environment.UserName, System.Environment.MachineName);
            shell.Notifications = DateTime.Now.ToString("H:m M:dd:yyyy");
             
            string input = null;

            Get.Loop(() => {
                input = shell.StartInput();
                if (input == "exit")
                {
                    Environment.Exit(0);
                    return;
                } if (input == "back" || input == "go-back" || input =="go back")
                {
                    Get.Break();
                    return;
                }
                else
                {
                    parser = new CodeParser(input);
                    parser.Start();
                    return;
                }
            });
            new MainMenu().Start();
        }
    }
}
