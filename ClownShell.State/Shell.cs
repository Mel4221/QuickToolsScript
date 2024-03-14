using QuickTools.QCore;
using QuickTools.QData;
using Settings;
namespace States
{
    /// <summary>
    /// Contains The Shell current status such as current path and basic information
    /// about the shell current operations 
    /// </summary>
   public static class Shell
   {
        /// <summary>
        /// Contains the Shell name
        /// </summary>
		public const string Name = "ClownShell";
        /// <summary>
        /// Contains a randome id for the current session of the shell 
        /// </summary>
		public static string SessionID = IRandom.RandomText(64);
        /// <summary>
        /// Will be used to display a title along side the Shell name
        /// </summary>
		public static string Title { get; set; } = null;
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        public static string Message { get; set; } = null;
        /// <summary>
        /// Gets or sets the current path of the shell
        /// </summary>
        /// <value>The current path.</value>
        public static string CurrentPath { get; set; } = ShellSettings.ShellDefaultStartPath;
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:States.Shell"/> exit request.
        /// </summary>
        /// <value><c>true</c> if exit request; otherwise, <c>false</c>.</value>
		public static bool ExitRequest { get; set; } = false;
        /// <summary>
        /// Gets or sets the selected object.
        /// </summary>
        /// <value>The selected object.</value>
		public static string SelectedObject { get; set; } = "none";
        /// <summary>
        /// Gets or set the Virtual Stack of the shell
        /// </summary>
        /// <value>The VS tack.</value>
		public static VirtualStack VStack { get; set; } = new VirtualStack();
        /// <summary>
        /// Contains the default MiniDB
        /// </summary>
        /// <value>The mini db.</value>
		public static MiniDB MiniDB { get; set; } = new MiniDB(); 


        /// <summary>
        /// Exit this Shell.
        /// </summary>
        public static void Exit()
        {
            BackGroundJob.KillAll();
            Shell.ExitRequest = true;
        }

    }
}
