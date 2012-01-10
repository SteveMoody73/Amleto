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
            string baseDir = "" + Directory.GetParent(ClientServices.ClientDir + "\\Content\\");
            
			try
			{
				Directory.Delete(baseDir, true);
			}
            catch (Exception ex)
			{
				Debug.WriteLine("Deleting Directory: " + ex.ToString());
			}

            try
            {
            	Directory.CreateDirectory(baseDir);
            }
            catch (Exception ex)
            {
				Debug.WriteLine("Creating Directory: " + ex.ToString());
            }

        }
    }
}
