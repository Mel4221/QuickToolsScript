using System;
using System.Text;
 
namespace States
{
	public static class ShellTrace
	{
		private static StringBuilder Trace  = new StringBuilder();
		public static int TraceMaxSize { get; set; } = 1024;
		private static int Index = 0; 
		public static string GetTrace()
		{
			string trace = Trace.ToString();
			Trace = new StringBuilder();
			return trace;
		}
 
	 
		public static void AddTrace(string code)
		{
			if(Index  < TraceMaxSize)
			{
				Trace.Append($"Trace: {code} At:{DateTime.Now.ToString("M/dd/yyyy hh:m:ss")}\n");
				Index++;
				return;
			}else{
				Trace = new StringBuilder(code);
				Trace.Append($"Trace: {code} At:{DateTime.Now.ToString("M/dd/yyyy hh:m:ss")}\n");
				Index = 0; 
			}

		}
	}
}
