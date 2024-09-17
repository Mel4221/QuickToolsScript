﻿using System;
using System.Collections.Generic;
using QuickTools.QCore;
using System.Threading;
using QuickTools.QIO;
//using Settings;


namespace States
{
    /// <summary>
    /// Defienes all the properties for a background Job.
    /// </summary>
	public class Job
		{
            /// <summary>
            /// Gets or sets the BackGround thread to execute the given Job.
            /// </summary>
            /// <value>The BT hread.</value>
			public Thread BThread { get; set; }
            /// <summary>
            /// Gets or sets the name for the Job
            /// </summary>
            /// <value>The name.</value>
			public string Name { get; set; } = string.Empty;
            /// <summary>
            /// Gets or sets the identifier for the Job
            /// </summary>
            /// <value>The identifier.</value>
			public int ID { get; set; } = 0;
            /// <summary>
            /// Gets or sets the job action.
            /// </summary>
            /// <value>The job action.</value>
			public Action JobAction { get; set; }
            /// <summary>
            /// Gets or sets a value indicating whether this <see cref="T:States.Job"/> allow to be killed.
            /// </summary>
            /// <value><c>true</c> if allow to be killed; otherwise, <c>false</c>.</value>
			public bool AllowToBeKilled { get; set; } = true;
            /// <summary>
            /// Gets or sets the text status.
            /// </summary>
            /// <value>The text status.</value>
			public string TextStatus { get; set; } = string.Empty;
            /// <summary>
            /// Gets or sets the status for the Job.
            /// </summary>
            /// <value>The status.</value>
			public double Status { get; set; } = 0;
            /// <summary>
            /// Provides extra information for the Job.
            /// </summary>
			public string Info = string.Empty;
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:States.Job"/> is internal job.
        /// </summary>
        /// <value><c>true</c> if is internal job; otherwise, <c>false</c>.</value>
        public bool IsInternalJob { get; set; } = false; 
        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:States.Job"/>.
        /// </summary>
        /// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:States.Job"/>.</returns>
        public override string ToString()
			{
				return $"ID: [{this.ID}] Job Name: [{this.Name}] Status: [{this.Status}] Job Info: [{this.Info}] AlowToBeKilled: [{this.AllowToBeKilled}]";
			}
		}
        /// <summary>
        /// Back ground job.
        /// </summary>
		public static class BackGroundJob
		{
            /// <summary>
            /// Gets or sets the name of the back ground job log file.
            /// </summary>
            /// <value>The name of the back ground job log file.</value>
			public static string BackGroundJobLogFileName { get; set; } // = ShellSettings.LogsFile;
			/// <summary>
            /// Gets or sets a value indicating whether this <see cref="T:States.BackGroundJob"/> monitor started.
            /// </summary>
            /// <value><c>true</c> if monitor started; otherwise, <c>false</c>.</value>
            public static bool MonitorStarted { get; set; }
			private static Thread MonitorThread { get; set; }
			private static int Indexer { get; set; } = 0;
            static List<Job> Jobs { get; set; } = new List<Job>();
			/// <summary>
            /// Gets or sets a value indicating whether this <see cref="T:States.BackGroundJob"/> has jobs.
            /// </summary>
            /// <value><c>true</c> if has jobs; otherwise, <c>false</c>.</value>
        public static bool HasUserJobs()
        {
            for(int job = 0;  job < BackGroundJob.Jobs.Count; job++)
            {
                if(BackGroundJob.Jobs[job].IsInternalJob == false)
                {
                    return true; 
                }
            }
            return false;
        }
        /// <summary>
        /// Gets or sets the monitor thread speed.
        /// </summary>
        /// <value>The monitor thread speed.</value>
        public static int MonitorThreadSpeed { get; set; } = 500;


			private static void RemoveTerminatedJobs()
			{
				for (int job = 0; job < Jobs.Count; job++)
				{
					if (!Jobs[job].BThread.IsAlive)
					{
						Log.Event(BackGroundJobLogFileName, $"Job {Jobs[job].ToString()} Completed");
						Jobs.RemoveAt(job);

					}
				}
			}
			private static void WatchJobs()
			{
				while (Jobs.Count > 0)
				{
					RemoveTerminatedJobs();
                    //HasJobs = Jobs.Count == 0 ? false : true;
					Thread.Sleep(MonitorThreadSpeed);
                   // HasJobs = Jobs.Count == 0 ? false : true;
                }
                //HasJobs = Jobs.Count == 0 ? false : true;
            }

        /// <summary>
        /// Retourns the backgrounds Jobs in execution
        /// </summary>
        /// <returns>The count.</returns>
        public static int JobsCount() => Jobs.Count;
			/// <summary>
            /// Prints the running jobs.
            /// </summary>
            public static void PrintRunningJobs()
			{
				if (Jobs.Count == 0)
				{
					Get.White("No Background Jobs");
					return;
				}
				//Top Table
				Get.White();
				Get.Write($"[ID] ");
				Get.Green();
				Get.Write($"[Name] ");
				Get.White();
				Get.Write($"[Description] ");
				Get.Red();
				Get.Write($"[Killable] ");
				Get.Yellow();
				Get.Write($"[IsAlive] ");
                Get.Blue();
                Get.Write($"[IsInternal]\n");
				foreach (Job job in Jobs)
				{

					//Button Line
					Get.White();
					Get.Write($"[{job.ID}] ");
					Get.Green();
					Get.Write($"[{job.Name}] ");
					Get.White();
					Get.Write($"[{job.Info}] ");
					Get.Red();
					Get.Write($"[{job.AllowToBeKilled}] ");
					Get.Yellow();
					Get.Write($"[{job.BThread.IsAlive}] ");
                    Get.Blue();
                    Get.Write($"[{job.IsInternalJob}]\n");
				}
			}
            /// <summary>
            /// Gets the job info.
            /// </summary>
            /// <returns>The job info.</returns>
            /// <param name="id">Identifier.</param>
			public static string GetJobInfo(int id)
			{
				if (id > Jobs.Count || id < 0)
				{
					return string.Empty;
				}
				else
				{
					return Jobs[id].ToString();
				}

			}
            /// <summary>
            /// Adds the job.
            /// </summary>
            /// <param name="job">Job.</param>
			public static void AddJob(Job job)
			{


				Jobs.Add(new Job()
				{
					Name = job.Name,
					ID = job.ID == 0? Indexer:job.ID,
					JobAction = job.JobAction,
					AllowToBeKilled = job.AllowToBeKilled,
					TextStatus = job.TextStatus,
					Status = job.Status,
					Info = job.Info,
                    IsInternalJob = job.IsInternalJob

				});
				Log.Event(BackGroundJobLogFileName, $"Job Added {Jobs[Indexer].ToString()}");
				Indexer++;
            if (!job.IsInternalJob) Get.White($"BackGroundJob Added ID:[{Indexer}]");
			}
            /// <summary>
            /// Run the specified id.
            /// </summary>
            /// <param name="id">Identifier.</param>
			public static void Run(int id)
			{
				if (Jobs != null)
				{
					if (!Jobs[id].BThread.IsAlive)
					{
						Jobs[id].BThread = new Thread(() => {
							Jobs[id].JobAction();
						});
						Jobs[id].BThread.Start();
						Log.Event(BackGroundJobLogFileName, $"Job Manually Started {Jobs[id].ToString()}");

					}
				}
			}
            /// <summary>
            /// Kills all the backgrounds Jobs
            /// </summary>
			public static void KillAll()
			{
				if (Jobs != null)
				{
                try
                {

                    Jobs.ForEach((item) =>
                    {

                        if (!item.AllowToBeKilled)
                        {
                            throw new Exception("Item Not Allwed To be Killed");
                        }
                        if (item.BThread.IsAlive)
                        {
                            Log.Event(BackGroundJobLogFileName, $"KILL-ALL-REQUEST {item.ToString()}");

                            try
                            {
                                try
                                {

                                    item.BThread.Abort();
                                    RemoveTerminatedJobs();
                                }
                                catch
                                {

                                }
                            }
                            catch
                            {

                            }
                        }
                    });
                    Jobs.Clear();
                    Indexer = 0;
                }
                catch
                {

                }
            
            }
			}
            /// <summary>
            /// Kill the specified Job by its ID
            /// </summary>
            /// <param name="id">Identifier.</param>
			public static void Kill(int id)
			{
				if (Jobs != null)
				{
					for (int current = 0; current < Jobs.Count; current++)
					{
						if (Jobs[current].ID == id)
						{
							if (!Jobs[current].AllowToBeKilled)
							{
								throw new Exception("The job don't support to be killed");
							}


							if (Jobs[current].BThread.IsAlive)
							{
								string msg = $"Job Killed ID:[{Indexer}] Name: [{Jobs[current].Name}] Info: [{Jobs[current].Info}]";
								Log.Event(BackGroundJobLogFileName, msg);

								try
								{
									Jobs[current].BThread.Abort();
									Get.Red(msg);
									Jobs.RemoveAt(current);
								}
								catch (ThreadAbortException)
								{
									Get.Red(msg);
								}

							}
						}
					}

				}
			}
            /// <summary>
            /// Trys to Pause the specified Background Job it may not be stoped in all the cases
            /// </summary>
            /// <param name="id">Identifier.</param>
			public static void Pause(int id)
			{
				if (Jobs != null)
				{
					for (int current = 0; current < Jobs.Count; current++)
					{
						if (Jobs[current].ID == id)
						{
							if (Jobs[current].BThread.IsAlive)
							{
								Log.Event(BackGroundJobLogFileName, $"Job Suspended {Jobs[current].ToString()}");

#pragma warning disable CS0618 // Type or member is obsolete
								Jobs[current].BThread.Suspend();
#pragma warning restore CS0618 // Type or member is obsolete
							}
						}
					}
				}
			}

			/// <summary>
			/// Resume the background worker
			/// </summary>
			/// <param name="id"></param>
			public static void Resume(int id)
			{
				if (Jobs != null)
				{
					for (int current = 0; current < Jobs.Count; current++)
					{
						if (Jobs[current].ID == id)
						{
							if (Jobs[current].BThread.IsAlive)
							{
								Log.Event(BackGroundJobLogFileName, $"Job Resumed {Jobs[current].ToString()}");

#pragma warning disable CS0618 // Type or member is obsolete
								Jobs[current].BThread.Resume();
#pragma warning restore CS0618 // Type or member is obsolete
							}
						}
					}
				}
			}

			/// <summary>
			/// Run the background jobs
			/// </summary>
			public static void RunJobs()
			{

				if (Jobs != null)
				{
                    //WatchJobs();
					Jobs.ForEach((item) => {
						if (item.JobAction != null)
						{
							item.BThread = new Thread(() =>
							{
								item.JobAction();
							});
							item.BThread.Start();
						}
					});
					if (!MonitorStarted)
					{
						MonitorThread = new Thread(() => {
							WatchJobs();
						});
						MonitorThread.Start();
					}

				}
			}

		}
	}
