using System;
using System.Text;
 
namespace States
{

    /// <summary>
    /// Contains the shell trace of some of the most important process
    /// executed from the shell
    /// </summary>
	public static class ShellTrace
	{ 
		private static StringBuilder Trace  = new StringBuilder();
        /// <summary>
        /// Gets or sets the size of the trace.
        /// </summary>
        /// <value>The size of the trace max.</value>
		public static int TraceMaxSize { get; set; } = 1024;
		private static int Index = 0; 

        /// <summary>
        /// Returns the trace and clear previus Trace
        /// </summary>
        /// <returns>The trace.</returns>
		public static string GetTrace()
		{
			string trace = Trace.ToString();
			Trace = new StringBuilder();
			return trace;
		}
 
	    /// <summary>
        /// Adds the a trace
        /// </summary>
        /// <param name="code">Code.</param>
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
