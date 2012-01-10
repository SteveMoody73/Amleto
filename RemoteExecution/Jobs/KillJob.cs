using System;
using System.Collections.Generic;


namespace RemoteExecution.Jobs
{
    [Serializable]
    public class KillJob : Job
    {
    	public override void ExecuteJob(MessageBack messageBack, Queue<Job> jobs)
        {
            messageBack(0,"Should kill render process");
            ClientServices.KillRenderProcess();
        }
    }
}
