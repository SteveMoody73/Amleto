using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;

namespace RemoteExecution.Jobs
{
    [Serializable]
    public class DeleteOutputJob : Job
    {
    	public override void ExecuteJob(MessageBack messageBack, Queue<Job> jobs)
        {
            string baseDir = Path.Combine(ClientServices.GetClientDir(), "Output");

            try
            {
            	Directory.Delete(baseDir,true);
            } 
			catch (Exception ex)
			{
                Tracer.Exception(ex);
            }
            
			try
			{
				Directory.CreateDirectory(baseDir);
			} 
			catch (Exception ex)
			{
                Tracer.Exception(ex);
            }
        }
    }
}
