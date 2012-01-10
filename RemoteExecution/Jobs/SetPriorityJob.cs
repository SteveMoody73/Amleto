using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace RemoteExecution.Jobs
{
    [Serializable]
    public class SetPriorityJob : Job
    {
        ProcessPriorityClass _priority;

        public SetPriorityJob(ProcessPriorityClass priority)
        {
            _priority = priority;
        }

        public override void ExecuteJob(MessageBack messageBack, Queue<Job> jobs)
        {
            messageBack(0,"Changed priority: " + _priority);
            ClientServices.SetRenderProcessPriority(_priority);
        }
    }
}
