namespace Parser.Types
{
	 public partial class CodeTypes
	{
		private string Action { get; set; } = null;
		private string Type { get; set; } = null;
		private string[] Parameters { get; set; } = new string[0];
		public CodeTypes(string action)
		{
			this.Action = action;	
		}
		public CodeTypes(string action, string type)
		{
			this.Action = action;
			this.Type = type;
		}
		public CodeTypes(string action,string type,string[] parameters) 
		{
			this.Action=action;	
			this.Type=type;
			this.Parameters = parameters;
		}	
	}

}
