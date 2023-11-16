using System.IO;
using ErrorHandelers;

namespace Parser
{
	public partial class CodeParser
	{
		public void RunExecutable(string file)
		{

			ErrorHandeler error = new ErrorHandeler();

			if (IsRootPath(file))
			{
				this.RunProcess(file);
				return;
			}
			if (File.Exists(this.GetPathWithType(file)))
			{
				this.RunProcess(this.GetPathWithType(file));// }";//"cmd.exe";
				return;
			}
			if (Helper.HasSpecialFolder(file) != null)
			{
				this.RunProcess(Helper.HasSpecialFolder(file));
				return;
			}
			else
			{
				error.DisplayError(ErrorType.NotImplemented, $"The code is recongnized but sadly there are only 2 wasy to run a program and is either by providing the entired path or  by being in the same directory on the shell and running the program");
			}
		}
	}
}
