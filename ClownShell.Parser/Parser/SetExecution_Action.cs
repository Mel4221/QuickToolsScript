using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;
using ErrorHandelers;
using ScriptRunner;
using QuickTools.QCore;
using QuickTools.QData;
using States;
using QuickTools.QIO;
using System.Diagnostics;
using Settings;
using Security; 

namespace Parser
{
    public partial class CodeParser
    {
        public void SetExecution(string action)
        {
        
			ErrorHandeler error = new ErrorHandeler();
			Runner runner = new Runner();
			ProcessStartInfo process;
			ShellTrace.AddTrace($"Execution Set With Action: {action} ");

			if(this.HasExecutable(action))
			{
				this.RunExecutable(action); 
				return;
			}if(Get.FileExention(action) == "clown")
			{
			    if(!this.IsRootPath(action))
                {   
                    if (File.Exists(this.GetPathWithType(action)))
                    {
                       // this.ParseFromFile(action);
                    }
                }
                return;
			}
            switch (action)
            {
                case "sync-settings":
                    runner.Run(() => {
                        ShellSettings.SyncSettings();
                    });
                    break;
                case "reset-settings":
                    runner.Run(() => {
                    ShellSettings.ResetToDefault();
                    });
                    break;
                case "load-settings":
                    runner.Run(() => {
                    ShellSettings.LoadSettings();
                    });
                    break;
                case "qt-data?":
                    Get.Yellow(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
                    break;
				case "collect":
				case "gc":
					GC.Collect();
					break;
				case "return":
					break;
				case "input":
					runner.Run(() => {
						Shell.VStack.Free("input");
						Shell.VStack.SetVariable(new Variable()
						{
							Name = $"input",
							Value = null,
							IsConstant = false
						});
					});
					break;
				case "print-stack-size":
				case "print-vstack-size":
				case "stack-size":
				case "vstack":
				case "stack":
					runner.Run(() => { Get.Yellow(Shell.VStack.VirtualStackSize());});
					break;
				case "clear-objects":
				case "free-objecs":
					runner.Run(()=> { this.FreeObjects(); });
					break;
                case "clear":
                    runner.Run(() => { Get.Clear(); });
                    break;
                case "exit":
                    runner.Run(() => {
                        Get.Blue(BackGroundJob.HasUserJobs()); 
                        if(BackGroundJob.HasUserJobs())
                        {
                            BackGroundJob.PrintRunningJobs();
                            Get.Alert($"There are jobs running kill them all first or wait them to finish");
                            return;
                        }
                        Shell.Exit();
						//Shell.ExitRequest = true; 
                    });
                    break;
				case "{":
					runner.Run(() => {
						List<string> lines = new List<string>();
						string line;
						while (true)
						{
							line = Get.Input().Text;
							if (line == "}")
							{
								break;
							}
							if (line != "")
							{
								lines.Add(line);
							}
						}
						lines.ForEach(item => {
							Get.Green($"Running: {item}");
							string[] code = IConvert.TextToArray(item);
							new CodeParser(code).Start();
						});

					});
					break;
				case "free-stack":
				case "clear-stack":
					runner.Run(() => {
						Shell.VStack.Flush();
					});
					break;
				case "whoami":
					runner.Run(() => {
						Get.Green(ShellUser.Name);
					});
					break;
				case "singup":
					runner.Run(() => {
						new Credentials().SingUp();
					});
					break;
				case "login":
					runner.Run(() => {
						new Credentials().Login();
					});
					break;
				case "beep":
					runner.Run(() => {
						Console.Beep();
					});
					break;
				case "kill-all":
					runner.Run(() => {
						BackGroundJob.KillAll();
					});
					break;
				case "jobs":
				case "job?":
					runner.Run(() => {
						BackGroundJob.PrintRunningJobs();
					});
					break;
				case "hold":
					runner.RunningCodeInfo = "This function just creates a hold in a thread as Thread.Sleep(1000)";
					runner.RunningBackGroundCodeName = "Thread Hold";
					runner.Run(() => {
						this.Call("sleep", "1000");
					});
					break;
				case "reset-title":
					runner.Run(() => {
						Get.Title(Shell.Name);
					});
					break;
				case "reset-path":
					Shell.CurrentPath = Directory.GetCurrentDirectory();
					break;
				case "clear-cache":
				case "cache-reset":
					runner.Run(() => { /*cache.ClearCache();*/ });
					break;
				case "clear-logs":
					runner.Run(() => { Log.ClearLogs(); });
					break;
				case "ls":
					runner.Run(() => {

						Get.Ls(Shell.CurrentPath);
					});
					// Get.Ls(ShellLoop.CurrentPath);
					break;
				case "select":
				case "-S":
					error.DisplayError(ErrorType.NotImplemented, $"The command is recognized but is not currently implemented or is Disabled");

					//runner.Run(() => {
					//    string[] files = new FilesMaper().GetFiles();
					//    string[] folders = Directory.GetDirectories(this.Target);
					//    string[] both = new string[files.Length+folders.Length];
					//    if (files.Length > 0)
					//    {
					//        for (int current = 0; current < files.Length; current++)
					//        {
					//            both[current] = Get.FileNameFromPath(files[current]);
					//        }
					//    }
					//    int bothLength = both.Length - 1;  
					//    if(folders.Length > 0)
					//    {
					//        for (int current = 0; current < folders.Length; current++)
					//        {
					//            string path = folders[current];
					//            both[bothLength] = path.Substring(path.LastIndexOf(Get.Slash()) + 1);
					//            bothLength--;
					//        }
					//    }

					//    Options option = new Options(both);
					//    option.Label = this.Target;
					//    option.SelectorL = "> ";
					//    option.SelectorR = ""; 
					//    int selection = option.Pick();
					//    string str = null;
					//    str = this.Target[this.Target.Length - 1].ToString() == Get.Slash() ? "" : Get.Slash(); 
					//    ShellLoop.SelectedOject = $">{this.Target}{Get.Slash()}{both[selection]}";
					//    //Get.Yellow(this.Target);
					//    //Get.Wait(ShellLoop.SelectedOject);
					//});
					break;
				case "select?":
				case "selected":
				case "-S?":
					runner.Run(() => {
						Get.Blue();
						Get.WriteL(" ");
						Get.Write($"Object Selected: ");
						Get.Yellow();
						string str = Shell.SelectedObject == "" || Shell.SelectedObject  == null ? "NONE" : Shell.SelectedObject;
						Get.Write(str);
					});
					break;
				case "clear-selected":
				case "clear-S":
					runner.Run(() => {
						Shell.SelectedObject = null;
					});
					break;
				case "history":
					runner.Run(() => {

						MiniDB db;
						db =  new MiniDB(ShellSettings.ShellHistoryFileName);
						db.Load();
						Get.WriteL(" ");
						db.DataBase.ForEach((item) => {
							Get.Green();
							Get.Write($"No: {item.Id} ");
							Get.Yellow();
							Get.Write($"Command: {item.Value} ");
							Get.Blue();
							Get.Write($"Date: {item.Relation} ");
							Get.WriteL(" ");
						});
					});
					break;   
				case "disks":
				case "disk?":
					runner.Run(() => { Get.PrintDisks(); });
					break;
				case "get-logs":
					runner.Run(() => {
						var db = new MiniDB(ShellSettings.LogsFile);
						db.Load();
						db.DataBase.ForEach(item=>Get.Write(item.ToString()));	
					});
					break;
				case "shutdown":
					runner.Run(() => {
						switch (Get.IsWindow())
						{
							case true:
								process = new ProcessStartInfo("shutdown", "/s /t 0");
								process.CreateNoWindow = true;
								process.UseShellExecute = false;
								Process.Start(process);
								break;
							default:
								process = new ProcessStartInfo("shutdown", "0");
								process.CreateNoWindow = true;
								process.UseShellExecute = false;
								Process.Start(process);
								break;
						}


					});
					break;
				case "reboot":
					runner.Run(() => {//shutdown -r -t 0
						switch (Get.IsWindow())
						{
							case true:
								process = new ProcessStartInfo("shutdown", "-r -t 0");
								process.CreateNoWindow = true;
								process.UseShellExecute = false;
								Process.Start(process);
								break;
							default:
								process = new ProcessStartInfo("reboot", "0");
								process.CreateNoWindow = true;
								process.UseShellExecute = false;
								Process.Start(process);
								break;
						}

					});
					break;
				case "cmd":
					runner.Run(() => {
						Process cmd = new Process();

						cmd.StartInfo.FileName = "cmd";//"cmd.exe";
													   //cmd.StartInfo.Arguments;
													   //cmd.StartInfo.RedirectStandardInput = true;
						cmd.StartInfo.RedirectStandardOutput = false;  // true;
						cmd.StartInfo.CreateNoWindow = false;
						cmd.StartInfo.UseShellExecute = false;
						//cmd.StartInfo.Arguments = "ping www.google.com"; //Helper.ResolvePath(this).Target;

						cmd.Start();
						cmd.WaitForExit();
						/* execute "dir" */

						//cmd.StandardInput.WriteLine(this.SubTarget);
						//cmd.StandardInput.Flush();
						//cmd.StandardInput.Close();
						//Console.WriteLine(cmd.StandardOutput.ReadToEnd());
					});
					break;
				case "vim":
					runner.Run(() => {
						Process cmd = new Process();

						cmd.StartInfo.FileName = "editors/vim/vim.exe";//"cmd.exe";
															  //cmd.StartInfo.Arguments;
															  //cmd.StartInfo.RedirectStandardInput = true;
						cmd.StartInfo.RedirectStandardOutput = false;  // true;
						cmd.StartInfo.CreateNoWindow = false;
						cmd.StartInfo.UseShellExecute = false;
						//cmd.StartInfo.Arguments = "ping www.google.com"; //Helper.ResolvePath(this).Target;

						cmd.Start();
						cmd.WaitForExit();
						/* execute "dir" */

						//cmd.StandardInput.WriteLine(this.SubTarget);
						//cmd.StandardInput.Flush();
						//cmd.StandardInput.Close();
						//Console.WriteLine(cmd.StandardOutput.ReadToEnd());
					});
					break;
				case "nano":
					runner.Run(() => {
						Process cmd = new Process();

						cmd.StartInfo.FileName = "editors/nano/nano.exe";//"cmd.exe";
																	   //cmd.StartInfo.Arguments;
																	   //cmd.StartInfo.RedirectStandardInput = true;
						cmd.StartInfo.RedirectStandardOutput = false;  // true;
						cmd.StartInfo.CreateNoWindow = false;
						cmd.StartInfo.UseShellExecute = false;
						//cmd.StartInfo.Arguments = "ping www.google.com"; //Helper.ResolvePath(this).Target;

						cmd.Start();
						cmd.WaitForExit();
						/* execute "dir" */

						//cmd.StandardInput.WriteLine(this.SubTarget);
						//cmd.StandardInput.Flush();
						//cmd.StandardInput.Close();
						//Console.WriteLine(cmd.StandardOutput.ReadToEnd());
					});
					break;
				case "powershell":
					runner.Run(() => {
						Process cmd = new Process();

						cmd.StartInfo.FileName = "powershell";//"cmd.exe";
															  //cmd.StartInfo.Arguments;
															  //cmd.StartInfo.RedirectStandardInput = true;
						cmd.StartInfo.RedirectStandardOutput = false;  // true;
						cmd.StartInfo.CreateNoWindow = false;
						cmd.StartInfo.UseShellExecute = false;
						//cmd.StartInfo.Arguments = "ping www.google.com"; //Helper.ResolvePath(this).Target;

						cmd.Start();
						cmd.WaitForExit();
						/* execute "dir" */

						//cmd.StandardInput.WriteLine(this.SubTarget);
						//cmd.StandardInput.Flush();
						//cmd.StandardInput.Close();
						//Console.WriteLine(cmd.StandardOutput.ReadToEnd());
					});
					break;
				default:
					Variable v = Shell.VStack.GetVariable(action);
					if(!v.IsEmpty){
						Get.White(v.Value);
						return;
					}
					ShellTrace.AddTrace($"Action Was not Recognized as a valid Action");
                    error.DisplayError(ErrorType.NotValidAction, $"At: Execution With Action: '{action}' Trace: \n{ShellTrace.GetTrace()}");
                    break;
            }
        }
    }
}
