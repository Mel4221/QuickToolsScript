using Parser;
using States;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainLoop
{
	public partial class ShellLoop
	{
		private void CheckArguments()
		{
			int length = this.Arguments.Length;
			string path = this.Arguments[0];
			//if is a direct path just set the current path as the path
			if (length == 1 && new CodeParser().IsRootPath(path) && Directory.Exists(path))
			{
				Shell.CurrentPath = path;
				this.RunShellLoop();
				return; 
			}
			//if the path is an special folder get the root path and set it as 
			// the path
			if (length == 1 && Helper.HasSpecialFolder(path) != null)
			{
				path = Helper.HasSpecialFolder(path);
				if (Directory.Exists(path))
				{
					Shell.CurrentPath = path;
					this.RunShellLoop();
					return; 
				}
			}
			else
			{
				this.parser = new CodeParser(this.Arguments);
				this.parser.Start();
				return;
			}
		}
	}
}
