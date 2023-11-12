using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;
using ErrorHandelers;
using ScriptRunner;
using QuickTools.QCore; 
namespace Parser
{
    public partial class CodeParser
    {
        public void SetExecution(string action,string type)
        {
            
            ErrorHandeler error = new ErrorHandeler();
            Runner runner = new Runner();
            switch (action)
            {
                case "fuck":
                    
                    break;
                case "echo":
                case "write":
                case "print":
                    runner.Run(() => {
                        Get.Write(type);
                    });
                    break;
                default:
                    error.DisplayError(ErrorType.NotValidAction, $"'{action}' {type}");
                    break;
            }
        }
    }
}
