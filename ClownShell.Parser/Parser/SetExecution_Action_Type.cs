using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;
using ErrorHandelers;
using ScriptRunner;
using QuickTools.QCore;
using System.Diagnostics;
using States;
using QuickTools.QIO;
using QuickTools.QConsole;
using QuickTools.QData;

namespace Parser
{
	public partial class CodeParser
	{
		public void SetExecution(string action,string type)
        {
            
            ErrorHandeler error = new ErrorHandeler();
            Runner runner = new Runner();
			ProcessStartInfo process;
			string file, path;
			//bool isBackGroundAction;
			ShellTrace.AddTrace($"Execution Started With Action: {action} Type: {type}");
			switch (action)
            {

                case "diff":
                    runner.Run(() => {
                    if (type == "dependencies" ||
                       type == "-d")
                    {
                        path = Get.Path;
                        FilesMaper maper = new FilesMaper(path);
                        maper.Map();
                        Func<string[]> f = () => {
                            List<string> exeAndDll = new List<string>();
                            maper.Files.ForEach((item) => {
                                if (Get.FileExention(item) == "exe" ||
                                   Get.FileExention(item) == "dll")
                                {
                                    exeAndDll.Add(item);
                                }

                            });

                            return exeAndDll.ToArray();
                        };

                        string[] files = f();
                        Print.List(files, true);
                        maper.Directories.ForEach((dir) => Get.Blue($"{dir} DIR"));
                            Get.Yellow($"Total Dependencies [{files.Length}]");
                            return;
                        }

                    }); 
                    break; 
                case "status":
                    runner.Run(() => { 

                    });
                    break;
				case "install":
					error.DisplayError(ErrorType.NotImplemented);
					break;

				case "read":
					runner.Run(() => {

						file = null;


						if(this.IsRootPath(type))
						{

							file = type;
							byte[] bytes = Binary.Reader(file);
							Shell.VStack.SetVariable(new Variable()
							{
								Name = $"read{Shell.VStack.GetIndex()}",
								Value = null,
								Buffer = bytes,
								IsConstant = false,
								IsEmpty = false
							});
							Get.Green($"OK: {Get.FileSize(bytes)}");
							return;
						}
						if (Helper.HasSpecialFolder(type) != null)
						{
							file = Helper.HasSpecialFolder(type);
							byte[] bytes = Binary.Reader(file);
							Shell.VStack.SetVariable(new Variable()
							{
								Name = $"read{Shell.VStack.GetIndex()}",
								Value = null,
								Buffer = bytes,
								IsConstant = false,
								IsEmpty = false
							});
							Get.Green($"OK: {Get.FileSize(bytes)}");
							return;
						}
						if (file == null)
						{
							file = this.GetPathWithType(type);
							byte[] bytes = Binary.Reader(file);
							Shell.VStack.SetVariable(new Variable()
							{
								Name = $"read{Shell.VStack.GetIndex()}",
								Value = null,
								Buffer = bytes,
								IsConstant = false,
								IsEmpty = false
							});
							return;
						}
						if (!File.Exists(file))
						{
							Get.Red($"The file {file} was not found!!!");
							return;
						}
						

					});
					break;
				case "write":
					runner.Run(() => {
						Console.Write(type);
					});
					break;
				case "echo":
				case "print":
				case "log":
					runner.Run(() => {
                        Console.WriteLine(type);
                    });
                    break;
				case "free":
					runner.Run(() => {
						Shell.VStack.Free(type);
					});
					break;
				case "wait":
					runner.RunningCodeInfo = $"waiting for {type}ms";
					runner.RunningBackGroundCodeName = "Process Wait...";
					runner.Run(() => {
						//this.Call("clear");
						this.Call("sleep", type);
						this.Call("write", "|");
						this.Call("title", "|");

						//this.Call("clear");
						this.Call("sleep", type);
						this.Call("write", "/");
						this.Call("title", "/");

						this.Call("clear");
						this.Call("sleep", type);
						this.Call("write", "-");
						this.Call("title", "-");

						//this.Call("clear");
						this.Call("sleep", type);
						this.Call("write", "\\");
						this.Call("title", "\\");

						//this.Call("clear");
						this.Call("sleep", type);
						this.Call("write", "|");
						this.Call("title", "|");

						//this.Call("clear");
						this.Call("sleep", type);
						this.Call("reset-title");
						this.Call("write", "Done!!!");
						return;
					}, true);
					break;
				case "resume":
					runner.Run(() => {
						BackGroundJob.Resume(int.Parse(type));
					});
					break;
				case "pause":
				case "stop":
					runner.Run(() => {
						BackGroundJob.Pause(int.Parse(type));
					});
					break;
				case "kill":
					runner.Run(() => {
						BackGroundJob.Kill(int.Parse(type));
					});
					break;
				case "set-title":
				case "title":
					runner.Run(() => {
						Get.Title(type);
					});
					break;
				case "mkdir":
					runner.Run(() => 
					{

						if(this.IsRootPath(type))
						{
							Make.Directory(type); 
							return;
						}
						if (Helper.HasSpecialFolder(type) != null)
						{
							path = Helper.HasSpecialFolder(type);
							Get.Cyan(path);
							Make.Directory(path);
							return;
						}
						if (Helper.ReferToDisk(type.ToUpper()))
						{
							Make.Directory(type);
							return;
						}else
						{
							path = GetPathWithType(type);
							Make.Directory(path);
						}
						
					});
					break;
				case "touch":
				case "create":
					runner.Run(() => { Make.File(type); });
					break;
				case "set-color-pink":
				case "pink":
					runner.Run(() => { Get.Pink(type); });
					break;
				case "set-color-red":
				case "red":
					runner.Run(() => { Get.Red(type); });
					break;
				case "set-color-blue":
				case "blue":
					runner.Run(() => { Get.Blue(type); });
					break;
				case "set-color-yellow":
				case "yellow":
					runner.Run(() => { Get.Yellow(type); });
					break;
				case "set-color-green":
				case "green":
					runner.Run(() => { Get.Green(type); });
					break;
				case "set-color-gray":
				case "gray":
					runner.Run(() => { Get.Gray(type); });
					break;
				case "set-color-cyan":
				case "cyan":
					runner.Run(() => { Get.Cyan(type); });
					break;
				case "set-color-black":
				case "black":
					runner.Run(() => { Get.Black(type); });
					break;
				case "rm":
				case "remove":
				case "delete":
				case "rmdir":
					file = type;
					runner.Run(() =>
					{
						if (Helper.HasSpecialFolder(file)!= null)
						{
							file = Helper.HasSpecialFolder(file);
						}
						if (Helper.ReferToDisk(file))
						{
							file = $"{file}";
						}
						if (File.Exists(file))
						{

							//  Get.Yellow(this.Target);
							GC.Collect();
							GC.WaitForPendingFinalizers();
							File.Delete(file); 
							Get.Red(file);
							return;
						}
						if(Directory.Exists(this.GetPathWithType(type)))
						{
							GC.Collect();
							GC.WaitForPendingFinalizers();
							Directory.Delete(type);
							return;
						}
						if (Directory.Exists(type))
						{
							GC.Collect();
							GC.WaitForPendingFinalizers();
							Directory.Delete(type);
							return;
						}
						else
						{
							error.DisplayError(ErrorType.NotValidType, $"File or Directory '{type}' was not found!!!");

						}
					});
					break;
				case "ls":
				case "list":
				case "list-files":
					runner.Run(() => {


						if (this.IsRootPath(type))
						{
							Get.Ls(type);
							return;
						}
						if (type == "disk")
						{
							Get.PrintDisks();
							return;
						}
						if (type == "-l")
						{
							Get.Ls(Shell.CurrentPath, null);
							return;
						}
						if (type == ".")
						{
							Get.Ls(Shell.CurrentPath);
							return;
						}
						if (Directory.Exists(this.GetPathWithType(type)))
						{
							Get.Ls(this.GetPathWithType(type));
							return;
						}
						if (Helper.HasSpecialFolder(type) != null)
						{
							Get.Ls(Helper.HasSpecialFolder(type));
							return;
						}
						if (type.Contains(".")&&type.Length >=2)
						{
							//path = ShellLoop.CurrentPath[ShellLoop.CurrentPath.Length-1]==Get.Slash()[0] ? $"{ShellLoop.CurrentPath}{type}" : $"{ShellLoop.CurrentPath}{Get.Slash()}{type}";
							Get.Ls(this.GetPathWithType(type));
							return;
						}
						else
						{
							Get.Ls(type);
						}


						//Get.Yellow($"{this.Target}     ClearTarget: {this.SubTarget} Type: {type}");
						//Get.Blue(Path.GetDirectoryName(this.Target));
						// Get.Yellow(ShellLoop.RelativePath);
						// Get.Wait(type);
						//this.SubTarget = type; 
						//CodeParser helper = Helper.ResolvePath(this);
						//this.Target = helper.Target;
						//this.SubTarget = helper.SubTarget;
						//Get.Cyan($"Target: {this.Target} SubTarget: {this.SubTarget}");

						//if(type == "-l")
						//{
						//    Get.Ls(helper.Target, "");
						//    return; 
						//}
						//if (type.Contains('*'))
						//{
						//    //Get.Wait($"{this.Target.Substring(0,this.Target.LastIndexOf("*"))} {type.Substring(1)}");
						//    // Get.Wait(type);  //Get.FileExention(type)
						//    //Get.Wait(this.Target.Substring(this.Target.LastIndexOf(Get.Slash())));
						//    //this get all the files that has this given type 
						//    Get.Ls(this.Target,this.SubTarget,true);
						//    return;
						//}
						//else
						//{
						//    if (Directory.Exists(this.Target))
						//    {
						//        Get.Ls(this.Target);
						//        return;
						//    }

						//}
					});
					break;
				case "sleep":
					runner.Run(() => {
						int number;
						if (Get.IsNumber(type))
						{
							Get.Yellow($"Sleepying... [{type}ms]");
							number = int.Parse(type);
							Thread.Sleep(number);
							return;
						}
						else
						{
							error.DisplayError(ErrorType.NotValidParameter, "The Value Must Be a number");
						}
					});
					break;
				case "cd":
					runner.Run(() => {

						//Get.Yellow($"{this.Target}     ClearTarget: {this.SubTarget}");
						// Print.List(Environment.GetLogicalDrives());
						//   Get.Wait();
						//  Get.Cyan(ShellLoop.CurrentPath);
						//Get.Wait(type.ToUpper());
						//Get.Wait(new ShellLoop().ReferToDisk(type.ToUpper()));
						path = null;

						if (Helper.ReferToDisk(type.ToUpper()))
						{
							Shell.CurrentPath = type;
							return;
						}
						if (Helper.HasSpecialFolder(type) != null)
						{
							Shell.CurrentPath = Helper.HasSpecialFolder(type);
							return;
						}

						if (type.Contains(".") && type.Length <= 3)
						{
							path = $"{Shell.CurrentPath}{Get.Slash()}../";
							Shell.CurrentPath = Helper.RemoveDirs(path, Helper.CountDirs(path));
							return;
						}
						else
						{
							path = $"{Shell.CurrentPath}{Get.Slash()}{type}";
							if (type == "/" || type == "\\")
							{
								switch (Get.IsWindow())
								{
									case true:
										Shell.CurrentPath = $"{Directory.GetDirectoryRoot(Get.Slash())}";
										break;
									case false:
										Shell.CurrentPath = type;
										break;
								}
								return;
							}
							if (Directory.Exists(path))
							{
								path = Shell.CurrentPath[Shell.CurrentPath.Length-1]!=Get.Slash()[0] ? $"{Get.Slash()}{type}" : $"{type}";
								Shell.CurrentPath += path;
								return;
							}else
							{
								error.DisplayError(ErrorType.NotValidParameter, $"I'M sorry but the path {path} does not exist!!!");
							}

						}

					});
					break;
				case "cat":
					runner.Run(() => {

					file = null;

					if (Helper.HasSpecialFolder(type) != null)
					{
						file = Helper.HasSpecialFolder(type);
					}
					if (file == null)
					{
						file = this.GetPathWithType(type);
					}
					if (!File.Exists(file))
					{
						Get.Red($"The file {file} was not found!!!");
						return;
					}
					else
					{
						try
						{
							//Get.Wait("Reading...",() => {
					        
							byte[] bytes = Binary.Reader(file);
							string text = IConvert.ToString(bytes);
							
							Shell.VStack.SetVariable(new Variable() 
							{
								Name = $"cat{Shell.VStack.GetIndex()}",
								Value = text,
								Buffer = bytes,
								IsConstant = false,
								IsEmpty = false
							});
							Get.Write(text);
								
								////});//
							}
							catch (Exception ex)
							{
								Get.Red($"There was an error while reading the file more info: \n{ex.Message}");
							}
						}

					});
					break;
				case "size":
				case "du":
					runner.Run(() =>
					{



						file =  type;
						if (!this.IsRootPath(file))
						{
							file = this.GetPathWithType(file);
						}

						long size = 0;

						if (Directory.Exists(file))
						{
							FilesMaper maper = new FilesMaper(file);
							maper.Map();

							foreach (string f in maper.Files)
							{

								if (maper.Files.Count < 100)
								{
									Get.Print(f, Get.FileSize(f));
								}
								if (maper.Files.Count > 100)
								{
									Get.FileSize(f);
								}
								size += Get.LongNumber;
							}

							Get.White($"Total Size of {type}: {Get.FileSize(size)}");
							return;
						}
						if (!File.Exists(file))
						{
							Get.Red($"The {file} Was not found or does not exist");
							return;
						}

						Get.Print(type, Get.FileSize(file));
					});
					break;
				case "select":
				case "-S":
					runner.Run(() => {
						//Get.Wait(type);
						if (type[0] == '*')
						{
							string[] files = new FilesMaper().GetFiles(type);
							List<string> withExt = new List<string>();
							foreach (string f in files)
							{
								if (Get.FileExention(f) == Get.FileExention(type))
								{
									withExt.Add(f);
								}
							}
							if (withExt != null || withExt.Count > 0)
							{
								files = IConvert.ToType<string>.ToArray(withExt);
							}
							//Print.List(withExt);
							//Get.Wait();
							Options option = new Options(files);
							option.Label = type;
							option.SelectorL = "> ";
							option.SelectorR = "";
							int selection = option.Pick();
							Shell.SelectedObject = $"{type}{Get.Slash()}{Get.FileNameFromPath(files[selection])}";
							return;
						}
						// C: \Users\William\Desktop\~\Desktop\Q.dll
					});
					break;
				case "get-hash":
				case "hash":
					runner.Run(() => {
						Get.Yellow($"Target: {Helper.HasSpecialFolder(type)}");
						if(File.Exists(Helper.HasSpecialFolder(type)))
						{
							type = Helper.HasSpecialFolder(type);
						}
						if(File.Exists(this.GetPathWithType(type)))
						{
							type = this.GetPathWithType(type);
						}
						if (File.Exists(type))
						{
							byte[] bytes = Binary.Reader(type);
							Get.Print($"File: {Get.FileNameFromPath(type)}", $"Hash: {Get.HashCode(bytes)}");
							return;
						}
						else
						{
							Get.Red($"File Not Found: {type}");
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

						cmd.StartInfo.FileName = $"editors/vim/vim.exe";//"cmd.exe";
																				  //cmd.StartInfo.Arguments;
																				  //cmd.StartInfo.RedirectStandardInput = true;
						cmd.StartInfo.RedirectStandardOutput = false;  // true;
						cmd.StartInfo.CreateNoWindow = false;
						cmd.StartInfo.UseShellExecute = false;
						cmd.StartInfo.Arguments = type; //"ping www.google.com"; //Helper.ResolvePath(this).Target;

						cmd.Start();
						cmd.WaitForExit();
					});
					break;
				case "nano":
					runner.Run(() => {
						Process cmd = new Process();

						cmd.StartInfo.FileName = $"editors/nano/nano.exe";//"cmd.exe";
																					//cmd.StartInfo.Arguments;
																					//cmd.StartInfo.RedirectStandardInput = true;
						cmd.StartInfo.RedirectStandardOutput = false;  // true;
						cmd.StartInfo.CreateNoWindow = false;
						cmd.StartInfo.UseShellExecute = false;
						cmd.StartInfo.Arguments = type; //Helper.ResolvePath(this).Target;

						cmd.Start();
						cmd.WaitForExit();
					});
					break;
				case "xxd":
					runner.Run(() => {
						Process cmd = new Process();

						cmd.StartInfo.FileName = $"editors/vim/xxd.exe";//"cmd.exe";
																				  //cmd.StartInfo.Arguments;
																				  //cmd.StartInfo.RedirectStandardInput = true;
						cmd.StartInfo.RedirectStandardOutput = false;  // true;
						cmd.StartInfo.CreateNoWindow = false;
						cmd.StartInfo.UseShellExecute = false;
						cmd.StartInfo.Arguments = type; //"ping www.google.com"; //Helper.ResolvePath(this).Target;

						cmd.Start();
						cmd.WaitForExit();
					});
					break;
				case "edit":
				case "notepad":
					runner.Run(() => {

						Process cmd = new Process();
						if (Get.IsWindow())
						{
							cmd.StartInfo.FileName = "notepad";
							cmd.StartInfo.Arguments = type;

						}
						if (!Get.IsWindow())
						{
							cmd.StartInfo.FileName = "open";
							cmd.StartInfo.Arguments = type;
						}

						//cmd.StartInfo.Arguments;
						cmd.StartInfo.RedirectStandardInput = true;
						cmd.StartInfo.RedirectStandardOutput = true;
						cmd.StartInfo.CreateNoWindow = true;
						cmd.StartInfo.UseShellExecute = false;


						//Get.Yellow($"Target: {this.Target} SubTarget: {this.SubTarget}");
						//CodeParser parser = 
						//Get.Wait($"Target: {parser.Target} SubTarget: {parser.SubTarget}");
						cmd.Start();

						/* execute "dir" */
						//cmd.StandardInput.WriteLine();
						cmd.StandardInput.Flush();
						cmd.StandardInput.Close();
						Console.WriteLine(cmd.StandardOutput.ReadToEnd());
					});
					break;
				default:
					ShellTrace.AddTrace($"Action Was not Recognized as a valid Action");
					error.DisplayError(ErrorType.NotValidAction, $"At: Execution Action With Type '{action}' {type} Trace: \n{ShellTrace.GetTrace()}");
                    break;
            }
        } 
    }
}
