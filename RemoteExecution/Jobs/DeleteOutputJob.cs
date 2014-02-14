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
                logger.ErrorException("Unable to delete output directory", ex);
            }
            
			try
			{
				Directory.CreateDirectory(baseDir);
			} 
			catch (Exception ex)
			{
                logger.ErrorException("Unable to create output directory", ex);
            }
        }
    }
}
