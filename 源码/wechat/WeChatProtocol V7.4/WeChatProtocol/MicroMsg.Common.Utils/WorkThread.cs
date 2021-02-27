namespace MicroMsg.Common.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    public class WorkThread
    {
        private List<JobParam> jobList = new List<JobParam>();
        private Thread thread = new Thread(new ParameterizedThreadStart(WorkThread.WorkThreadLoop));
        private AutoResetEvent workEvent = new AutoResetEvent(false);

        public WorkThread()
        {
            thread.Name = "backGroundWorkThread";
            thread.IsBackground = true;
            thread.Start(this);
        }

        public bool add_job(Action doJobAction)
        {
            if (doJobAction == null)
            {
                return false;
            }
            JobParam item = new JobParam
            {
                doJob = doJobAction
            };
            lock (this.jobList)
            {
                this.jobList.Add(item);
            }
            this.workEvent.Set();
            return true;
        }

        private JobParam getJob()
        {
            JobParam param;
            lock (this.jobList)
            {
                if (this.jobList.Count <= 0)
                {
                    return null;
                }
                param = this.jobList[0];
                this.jobList.RemoveRange(0, 1);
            }
            return param;
        }

        private static void WorkThreadLoop(object parameter)
        {
            WorkThread thread = parameter as WorkThread;
            while (true)
            {
                JobParam param;
                thread.workEvent.WaitOne();
                while ((param = thread.getJob()) != null)
                {
                    param.doJob();
                }
            }
        }

        public class JobParam
        {
            public object arg1;
            public object arg2;
            public Action doJob;
        }
    }
}

