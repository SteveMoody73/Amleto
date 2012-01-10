using System;
using System.Collections.Generic;


namespace RemoteExecution.Jobs
{
    [Serializable]
    public class SendLogJob : Job
    {
        public delegate void LogReceiver(List<string> log);
        private event LogReceiver ReceiverLog;

        public SendLogJob(LogReceiver receiver)
        {
            ReceiverLog += receiver;
        }

        public override void ExecuteJob(MessageBack messageBack, Queue<Job> jobs)
        {
            messageBack(0,"Sending log back to server");
            ReceiverLog(ClientServices.OldMessages);
        }
    }
}
