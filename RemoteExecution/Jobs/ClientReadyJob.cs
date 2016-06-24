using System;
using System.Collections.Generic;

namespace RemoteExecution.Jobs
{
    [Serializable]
    public class ClientReadyJob : Job
    {
    	public override void ExecuteJob(MessageBack messageBack, Queue<Job> jobs)
        {
            messageBack(0,"Batch finished...");
            Server.SetReady();
        }
    }
}
