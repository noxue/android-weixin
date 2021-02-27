namespace MicroMsg.Network
{
    using MicroMsg.Common.Utils;
    using MicroMsg.Protocol;
    using System.Threading;

    public class NetHandler
    {
        private static AutoResetEvent idleEvent = new AutoResetEvent(false);
        private static int mRunStatus = 0;
        private static Thread mThread = null;
        private static ThreadStart mThreadStart = null;
        private const int STATUS_NULL = 0;
        private const int STATUS_PAUSED = 2;
        private const int STATUS_RUNNING = 1;

        public static int getIdleTime()
        {
            double num = SessionPack.getNow();
            double mLastActiveTimestamp = Connector.mLastActiveTimestamp;
            int num3 = (int) ((num - mLastActiveTimestamp) / 100.0);
            if (num3 > 0x3e8)
            {
                num3 = 0x3e8;
            }
            if (num3 < 10)
            {
                num3 = 10;
            }
            return num3;
        }

        public static void onApplicationActivated()
        {
            if (mRunStatus == 2)
            {
                SessionPackMgr.cleanAllTimeoutPoint(3);
                SessionPackMgr.markAllToNotSended(3);
                if (SessionPackMgr.getAuthStatus() == 1)
                {
                    SessionPackMgr.setAuthStatus(0);
                }
                Connector.onPrepareSend(true);
                mRunStatus = 1;
            }
        }

        public static void onApplicationDeactivated()
        {
            if (mRunStatus == 1)
            {
                mRunStatus = 2;
            }
        }

        public static void run()
        {
            int millisecondsTimeout = 100;
            HostService.init();
            //NetworkDeviceWatcher.initWatcher();
            while (mRunStatus != 0)
            {
                
                idleEvent.WaitOne(millisecondsTimeout);
                if (mRunStatus == 1)
                {
                    //Sender.getInstance().checkSender();
                    SessionPackMgr.checkPackTimeout();
                    SessionPackMgr.clearCompletedOrCancel();
                    SessionPackMgr.checkSendTimeout();
                    Connector.checkReady();
                    Sender.getInstance().handler();
                    Receiver.getInstance().handler();
                    millisecondsTimeout = getIdleTime();
                }
                
            }
            mThread = null;
            mThreadStart = null;
        }

        public static void startup()
        {
            if (mRunStatus == 0)
            {
                mRunStatus = 1;
                Log.i("Network", "network handler startup....");
                Log.i("Network", "device id = " + Util.getDeviceUniqueId());
                Log.i("Network", "device type = " + ConstantsProtocol.DEVICE_TYPE);
                Log.i("Network", "Min version = 0x"+ ConstantsProtocol.CLIENT_MIN_VERSION.ToString("x") + " Max version = 0x" + ConstantsProtocol.CLIENT_MAX_VERSION.ToString("x"));
                if (mThreadStart == null)
                {
                    mThreadStart = new ThreadStart(NetHandler.run);
                }
                if (mThread == null)
                {
                    mThread = new Thread(mThreadStart);
                    mThread.Name = "NetworkHandlerThread";
                    mThread.Start();
                }
            }
        }

        private static void stop()
        {
            mRunStatus = 0;
            if (mThread != null)
            {
                mThread.Abort();
                mThread.Join(100);
            }
            mThread = null;
            mThreadStart = null;
        }

        public static void wakeUp()
        {
            idleEvent.Set();
        }
        //#region 内存回收
        //[DllImport("kernel32.dll", EntryPoint = "SetProcessWorkingSetSize")]
        //public static extern int SetProcessWorkingSetSize(IntPtr process, int minSize, int maxSize);
        ///// <summary>
        ///// 释放内存
        ///// </summary>
        //private static void ClearMemory()
        //{
        //    GC.Collect();
        //    GC.WaitForPendingFinalizers();
        //    //if (Environment.OSVersion.Platform == PlatformID.Win32NT)
        //   // {
        //        SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
        //   // }
        //}
        //#endregion
    }
}

