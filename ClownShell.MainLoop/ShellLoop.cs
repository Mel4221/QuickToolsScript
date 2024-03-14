    using System;
using QuickTools.QConsole;
using QuickTools.QCore;
using Parser;
using States;
using Security;
using QuickTools.QData;
using System.IO;
using System.Threading; 

namespace MainLoop
	{	

		public partial class ShellLoop
		{
		//private Thread TitileUpdateThread;
		 
		//private void AutoUpdateTitle()
		//{
		//	while(true)
		//	{
		//		string title = Shell.Title == null ? Shell.Name : Shell.Title;
		//		Console.Title = $"{title} V[{Shell.VStack.VirtualStackSize()}] J[{BackGroundJob.JobsCount()}]";
		//		Get.WaitTime(100);
		//	}
		//}
		private ShellInput shell;
		private CodeParser parser;
		private void RunShellLoop()
		{
			ShellTrace.AddTrace($"ShellLoop Started {this}");
			shell = new ShellInput(Environment.UserName, Environment.MachineName);
			shell.ProgramName = Shell.Name;
			////TitileUpdateThread = new Thread(() => { this.AutoUpdateTitle(); });
			////TitileUpdateThread.Start();
			ShellTrace.AddTrace($"LoopInput Started {this}");
            /*
                ClownShell loop
                here is the main loop that holds everything
                together  this loop is the one that 
                ask for each comand and every time goes back
                to stage one                
            */
            while (!Shell.ExitRequest)
			{

                shell.CurrentPath = Shell.CurrentPath;
				shell.Notifications = ShellUser.Name == null ? $"'{Environment.UserName}' Without Credentials" : ShellUser.Name;
				////shell.TextSimbol = Shell.VStack.VirtualStackSize()=="0B" ? ">" : $"[{Shell.VStack.VirtualStackSize()}]>";
			    ////shell.ProgramName = $"{Shell.Name} [{Shell.VStack.VirtualStackSize()}]";
				string input = shell.StartInput();
				this.SaveHistory(input);
				string[] commands = IConvert.TextToArray(input);
				ShellTrace.AddTrace($"Commands Inputed Length: {commands.Length}");
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
