using System;
using System.Threading;
using ErrorHandelers; 
namespace ScriptRunner
{
	
	public partial class Runner
    {
		private ErrorHandeler error = new ErrorHandeler();
		public void Run(Action code)
        {
            try 
            {
                code();
            }catch (Exception ex)
            {
                error.DisplayError(ErrorType.ExecutionError, $"{ex}"); 
            }
        }
        public void Run(Action code,bool backGround)
        {
            try
            {
				Job job = new Job();
				job.JobAction = code;
				job.Info = this.RunningCodeInfo;
				job.Name = this.RunningBackGroundCodeName;
				BackGroundJob.AddJob(job);
				BackGroundJob.RunJobs();
			}
			catch (Exception ex) 
            {
                error.DisplayError(ErrorType.ExecutionError, $"There was an error while running a background Job \n{ex}");
            }
        }

        public Runner() { }
    }
}
