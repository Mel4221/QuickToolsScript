using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace QuickToolsScript
{
    public class ScriptRunner
    {

        public Thread ScriptRunnerThread;

        public void Run(Action code,bool runInBackGround)
        {
            if (runInBackGround)
            {
                try
                {
                    ScriptRunnerThread = new Thread(() => { code(); });
                    ScriptRunnerThread.Start(); 
                }
                catch (Exception error)
                {
                    new ErrorHandeler().DisplayError(ErrorHandeler.ErrorType.ExeutionError, error.ToString());
                }
                return;
            }
            else
            {
                Run(code);
                return;
            }
        }

        public void Run(Action code)
        {
            try
            {
                code();
            }catch(Exception error)
            {
                new ErrorHandeler().DisplayError(ErrorHandeler.ErrorType.ExeutionError, error.ToString()); 
            }
        }
    }
}
