using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErrorHandelers;
using QuickTools.QCore;

using QuickTools.QData;

namespace Parser
{
    public partial class CodeParser
    {
		private void Parse(CodeType codeType)
		{
			string action, type;
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
							error.DisplayError(ErrorType.NotImplemented);
							//type = $"@{ShellLoop.SelectedOject}";
							break;
						default:

							break;
					}
					if (type.Contains("*"))
					{
						if (type[0] == '*')
						{
							error.DisplayError(ErrorType.NotImplemented);

							//Variable v = VStack.GetVariable(type.Substring(1));
							//switch (v.IsEmpty)
							//{
							//	case true:
							//		type = "";
							//		break;
							//	case false:
							//		type = v.Value;
							//		break;
							//}
						}
					}
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
							error.DisplayError(ErrorType.NotImplemented);

							//type = $"{ShellLoop.SelectedOject}";
							break;

					}

					this.SetExecution(action, type, this.Code);
					break;
			}
		}

	}
    }
