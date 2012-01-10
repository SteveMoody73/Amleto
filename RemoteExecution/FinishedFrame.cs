using System;

namespace RemoteExecution
{
    [Serializable]
    public class FinishedFrame
    {
		public string Nodename { get; set; }
		public string Filename { get; set; }
		
		public FinishedFrame(string nodename, string filename)
        {
            Nodename = nodename;
            Filename = filename;
        }

    }
}
