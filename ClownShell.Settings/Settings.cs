    using QuickTools.QCore;
    using System;
    using System.IO; 
    using System.Threading;
    using QuickTools.QData;
    using States;


    namespace Settings
        {
        /// <summary>
        /// Contains all the information related with the Settings of the Shell
        /// and stuff that could be modified fromt he shell
        /// </summary>
        public static class ShellSettings
        {
                private static string RootPath()
                {
                    return Get.DataPath("shell");
                }
               
                   

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
                public static string LogsFile { get; set; } = $"{Path}ClownShell.log";

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
                
                //private static Thread SettingsWatcher;

                //private static Thread ThreadWatcher;
                
                /// <summary>
                /// Starts the settings watcher and defines the properties that
                /// has to be checked before syncing the changes on the settings
                /// </summary>
                private static void StartSettingsWatcher()
                {

                    Job job = new Job()
                    {
                        Name = "SettingsWatcher",
                        Info = @"Checks the settings to make sure that they are 
                        always on sync with the ClownShell.settings file.",
                        ID = 0001,
                        IsInternalJob = true,
                        JobAction = () => 
                        {
                            while (!Shell.ExitRequest)
                            {
                                if (LastPath != Shell.CurrentPath)
                                {
                                    LastPath = Shell.CurrentPath;
                                }
                                SyncSettings();
                                Thread.Sleep(SettingsSyncRate);
                            }
                        }
                    };


                    BackGroundJob.AddJob(job);
                    BackGroundJob.RunJobs();
            /*
                    if(SettingsWatcher == null)
                    {

                      ThreadWatcher = new Thread(() =>
                     {
                        while(!Shell.ExitRequest)
                        {
                   
                        }
                    });
               
            SettingsWatcher = new Thread(() => { 
                            while(!Shell.ExitRequest)
                            {
                                 if(LastPath != Shell.CurrentPath)
                                {
                                    LastPath = Shell.CurrentPath; 
                                }
                                SyncSettings();
                                Thread.Sleep(SettingsSyncRate);
                            }
                        });
                                    SettingsWatcher.Start();
                        ThreadWatcher.Start();
                         */

        
                }

            /// <summary>
            /// Start this instance.
            /// </summary>
            public static void StartSettingsManager()
            {
                SettingsManager = new MiniDB();
                SettingsManager.DBName = ShellSettingsFile;
                LoadSettings();
                StartSettingsWatcher();
            }
            /// <summary>
            /// Syncs the settings and is already synced autmatically by the  SettingsWatcher
            /// </summary>
            public static void SyncSettings()
                {
                    Shell.Title = $"Settings Sync: {DateTime.Now}";
                    //Get.Beep();
                    if(File.Exists(ShellSettingsFile))
                    {
                        if(Get.HashCodeFromFile(ShellSettingsFile) != ShellSettingsFileHash)
                        {
                            LoadSettings(); 
                        }
                    }

                }
                /// <summary>
                /// Resets to default.
                /// </summary>
                public static void ResetToDefault()
                {
                        /*
                        new Key(){Name="ShellDefaultStartPath",Value="/"},
                                           
                        new Key(){Name="ShellVariablesDB",Value="ClownShellLocalVariables.db"},
                                           
                        new Key(){Name="ShellHistoryFileName",Value= $"{ShellSettings.Path}ClownShell.history"},

                        new Key(){Name="LogsFile",Value=$"{Path}ClownShell.log"},

                        new Key(){Name="ShellSettingsFile",Value=$"{Path}ClownShell.settings"},

                        new Key(){Name="SettingsSyncRate",Value="5000"}
                        */
                    
                    SettingsManager = new MiniDB();
                    SettingsManager.DBName = ShellSettingsFile; 

                    SettingsManager.AddKeyOnHot("ShellDefaultStartPath", "/", "ClownShell_Setting");
                    SettingsManager.AddKeyOnHot("ShellVariablesDB", "ClownShellLocalVariables.db", "ClownShell_Setting");
                    SettingsManager.AddKeyOnHot("ShellHistoryFileName", $"{ShellSettings.Path}ClownShell.history", "ClownShell_Setting");
                    SettingsManager.AddKeyOnHot("LogsFile", $"{Path}ClownShell.log", "ClownShell_Setting");
                    SettingsManager.AddKeyOnHot("ShellSettingsFile", $"{Path}ClownShell.settings", "ClownShell_Setting");
                    SettingsManager.AddKeyOnHot("SettingsSyncRate", $"5000", "ClownShell_Setting");
                    SettingsManager.AddKeyOnHot("LastPath", $"", "ClownShell_Setting");
                    SettingsManager.AddKeyOnHot("SettingsID",IRandom.RandomText(64), "ClownShell_Setting");
                    
                    


                //SettingsManager.AllowDebugger = true;
                SettingsManager.Create();
                SettingsManager.SaveChanges();
                        
                    
                }   
                /// <summary>
                /// Loads the settings.
                /// </summary>
                public static void LoadSettings()
                {
                    SettingsManager = new MiniDB();
            //SettingsManager.AllowDebugger = true;
                    SettingsManager.DBName = ShellSettingsFile; 
                    bool loaded = SettingsManager.Load();
                    if(!loaded)
                    {
                        Get.Wrong($"Failed to load the SettingsFile: {ShellSettingsFile}");
                        Get.Yellow($"Restored Default Settigns");
                        ResetToDefault();
                        return;
                    }
                    
                    ShellSettingsFileHash = Get.HashCodeFromFile(ShellSettingsFile);
                    ShellDefaultStartPath = SettingsManager.SelectWhereKey("ShellDefaultStartPath").Value;
                    ShellVariablesDB = SettingsManager.SelectWhereKey("ShellVariablesDB").Value;
                    ShellHistoryFileName = SettingsManager.SelectWhereKey("ShellHistoryFileName").Value;
                    LogsFile = SettingsManager.SelectWhereKey("LogsFile").Value;
                    ShellSettingsFile = SettingsManager.SelectWhereKey("ShellSettingsFile").Value;
                    SettingsSyncRate = int.Parse(SettingsManager.SelectWhereKey("SettingsSyncRate").Value);
                    LastPath = SettingsManager.SelectWhereKey("LastPath").Value; 
                
                }

                /// <summary>
                /// Saves the settings.
                /// </summary>
                public static void SaveSettings()
                {

                }
            }
        }