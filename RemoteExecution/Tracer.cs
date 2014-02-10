using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;

namespace RemoteExecution
{
	public enum MessageLevel
	{
		Debug = 0,
		Critical = 1,
		Warning = 2,
		Exception = 3,
        Off = 4
	}

	public class Tracer
	{
        private static string _appName = string.Empty;
		private static Tracer _tracer;
		private static string _logPath = string.Empty;
		private const string _logSeparator = "~";
	    private static MessageLevel _logLevel = MessageLevel.Debug;
        private string _appendFile;

		static Tracer()
		{
		}

        public static void Initialize(string sApp, string sPath)
        {
            _logPath = sPath;
            if (!Directory.Exists(sPath))
                Directory.CreateDirectory(sPath);
            _appName = sApp;
            _tracer = new Tracer();
            _tracer.GetAppender();
        }

		public static string ApplicationName
		{
			get
			{
				return _appName;
			}
			set
			{
			    _appName = value;
			}
		}

		public static MessageLevel MessageLogLevel
		{
			get
			{
                return _logLevel;
			}
			set 
			{
                _logLevel= value;
			}
		}

		public static string LogFilePath
		{
			set
			{
				_logPath = value;
			}
		}

        protected void GetAppender()
		{
            try
            {
                if (String.IsNullOrEmpty(_logPath.Trim()))
                {
                    // Get application install path
                    string path = (new Uri(Assembly.GetExecutingAssembly().CodeBase)).LocalPath;
                    path = Directory.GetParent(path).FullName;
                    _logPath = path;
                }

                if (!Directory.Exists(_logPath))
                    Directory.CreateDirectory(_logPath);

                _appendFile = Path.Combine(_logPath, _appName + "-log.txt");

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(ex == null);
            }
        }

		private bool GetMessageLogLevel(MessageLevel level)
		{
            if (level.CompareTo(MessageLogLevel) >= 0)
			{
				return true;
			}
			return false;
		}

		private void DoWrite(MessageLevel level, object objToWrite)
		{
            if (level == MessageLevel.Off)
                return;

            try
            {
                string appendDate = String.Format("{0:yyyy_MM_dd}", DateTime.Now);
                string appendFile = _appendFile;

                if (appendFile.LastIndexOf(".") > appendFile.LastIndexOf("\\"))
                    appendFile = appendFile.Substring(0, appendFile.LastIndexOf(".")) + "_" + appendDate +
                            appendFile.Substring(appendFile.LastIndexOf("."));

                string sLogMsg = String.Format("{0:dd-MM-yyyy HH:mm:ss,fff}", DateTime.Now) + _logSeparator + level +
                                 _logSeparator + GetCallerFunctionName() + _logSeparator + objToWrite;

                bool open = false;
                StreamWriter sw = null;
                int retry = 3;

                while (!open && retry >= 0)
                {
                    try
                    {
                        sw = new StreamWriter(appendFile, true, Encoding.Unicode);
                        open = true;
                    } 
                    catch (Exception)
                    {
                        Thread.Sleep(100);
                        retry--;
                    }
                }
                if (open)
                {
                    try
                    {
                        sw.WriteLine(sLogMsg);
                        sw.Flush();
                    }
                    catch (Exception)
                    { }
                    sw.Close();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(ex == null);
            }
        }

		private static string GetCallerFunctionName()
		{
			string returnValue = string.Empty;

            try
			{
                MethodBase currentBase;
                StackTrace currentTrace = new StackTrace();

                for (int i = 2; i < currentTrace.FrameCount; i++)
                {
                    currentBase = currentTrace.GetFrame(i).GetMethod();

                    if (currentBase.DeclaringType.Name != "Tracer")
                    {
                        returnValue = currentBase.DeclaringType.FullName;
                        
                        if (returnValue.IndexOf('.') >= 0)
                            returnValue = returnValue.Substring(returnValue.LastIndexOf('.') + 1);
                        
                        if (returnValue.IndexOf('+') >= 0)
                            returnValue = returnValue.Replace("+", "::");
                        
                        if (currentBase.Name == ".ctor")
                            returnValue += "::" + currentBase.DeclaringType.Name;
                        else if (currentBase.Name == "Finalize")
                            returnValue += "::~" + currentBase.DeclaringType.Name;
                        else
                            returnValue += "::" + currentBase.Name;
                        
                        if (currentBase.ToString().IndexOf('(') >= 0)
                            returnValue += currentBase.ToString().Substring(currentBase.ToString().IndexOf('('));
                        break;
                    }
                }

				return returnValue;
			}
            catch (Exception exT)
            {
                System.Diagnostics.Debug.Assert(exT == null);
			}
		    return returnValue;
		}

        private static bool CallerFunctionHasParent()
        {
            bool returnValue = true;
        
            try
            {
                MethodBase currentBase;
                StackTrace currentTrace = new StackTrace();

                for (int i = 2; i < currentTrace.FrameCount; i++)
                {
                    currentBase = currentTrace.GetFrame(i).GetMethod();
                    
                    if (currentBase.DeclaringType.Name != "Tracer")
                    {
                        if (i == (currentTrace.FrameCount - 1))
                        {
                            returnValue = false;
                        }
                        else if (
                            !currentBase.DeclaringType.Namespace.StartsWith(currentTrace.GetFrame(i + 1).GetMethod().DeclaringType.Namespace) &&
                            !currentTrace.GetFrame(i + 1).GetMethod().DeclaringType.Namespace.StartsWith(currentBase.DeclaringType.Namespace))
                        {
                            returnValue = false;
                        }
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(ex == null);
            }

            return returnValue;
        }

        private static string RebuildTrace(Exception ex)
        {
            string sTrace = "";

            if (ex.InnerException != null)
                sTrace = RebuildTrace(ex.InnerException);
            
            string sTemp = ex.StackTrace;
            string sPrimary = sTemp.Substring(0, sTemp.Contains("\r\n") ? sTemp.IndexOf("\r\n") : sTemp.Length).ToLower();
            
            if (sPrimary.Contains("tracerutils.tracer"))
            {
                while (sPrimary.Contains("tracerutils.tracer"))
                {
                    sTemp = sTemp.Substring(sTemp.IndexOf("\r\n") + 2);

                    if (sTemp.IndexOf("\r\n") >= 0)
                        sPrimary = sTemp.Substring(0, sTemp.IndexOf("\r\n")).ToLower();
                    else
                        break;
                }

                if (sTemp.IndexOf("\r\n")>=0)
                    sTemp = sTemp.Substring(sTemp.IndexOf("\r\n") + 2);
            }

            sTrace = sTrace + (string.IsNullOrEmpty(sTrace)?"":"\r\n") + sTemp;
            return sTrace;
        }

        public static void Exception(Exception ex)
        {
            Exception(ex, false, false);
        }
        
        public static void Exception(Exception ex, bool bNoMsgBox, bool bRethrow)
        {
            bool bCallerHasParent = true;
        
            try
            {
                if (_tracer != null && _tracer.GetMessageLogLevel(MessageLevel.Exception))
                {
                    if (!(bCallerHasParent = CallerFunctionHasParent()) || !bRethrow)
                    {
                        string sTrace = RebuildTrace(ex);
                        sTrace = "Exception:" + ex.Message + "\r\n" + sTrace;
                        int iLen = (MessageLevel.Exception + _logSeparator + GetCallerFunctionName() + _logSeparator).Length + 21;
                        
                        string sSpace = new string(' ', iLen);
                        sTrace = sTrace.Replace("\r\n", "\r\n" + _logSeparator + _logSeparator + sSpace + _logSeparator);
                        sTrace = sTrace.Replace(" in ", "~");
                        sTrace = sTrace.Replace(":line ", "~line ");
                        _tracer.DoWrite(MessageLevel.Exception, sTrace);
                    }
                }
            }
            catch (Exception)
            {
            }

            if (!ex.Message.StartsWith("Receive queue not blocking", StringComparison.InvariantCultureIgnoreCase) &&
                !ex.Message.StartsWith("Cannot access a disposed object", StringComparison.InvariantCultureIgnoreCase) &&
                !ex.Message.StartsWith("Invoke or BeginInvoke cannot be called on a control until the window handle has been created.", StringComparison.InvariantCultureIgnoreCase))
            {
                int i = 0;
                i++;
            }

            if (bCallerHasParent && bRethrow)
            {
                Exception e = new Exception(ex.Message, ex);
                foreach (object objKey in ex.Data.Keys)
                    e.Data.Add(objKey, ex.Data[objKey]);
                throw e;
            }
        }

        public static void ExceptionMsg(Exception ex)
        {
            if (_tracer != null && _tracer.GetMessageLogLevel(MessageLevel.Exception))
            {
                _tracer.DoWrite(MessageLevel.Exception, "Exception:" + ex.Message);
                _tracer.DoWrite(MessageLevel.Exception, ex.StackTrace);
            }
        }

        public static void ExceptionMsg(Exception ex, object sMsg, object sTitle)
        {
            if (_tracer != null && _tracer.GetMessageLogLevel(MessageLevel.Exception))
            {
                string sTrace, sMessage = RebuildTrace(ex);
                sMessage = "Exception:" + ex.Message + "\r\n" + sMessage;
                sTrace = sMessage;

                int iLen = (MessageLevel.Exception.ToString() + _logSeparator + GetCallerFunctionName() + _logSeparator).Length + 21;

                string sSpace = new string(' ', iLen);
                sTrace = sTrace.Replace("\r\n", "\r\n" + _logSeparator + _logSeparator + sSpace + _logSeparator);
                sTrace = sTrace.Replace(" in ", "~");
                sTrace = sTrace.Replace(":line ", "~line ");
                _tracer.DoWrite(MessageLevel.Exception, sTrace);
            }
        }

        [Conditional("DEBUG")]
        public static void FunctionPrologue()
        {
            if (_tracer != null && _tracer.GetMessageLogLevel(MessageLevel.Debug))
                _tracer.DoWrite(MessageLevel.Debug, "Enter.");
        }
        [Conditional("DEBUG")]
        public static void FunctionEpilogue()
        {
            if (_tracer != null && _tracer.GetMessageLogLevel(MessageLevel.Debug))
                _tracer.DoWrite(MessageLevel.Debug, "Leave.");
        }

        public static void Debug(object message)
		{
            if (_tracer != null && message != null && _tracer.GetMessageLogLevel(MessageLevel.Debug))
    			_tracer.DoWrite(MessageLevel.Debug, message);
		}

		public static void Critical(object message)
		{
            if (_tracer != null && message != null && _tracer.GetMessageLogLevel(MessageLevel.Critical))
                _tracer.DoWrite(MessageLevel.Critical, message);
		}

        public static void Warning(object message)
		{
            if (_tracer != null && message != null && _tracer.GetMessageLogLevel(MessageLevel.Warning))
                _tracer.DoWrite(MessageLevel.Warning, message);
		}

        public static void Info(object message)
        {
            if (_tracer != null && message != null)
                _tracer.DoWrite(MessageLevel.Debug, message);
        }
    }
}
