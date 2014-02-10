using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;

namespace RemoteExecution
{
    public class BroadcastFinder
    {
		public string Server { get; set; }
		public int Port { get; set; }
		
		public BroadcastFinder()
        {
        	Server = "";
        	IPAddress[] a = Dns.GetHostEntry(Dns.GetHostName()).AddressList;
            string broadcastAddress="";
            foreach (IPAddress t in a)
            {
            	if (!t.ToString().Contains(":"))
            	{
            		broadcastAddress = t.ToString();
            		break;
            	}
            }
			Port = 2080;
            broadcastAddress = broadcastAddress.Substring(0, broadcastAddress.LastIndexOf('.')) + ".255";

            Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPEndPoint iep = new IPEndPoint(IPAddress.Broadcast, 61111);
            IPEndPoint iep2 = new IPEndPoint(IPAddress.Parse(broadcastAddress), 61111);
            EndPoint ep = (EndPoint)iep;

            sock.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);
            sock.ReceiveTimeout = 200;

            byte[] bs = Encoding.ASCII.GetBytes("Amleto client search");

            for (int i = 0; i < 3; i++)
            {
                try
                {
                    sock.SendTo(bs, iep);
                    sock.SendTo(bs, iep2);
                    
					byte[] data = new byte[1024];
                    int res = sock.ReceiveFrom(data, ref ep);
                    string sres = Encoding.ASCII.GetString(data, 0, res);
                    string[] parts = sres.Split(' ');

                    if (parts[0] == "Amleto" && parts[1] == "server")
                    {
                        Server = parts[3];
                        Port = Convert.ToInt32(parts[4]);
                        break;
                    }
                }
                catch (Exception ex)
                {
                    Tracer.Exception(ex);
                }
            }
        }
    }
}
