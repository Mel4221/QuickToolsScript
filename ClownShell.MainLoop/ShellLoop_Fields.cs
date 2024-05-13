using QuickTools.QData;
using Settings;


namespace MainLoop
{
	public partial class ShellLoop
	{

		public string HistoryFile = ShellSettings.ShellHistoryFileName;
		public bool AllowToSaveHistory { get; set; } = true;
		private MiniDB db;
        private string[] Arguments { get; set; } = new string[] { };
    }
}
