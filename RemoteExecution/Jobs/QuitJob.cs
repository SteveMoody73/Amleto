using System;
using System.Collections.Generic;

namespace RemoteExecution.Jobs
{
    [Serializable]
    public class QuitJob : Job
    {
    	public override void ExecuteJob(MessageBack messageBack, Queue<Job> jobs)
        {
            messageBack(0, "Controller asked to quit.");
            ClientServices.Shutdown();
            Environment.Exit(0);
        }
    }
}
