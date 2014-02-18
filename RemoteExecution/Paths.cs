using System;
using System.IO;

namespace RemoteExecution
{
    public class Paths
    {
        public static string GetLocalPath()
        {
            string localPath;

#if __MonoCS__
            localPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Amleto");
#else
            localPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Amleto");

#endif
            return localPath;
        }
    }
}
