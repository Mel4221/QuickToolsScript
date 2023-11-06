using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading; 
namespace ClownShell.ScripRunner
{
    public partial class ScriptRunner
    {
        private static Thread Loop;

        //private CodeParser Parser { get; set; } = new CodeParser(); 

        /// <summary>
        /// Stablish the current running code
        /// </summary>
        public string[] RunningCode { get; set; } = new string[0];
        public string RunningCodeInfo { get; set; } = string.Empty;
        public string RunningBackGroundCodeName { get; set; } = string.Empty; 

        public static Thread CurrentScript;
        public bool AllowToCancell;
    }
}
