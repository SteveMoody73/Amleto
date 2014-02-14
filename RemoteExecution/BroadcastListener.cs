using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using NLog;

namespace RemoteExecution
{
    public class BroadcastListener
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        Thread _backgroundJob;
        private List<string> _hostAddressList = new List<string>();
    	private EndPoint _endPoint;
        private Socket _socket;
		
		public int Port { get; set; }

        public BroadcastListener(int port)
        {
            foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (ni.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 || ni.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                {
                    Console.WriteLine(ni.Name);
                    foreach (UnicastIPAddressInformation ip in ni.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            _hostAddressList.Add(ip.Address.ToString());
                            logger.Log(LogLevel.Info, "Detected network card " + ni.Description + " with IP address " + ip.Address);
                        }
                    }
                }
            }

            Port = port;
         
            _backgroundJob = new Thread(Listen);
            _backgroundJob.IsBackground = true;
            _backgroundJob.Start();
        }

    	private void Listen()
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
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
                        string res = "";
                        foreach (string host in _hostAddressList)
                        {
                            res = "Amleto server at " + host + " " + Port + "\n";
                        }
                        byte[] bres = Encoding.ASCII.GetBytes(res);
                        _socket.SendTo(bres, _endPoint);
                        logger.Log(LogLevel.Info, res);
                    }
                    
                }
                catch (Exception ex)
                {
                    logger.ErrorException("Unable to start Listener", ex);
                }
            }
        }
    }
}
