using QuickTools.QData;
using System;


namespace MainLoop
{
	public partial class ShellLoop
	{
		public MiniDB GetHistory()
		{
			this.db = new MiniDB(HistoryFile, true);
            //this.db.AllowDebuger = true;
			this.db.Load();
			return this.db;
		}
		private bool IsShellCommand(string command)
		{
			switch (command)
			{
				case "history":
				case "exit":
                case "clear-history":
                case "reboot":
					return true;
				default:
					return false;
			}
		}
		public void SaveHistory(string command)
		{
			if (this.IsShellCommand(command)) return;// if is a shell command don't save it
			if (this.AllowToSaveHistory == false) return;// if is not allowed to show history return
            db = new MiniDB();
            db.DBName = HistoryFile;
            db.AllowRepeatedKeys = true;
            db.AllowDebugger = false; 
			db.Create();
			db.Load();
			db.AddKeyOnHot("command", command, DateTime.Now.ToLongDateString());
			db.HotRefresh();
            return;
		}
	}
}
