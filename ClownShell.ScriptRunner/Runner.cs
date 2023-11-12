using System;
using System.Threading;
using ErrorHandelers; 
namespace ScriptRunner
{
	
	public partial class Runner
    {
        public void Run(Action code)
        {
            try 
            {
                code();
            }catch (Exception ex)
            {
                ErrorHandeler error = new ErrorHandeler();
                error.DisplayError(ErrorType.ExecutionError, $"{ex}"); 
            }
        }




        public Runner() { }
    }
}
