using QuickTools.QData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickToolsScript
{
    public partial class CodeParser 
    {

        private DataCacher cache;
        private ScriptRunner runner;
        private ErrorHandeler error;
        
        /// <summary>
        /// Holds the redirected text 
        /// </summary>
        public StringBuilder RedirectedText; 
        /// <summary>
        /// Containst the path plus the given folder or file given to the shellLoop
        /// </summary>
        public string Target;

        /// <summary>
        /// Contains only the current path
        /// </summary>
        public string ClearTarget;


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
