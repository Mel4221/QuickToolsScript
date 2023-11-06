using System.Text;
using QuickTools.QData;
using QuickTools.QCore;
using System.Collections.Generic;
using ClownShell.ScripRunner;
using ClownShell.ErrorHandler;

namespace ClownShell.Parser
{
    public partial class CodeParser 
    {

        private DataCacher cache;
        private ScriptRunner runner;
        private ErrorHandeler error;

        /// <summary>
        /// This is a class that contains info about the code that is being process
        /// </summary>
        public class CodeResult
        {
            public bool IsValid { get; set; } = false;
            public string Code { get; set; } = null;
        }

        /// <summary>
        /// Holds the redirected text 
        /// </summary>
        public StringBuilder RedirectedText;

        /// <summary>
        /// Contains a Virtual stack 
        /// </summary>
        public static VirtualStack VStack { get; set; } = new VirtualStack();
       
        /// <summary>
        /// provides the given action type 
        /// </summary>
        public string Action;

        /// <summary>
        /// provides the type of execution 
        /// </summary>
        public string Type; 

        /// <summary>
        /// Containst the path plus the given folder or file given to the shellLoop
        /// </summary>
        public string Target;

        /// <summary>
        /// Contains only the current path
        /// </summary>
        public string SubTarget;

        /// <summary>
        /// contains the given parameters to the method CodeParser.GetExecution(Action = Type = Parameter)
        /// </summary>
        public string[] Parameters; 

        /// <summary>
        /// Contains the Code array
        /// </summary>
        public string[] Code;

        /// <summary>
        /// notify that the path was resolved
        /// </summary>
        public bool PathResolved;

        /// <summary>
        /// Determines if the script will be readed line by line until meet with an special character
        /// </summary>
        public bool RunContinues;

        /// <summary>
        /// this character will be required at the end of each line 
        /// </summary>
        public char LineEndingChar { get; set; } = ';'; 


        /// <summary>
        /// Contains the list of commands that the code will be executting
        /// </summary>
        public List<string> Commands;
 
        /// <summary>
        /// Contains the types of code that will have to be parse 
        /// </summary>
        /// 
        public enum CodeType
        {
            Action,
            ActionWithType,
            Complete
        }
        public override string ToString()
        {
            return IConvert.ArrayToText(this.Code);
            //return $"\nAction: {this.Action} \nType: {this.Type} \nParameters: {IConvert.ArrayToText(this.Parameters)} \nCode: {IConvert.ArrayToText(this.Code)}"; 
        }
    }
}
