using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections;

namespace ClownShell
{
    public class BackGroundJob
    {
        private Thread BThread;
        public string JobName;
        private int count;
        public int ID;
        public Action JobAction;
        public bool AllowToBeKilled = true;
        public string TextStatus;
        public double Status;
        public string JobInfo;
        public List<BackGroundJob> Jobs; 



        public override string ToString()
        {
            return $"ID: {this.ID} Job Name: {this.JobName} Status: {this.Status} Job Info: {this.JobInfo} AlowToBeKilled: {this.AllowToBeKilled}";
        }
        public void AddJob(BackGroundJob job)
        {
            count++;
            Jobs.Add(new BackGroundJob()
            {
                JobName = job.JobName,
                ID = count,
                JobAction = job.JobAction,
                AllowToBeKilled = job.AllowToBeKilled,
                TextStatus = job.TextStatus,
                Status = job.Status,
                JobInfo = job.JobInfo
            });

        }
        public void Run(int id)
        {
            if (this.Jobs != null)
            {
                if (!this.Jobs[id].BThread.IsAlive)
                {
                    this.Jobs[id].BThread = new Thread(() => {
                        this.Jobs[id].JobAction();
                    });
                    this.Jobs[id].BThread.Start();
                }
            }
        }

        public void KillAll()
        {
            if (this.Jobs != null)
            {
                Jobs.ForEach((item) => {
                    if (!item.AllowToBeKilled)
                    {
                        throw new Exception("Item Not Allwed To be Killed");
                    }
                    if (item.BThread.IsAlive)
                    {
                        item.BThread.Abort();
                    }
                });
            }
        }
        public void Kill(int id)
        {
            if (this.Jobs != null)
            {
                for (int current = 0; current < this.Jobs.Count; current++)
                {
                    if (this.Jobs[current].ID == id)
                    {
                        if (!this.Jobs[current].AllowToBeKilled)
                        {
                            throw new Exception("Item Not Allwed To be Killed");
                        }


                        if (this.Jobs[current].BThread.IsAlive)
                        {
                            this.Jobs[current].BThread.Abort();
                        }
                    }
                }
            }
        }

        public void Pause(int id)
        {
            if (this.Jobs != null)
            {
                for (int current = 0; current < this.Jobs.Count; current++)
                {
                    if (this.Jobs[current].ID == id)
                    {
                        if (this.Jobs[current].BThread.IsAlive)
                        {
                            this.Jobs[current].BThread.Suspend();
                        }
                    }
                }
            }
        }
        public void Resume(int id)
        {
            if (this.Jobs != null)
            {
                for (int current = 0; current < this.Jobs.Count; current++)
                {
                    if (this.Jobs[current].ID == id)
                    {
                        if (this.Jobs[current].BThread.IsAlive)
                        {
                            this.Jobs[current].BThread.Resume();
                        }
                    }
                }
            }
        }
        public void RunJobs()
        {
            if (this.Jobs != null)
            {
                this.Jobs.ForEach((item) => {
                    if (item.JobAction != null)
                    {
                        item.BThread = new Thread(() =>
                        {
                            item.JobAction();
                        });
                        item.BThread.Start();
                    }
                });
            }
        }

        public BackGroundJob()
        {
            this.Jobs = new List<BackGroundJob>();
        }

    }


}
