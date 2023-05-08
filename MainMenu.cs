using QuickTools.QConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickToolsScript
{
    internal class MainMenu
    {
        internal void Start()
        {
            string[] optionsList = {"Create Script","Run Script","Export Script","Settings","Exit"};
            Options options = new Options(optionsList);
            
            switch (options.Pick())
            {
                case 0:

                default:
                    break; 
            }
        }
    }
}
