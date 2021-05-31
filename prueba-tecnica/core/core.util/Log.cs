using log4net;
using System;

namespace core.util
{
    public static class Log
    {

        public static void Write(Exception ex)
        {
            ILog x = LogManager.GetLogger("AppenderLog".GetValueAppSetting<string>());
            x.Info(ex.Message, ex);
            if (ex.InnerException != null)
                Write(ex.InnerException);
        }

        public static void Write(string msj)
        {
            ILog x = LogManager.GetLogger("AppenderLog".GetValueAppSetting<string>());
            x.Info(msj);
        }
        public static void Write(string msj, Exception ex)
        {
            ILog x = LogManager.GetLogger("AppenderLog".GetValueAppSetting<string>());
            x.Warn(msj, ex);
        }
    }
}
