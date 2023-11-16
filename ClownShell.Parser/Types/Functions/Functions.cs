using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Types.Functions
{
	public class Functions
	{
		public static bool IsFunction (string code)
		{
			//Run()
			if(code.Length < 2)
			{
				return false; 
			}if(code[code.Length-2] == '(' && code[code.Length-1] == ')')
			{
				return true; 
			}else{
				return false;		
			}
		}
	}
}
