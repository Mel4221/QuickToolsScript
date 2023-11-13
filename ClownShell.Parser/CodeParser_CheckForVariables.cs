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
				if (type.Contains("*"))
				{
					if (type[0] == '*' && type[1] != '.')
					{
						Variable v = Shell.VStack.GetVariable(type.Substring(1));
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
				for (int word = 0; word < parameters.Length; word++)
				{
					if (parameters[word].Contains("*"))
					{
						if (parameters[word][0] == '*' && parameters[word][1] != '.')
						{
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
