namespace MicroMsg.Network
{
    using MicroMsg.Common.Event;
    using MicroMsg.Common.Timer;
    using MicroMsg.Common.Utils;
    using MicroMsg.Protocol;
    using System;
    using System.Windows;

    public class ServerBulletin
    {
        private static BulletinInfo mBulletinInfo = new BulletinInfo();
        private static TimerObject mTimerObject = null;
        private const string TAG = "SeverBulletin";

        private static void closeCheckTimer()
        {
            if (mTimerObject != null)
            {
                mTimerObject.stop();
                mTimerObject = null;
            }
        }

        public static string getBulletinUrl(string username, string lang)
        {
            return string.Format("http://w.mail.qq.com/cgi-bin/report_mm?failuretype=1&devicetype={0}&clientversion={1}&os={2}&username={3}&iport={4}&t=weixin_bulletin&f=xhtml&lang={5}", new object[] { 5, ConstantsProtocol.CLIENT_MIN_VERSION, Environment.OSVersion.Version, username, mBulletinInfo.strHost, lang });
        }

        public static bool isFaultMode()
        {
            if (mBulletinInfo.type != 1)
            {
                return false;
            }
            if (isTimeoutDate((uint) mBulletinInfo.validTime))
            {
                mBulletinInfo.type = 0;
                return false;
            }
            return true;
        }

        private static bool isTimeoutDate(uint time)
        {
            DateTime time2 = DateTime.Now.ToUniversalTime();
            DateTime time3 = new DateTime(0x7b2, 1, 1);
            return (((uint) time2.Subtract(time3).TotalSeconds) > time);
        }

        public static void onCheckFaultModeTimeout(object sender, EventArgs e)
        {
            if (mBulletinInfo.type != 1)
            {
                closeCheckTimer();
            }
            else if (isTimeoutDate((uint) mBulletinInfo.validTime))
            {
                mBulletinInfo.type = 0;
                EventCenter.postEvent(EventConst.ON_NET_BULLETIN, false, null);
                closeCheckTimer();
            }
        }

        public static void onRecvBulletin(byte[] data)
        {
            if ((data == null) || (data.Length < 8))
            {
                Log.e("SeverBulletin", "received invalid bulletin data, ignored.");
            }
            else
            {
                bool flag = isFaultMode();
                int offset = 0;
                mBulletinInfo.type = Util.readInt(data, ref offset);
                mBulletinInfo.validTime = Util.readInt(data, ref offset);
                mBulletinInfo.strHost = HostService.mLongConnHosts.getCurrentHostString();
                Log.i("SeverBulletin", string.Concat(new object[] { "received bulletin：type =", mBulletinInfo.type, ", validtime=", mBulletinInfo.validTime }));
                bool flag2 = isFaultMode();
                if (flag2 != flag)
                {
                    EventCenter.postEvent(EventConst.ON_NET_BULLETIN, flag2, null);
                    if (flag2)
                    {
                        startCheckTimer();
                    }
                }
            }
        }

        public static void onRecvBulletin(string headcontent)
        {
            if (!string.IsNullOrEmpty(headcontent))
            {
                string[] strArray = headcontent.Split(new char[] { ',' });
                if ((strArray != null) && (strArray.Length >= 2))
                {
                    bool flag = isFaultMode();
                    mBulletinInfo.type = Util.stringToInt(strArray[0]);
                    mBulletinInfo.validTime = Util.stringToInt(strArray[1]);
                    mBulletinInfo.strHost = HostService.mShortConnHosts.getCurrentHostString();
                    Log.i("SeverBulletin", string.Concat(new object[] { "received bulletin in http headers：type =", mBulletinInfo.type, ", validtime=", mBulletinInfo.validTime }));
                    bool flag2 = isFaultMode();
                    if (flag2 != flag)
                    {
                        EventCenter.postEvent(EventConst.ON_NET_BULLETIN, flag2, null);
                        if (flag2)
                        {
                            startCheckTimer();
                        }
                    }
                }
            }
        }

        private static void startCheckTimer()
        {
            if (mTimerObject == null)
            {
                //Deployment.get_Current().get_Dispatcher().BeginInvoke(delegate {
                    mTimerObject = TimerService.addTimer(20, new EventHandler(ServerBulletin.onCheckFaultModeTimeout));
                    mTimerObject.start();
              //  });
            }
        }

        public class BulletinInfo
        {
            public string strHost = "";
            public int type;
            public int validTime;
        }
    }
}

