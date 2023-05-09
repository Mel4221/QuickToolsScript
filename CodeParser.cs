using QuickTools.QIO;
using QuickTools.QNet;
using QuickTools.QData;
using QuickTools.QCore;
using QuickTools.QColors;
using QuickTools.QConsole;
using QuickTools.QSecurity;
using QuickTools.QCore;
using QuickTools.QSecurity.FalseIO;
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
        public  enum CodeType
        {
            Action,
            ActionWithType,
            Complete
        }


 


        private string[] GetParameters(string[] parameters)
        {
            string[] param = null;


            return param; 
        }
   


        private void Parse(CodeType codeType)
        {
            string action, type;

            switch (codeType)
            {
                case CodeType.Action:
                    action = this.Code[0];
                    this.SetExecution(action);
                    break;
                case CodeType.ActionWithType:
                    action = this.Code[0];
                    type = this.Code[1];
                    this.SetExecution(action, type); 
                    break;
                case CodeType.Complete:
                    action = this.Code[0];
                    type = this.Code[1];
                    this.SetExecution(action, type,this.Code);
                    break; 
            }
        }
        
        public void Start()
        {
            switch(this.Code.Length)
            {
                case 0:
                    new ErrorHandeler().DisplayError(ErrorHandeler.ErrorType.NotValidAction,this.Code);
                    break; 
                case 1:
                    this.Parse(CodeType.Action); 
                    break;
                case 2:
                    this.Parse(CodeType.ActionWithType);
                    break;
                default:
                    this.Parse(CodeType.Complete);
                    break;
            }

        }

        public CodeParser(string frofileOrCode)
        {
            this.Code = IConvert.TextToArray(frofileOrCode);
        }
        public CodeParser(string[] fromArgs)
        {
            this.Code = fromArgs;
        }

    }
}
