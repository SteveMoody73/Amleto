using System;
using System.Collections.Generic;
using System.Threading;
using System.Net.Sockets;
using System.IO;
using System.Net;
using System.Diagnostics;

namespace RemoteExecution
{
    public class WebServer
    {
		private Thread _threadListener;
		private TcpListener _listener;
        private bool _needToStop;
		private MasterServer _server;

        public WebServer(MasterServer server,int port)
        {
            _server = server;

            try
            {
                _listener = new TcpListener(IPAddress.Any,port);
                _listener.Start(10);
                _needToStop = false;
                _threadListener = new Thread(StartAccepting);                
                _threadListener.IsBackground = true;
                _threadListener.Start();
            }
            catch (Exception ex)
            {
				Debug.WriteLine("Cannot start web server: " + ex);
                ServerServices.AddMessage(1, "Cannot start web server on port " + port);
                return;
            }
            ServerServices.AddMessage(0, "Web server ready to accept connections on port " + port);
        }

        public void StartAccepting()
        {
            while (!_needToStop)
            {
                if (!_listener.Pending())
                {
                    Thread.Sleep(10);
                    continue;
                }
                Socket socket=_listener.AcceptSocket();
                NetworkStream ns = new NetworkStream(socket);
                StreamReader reader = new StreamReader(ns);
                StreamWriter writer = new StreamWriter(ns);
                string httpRequest=reader.ReadLine();
            	if (httpRequest != null)
            	{
            		string[] p = httpRequest.Split(' ');
            		string request = p[1];
            		//skip header
            		while (true)
            		{
            			String l = reader.ReadLine();
            			if (l != null && l.Trim() == "")
            				break;
            		}

            		writer.WriteLine("HTTP/1.0 200 Ok");
            		writer.WriteLine("Server: Amleto 3.1");
            		writer.WriteLine("Content-type: text/html");
            		writer.WriteLine("Expires: now");
            		writer.WriteLine("");

            		writer.WriteLine("<HTML><HEAD><TITLE>Amleto 3.1</TITLE></HEAD>");
            		writer.WriteLine("<style type='text/css'>");
            		writer.WriteLine("<!--");
            		writer.WriteLine("td {  font-family: Arial, Helvetica, sans-serif; font-size: 9pt}");
            		writer.WriteLine("body {  font-family: Arial, Helvetica, sans-serif; font-size: 9pt}");
            		writer.WriteLine("h2 {  font-family: Arial, Helvetica, sans-serif; font-size: 14pt; font-weight: bold}");
            		writer.WriteLine("h3 {  font-family: Arial, Helvetica, sans-serif; font-size: 12pt; font-weight: bold}");
            		writer.WriteLine("h4 {  font-family: Arial, Helvetica, sans-serif; font-size: 9pt; font-weight: bold}");
            		writer.WriteLine("h1 {  font-family: Arial, Helvetica, sans-serif; font-size: 14pt; font-weight: bold}");
            		writer.WriteLine("a {  font-family: Arial, Helvetica, sans-serif; font-size: 9pt; text-decoration: none}");
            		writer.WriteLine("th {  font-family: Arial, Helvetica, sans-serif; font-size: 12pt; font-weight: bold}");
            		writer.WriteLine("ul {  font-family: Arial, Helvetica, sans-serif; font-size: 10pt}");
            		writer.WriteLine("-->");
            		writer.WriteLine("</style>");
            		writer.WriteLine("<BODY BGCOLOR=#FFFFFF>");

            		if (request.StartsWith("/messages"))
            			ShowMessages(writer);
            		else
            			ShowNodesProjects(writer);
            	}

            	writer.Close();
                writer.Dispose();
                reader.Close();
                reader.Dispose();
                ns.Close();
                ns.Dispose();
                socket.Disconnect(false);
            }
        }

        private void ShowMessages(StreamWriter writer)
        {
            writer.WriteLine("<CENTER>[<A HREF=/nodes>Show nodes & projects</A>]</CENTER><BR><BR>");

            List<string> messages = _server.GetOldMessages();
            writer.WriteLine("<TABLE BORDER=0 CELLSPACING=0 CELLPADDING=0 WIDTH=100% STYLE='border: 1px solid #000000'>");
            writer.WriteLine("<TR BGCOLOR=#A0A0A0><TD ALIGN=CENTER><B>Messages</B></TD></TR>");
            writer.WriteLine("<TR><TD><DIV STYLE='width: 100%; height: 300; overflow: auto;'>");
            writer.WriteLine("<TABLE BORDER=0 CELLSPACING=0 WIDTH=100%>");
            writer.WriteLine("<TR BGCOLOR=#A0A0A0><TD><B>Date</B></TD><TD><B>Message</B></TD></TR>");
            foreach (String s in messages)
            {
                string[] p = s.Split('|');
                writer.Write("<TR>");
                writer.Write("<TD STYLE='border-bottom: 1px solid #A0A0A0;'>" + p[1] + "</TD>");
                writer.Write("<TD STYLE='border-bottom: 1px solid #A0A0A0;'>" + p[2] + "</TD>");
                writer.WriteLine("</TR>");
            }
            writer.WriteLine("</TABLE></DIV></TD></TR></TABLE>");
        }

        private void ShowNodesProjects(StreamWriter writer)
        {
            writer.WriteLine("<CENTER>[<A HREF=/messages>Show messages</A>]</CENTER><BR><BR>");

            List<ConfigSet> configs = _server.GetConfigs();

            List<ClientConnection> clients = _server.GetConnectedHosts();
            writer.WriteLine("<TABLE BORDER=0 CELLSPACING=0 CELLPADDING=0 WIDTH=100% STYLE='border: 1px solid #000000'>");
            writer.WriteLine("<TR BGCOLOR=#A0A0A0><TD ALIGN=CENTER><B>Nodes</B></TD></TR>");
            writer.WriteLine("<TR><TD><DIV STYLE='width: 100%; height: 300; overflow: auto;'>");
            writer.WriteLine("<TABLE BORDER=0 CELLSPACING=0 WIDTH=100%>");
            writer.WriteLine("<TR BGCOLOR=#A0A0A0><TD><B>Id</B></TD><TD><B>Host name</B></TD><TD><B>Config</B></TD><TD><B>Status</B></TD></TR>");
            foreach (ClientConnection c in clients)
            {
                writer.Write("<TR>");
                writer.Write("<TD STYLE='border-bottom: 1px solid #A0A0A0;'>" + c.Id + "</TD>");
                writer.Write("<TD STYLE='border-bottom: 1px solid #A0A0A0;'>" + c.HostName + ":" + c.Instance + "</TD>");
                writer.Write("<TD STYLE='border-bottom: 1px solid #A0A0A0;'>" + configs[c.Config].ConfigLabel + "</TD>");
                writer.Write("<TD STYLE='border-bottom: 1px solid #A0A0A0;'>" + c.CurrentJob + "&nbsp;</TD>");
                writer.WriteLine("</TR>");
            }
            writer.WriteLine("</TABLE></DIV></TD></TR></TABLE>");

            writer.WriteLine("<BR>");

            List<RenderProject> projects = _server.GetProjects();
            writer.WriteLine("<TABLE BORDER=0 CELLSPACING=0 CELLPADDING=0 WIDTH=100% STYLE='border: 1px solid #000000'>");
            writer.WriteLine("<TR BGCOLOR=#A0A0A0><TD ALIGN=CENTER><B>Projects</B></TD></TR>");
            writer.WriteLine("<TR><TD><DIV STYLE='width: 100%; height: 300; overflow: auto;'>");
            writer.WriteLine("<TABLE BORDER=0 CELLSPACING=0 WIDTH=100%>");
            writer.WriteLine("<TR BGCOLOR=#A0A0A0><TD><B>Id</B></TD><TD><B>Scene</B></TD><TD><B>Start</B></TD><TD><B>End</B></TD><TD><B>Status</B></TD></TR>");
            foreach (RenderProject p in projects)
            {
                writer.Write("<TR>");
                writer.Write("<TD STYLE='border-bottom: 1px solid #A0A0A0;'>" + p.SceneId + "</TD>");
                writer.Write("<TD STYLE='border-bottom: 1px solid #A0A0A0;'>" + p.SceneFile.Substring(p.SceneFile.LastIndexOf('\\') + 1) + "</TD>");
                writer.Write("<TD STYLE='border-bottom: 1px solid #A0A0A0;'>" + p.StartFrame + "</TD>");
                writer.Write("<TD STYLE='border-bottom: 1px solid #A0A0A0;'>" + p.EndFrame + "</TD>");
                if (p.Paused)
                    writer.Write("<TD STYLE='border-bottom: 1px solid #A0A0A0;'>(Paused) " + ((p.StartJobs - p.NbRemainingJobs()) * 100 / p.StartJobs) + "%</TD>");
                else
                    writer.Write("<TD STYLE='border-bottom: 1px solid #A0A0A0;'>" + ((p.StartJobs - p.NbRemainingJobs()) * 100 / p.StartJobs) + "%</TD>");
                writer.WriteLine("</TR>");
            }
            writer.WriteLine("</TABLE></DIV></TD></TR></TABLE>");
        }
    }
}
