using QuickTools.QCore;
using States;
using System.Diagnostics;
using ScriptRunner;
using System.IO;
using System.Collections.Generic;

namespace Parser
{
	public partial class CodeParser
	{
		/// <summary>
		/// Collects all the data related with the code that is running and returns it in an string format
		/// </summary>
		/// <returns></returns>
		public override string ToString() => this.Code.Length > 0 ? " Code: "+IConvert.ArrayToText(this.Code) : " Code: ";
			 
		
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

        private string _ = Shell.CurrentPath[Shell.CurrentPath.Length - 1] != Get.Slash()[0] ? Get.Slash() : "";
        /// <summary>
        /// Binds pathA with the pathB.
        /// </summary>
        /// <returns>The with path.</returns>
        /// <param name="pathA">Path a.</param>
        /// <param name="pathB">Path b.</param>
        public string BindWithPath(string pathA , string pathB) => $"{pathA}{_}{pathB}";
         

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
			//if(code.Length <= 3){
			//	return code;
			//}
			List<string> text = new List<string>();
			bool isOpen, isNext;
			isOpen = false;
			isNext = false;
			 
			string temp = "";
			int index, start;
		


			/*
                this is not an string formatter this is more like an array 
                unifier this joings the arrays of strings into one if it finds 
                that they have an open and closing qoutes 
             */
			index = 0;
			start = 0; 
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
					//text.Add(temp.Replace('"'.ToString(),""));
					temp ="";
					isOpen = false;
					isNext = true;
				}
				if (cmd.Contains('"'.ToString()) && isNext == false)
				{
					isOpen = true;
					start = index; 
				}
				if (isOpen)
				{
					temp+=cmd+" ";
				}

				isNext = false;
				index++; 
			}


			//Print.List(text);
			//Get.Wait(); 
			//text[start] = text[start].Replace('"'.ToString(), "");
			return text.ToArray();

			//return new int[] {-1,-1};
		}

		/// <summary>
		/// fix the given string format
		/// </summary>
		public void FixStringFormat() => this.Code = this.FormatStrings(this.Code);

		/// <summary>
		/// Checks if the variable is illigal and if is illigal it is an 
		/// internal variable
		/// </summary>
		/// <param name="variable"></param>
		/// <returns></returns>
		public bool IsInternalVariable(string variable) => this.IsIlligalVariableName(variable) == true ? true : false;
		/// <summary>
		/// Checks if is listed an Illigal kind of variable name
		/// </summary>
		/// <param name="variableName"></param>
		/// <returns></returns>
		bool IsIlligalVariableName(string variableName)	
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

		/// <summary>
		/// Gets the itmes of the array from the 3 item up like: 
		/// array = {1,2,3,4,5} 
		/// it will return array = {3,4,5}
		/// </summary>
		/// <param name="parameters"></param>
		/// <returns></returns>
		private string[] GetParameters(string[] parameters)
		{
			string[] param;
			int current, goal;
			goal = parameters.Length;
			string cmds = "";
			for (current = 2; current < goal; current++)
			{
				cmds +=  Get.FixPath(parameters[current])+" ";
			}
			param = IConvert.TextToArray(cmds);
			return param;
		}
	}
}
