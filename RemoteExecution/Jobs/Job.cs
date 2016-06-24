using System;
using System.Collections.Generic;

namespace RemoteExecution.Jobs
{
    [Serializable]
    public abstract class Job
    {
        [NonSerialized]
        protected ServerServices Server;
        private static int _nextJobId;
        private int _jobId = (_nextJobId++);
        
		public delegate void MessageBack(int icon,string msg);
        public ClientConnection AssignedTo;

        public void SetServer(ServerServices server)
        {
            Server = server;
        }

        public int JobId
        {
            get { return _jobId; }
        }

        public abstract void ExecuteJob(MessageBack messageBack, Queue<Job> jobs);
    }
}
