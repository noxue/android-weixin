namespace MicroMsg.Common.Timer
{
    using System;

    public class TimerParams
    {
        public TimerEventArgs mEventArgs;
        public EventHandler mEvtHandler;
        public int mFirstFire;
        public int mInterval;
        public int mRepeatCount;
    }
}

