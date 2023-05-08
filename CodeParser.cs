using QuickTools.QCore;
using QuickTools.QData;
using QuickTools.QIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickToolsScript
{
    public class CodeParser
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


        /// <summary>
        /// runs the action without any other 
        /// </summary>
        /// <param name="action"></param>
        public void SetExecution(string action)
        {
            DataCacher cache = new DataCacher();
            ScriptRunner runner = new ScriptRunner();
            switch (action)
            {
                case "console-clear":
                case "clear":
                    runner.Run(() => { Get.Clear(); });
                    break;
                case "set-color-pink":
                case "pink":
                    runner.Run(() => { Get.Pink(); });
                    break;
                case "set-color-red":
                case "red":
                    runner.Run(() => { Get.Red(); });
                    break;
                case "set-color-blue":
                case "blue":
                    runner.Run(() => { Get.Blue(); });
                    break;
                case "set-color-yellow":
                case "yellow":
                    runner.Run(() => { Get.Yellow(); });
                    break;
                case "set-color-green":
                case "green":
                    runner.Run(() => { Get.Green(); });
                    break;
                case "set-color-gray":
                case "gray":
                    runner.Run(() => { Get.Gray(); });
                    break;
                case "set-color-cyan":
                case "cyan":
                    runner.Run(() => { Get.Cyan(); });
                    break;
                case "set-color-black":
                case "black":
                    runner.Run(() => { Get.Black(); });
                    break;
                case "clear-cache":
                case "cache-reset":
                    runner.Run(() => { cache.ClearCache(); });
                    break;
                case "get-input":
                case "input":
                    runner.Run(() => {
                        cache.Cach("EntryInput", Get.Input("Type Something: ").Text);
                    });
                    break;
                default:
                    new ErrorHandeler().DisplayError(ErrorHandeler.ErrorType.NotValidAction, this.Code);
                    break; 
            }
        }
        public void SetExecution(string action,string type)
        {

        }


        private string[] GetParameters(string[] parameters)
        {
            string[] param = null;


            return param; 
        }
        public void SetExecution(string action, string type, string[] parameters )
        {

        }



        private void Parse(CodeType codeType)
        {
            string action, type, parameters;

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
