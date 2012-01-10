using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Win32;

namespace RemoteExecution
{
    [Serializable]
    public class MapDrive
    {
    	public string Password { get; set; }
    	public string Username { get; set; }
    	public string Share { get; set; }
    	public string Drive { get; set; }

		public void StoreRegistry(RegistryKey key)
        {
            RegistryKey subKey = key.CreateSubKey(Drive.Substring(0, 1));
			if (subKey != null)
			{
				subKey.SetValue("Drive", Drive);
				subKey.SetValue("Share", Share);
				subKey.SetValue("Username", Username);
				subKey.SetValue("Password", Password);
				subKey.Close();
			}
        }

        public void RestoreRegistry(RegistryKey key)
        {
            Drive = (string)key.GetValue("Drive");
            Share = (string)key.GetValue("Share");
            Username = (string)key.GetValue("Username");
            Password = (string)key.GetValue("Password");
        }

        public string Mount()
        {
            string res = "";
            try
            {
                NetworkDrive netDrive = new NetworkDrive();
                netDrive.Drive = Drive.Substring(0, 2);
                netDrive.ShareName = Share;
                if(Username != "")
                    netDrive.MapDrive(Username,Password);
                else
                    netDrive.MapDrive();
            }
            catch (Exception ex)
            {
                res=ex.Message;
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
            catch(Exception ex)
            {
				Debug.WriteLine("Error Unmounting drive: " + ex);
            }
            return false;
        }

        public static List<MapDrive> ReadMapDrives(RegistryKey key)
        {
            List<MapDrive> res = new List<MapDrive>();
            RegistryKey subKey = key.CreateSubKey("NetDrives");
        	if (subKey != null)
        	{
        		string[] drives = subKey.GetSubKeyNames();

        		foreach (string drive in drives)
        		{
        			MapDrive map = new MapDrive();
        			RegistryKey mapKey = subKey.CreateSubKey(drive);
        			map.RestoreRegistry(mapKey);
        			
					if (mapKey != null) 
						mapKey.Close();
        			res.Add(map);
        		}
        	}

        	if (subKey != null) 
				subKey.Close();
        	return res;
        }

        public static void StoreMapDrives(RegistryKey key, List<MapDrive> mapDrives)
        {
            RegistryKey subKey = key.CreateSubKey("NetDrives");

            // Remove the old subkeys to ensure that we have a clean situation
        	if (subKey != null)
        	{
        		string[] drives = subKey.GetSubKeyNames();
        		foreach (string drive in drives)
        			subKey.DeleteSubKey(drive);
        	}

        	// Now store the new keys
            foreach (MapDrive mapDrive in mapDrives)
                mapDrive.StoreRegistry(subKey);
        	if (subKey != null) 
				subKey.Close();
        }
    }
}
