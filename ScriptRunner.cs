
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

namespace QuickToolsScript
{

    public class ScriptRunner
    {
        private static BackGroundJob Job = new BackGroundJob();
        private static Thread Loop;
        public  static Thread CurrentScript;
        public  bool AllowToCancell; 
        public void Run(Action code,bool runInBackGround)
        {
            if (runInBackGround)
            {
                BackGroundJob job = new BackGroundJob();
                job.JobAction = code;
                job.JobInfo = "Testing this new class";
                try
                {
                    Job.AddJob(job);
                    Job.RunJobs();
                }
                catch (Exception error)
                {
                    new ErrorHandeler().DisplayError(ErrorHandeler.ErrorType.ExeutionError, error.ToString());
                }
                return;
            }if(!runInBackGround)
             
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
                if (this.AllowToCancell)
                {


                    CurrentScript = new Thread(() =>
                    {
                        code();
                        Get.White("\n Task Completed Press any key to continue");

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

                            //if(this.RunCode != null)
                            //{

                            //}
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
                    Console.Title = Program.Name; 
                    return;
                }
                else
                {
                    code();
                    return;
                }

            }catch(Exception error)
            {
                new ErrorHandeler().DisplayError(ErrorHandeler.ErrorType.ExeutionError, error.ToString()); 
            }
        }
    }
}
