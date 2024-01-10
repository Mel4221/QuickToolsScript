using System;
using QuickTools.QCore;
using QuickTools.QData;
namespace States
{
   public static class Shell
   {
		public const string Name = "ClownShell";
		public static string SesionID = IRandom.RandomText(64);
		public static string Title = null;
		public static string CurrentPath { get; set; } = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
		public static bool ExitRequest { get; set; } = false;
		public static string SelectedObject { get; set; } = "none";
		public static VirtualStack VStack { get; set; } = new VirtualStack();
		public static MiniDB MiniDB { get; set; }	

	}
}
