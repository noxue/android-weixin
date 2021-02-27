namespace MicroMsg.Common.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.IO.IsolatedStorage;
    using System.Threading;
    using WeChatProtocol;
    public static class Log
    {
        private const string BAK_FILENAME = "/Log/MicroMsg.bak";
        //private static bool bFlushFile = false;
        private const int INTERVAL_CHECK_LOG = 0x7530;
        private static int level = 1;
        public const int LEVEL_DEBUG = 1;
        public const int LEVEL_ERROR = 4;
        public const int LEVEL_INFO = 2;
        public const int LEVEL_NONE = 5;
        public const int LEVEL_VERBOSE = 0;
        public const int LEVEL_WARNING = 3;
        private const string LOG_FILENAME = "/Log/MicroMsg.log";
        private static List<string> logCache = new List<string>(10);
        private static AutoResetEvent logEvent = new AutoResetEvent(false);
        private static AutoResetEvent logFlushOK = new AutoResetEvent(false);
        private static Thread m_thread = null;
        public static int MAX_FILE_LEN = 0x300000;
        public static bool sBIsWriteToLogView = true;
        private const int STRING_LOG_BUFFER_SIZE = 0x80000;
        private const string TAG = "Log";
        private static bool writeToFile = true;
        public  delegate void ShowConsoleMsg(string index, string msg);
        public static ShowConsoleMsg dShowMsgLog;
       // public  Program ads = new Program();
        //Program ad = new Program();
       // Program.
        
        
        
        
       
       
        
        //Program.ShowConsoleMsg
      
        
        //private delegate Program.ShowConsoleMsg dlog = new Program.ShowConsoleMsg();

        public static void d(string TAG, object msg)
        {
            
            if (level <= 1)
            {
                print(" -d ", TAG, msg);
            }
        }

        public static void e(string TAG, object msg)
        {
            if (level <= 4)
            {
                print(" -e ", TAG, msg);
            }
        }

        public static void e(string TAG, object msg, Exception e)
        {
            
            if (level <= 4)
            {
                print(" -e ", TAG, msg + ((e != null) ? (e.GetType().FullName + ":" + e.Message) : ""));
            }
        }

        public static void end()
        {
            forceFlush();
        }

        public static void forceFlush()
        {
            if (writeToFile && (m_thread != null))
            {
                print(" -a ", "LOG", "-------------- log force flush!!!");
                logFlushOK.Reset();
                //bFlushFile = true;
                logEvent.Set();
                logFlushOK.WaitOne();
            }
        }



        public static void i(string TAG, object msg)
        {
            if (level <= 2)
            {
                print(" -i ", TAG, msg);
            }
        }


        public static void m(string TAG, object msg)
        {
        }

        private static void print(string level, string context, object msg)
        {
            string item = string.Concat(new object[] { DateTime.Now.ToString("yyyy年MM月dd日HH時mm分ss秒 "), "Thread id:"+Thread.CurrentThread.ManagedThreadId.ToString(), level, "/", context.PadRight(15).Substring(0, 15), ": ", msg }) + "\n";
            if (writeToFile)
            {
                lock (logCache)
                {

                    dShowMsgLog.Invoke(level, item);    

                }
            }
        }

        public static string[] strSplit(string str, int sub_len)
        {
            int num = str.Length / sub_len;
            int length = str.Length % sub_len;
            if (length != 0)
            {
                num++;
            }
            string[] strArray = new string[num];
            for (int i = 0; i < num; i++)
            {
                if ((length != 0) && (i == (num - 1)))
                {
                    strArray[i] = str.Substring(i * sub_len, length);
                    return strArray;
                }
                strArray[i] = str.Substring(i * sub_len, sub_len);
            }
            return strArray;
        }

        public static void testLog()
        {
            m("class2", "this is message");
            Log.e("class2", "this is error");
            Exception e = new Exception("test exception");
            Log.e("class2", "this is error with exception message", e);
            w("class2", "this is warn");
            i("class2", "this is info ");
            d("class2", "this is debug");
        }


        public static void v(string TAG, object msg)
        {
            if (level <= 6)
            {
                print(" -v ", TAG, msg);
            }
        }

        public static void w(string TAG, object msg)
        {
            if (level <= 3)
            {
                print(" -w ", TAG, msg);
            }
        }

        public static int Level
        {
            set
            {
                level = value;
                print(" -a ", "Log", "--------------new log begin, level: " + value + "------------------\n");
            }
        }

        public static bool OutputToFile
        {
            set
            {
                writeToFile = value;
                //if (writeToFile && (m_thread == null))
                //{
                //    m_thread = new Thread(new ThreadStart(Log.LogThreadRun));
                //    m_thread.Start();
                //}
            }
        }
    }
}

