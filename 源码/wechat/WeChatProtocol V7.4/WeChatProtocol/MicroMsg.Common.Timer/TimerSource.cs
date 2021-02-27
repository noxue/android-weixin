namespace MicroMsg.Common.Timer
{
    using System;
    using System.Threading;
    using MicroMsg.Common.Utils;

    public class TimerSource
    {
        //private static DispatcherTimer mSystemTimer;
        //private static System.Timers.Timer mSystemTimer;
        private static Timer mSystemTimer;
        private static int mTicks;

        public static void checkSystemTimer()
        {
            if (mSystemTimer == null)
            {
                mSystemTimer = new Timer(new TimerCallback(TimerSource.onSystemTimerCallBack), null, 0, 1000);
                //mSystemTimer.Tick+=TimerSource.onSystemTimerCallBack;
                //mSystemTimer.Interval=new TimeSpan(0, 0, 0, 0, 0x3e8);
                //mSystemTimer.Start();
                //mSystemTimer = new System.Timers.Timer();
                //mSystemTimer.Elapsed += TimerSource.onSystemTimerCallBack;
                //mSystemTimer.Interval = 1000;
                //mSystemTimer.Start();
            }
        }

        public static int getTicks()
        {
            return mTicks;
        }

        public static void onSystemTimerCallBack(object sender)
        {
            if (mSystemTimer != null)
            {
                mTicks++;
                //Log.i("mSystemTimer", mTicks.ToString());
                TimerService.runAllTimer();
                // if (((mTicks + 1) % 10) == 0)
                // {
                TimerService.cleanStopTimer();
                // }
            }
        }
    }
}

