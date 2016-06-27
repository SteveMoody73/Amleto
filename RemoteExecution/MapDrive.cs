using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Win32;
using NLog;
using System.Xml.Serialization;

namespace RemoteExecution
{
    [Serializable]
    public class MapDrive
    {
        [XmlIgnore]
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public string Password { get; set; }
        public string Username { get; set; }
        public string Share { get; set; }
        public string Drive { get; set; }

        public string Mount()
        {
            string res = "";
            try
            {
                NetworkDrive netDrive = new NetworkDrive();
                netDrive.Drive = Drive.Substring(0, 2);
                netDrive.ShareName = Share;
                if (Username != "")
                    netDrive.MapDrive(Username, Password);
                else
                    netDrive.MapDrive();
            }
            catch (Exception ex)
            {
                res = ex.Message;
                logger.Error(ex, "Unable to mount the network drive");
            }
            return res;
        }

        public bool Unmount()
        {
            try
            {
                NetworkDrive netDrive = new NetworkDrive();
                netDrive.Drive = Drive.Substring(0, 2);
                netDrive.UnMapDrive();
                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Unable to unmount the network drive");
            }
            return false;
        }
    }
}
