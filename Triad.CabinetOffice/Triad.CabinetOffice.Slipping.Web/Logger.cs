using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;

namespace Triad.CabinetOffice.Slipping.Web
{
    public static class Logger
    {
        public static void LogMessage(string msg)
        {
            Trace.TraceInformation(msg);
        }

        public static void LogMessage(string format, params object[] args)
        {
            Trace.TraceInformation(format, args);
        }

        public static void LogException(Exception ex)
        {
            StringBuilder msg = new StringBuilder();

            while (ex != null)
            {
                msg.AppendFormat("[{0}] {1}", ex.GetType().Name, ex.Message);
                msg.AppendLine(ex.StackTrace);
                msg.AppendLine();

                if (ex.InnerException != null)
                {
                    msg.AppendLine("----- Inner exception details -----");
                }

                ex = ex.InnerException;
            }

            Trace.TraceError(msg.ToString());
        }
    }
}