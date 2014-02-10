using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace RemoteExecution.Jobs
{
	[Serializable]
    public class DeleteContentJob : Job
	{
		public override void ExecuteJob(MessageBack messageBack, Queue<Job> jobs)
        {
            string baseDir = Path.Combine(ClientServices.GetClientDir(), "Content");
            
			try
			{
				Directory.Delete(baseDir, true);
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
