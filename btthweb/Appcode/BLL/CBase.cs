using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Web;

namespace btthweb.Appcode.BLL
{
    public class CBase
    {
        private const string DEFAULT_LAN_IP = "10.26.2.xxx";

        // .net 4.0 ko ghi chi tiet duoc error
        // .net 4.5 ghi chi tiet den loi tai dong nao
        public static string GetDetailError(Exception ex)
        {
            string strDetailError = "" +
                "\r\nSource\t\t= " + ex.Source +
                "\r\nTargetSite\t= " + ex.TargetSite +
                "\r\nMessage\t\t= " + ex.Message +
                "\r\nStackTrace\t= " + ex.StackTrace;
            return strDetailError;
        }

        /// <summary>
        /// 3=>0 = Microsoft.Samples.AspNetRouteIntegration.Service->HelloWorld=>BaseLib.CSQL->ExecuteSP=>BaseLib.CBase->GetDeepCaller=>BaseLib.CBase->GetCaller=>
        /// 3=>2 = Microsoft.Samples.AspNetRouteIntegration.Service->HelloWorld=>BaseLib.CSQL->ExecuteSP=>
        /// </summary>
        /// <returns></returns>
        public static string GetDeepCaller()
        {
            string strCallerName = "";
            for (int i = 3; i >= 2; i--)
                strCallerName += GetCaller(i) + "=>";

            //returns a composite of the namespace, class and method name.
            return strCallerName;
        }

        /// <summary>
        /// get caller function name
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public static string GetCaller(int level = 2)
        {
            var m = new StackTrace().GetFrame(level).GetMethod();

            if (m.DeclaringType == null) return ""; //9:33 AM 6/18/2014 Exception Details: System.NullReferenceException: Object reference not set to an instance of an object.

            // .Name is the name only, .FullName includes the namespace
            var className = m.DeclaringType.FullName;

            //the method/function name you are looking for.
            var methodName = m.Name;

            //returns a composite of the namespace, class and method name.
            return className + "->" + methodName;
        }

        /// <summary>
        /// /// VB function : Right
        /// </summary>
        /// <param name="value"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string Right(string value, int length)
        {
            return value.Substring(value.Length - length);
        }

        /// <summary>
        /// VB function : Left
        /// </summary>
        /// <param name="s"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string Left(string s, int len)
        {
            if (s == null)
                return s;
            else if (len == 0 || s.Length == 0)
                return "";
            else if (s.Length <= len)
                return s;
            else
                return s.Substring(0, len);
        }

        /// <summary>
        /// get app name
        /// MyApp.exe => MyApp
        /// </summary>
        static public string GetAppName()
        {
            string strFullName = Assembly.GetEntryAssembly().Location;
            string strAppName = Path.GetFileNameWithoutExtension(strFullName);
            return strAppName;
        }

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            return DEFAULT_LAN_IP;
        }

        public static string ReadFile(string strFullPath)
        {
            try
            {
                // C:\Log\5G_QuoteFeeder_HOSE\SQL\2014_12_04.txt
                // read
                string strBody = "";
                using (StreamReader sr = new StreamReader(strFullPath))
                {
                    strBody = sr.ReadToEnd();
                }

                return strBody;
            }
            catch (Exception)
            {
                // do nothing
                return "";
            }
            finally
            {
            }
        }
    }
}