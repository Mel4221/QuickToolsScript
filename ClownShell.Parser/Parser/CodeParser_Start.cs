using ErrorHandelers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser
{
	public partial class CodeParser
	{
	
		public async Task Start()
		{
			this.FixStringFormat();
			ErrorHandeler error = new ErrorHandeler();
            switch (this.Code.Length)
			{
				case 0:
					error.DisplayError(ErrorType.NotValidAction, this.Code);
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
	}
}
