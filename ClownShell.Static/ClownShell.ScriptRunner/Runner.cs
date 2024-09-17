using System;
using ErrorHandelers;
using States;
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
                if (backGround)
                {
                    Job job = new Job();
                    job.JobAction = code;
                    job.Info = this.RunningCodeInfo;
                    job.Name = this.RunningBackGroundCodeName;
                    BackGroundJob.AddJob(job);
                    BackGroundJob.RunJobs();
                    return;
                }else{
                    this.Run(code); return;
                }
			}
			catch (Exception ex) 
            {
                error.DisplayError(ErrorType.ExecutionError, $"There was an error while running a background Job \n{ex}");
            }
        }

        public Runner() { }
    }
}
