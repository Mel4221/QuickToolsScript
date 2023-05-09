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

namespace QuickToolsScript
{
    internal class MainMenu
    {
        internal void Start()
        {
        

            string[] optionsList = {"Shell","Create Script","Run Script","Export Script","Settings","Exit"};
            Options options = new Options(optionsList);
            ShellLoop shellLoop;

           // Get.Wait("Working...");
            switch (new Options(optionsList).Pick())
            {
                case 0:
                    shellLoop = new ShellLoop();
                    shellLoop.Start();
                    break;
                case 5:
                    Environment.Exit(0); 
                    break;
                default:
                    Get.Yellow("Still Not Implemented YET");
                    Get.WaitTime();
                    this.Start();
                    break; 
            }
        }
    }
}
