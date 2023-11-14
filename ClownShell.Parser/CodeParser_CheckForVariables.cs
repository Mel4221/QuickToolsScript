using ErrorHandelers;
using QuickTools.QData;
using States;
using System.Collections.Generic;



namespace Parser
{
	public partial class CodeParser
	{
		/// <summary>
		/// This checks for the type to make sure if there could be a variable
		/// with that value if there is not a varialbe with that value it returns it back
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public string CheckTypeForVariables(string type)
		{
			try
			{
				string variable = type.Substring(1);
				if (type.Contains("*") || type.Contains("$"))
				{
					if (type[0] == '*' || type[0] == '$' && type[1] != '.')
					{
						if(this.IsInternalVariable(variable))
						{
							switch(variable) 
							{
								case "shell-path":
									return Shell.CurrentPath;
								default:
									return null;
							}
						}
						Variable v = Shell.VStack.GetVariable(variable);
						if (!v.IsEmpty) return v.Value;
					}
				}
				return type;
			}catch{
				return type; 
			}
		}

		/// <summary>
		/// Checks the parameters to find if there could be a variable inside of it
		/// and replace it with it's value 
		/// </summary>
		/// <param name="parameters"></param>
		/// <returns></returns>
		public string[] CheckParamForVariables(string[] parameters)
		{
			try
			{
				string variable;
				for (int word = 0; word < parameters.Length; word++)
				{
					variable = parameters[word].Substring(1);
					if (parameters[word].Contains("*") || parameters[word].Contains("$"))
					{
						if (parameters[word][0] == '*' || parameters[word][0] == '$' && parameters[word][1] != '.')
						{
							if (this.IsInternalVariable(variable))
							{
								switch (variable)
								{
									case "shell-path":
										parameters[word] = Shell.CurrentPath;
										continue;
									default:
										continue;
								}
							}
							Variable v = Shell.VStack.GetVariable(parameters[word].Substring(1));
		
							switch (v.IsEmpty)
							{
								case false:
									parameters[word] = v.Value;
									break;
							}

						}
					}
				}
				return parameters;
			}catch{
				return parameters;
			}
		}
	}
}
