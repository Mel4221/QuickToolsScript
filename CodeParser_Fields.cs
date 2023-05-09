using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickToolsScript
{
    public partial class CodeParser 
    {
        /// <summary>
        /// Contains the Code array
        /// </summary>
        public string[] Code;

        /// <summary>
        /// Contains the list of commands that the code will be executting
        /// </summary>
        public List<string> Commands;

        /// <summary>
        /// Contains the types of code that will have to be parse 
        /// </summary>
        public enum CodeType
        {
            Action,
            ActionWithType,
            Complete
        }
    }
}
