using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using NLog;

namespace RemoteExecution.Jobs
{
    [Serializable]
    public class DeleteOutputJob : Job
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

    	public override void ExecuteJob(MessageBack messageBack, Queue<Job> jobs)
        {
            string baseDir = Path.Combine(ClientServices.GetClientDir(), "Output");

            try
            {
            	Directory.Delete(baseDir,true);
            } 
			catch (Exception ex)
			{
                logger.Error(ex, "Unable to delete output directory");
            }
            
			try
			{
				Directory.CreateDirectory(baseDir);
			} 
			catch (Exception ex)
			{
                logger.Error(ex, "Unable to create output directory");
            }
        }
    }
}
