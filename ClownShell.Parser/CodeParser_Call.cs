using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser
{
public partial class CodeParser
	{
		private void Call(string action, string type, string[] param)
		{
			this.SetExecution(action, type, param);
		}
		private void Call(string action, string type)
		{
			this.SetExecution(action, type);
		}
		private void Call(string action)
		{
			this.SetExecution(action);
		}
	}
}
