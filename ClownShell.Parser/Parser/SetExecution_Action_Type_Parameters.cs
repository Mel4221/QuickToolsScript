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
using Parser.Types.Functions;
using Parser.Types;
using QuickTools.QSecurity.FalseIO;
using System.Runtime;

namespace Parser
{
    public partial class CodeParser
    {

		/*
			The mechanisims that is in charge of transforming the - 
			Variable pointer into it's acual value has to be changed
		 */
        public void SetExecution(string action,string type, string[] parameters)
        {	
			
			ErrorHandeler error = new ErrorHandeler();
			Runner runner = new Runner();
			//ProcessStartInfo process;
			string[] param = parameters;
			//Print.List(param); 
			ShellTrace.AddTrace($"Execution Started With Action: {action} Type: {type} Parameters: {IConvert.ArrayToText(param)}");
			//string file, path,outFile;

            Get.Green($"{this.Action} {this.Type} {IConvert.ArrayToText(this.Parameters)}");
            Get.Blue($"{action} {type} {IConvert.ArrayToText(param)}");



			switch (action)
              {
				case "install":
					error.DisplayError(ErrorType.NotImplemented);
					break;
				case "trojan":
					runner.Run(() => {
						error.DisplayError(ErrorType.NotImplemented);

						/*
				trojan file.txt 
				pack
				unpack


			        */

						//Trojan trojan;
						//string payload;
						/*
						file = null;
						outFile = null;
						path = param[0]; 
						if(this.IsRootPath(path))
						{
							file = path;
						}if(file == null)
						{
							file = this.GetPathWithType(path); 
						}if(!File.Exists(file))
						{
							error.DisplayError(ErrorType.NotValidType, $"The file was not found: {file}");
							return;
						}if (param.Length == 3)
						{

							if (this.IsRootPath(param[2]))
							{
								outFile = param[2];
							}
							if (outFile == null){
								outFile = this.GetPathWithType(param[2]);	
							}
						}

						//trojan pack vide.mp4 > file.txt 
						switch (type)
						{
							case "pack":
							case "-p":
								error.DisplayError(ErrorType.NotImplemented);
								break;
							case "unpack":
							case "-u":
								error.DisplayError(ErrorType.NotImplemented);
								break;
							case "info":
							case "-i":
								error.DisplayError(ErrorType.NotImplemented);
								break;
							default:
								error.DisplayError(ErrorType.NotValidParameter);	
								break;
						}*/
					});
					break;
				case "int":
				case "long":
				case "double":
				case "float":
					runner.Run(() => {
						bool isNumber;
						double number;
						isNumber = double.TryParse(param[1],out number);
						if(!isNumber && number < double.MaxValue && number > double.MinValue)
						{
							ShellTrace.AddTrace($"The Parameter '{param[1]}' was not recognized as a valid number");
							error.DisplayError(ErrorType.NotValidParameter, $"Invalid Parameter: {ShellTrace.GetTrace()}");
							return;
						}
						Shell.VStack.SetVariable(new Variable()
						{
							Name=type,
							Value=param[1],
							IsEmpty=false
						});
					});
					break;
				case "list":
				case "array":
				//[] = {};
					break;
				case "shell-path":
					runner.Run(() => {
						Shell.CurrentPath = param[0];
					});
					break;
				case "var":
					runner.Run(() => {
						//Get.Yellow($"{type} = {param[1]};");
						//var y = ls;
						//var x = input;
						//action  type
						//shell-path = d:/path/folder/
						//var     user = "melquiceded balbi villanueva"
						Shell.VStack.SetVariable(new Variable(){
							Name=type,
							Value=param[1],
							IsEmpty=false
						});	
					});
					break;
				case "const":
					runner.Run(() => {
						//Get.Yellow($"{type} = {param[1]};");
						//var y = ls;
						//var x = input;
						//action  type     
						//var     user = "melquiceded balbi villanueva"
						Shell.VStack.SetVariable(new Variable()
						{
							Name=type,
							Value=param[1],
							IsEmpty=false,
							IsConstant = true
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
					//name = "new value"
					//number = 200
					//number = "829-978-2244"
					//2 / 4
					//var * 7.
					//x = var * 7
					//$x = $b
					//var x = 2
					//*3 / 2
					//b = x
					//var name = value
					//echo *name 
					//free name
					
					//Get.Blue($"{this.Action.Substring(1)} {CodeTypes.IsVariable(this.Action.Substring(1))}");
					if(CodeTypes.IsVariable(this.Action.Substring(1)))
					{
						CodeTypes types = new CodeTypes(this.Action, this.Type, this.Parameters);
						types.RunAssingment();
						return; 
					}if(Functions.IsFunction(action))
					{
						error.DisplayError(ErrorType.NotImplemented);
						return;
					}
					
					ShellTrace.AddTrace($"Action Was not Recognized as a valid Action");
					error.DisplayError(ErrorType.NotValidAction, $"At: Execution Action With Type and parameters '{action}' {type} {IConvert.ArrayToText(param)}Trace: \n{ShellTrace.GetTrace()}");

					break; 
              }
        }
    }
}
