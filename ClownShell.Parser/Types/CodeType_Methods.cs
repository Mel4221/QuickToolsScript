using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using States;
namespace Parser.Types
{
	public partial class CodeTypes
	{
			public static bool IsVariable(string variable) => Shell.VStack.GetVariable(variable).IsEmpty==false?true:false;
	}
}
