namespace MicroMsg.Common.Timer
{
    using System;
    using System.Collections.Generic;

    public class TimerService
    {
        private static object listLock = new object();
        private static List<TimerObject> listTimerObject = new List<TimerObject>();

        public static TimerObject addTimer(int interval, EventHandler handler)
        {
            return createTimerEx(interval, handler, interval, -1, null);
        }

        public static TimerObject addTimer(int interval, EventHandler handler, TimerEventArgs args)
        {
            return createTimerEx(interval, handler, interval, -1, args);
        }

        public static TimerObject addTimer(int interval, EventHandler handler, int first, int repeat)
        {
            return createTimerEx(interval, handler, first, repeat, null);
        }

        public static TimerObject addTimer(int interval, EventHandler handler, int first, int repeat, TimerEventArgs args)
        {
            return createTimerEx(interval, handler, first, repeat, args);
        }

        private static void addTimerEx(TimerObject timerObject)
        {
            lock (listLock)
            {
                if (!listTimerObject.Contains(timerObject))
                {
                    listTimerObject.Add(timerObject);
                }
            }
        }

        public static void cleanStopTimer()
        {
            TimerObject timerObject = getStopTimer();
            if (timerObject != null)
            {
                removeTimer(timerObject);
            }
        }

        private static TimerObject createTimerEx(int interval, EventHandler evtHandler, int firstFire, int repeatCount, TimerEventArgs evtArgs)
        {
            TimerSource.checkSystemTimer();
            TimerParams param = new TimerParams {
                mEvtHandler = evtHandler,
                mRepeatCount = repeatCount,
                mEventArgs = evtArgs,
                mInterval = interval,
                mFirstFire = firstFire
            };
            TimerObject timerObject = new TimerObject();
            timerObject.init(param);
            addTimerEx(timerObject);
            return timerObject;
        }

        public static TimerObject getStopTimer()
        {
            lock (listLock)
            {
                foreach (TimerObject obj2 in listTimerObject)
                {
                    if ((obj2 != null) && obj2.isStop())
                    {
                        return obj2;
                    }
                }
            }
            return null;
        }

        public static void removeTimer(TimerObject timerObject)
        {
            if (timerObject != null)
            {
                lock (listLock)
                {
                    listTimerObject.Remove(timerObject);
                }
            }
        }

        public static void runAllTimer()
        {
            lock (listLock)
            {
                foreach (TimerObject obj2 in listTimerObject)
                {
                    if (obj2 != null)
                    {
                        obj2.run();
                    }
                }
            }
        }
    }
}

