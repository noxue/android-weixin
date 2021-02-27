namespace MicroMsg.Common.Timer
{
    using System;
    using System.Windows;

    public class TimerObject
    {
        private int mFireCount;
        private int mNextFire;
        private int mStatus;
        private TimerParams mTimerParams;
        private const int STATUS_INIT = 0;
        private const int STATUS_START = 1;
        private const int STATUS_STOP = 2;

        private void countNextFire()
        {
            if ((this.mTimerParams.mFirstFire < this.mTimerParams.mInterval) && (this.mTimerParams.mFirstFire >= 0))
            {
                this.mNextFire = this.mTimerParams.mFirstFire + this.getNowTicks();
            }
            else
            {
                this.mNextFire = this.mTimerParams.mInterval + this.getNowTicks();
            }
        }

        public void doTimerHandler(object sender, EventArgs e)
        {
            TimerObject obj2 = sender as TimerObject;
            if (((obj2 != null) && (obj2.mTimerParams != null)) && !obj2.isStop())
            {
                obj2.mTimerParams.mEvtHandler(obj2, obj2.mTimerParams.mEventArgs);
            }
        }

        private int getNowTicks()
        {
            return TimerSource.getTicks();
        }

        public void init(TimerParams param)
        {
            this.mTimerParams = param;
        }

        public bool isStop()
        {
            return (this.mStatus == 2);
        }

        public void run()
        {
            if ((this.mStatus == 1) && (this.getNowTicks() >= this.mNextFire))
            {
                if (this.mFireCount >= this.mTimerParams.mRepeatCount && this.mTimerParams.mRepeatCount !=-1)
                {
                    this.stop();
                }
                else
                {
                    object[] objArray = new object[2];
                    objArray[0] = this;
                    EventHandler eh = new EventHandler(this.doTimerHandler);
                    eh.BeginInvoke(this, null, null, objArray);

                    this.mFireCount++;
                    this.mNextFire = this.getNowTicks() + this.mTimerParams.mInterval;
                }
            }
        }

        public void start()
        {
            this.countNextFire();
            this.mFireCount = 0;
            this.mStatus = 1;
           // run();
        }

        public void stop()
        {
            this.mStatus = 2;
        }

        public int FireCount
        {
            get
            {
                return this.mFireCount;
            }
        }
    }
}

