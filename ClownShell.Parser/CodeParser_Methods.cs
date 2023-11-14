using QuickTools.QCore;
using States;
using System.Diagnostics;
using ScriptRunner;
using System.IO;
using System.Collections.Generic;

using QuickTools.QCore; 
namespace Parser
{
	public partial class CodeParser
	{
		/// <summary>
		/// Collects all the data related with the code that is running and returns it in an string format
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			string code = this.Code.Length > 0 ? IConvert.ArrayToText(this.Code) : null;
			return $" Code: {code}";
		}
		/// <summary>
		/// adds Quotes to the string around the string given without braking the string format
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public string ToQoutesString(string input) => $"'{input}'".Replace("'", '"'.ToString());


		/// <summary>
		/// gets the <see cref="States.Shell.CurrentPath"/>and returns it with the given type without braking the path
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public string GetPathWithType(string type) => Shell.CurrentPath[Shell.CurrentPath.Length-1]==Get.Slash()[0] ? $"{Shell.CurrentPath}{type}" : $"{Shell.CurrentPath}{Get.Slash()}{type}";

		/// <summary>
		/// Returns either the given path contains a file
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public bool HasExecutable(string path)
		{

			for (int ch = path.Length-1; ch > 0; ch--)
			{
				if (path[ch] == '/' || path[ch] == '\\')
				{
					if (path[ch -1] == '.')
					{
						return true;
					}
				}
			}
			return false;
		}

		/// <summary>
		/// Run the given process but is more like run the program given as code
		/// </summary>
		/// <param name="code"></param>
		public void RunProcess(string code)
		{
			Runner runner = new Runner();
			Process cmd = new Process();
			runner.Run(() => {

				cmd.StartInfo.FileName = $"{code}";//"cmd.exe";
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
				return;
			});

		}

		/// <summary>
		/// Retuns either if is a root path or not without throwing an exeption
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public bool IsRootPath(string path)
		{
			try
			{
				return Path.IsPathRooted(path);
			}
			catch
			{
				return false;
			}
		}

		/// <summary>
		/// this is not an string formatter this is more like an array
		/// unifier this joings the arrays of strings into one if it finds
		/// that they have an open and closing qoutes
		/// </summary>
		/// <param name="code"></param>
		/// <returns></returns>
		public string[] FormatStrings(string[] code)
		{
			List<string> text = new List<string>();
			bool isOpen, isNext;
			isOpen = false;
			isNext = false;
			string temp = "";
			/*
                this is not an string formatter this is more like an array 
                unifier this joings the arrays of strings into one if it finds 
                that they have an open and closing qoutes 
             */
			foreach (string cmd in code)
			{

				if (!isOpen && !cmd.Contains('"'.ToString()))
				{
					text.Add(cmd);
				}
				if (isOpen && cmd.Contains('"'.ToString()))
				{
					temp+=cmd+" ";
					text.Add(temp);
					temp ="";
					isOpen = false;
					isNext = true;
				}
				if (cmd.Contains('"'.ToString()) && isNext == false)
				{
					isOpen = true;
				}
				if (isOpen)
				{
					temp+=cmd+" ";
				}

				isNext = false;
			}


			//Print.List(text);
			//Get.Wait(); 
			return text.ToArray();

			//return new int[] {-1,-1};
		}

		/// <summary>
		/// fix the given string format
		/// </summary>
		public void FixStringFormat() => this.Code = this.FormatStrings(this.Code);


		public bool IsInternalVariable(string variable) => this.IsIlligalVariableName(variable) == true ? true : false;
		public bool IsIlligalVariableName(string variableName)	
		{
			switch(variableName)
			{
				case "var":
				case "cat":
				case "read":
				case "echo":
				case "shell-path":
					return true; 
				default:
				return false;	
			}
		}

	}
}
