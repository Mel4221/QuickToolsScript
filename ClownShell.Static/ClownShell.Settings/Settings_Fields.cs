using QuickTools.QCore;
using System;
using System.IO;
using System.Threading;
using QuickTools.QData;
using States;
namespace Settings
{
    public static partial class ShellSettings
    {
        private static MiniDB SettingsManager;
        /// <summary>
        /// Gets or sets the shell default start path of the shell.
        /// </summary>
        /// <value>The shell default start path.</value>
        public static string ShellDefaultStartPath { get; set; } = Directory.GetDirectoryRoot(Get.Slash());
        /// <summary>
        /// Gets or sets the shell variables db.
        /// </summary>
        /// <value>The shell variables db.</value>
        public static string ShellVariablesDB { get; set; } = $"{RootPath()}ClownShellLocalVariables.db";

        /// <summary>
        /// Gets or sets the path.
        /// </summary>
        /// <value>The path.</value>
        public static string Path { get; set; } = RootPath();
        /// <summary>
        /// Gets or sets the name of the shell history file.
        /// </summary>
        /// <value>The name of the shell history file.</value>
        public static string ShellHistoryFileName { get; set; } = $"{Path}ClownShell.history";
        /// <summary>
        /// Gets or sets the logs file.
        /// </summary>
        /// <value>The logs file.</value>
        public static string LogsFile { get; set; } = $"{Path}ClownShell";

        /// <summary>
        /// Gets or sets the shell settings file.
        /// </summary>
        /// <value>The shell settings file.</value>
        public static string ShellSettingsFile { get; set; } = $"{Path}ClownShell.settings";

        /// <summary>
        /// Gets or sets the settings sync rate by default is set to 1 seconds or 1000 milliseconds.
        /// </summary>
        /// <value>The settings sync rate.</value>
        public static int SettingsSyncRate { get; set; } = 1000;

        /// <summary>
        /// Gets or sets the last path.
        /// </summary>
        /// <value>The last path.</value>
        public static string LastPath { get; set; } = "";

        /// <summary>
        /// Gets or sets the shell settings file hash.
        /// </summary>
        /// <value>The shell settings file hash.</value>
        public static double ShellSettingsFileHash { get; set; }

        /// <summary>
        /// Gets or sets the clown shell source database.
        /// </summary>
        /// <value>The clown shell source database.</value>
        public static string ClownShellSources { get; set; }

    }
}
