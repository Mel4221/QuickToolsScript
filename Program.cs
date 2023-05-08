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
        static int Main(string[] args)
        {
            CodeParser parser;
            MainMenu menu;
            /*
            while (true)
            {
                string str = Get.Input().Text;
                parser = new CodeParser(IConvert.TextToArray(str));
                parser.Start(); 
                Get.Wait("OKS");
            }

            */


            args = IConvert.TextToArray("secure-encrypt file.txt 1234 same");
            if(args.Length > 0)
            {
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
