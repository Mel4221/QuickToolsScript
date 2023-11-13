using System;
using System.IO; 
using System.Collections.Generic;
using System.Threading;
using ErrorHandelers;
using QuickTools.QData;
using ScriptRunner;
using System.Diagnostics;
using States;
using QuickTools.QCore;
using QuickTools.QIO; 
namespace Parser
{
    public partial class CodeParser
    {
        public void SetExecution(string action,string type, string[] parameters)
        {
			
			ErrorHandeler error = new ErrorHandeler();
			Runner runner = new Runner();
			ProcessStartInfo process;
			string[] param = parameters;
			Print.List(param); 
			switch (action)
              {
				case "var":
				case "mem":
					runner.Run(() => {
						Get.Yellow($"{type} = {param[1]};");
						//var y = ls;
						//var x = input;
						//action  type     
						//var     user = "melquiceded balbi villanueva"
						Shell.VStack.SetVariable(new Variable(){
							Name=type,
							Value=param[1],
							IsEmpty=false
						});	
					});
					break;
				case "input":
                    runner.Run(() => {
						//input => 
						//input = variable
						//action type param[0]

						if(Shell.VStack.Exist(new Variable() {Name = param[0] }))
						{
							error.DisplayError(ErrorType.VariableAlreadySet, $"Variable already Set at {action} {type} '{param[0]}'");
							return;
						}
						Shell.VStack.SetVariable(new Variable()
						{
							Name = param[0],
							Value = Get.Input().Text,
							IsConstant = false,
							IsEmpty = false
						});
					});
					break;
				case "echo":
					//Get.Wait($"{this.IsRootPath(type)} {this.IsRootPath(param[1])}");
					runner.Run(() => {
						// Get.Wait($"{this.GetPathWithType(param[1])}");

						//echo "this text" > file.txt
						//Print.List(param); 

						if (this.IsRootPath(type) && this.IsRootPath(param[1]))
						{
							//Get.Wait("Rooted");
							Binary.CopyBinaryFile(type, param[1]);

							return;
						}
						else
						{

							//Get.Yellow($"{this.GetPathWithType(param[1])} > {type}");
							string str = type.Replace('"'.ToString(), "");
							Writer.Write(this.GetPathWithType(param[1]), str);
							Get.Yellow($"{type} > {param[1]} {Get.FileSize(Get.Bytes(str))}");
						}
					});
					break;
				default:
					error.DisplayError(ErrorType.NotValidAction, $"'{action}' {type}");
				break; 
              }
        }
    }
}
