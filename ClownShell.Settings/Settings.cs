using QuickTools.QCore;
using System;
using System.Threading;

    namespace Settings
    {
        public static class ShellSettings
        {
            private static string RootPath()
            {
                return Get.DataPath("shell");
            }
            public static string ShellDefaultStartPath { get; set; } = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            public static string ShellVariablesDB { get; set; } = RootPath();
            public static string File { get; set; } = RootPath();
            public static string Path { get; set; } = RootPath();
            public static string ShellHistoryFileName { get; set; } = $"{ShellSettings.Path}ClownShell.history";
            public static string LogsFile { get; set; } = $"{ShellSettings.Path}ClownShell";

            private static Thread SettingsWatcher;

            private static void SyncSettings()
            {

            }
            public static void ResetToDefault()
            {

            }
            public static void LoadSettings()
            {

            }
            public static void SaveSettings()
            {

            }
        }
    }