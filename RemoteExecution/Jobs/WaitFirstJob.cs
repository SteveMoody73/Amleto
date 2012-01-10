using System;
using System.Collections.Generic;

namespace RemoteExecution.Jobs
{
    [Serializable]
    public class WaitFirstJob : Job
    {
    	public override void ExecuteJob(MessageBack messageBack, Queue<Job> jobs)
        {
            messageBack(0,"Waiting for first node to be ready...");

            while (Server.IsFirstClientReady() == false)
            {
                if (ClientServices.IsRunning == false)
                {
                    messageBack(0,"Need to quit.");
                    return;
                }
                System.Threading.Thread.Sleep(100);
            }

            messageBack(0,"Done, let's continue then.");
        }
    }
}
