
//
// ${Melquiceded Balbi Villanueva}
//
// Author:
//       ${Melquiceded} <${melquiceded.balbi@gmail.com}>
//
// Copyright (c) ${2089} MIT
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using QuickTools.QCore;
using ClownShell.BackGroundFunctions;
using ClownShell.Init;
using ClownShell.ErrorHandler;
using ClownShell.Parser;
using QuickTools.QIO;

namespace ClownShell.ScripRunner
{

    public partial class ScriptRunner
    {


        private void ThrowError(Exception error)
        {
            string cmd = this.RunningCode.Length == 0 ? "not-available" : IConvert.ArrayToText(this.RunningCode);//*/ : "not-available"; /*"not-available";*/
            new ErrorHandeler().DisplayError(ErrorType.ExecutionError, $"Code Executed: <( {cmd} )> \n Exception: \n{error}");
        }
        private void Reset()
        {
            Console.Title = Program.Name;
            this.RunningCode = new string[0];  
            this.RunningCodeInfo = string.Empty;
        }

        /// <summary>
        /// Run the given action on the background
        /// </summary>
        /// <param name="code"></param>
        /// <param name="runInBackGround"></param>
        public void Run(Action code, bool runInBackGround)
        {
            if (runInBackGround)
            {
                Job job = new Job();
                job.JobAction = code;
                job.Info = this.RunningCodeInfo;
                job.Name = this.RunningBackGroundCodeName; 
                //Jobs.JobAction = code;
                //Jobs.JobInfo = "BackGroundJob";

                try
                {
                    BackGroundJob.AddJob(job);
                    BackGroundJob.RunJobs();
                }
                catch (Exception error)
                {
                    ThrowError(error);
                    Reset(); 
                }
                return;
            }
            if (!runInBackGround)

            {
                Run(code);
                return;
            }
        }


        //Task RunCode;
        //Task CancellThread; 
        public void Run(Action code)
        {

            try
            {
                if (AllowToCancell)
                {


                    CurrentScript = new Thread(() =>
                    {
                        code();
                        //Get.White("\n Task Completed Press any key to continue");

                        //Thread.Sleep(100);
                        if (Loop.IsAlive)
                        {
                            Loop.Abort();
                        }
                    });
                    Loop = new Thread(() =>
                    {
                        Get.Title("Press Esc  to Cancel");
                        var key = Console.ReadKey();
                        
                        if (key.KeyChar == 'Z' && key.Modifiers.HasFlag(ConsoleModifiers.Control) || key.Key.ToString() == "Escape") // key.Key.ToString() == "Escape")
                        {
                            if (CurrentScript.IsAlive)
                            {
                                CurrentScript.Abort();
                            }
                        }

                    });

                    Loop.Start();
                    CurrentScript.Start();


                    while (Loop.IsAlive) { }
                    GC.Collect();
                    Reset(); 
                    return;
                }
                else
                {
                    code();
                    return;
                }

            }catch(ThreadAbortException ex)
            {
                //Get.Yellow("Job Killed");
                Log.Event("ExptectedErros", ex.Message);
                Reset();
                return; 
            }catch (Exception error)
            {
                ThrowError(error);
                Reset();
            }
        }

        public ScriptRunner()
        {
            //Parser = new CodeParser();
        }
     
    }
}
