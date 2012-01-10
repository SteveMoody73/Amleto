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
            string baseDir = "" + Directory.GetParent(ClientServices.ClientDir + "\\Output\\");

            try
            {
            	Directory.Delete(baseDir,true);
            } 
			catch (Exception ex)
			{
				Debug.WriteLine("Error deleting Directory: " + ex);
			}
            
			try
			{
				Directory.CreateDirectory(baseDir);
			} 
			catch (Exception ex)
			{
				Debug.WriteLine("Error creating Directory: " + ex);
			}
        }
    }
}
