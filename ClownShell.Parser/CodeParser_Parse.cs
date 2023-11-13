using System.Collections.Generic;
using ErrorHandelers;
using QuickTools.QCore;
using States;

namespace Parser
{
    public partial class CodeParser
    {
		private string[] GetParameters(string[] parameters)
		{
			string[] param;
			int current, goal;
			goal = parameters.Length;
			string cmds = "";
			for (current = 2; current < goal; current++)
			{
				cmds +=  Get.FixPath(parameters[current])+" ";
			}
			param = IConvert.TextToArray(cmds);
			return param;
		}
		
		private void Parse(CodeType codeType)
		{
			string action, type;
			string[] param;
			ErrorHandeler error = new ErrorHandeler();

			switch (codeType)
			{
				case CodeType.Action:
					action = this.Code[0];
					this.SetExecution(action);
					break;
				case CodeType.ActionWithType:
					action = this.Code[0];
					type = Get.FixPath(this.Code[1]);
					switch (type)
					{
						case "obj":
						case "object":
						case "selected-value":
						case "selected":
							//error.DisplayError(ErrorType.NotImplemented);
							type = Shell.SelectedObject==null ? "NONE" : Shell.SelectedObject;
							break;
						default:

							break;
					}
					type = this.CheckTypeForVariables(type); 
					this.SetExecution(action, type);
					break;
				case CodeType.Complete:
					action = this.Code[0];
					type = Get.FixPath(this.Code[1]);
					switch (type)
					{
						case "obj":
						case "object":
						case "selected-value":
						case "selected":
							//error.DisplayError(ErrorType.NotImplemented);
							type = Shell.SelectedObject==null ? "NONE" : Shell.SelectedObject;
							break;

					}
					type = this.CheckTypeForVariables(type);
					param = this.GetParameters(this.Code);
					param = this.CheckParamForVariables(param);
					param = this.FormatStrings(param);
					this.SetExecution(action, type,param);
					break;
			}
		}

	}
    }
