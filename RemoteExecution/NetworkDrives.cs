using System;
using System.Runtime.InteropServices;

namespace RemoteExecution
{
	public class NetworkDrive
	{

		[StructLayout(LayoutKind.Sequential)]
		private struct NetResource
		{
			public Int32 Scope;
			public Int32 Type;
			public Int32 DisplayType;
			public Int32 Usage;
			public string LocalName;
			public string RemoteName;
			public string Comment;
			public string Provider;
		}


		[DllImport("mpr.dll")]
		private static extern int WNetAddConnection2A(ref NetResource netRes, string password, string username, int flags);

		[DllImport("mpr.dll")]
		private static extern int WNetCancelConnection2A(string psName, int piFlags, int pfForce);

		private const int RESOURCETYPE_DISK = 0x1;

		private const int RESOURCE_GLOBALNET = 0x00000002;

		private const int RESOURCEDISPLAYTYPE_SHARE = 0x00000003;
		private const int RESOURCEUSAGE_CONNECTABLE = 0x00000001;


		private string _drive = "s:";
		private string _shareName = @"\\LocalHost\C$";

		/// <summary>
		/// Drive to be used in mapping / unmapping...
		/// </summary>
		public string Drive
		{
			get
			{
				return (_drive);
			}

			set
			{
				if (value.Length >= 1)
				{
					_drive = value.Substring(0, 1) + ":";
				}
				else
				{
					_drive = "";
				}
			}
		}


		public string ShareName
		{
			get { return (_shareName); }
			set { _shareName = value; }
		}


		// Map network drive
		public void MapDrive()
		{
			MapDrive(null, null);	
		}

		public void MapDrive(string username, string password)
		{
			//create struct data
			NetResource netRes = new NetResource();

			netRes.Scope = RESOURCE_GLOBALNET;
			netRes.Type = RESOURCETYPE_DISK;
			netRes.DisplayType = RESOURCEDISPLAYTYPE_SHARE;
			netRes.Usage = RESOURCEUSAGE_CONNECTABLE;
			netRes.RemoteName = _shareName;
			netRes.LocalName = _drive;

			if (username == "")
				username = null;

			if (password == "")
				password = null;
			
			// Map the network drive
			int retval = WNetAddConnection2A(ref netRes, password, username, 0);
			if (retval != 0)
			{
				throw new System.ComponentModel.Win32Exception(retval);
			}
		}

		
		public void UnMapDrive()
		{
			UnMapDrive(false);
		}

		public void UnMapDrive(bool force)
		{
			// Unmap the network drive
			int retval = WNetCancelConnection2A(_drive, 0, Convert.ToInt32(force));
			if (retval > 0)
			{
				throw new System.ComponentModel.Win32Exception(retval);
			}
		}
	}
}
