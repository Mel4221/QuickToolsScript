using QuickTools.QCore;
using System;
using System.IO; 
using System.Threading;
using States;
using System.Collections.Generic;
using QuickTools.QData;

namespace Settings
    {
    /// <summary>
    /// Contains all the information related with the Settings of the Shell
    /// and stuff that could be modified fromt he shell
    /// </summary>
    public class ShellSettings
    {
        private static string RootPath()
        {
            return Get.DataPath("shell");
        }
        public ShellSettings()
            {
                SettingsManager = new KeyManager(ShellSettingsFile);
            }
        private static KeyManager SettingsManager;
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
            public static string LogsFile { get; set; } = $"{Path}ClownShell.log";

            /// <summary>
            /// Gets or sets the shell settings file.
            /// </summary>
            /// <value>The shell settings file.</value>
            public static string ShellSettingsFile { get; set; } = $"{Path}ClownShell.settings";

            /// <summary>
            /// Gets or sets the settings sync rate by default is set to 5 seconds or 5000 milliseconds.
            /// </summary>
            /// <value>The settings sync rate.</value>
            public static int SettingsSyncRate { get; set; } = 5000; 
                
            
            private static Thread SettingsWatcher;



        private static void StartSettingsWatcher()
            {
                if(SettingsWatcher == null)
                {
                    SettingsWatcher = new Thread(() => { 
                        while(!Shell.ExitRequest)
                        {
                            SyncSettings();
                            Thread.Sleep(SettingsSyncRate);
                        }
                    });
                    SettingsWatcher.Start();
                }
            }

        /// <summary>
        /// Syncs the settings and is already synced autmatically by the  SettingsWatcher
        /// </summary>
        public static void SyncSettings()
            {
                Get.Title($"Settings Synced: {DateTime.Now}");
            }
            /// <summary>
            /// Resets to default.
            /// </summary>
            public static void ResetToDefault()
            {
                List<Key> keys = new List<Key>()
                {
                    new Key(){Name="ShellDefaultStartPath",Value="/"},
                    new Key(){Name="ShellVariablesDB",Value="ClownShellLocalVariables.db"},
                    new Key(){Name="ShellHistoryFileName",Value= $"{ShellSettings.Path}ClownShell.history"},
                    new Key(){Name="LogsFile",Value=$"{Path}ClownShell.log"},
                    new Key(){Name="ShellSettingsFile",Value=$"{Path}ClownShell.settings"},
                    new Key(){Name="SettingsSyncRate",Value="5000"}
                };
                KeyManager manager = new KeyManager(ShellSettingsFile);
                manager.AllowDebugger = true;
                manager.Create(); 
                manager.WriteKeys(keys); 
                
            }   
            /// <summary>
            /// Loads the settings.
            /// </summary>
            public static void LoadSettings()
            {
                KeyManager manager = new KeyManager(ShellSettingsFile);
                manager.AllowDebugger = true;
                manager.LoadKeys();
            }
        /// <summary>
        /// Saves the settings.
        /// </summary>
        public static void SaveSettings()
            {

            }
        }
    }