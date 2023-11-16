using QuickTools.QData;
using States;
using QuickTools.QCore;
using ErrorHandelers;
namespace Parser.Types
{
	public partial class CodeTypes
	{
		public void RunAssingment()
		{
			ShellTrace.AddTrace("Assinging  Variables");
			string action, parameter, type;
			action = this.Action.Substring(1);
			type = this.Type;
			parameter = this.Parameters[0].Substring(1);

			Variable a = Shell.VStack.GetVariable(action);
			Variable b = Shell.VStack.GetVariable(parameter);
			ErrorHandeler error = new ErrorHandeler();
			/*
			   var x = 2
			   x = 3
			   x = "some text"
			 */
			//Get.Yellow($"A: {a.ToString()} B: {b.ToString()}");
			//Get.Blue($"Action: {Action} {Type} {IConvert.ArrayToText(Parameters)}");
			if (b.IsEmpty && type == "=" && Functions.Functions.IsFunction(parameter))
			{
				error.DisplayError(ErrorType.NotImplemented);
				return;
			}
			if (b.IsEmpty && type == "=")
			{/*
				by this time we already know that be is a variable
				why not just to check if they have pointers 
			  */
				Shell.VStack.UpdateVariable(a.Name, parameter);
				//Get.Ok();
				return;
			}if(!b.IsEmpty && type == "=")
			{
				Shell.VStack.UpdateVariable(a.Name,b.Value);
				return;
			}

			switch(type)
			{
				case "+=":
					Shell.VStack.UpdateVariable($"{a.Name}",$"{a.Value}{b.Value}");
					break;
				default:
					error.DisplayError(ErrorType.InvalidOperator);
					return;
			}

		 
		}
	}
}
