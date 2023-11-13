using System;
using QuickTools.QConsole;
using QuickTools.QCore;
using Parser;
using States;
using Security;
using QuickTools.QData;
using System.IO;

namespace MainLoop
	{

		public partial class ShellLoop
		{

		private ShellInput shell;
		private CodeParser parser;
		private void RunShellLoop()
		{
			shell = new ShellInput(Environment.UserName, Environment.MachineName);

			while (!Shell.ExitRequest)
			{
				shell.CurrentPath = Shell.CurrentPath;
				shell.Notifications = ShellUser.Name == null ? $"'{Environment.UserName}' Without Credentials" : ShellUser.Name;
				string input = shell.StartInput();
				this.SaveHistory(input);
				string[] commands = IConvert.TextToArray(input);
				parser = new CodeParser(commands);
				parser.Start();
			}
		}


		public void Start()
			{
				
			
				if (this.Arguments.Length  > 0)
				{
					this.CheckArguments();
					return;
				}
				else
				{
					this.RunShellLoop(); 
				}
			}
			public ShellLoop() { }
			public ShellLoop(string[] args)
			{
				this.Arguments = args;
			}
		}
	}
