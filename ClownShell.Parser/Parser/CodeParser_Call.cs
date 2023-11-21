using States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuickTools.QCore; 
namespace Parser
{
public partial class CodeParser
	{
		private void Call(string action, string type, string[] param)
		{
			ShellTrace.AddTrace($"{this} Direct Call to Executed Action: {action} Type: {type} Parameters: {IConvert.ArrayToText(param)}");
			this.SetExecution(action, type, param);
		}
		private void Call(string action, string type)
		{
			ShellTrace.AddTrace($"{this} Direct Call to Executed Action: {action} Type: {type}");
			this.SetExecution(action, type);
		}
		private void Call(string action)
		{
			ShellTrace.AddTrace($"{this} Direct Call to Executed Action: {action}");
			this.SetExecution(action);
		}
	}
}
