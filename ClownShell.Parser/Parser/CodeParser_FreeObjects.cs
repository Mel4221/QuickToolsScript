using States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser
{
	public partial class CodeParser
	{

	    public async Task FreeObjects()
		{
            await Task.Run(() => 
            { 
			 for(int item = Shell.VStack.GetIndex()+1; item > 0; item--)
			 {
				Shell.VStack.Free($"cat{item}");
				Shell.VStack.Free($"echo{item}");
				Shell.VStack.Free($"read{item}");
			 }
            });
        }
	}
}
