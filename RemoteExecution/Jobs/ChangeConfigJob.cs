using System;
using System.Collections.Generic;

namespace RemoteExecution.Jobs
{
    [Serializable]
	public class ChangeConfigJob : Job
    {
        private string _configName;

		/// <summary>
		/// Sends a new configuration to the client
		/// </summary>
		/// <param name="configName"></param>
        public ChangeConfigJob(string configName)
        {
            _configName = configName;
        }

		/// <summary>
		/// Executes the Configuration task
		/// </summary>
		/// <param name="messageBack">Message sent back to the server</param>
		/// <param name="jobs">List of Jobs</param>
        public override void ExecuteJob(MessageBack messageBack, Queue<Job> jobs)
        {
            messageBack(0, "Set config to: " + _configName);
            Server.SetCurrentJob("Setting up new config");
            ClientServices.ConfigName = _configName;
            ClientServices.ReDoSetup();
        }
    }
}
