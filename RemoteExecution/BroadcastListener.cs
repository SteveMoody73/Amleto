using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Diagnostics;

namespace RemoteExecution
{
    public class BroadcastListener
    {
        Thread _backgroundJob;
        string _hostAddress;
    	EndPoint _endPoint;
        Socket _socket;
		
		public int Port { get; set; }

        public BroadcastListener(int port)
        {
            IPAddress[] a = Dns.GetHostEntry(Dns.GetHostName()).AddressList;
            foreach (IPAddress t in a)
            {
            	if (!t.ToString().Contains(":"))
            	{
            		_hostAddress = t.ToString();
            		break;
            	}
            }
        	Port = port;
         
            _backgroundJob = new Thread(Listen);
            _backgroundJob.IsBackground = true;
            _backgroundJob.Start();
        }

    	private void Listen()
        {
            _socket = new Socket(AddressFamily.InterNetwork,
                            SocketType.Dgram, ProtocolType.Udp);
            IPEndPoint iep = new IPEndPoint(IPAddress.Any, 61111);
            _socket.Bind(iep);
            _endPoint = (EndPoint)iep;

            while (true)
            {
                byte[] data = new byte[1024];
                try
                {
                    int recv = _socket.ReceiveFrom(data, ref _endPoint);
                    string s = Encoding.ASCII.GetString(data, 0, recv);
                    if (s == "Amleto client search")
                    {
                        string res = "Amleto server at " + _hostAddress + " " + Port;
                        byte[] bres = Encoding.ASCII.GetBytes(res);
                        _socket.SendTo(bres, _endPoint);
                    }
                }
                catch (Exception ex)
                {
					Debug.WriteLine(ex.ToString());
                }
            }
        }
    }
}
