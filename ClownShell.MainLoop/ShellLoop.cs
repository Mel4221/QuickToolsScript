using System;
using QuickTools.QConsole;
using QuickTools.QCore;
using Parser;
using States;
	namespace MainLoop
	{
		
		public partial class ShellLoop
		{

			public void Start()
			{

				ShellInput shell;
				CodeParser parser;

				if (this.Arguments.Length  > 0)
				{
				parser = new CodeParser(this.Arguments);
				parser.Start();
					return;
				}
				else
				{
					shell = new ShellInput(Environment.UserName, Environment.MachineName);
					
					while (!Shell.ExitRequest)
					{
						shell.CurrentPath = Shell.CurrentPath;
						string input = shell.StartInput();
						this.SaveHistory(input); 
						string[] commands = IConvert.TextToArray(input);
						parser = new CodeParser(commands);
						parser.Start(); 
					}
				}
			}
			public ShellLoop() { }
			public ShellLoop(string[] args)
			{
				this.Arguments = args;
			}
		}
	}
